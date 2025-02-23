using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Reflection.PortableExecutable;

namespace examination_system
{
    public interface IQuestions
    {
        string Body { get; set; }
        string Header { get; set; }
        int Marks { get; set; }
        //object Choices { get; }
        //object CorrectChoice { get; }
    }

    public enum TrueFalse
    {
        False = 0,
        True = 1,
    }

    public abstract class BasicQuestions : IQuestions
    {
        string body;
        string header;
        int marks;

        public BasicQuestions(string _header, string _body, int _marks)
        {
            header = _header;
            body = _body;
            marks = _marks;
        }

        public string Body
        {
            get { return body; }
            set { body = value; }
        }
        public string Header
        {
            get { return header; }
            set { header = value; }
        }
        public int Marks
        {
            get { return marks; }
            set { marks = value; }
        }



    }

    public class TrueFalseQuestion : BasicQuestions
    {
        public static readonly TrueFalse[] choices = new TrueFalse[] { TrueFalse.False, TrueFalse.True };
        TrueFalse correct;

        public TrueFalseQuestion(string _header, string _body, int _marks, TrueFalse _correct)
            : base(_header, _body, _marks)
        {
            if (!choices.Contains(_correct))
                Console.WriteLine("Correct choice must be either True or False.");
            else
                correct = _correct;
        }

        public TrueFalse[] Choices
        {
            get { return choices; }
        }
        public TrueFalse CorrectChoice
        {
            get { return correct; }
        }

        public override string ToString()
        {
            return $"Question Header: {Header}\n" +
                   $"{Body}\n" +
                   $"{string.Join("\n", Choices)}\n" +
                   $"Correct choices: {string.Join(" , ", correct)}\n" +
                   $"-----------------------------------------------";
        }
    }

    public class MultipleChoiceQuestion : BasicQuestions
    {
        string[] choices;
        string[] correct;

        public MultipleChoiceQuestion(string _header, string _body, int _marks, string[] _correct, string[] _choices)
            : base(_header, _body, _marks)
        {
            correct = _correct;
            choices = _choices;
        }

        public string[] Choices
        {
            get { return choices; }
        }
        public string[] CorrectChoice
        {
            get { return correct; }
        }

        public override string ToString()
        {
            return $"Question Header: {Header}\n" +
                   $"{Body}\n" +
                   $"{string.Join("\n", Choices)}\n" +
                   $"Correct choices: {string.Join(", ", correct)}\n" +
                   $"-----------------------------------------------";
        }
    }

    public class QuestionList
    {
        private string fileName;
        private ArrayList questions;

        public QuestionList(string listIdentifier)
        {
            fileName = $@"F:\\ITI 9months\\C#\\Day7\\logfile\\{listIdentifier}_questions.txt";
            questions = new ArrayList();
        }

        public void Add(IQuestions question)
        {
            if (question is BasicQuestions basicQuestion)
            {
                questions.Add(basicQuestion);
                LogQuestionToFile(basicQuestion);  
            }
            else
            {
                Console.WriteLine("Cannot cast to BasicQuestions");
            }
        }

