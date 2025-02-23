using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Day8Tasks.Questions;

namespace ExamSystem
{
    public class Subject
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public Subject(string name, string code)
        {
            Name = name;
            Code = code;
        }

        public override string ToString()
        {
            return $"{Name} ({Code})";
        }

        public override bool Equals(object obj)
        {
            if (obj is Subject other)
            {
                return Name == other.Name && Code == other.Code;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Code);
        }
    }

    public abstract class Exam : ICloneable, IComparable<Exam>
    {
        protected int Time;
        protected int NumberOfQuestions;
        protected Dictionary<BasicQuestions, string[]> QuestionAnswerDict = new Dictionary<BasicQuestions, string[]>();
        public Subject ExamSubject { get; set; }

        public Exam(int time, int numQuestions, Subject subject)
        {
            Time = time;
            NumberOfQuestions = numQuestions;
            ExamSubject = subject;
        }

        public void AddQuestion(BasicQuestions question, string[] correctAnswers)
        {
            QuestionAnswerDict[question] = correctAnswers;
        }

        public abstract void ShowExam();

        public override string ToString()
        {
            return $"Exam for Subject: {ExamSubject.Name}, Time: {Time} minutes, Questions: {NumberOfQuestions}";
        }

        public override bool Equals(object obj)
        {
            if (obj is Exam otherExam)
            {
                return Time == otherExam.Time && NumberOfQuestions == otherExam.NumberOfQuestions && ExamSubject.Equals(otherExam.ExamSubject);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Time, NumberOfQuestions, ExamSubject);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public int CompareTo(Exam other)
        {
            return NumberOfQuestions.CompareTo(other.NumberOfQuestions);
        }
    }

    public class AnswerList
    {
        public Dictionary<int, string[]> UserAnswers { get; set; } = new Dictionary<int, string[]>();

        public void AddAnswer(int questionNumber, string[] answer)
        {
            UserAnswers[questionNumber] = answer;
        }

        public bool CheckAnswer(int questionNumber, string[] correctAnswer)
        {
            if (UserAnswers.ContainsKey(questionNumber))
            {
                return UserAnswers[questionNumber].SequenceEqual(correctAnswer);
            }
            return false;
        }
    }

 
    public class PracticeExam : Exam
    {
        public PracticeExam(int time, int numQuestions, Subject subject) : base(time, numQuestions, subject) { }

        public override void ShowExam()
        {
            Console.WriteLine("\n--- Exam Start ---");

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

                int questionCount = 0;
                int questionNumber = 1;
                string header = "", body = "", correctAnswer = "";
                List<string> choices = new List<string>();

                while ((line = reader.ReadLine()) != null && questionCount < NumberOfQuestions)
                {
                    if (line.StartsWith("-----------------------------------------------"))
                    {
                        if (!string.IsNullOrEmpty(header) && !string.IsNullOrEmpty(body))
                        {
                            AskQuestion(questionNumber, header, body, choices, correctAnswer, userAnswers, correctAnswers);
                            questionNumber++;
                            questionCount++;
                        }

                        header = "";
                        body = "";
                        correctAnswer = "";
                        choices.Clear();
                    }
                    else if (line.StartsWith("Question Number: "))
                    {
                        header = line.Replace("Question Number: ", "").Trim();
                    }
                    else if (line.StartsWith("Correct choices:"))
                    {
                        correctAnswer = line.Replace("Correct choices:", "").Trim();
                    }
                    else
                    {
                        body = string.IsNullOrEmpty(body) ? line.Trim() : body + " " + line.Trim();
                        choices.Add(line.Trim());
                    }
                }

                if (!string.IsNullOrEmpty(header) && !string.IsNullOrEmpty(body) && questionCount < NumberOfQuestions)
                {
                    AskQuestion(questionNumber, header, body, choices, correctAnswer, userAnswers, correctAnswers);
                }

                if (this is FinalExam)
                {
                    Console.WriteLine("\n--- Exam Results ---");
                    int correctCount = 0;

                    foreach (var question in correctAnswers)
                    {
                        int qNumber = question.Key;
                        string correct = question.Value;
                        string userAnswer = userAnswers.ContainsKey(qNumber) ? userAnswers[qNumber] : "No Answer";

                        Console.WriteLine($"\nQuestion {qNumber}:");
                        Console.WriteLine($"Your Answer: {userAnswer}");
                        Console.WriteLine($"Correct Answer: {correct}");

                        if (userAnswer.Equals(correct, StringComparison.OrdinalIgnoreCase))
                            correctCount++;
                    }

                    Console.WriteLine($"\nFinal Score: {correctCount} / {correctAnswers.Count}");
                }
            }
        }

        private void AskQuestion(int questionNumber, string header, string body, List<string> choices, string correctAnswer,
                                 Dictionary<int, string> userAnswers, Dictionary<int, string> correctAnswers)
        {
            Console.WriteLine($"\nQuestion {questionNumber}: {header}");
            Console.WriteLine(body);

            for (int i = 0; i < choices.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {choices[i]}");
            }

            Console.Write("Enter your answer: ");
            string userResponse = Console.ReadLine()?.Trim() ?? "";

            userAnswers[questionNumber] = userResponse;
            correctAnswers[questionNumber] = correctAnswer;

            if (this is PracticeExam)
            {
                Console.WriteLine($"Correct Answer: {correctAnswer}\n");
            }
        }

    }

}
