 using System;
using System.Collections.Generic;
using System.IO;

namespace ExamSystem
{
    public class FinalExam : Exam
    {
        public FinalExam(int time, int numQuestions, Subject subject) : base(time, numQuestions, subject) { }

        public override void ShowExam()
        {
            int totalScore = 0;
            int userScore = 0;
            Console.WriteLine("\n--- Final Exam ---");

            string filePath = @"F:\\ITI 9months\\C#\\Day7\\Day8 logfile\\OOP_Exam_questions.txt";

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Error: File not found!");
                return;
            }

            using (StreamReader reader = new StreamReader(filePath))
            {
                string? line;
                Dictionary<int, string> userAnswers = new Dictionary<int, string>();
                Dictionary<int, string> correctAnswers = new Dictionary<int, string>();
                Dictionary<int, int> questionMarks = new Dictionary<int, int>();  

                int questionNumber = 1;
                string body = "", correctAnswer = "";
                int marks = 0;
                List<string> choices = new List<string>();

                while ((line = reader.ReadLine()) != null && questionNumber <= NumberOfQuestions)
                {
                    if (line.StartsWith("-----------------------------------------------"))
                    {
                        if (!string.IsNullOrEmpty(body))
                        {
                            AskQuestion(questionNumber, body, choices, correctAnswer, marks, userAnswers, correctAnswers, questionMarks);
                            totalScore += marks;
                            questionNumber++;
                        }

                        body = "";
                        correctAnswer = "";
                        marks = 0;
                        choices.Clear();
                    }
                    else if (line.StartsWith("Question Number: "))
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length == 2)
                        {
                            string questionNumPart = parts[0].Trim();
                            string marksPart = parts[1].Trim();

                            if (marksPart.StartsWith("Question Mark: "))
                            {
                                int.TryParse(marksPart.Replace("Question Mark: ", "").Trim(), out marks);
                            }
                        }
                    }
                    else if (line.StartsWith("Correct choices:"))
                    {
                        correctAnswer = line.Replace("Correct choices:", "").Trim();
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(body))
                            body = line.Trim();
                        else
                            choices.Add(line.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(body) && questionNumber <= NumberOfQuestions)
                {
                    AskQuestion(questionNumber, body, choices, correctAnswer, marks, userAnswers, correctAnswers, questionMarks);
                    totalScore += marks;
                }

                Console.WriteLine("\n--- Exam Results ---");
                int correctCount = 0;

                foreach (var question in correctAnswers)
                {
                    int qNumber = question.Key;
                    string correct = question.Value;
                    string userAnswer = userAnswers.ContainsKey(qNumber) ? userAnswers[qNumber] : "No Answer";
                    int questionMark = questionMarks[qNumber]; 

                    Console.WriteLine($"\nQuestion {qNumber}:");
                    Console.WriteLine($"Your Answer: {userAnswer}");
                    Console.WriteLine($"Correct Answer: {correct}");
                    Console.WriteLine($"Marks: {questionMark}");

                    if (userAnswer.Equals(correct, StringComparison.OrdinalIgnoreCase))
                    {
                        correctCount++;
                        userScore += questionMark;  
                    }
                }

                Console.WriteLine($"\nFinal Score: {userScore} / {totalScore}");  
            }
        }

        private void AskQuestion(int questionNumber, string body, List<string> choices, string correctAnswer, int marks,
                         Dictionary<int, string> userAnswers, Dictionary<int, string> correctAnswers, Dictionary<int, int> questionMarks)
        {
            Console.WriteLine($"\nQuestion {questionNumber}: {body}");

            for (int i = 0; i < choices.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {choices[i]}");
            }

            Console.Write("Your answer: ");
            string userInput = Console.ReadLine()?.Trim() ?? "";

            userAnswers[questionNumber] = userInput;

            correctAnswers[questionNumber] = correctAnswer;

            questionMarks[questionNumber] = marks;
        }
    }
}