        private void LogQuestionToFile(IQuestions question)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName, true))  
                {
                    if (question is BasicQuestions basicQuestion)
                    {
                        
                        if (question is TrueFalseQuestion tfQuestion)
                        {
                            writer.WriteLine( tfQuestion);
                        }
                        else if (question is MultipleChoiceQuestion mcQuestion)
                        {
                            writer.WriteLine(mcQuestion);  
                        }

                        
                    }
                }
            }
            catch (Exception )
            {
                Console.WriteLine($"Error while logging question ");
            }
        }

        public void ReadQuestionsFromFile()
        {
            try
            {
                using (StreamReader reader = new StreamReader(fileName))  
                {
                    string fileContent = reader.ReadToEnd();  
                    if (!string.IsNullOrWhiteSpace(fileContent))
                    {
                        Console.WriteLine(fileContent);
                    }
                    else
                    {
                        Console.WriteLine("No questions found in the file.");
                    }
                }
            }
            catch (Exception )
            {
                Console.WriteLine($"Error while reading from file ");
            }
        }
 
 
        
        public void RemoveAllQuestions()
        {
            questions.Clear(); //remove from array list 
            File.WriteAllText(fileName, string.Empty); //remove from log file
            Console.WriteLine("All questions removed successfully.");
        }

    }
 


    public abstract class Exam
    {
        protected int Time;
        protected int NumberOfQuestions;
        protected ArrayList Questions = new ArrayList();

        public Exam(int time, int numQuestions)
        {
            Time = time;
            NumberOfQuestions = numQuestions;
        }

        public void AddQuestion(BasicQuestions question)
        {
            
            Questions.Add(question);
        }

        public abstract  void ShowExam();
    }

    public class PracticeExam : Exam
    {
        public PracticeExam(int time, int numQuestions) : base(time, numQuestions) { }
 

        public override void ShowExam()
        {
            Console.WriteLine("\n--- Practice Exam ---");

            string filePath = @"F:\\ITI 9months\\C#\\Day7\\logfile\\OOP_Exam_questions.txt";

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Error: File not found!");
                return;
            }

            using (StreamReader reader = new StreamReader(filePath))
            {
                string? line;
                ArrayList questionData = new ArrayList();
                int questionCount = 0;

                while ((line = reader?.ReadLine()) != null && questionCount < NumberOfQuestions)
                {
                    if (line.StartsWith("-----------------------------------------------"))
                    {
                        if (questionData.Count > 0)
                        {
                             
                            PrintQuestion(questionData);
                            questionData.Clear();
                            questionCount++;
                        }
                    }
                    else
                    {
                        questionData.Add(line.Trim());
                    }
                }
 
                if (questionData.Count > 0 && questionCount < NumberOfQuestions)
                {
                    PrintQuestion(questionData);
                }
            }
        }

        private void PrintQuestion(ArrayList? questionData)
        {   
            if(questionData != null) 
            { 
                if (questionData?.Count < 3) return;

                string header = questionData[0].ToString().Replace("Question Header: ", "").Trim();
                string body = questionData[1].ToString().Trim();
                ArrayList choices = new ArrayList();
                string correctAnswer = "";

                for (int i = 2; i < questionData.Count; i++)
                {
                    string line = questionData[i].ToString().Trim();

                    if (line.StartsWith("Correct choices:"))
                    {
                        correctAnswer = line.Replace("Correct choices:", "").Trim();
                    }
                    else
                    {
                        choices.Add(line);
                    }
                }

                Console.WriteLine($"\n{header}\n{body}");

                foreach (object choice in choices)
                {
                    Console.WriteLine(choice.ToString());
                }

                Console.WriteLine($"Correct Answer: {correctAnswer}\n");
            }

        }
    }


    public class FinalExam : Exam
    {
        public FinalExam(int time, int numQuestions) : base(time, numQuestions) { }

        public override void ShowExam()
        {
            Console.WriteLine("\n--- Final Exam ---");

            string filePath = @"F:\\ITI 9months\\C#\\Day7\\logfile\\OOP_Exam_questions.txt";

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Error: File not found!");
                return;
            }

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                ArrayList questionData = new ArrayList();
                int questionCount = 0;

                while ((line = reader.ReadLine()) != null && questionCount < NumberOfQuestions)
                {
                    if (line.StartsWith("-----------------------------------------------"))
                    {
                        if (questionData.Count > 0)
                        {
                             
                            PrintQuestionWithoutAnswer(questionData);
                            questionData.Clear();
                            questionCount++;
                        }
                    }
                    else
                    {
                        questionData.Add(line.Trim());
                    }
                }

                 
                if (questionData.Count > 0 && questionCount < NumberOfQuestions)
                {
                    PrintQuestionWithoutAnswer(questionData);
                }
            }
        }

        private void PrintQuestionWithoutAnswer(ArrayList questionData)
        {
            if (questionData.Count < 3) return;

            string header = questionData[0].ToString().Replace("Question Header: ", "").Trim();
            string body = questionData[1].ToString().Trim();
            ArrayList choices = new ArrayList();

            for (int i = 2; i < questionData.Count; i++)
            {
                
                if (!questionData[i].ToString().StartsWith("Correct choices:"))
                {
                    choices.Add(questionData[i].ToString().Trim());
                }
            }

            Console.WriteLine($"\n{header}\n{body}");

            foreach (string choice in choices)
            {
                Console.WriteLine(choice);
            }

            Console.WriteLine($"-----------------------------------------------");
        }

    }
    //////////////////////////////////////////////////////////////

    class Program
    {
        static void Main(string[] args)
        {
            
            QuestionList questionList = new QuestionList("OOP_Exam");
            questionList.RemoveAllQuestions();
            questionList.Add(new TrueFalseQuestion("True/False Question 1", "C# is a statically typed language.", 1, TrueFalse.True));
            questionList.Add(new TrueFalseQuestion("True/False Question 2", "Interfaces in C# can contain implemented methods.", 1, TrueFalse.False));
            questionList.Add(new TrueFalseQuestion("True/False Question 3", "Garbage collection in C# is automatic.", 1, TrueFalse.True));
            questionList.Add(new TrueFalseQuestion("True/False Question 4", "C# supports multiple inheritance.", 1, TrueFalse.False));
            questionList.Add(new TrueFalseQuestion("True/False Question 5", "A struct in C# is a value type.", 1, TrueFalse.True));

             
            questionList.Add(new MultipleChoiceQuestion("MCQ Question 1", "Which keyword is used to define a class in C#?", 2,
                new string[] { "A. class" },
                new string[] { "A. class", "B. struct", "C. interface", "D. new" }));

            questionList.Add(new MultipleChoiceQuestion("MCQ Question 2", "What is the default access modifier for class members in C#?", 2,
                new string[] { "B. private" },
                new string[] { "A. public", "B. private", "C. protected", "D. internal" }));

            questionList.Add(new MultipleChoiceQuestion("MCQ Question 3", "Which data type is used for storing true/false values in C#?", 2,
                new string[] { "D. bool" },
                new string[] { "A. int", "B. string", "C. char", "D. bool" }));

            questionList.Add(new MultipleChoiceQuestion("MCQ Question 4", "Which loop is used when the number of iterations is known?", 2,
                new string[] { "C. for" },
                new string[] { "A. while", "B. do-while", "C. for", "D. switch" }));

            questionList.Add(new MultipleChoiceQuestion("MCQ Question 5", "Which of the following is NOT a primitive data type in C#?", 2,
                new string[] { "D. object" },
                new string[] { "A. int", "B. char", "C. bool", "D. object" }));
 
            questionList.Add(new MultipleChoiceQuestion("MCQ Multi 1", "Which of the following are access modifiers in C#?", 3,
                new string[] { "A. public", "B. private", "C. protected" },
                new string[] { "A. public", "B. private", "C. protected", "D. sealed" }));

            questionList.Add(new MultipleChoiceQuestion("MCQ Multi 2", "Which of the following are reference types in C#?", 3,
                new string[] { "A. string", "C. object", "D. array" },
                new string[] { "A. string", "B. int", "C. object", "D. array" }));

            questionList.Add(new MultipleChoiceQuestion("MCQ Multi 3", "Which of the following are looping statements in C#?", 3,
                new string[] { "A. for", "B. while", "C. do-while" },
                new string[] { "A. for", "B. while", "C. do-while", "D. switch" }));

             
            Console.WriteLine("\n=== Saved Questions in File ===");
          


            Exam exam;
            Console.WriteLine("Select Exam Type: 1 - Practice Exam | 2 - Final Exam");
            int choice = int.Parse(Console.ReadLine());

            if (choice == 1)
            {
                exam = new PracticeExam(30, 12);
                exam.ShowExam();
            }

            else if (choice == 2)
            {
                exam = new FinalExam(30, 12);
                exam.ShowExam();
            }

            else
                Console.WriteLine(" pleas correct number ");





        }
    }
    }



