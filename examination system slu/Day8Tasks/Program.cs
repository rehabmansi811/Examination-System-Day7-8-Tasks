using Day8Tasks.Correct_Answers;
using Day8Tasks.Questions;
using Day8Tasks.Questions_List;
using  ExamSystem;
namespace Day8Tasks
{
    public class Program
    {
        static void Main(string[] args)
        {


            QuestionList questionList = new QuestionList("OOP_Exam");
            questionList.RemoveAllQuestions();

            CorrectAnswers correctAnswers = new CorrectAnswers();


            var q1 = new TrueFalseQuestion("Encapsulation", "Encapsulation means hiding the implementation details from the user.", 2, 1, new string[] { "True" });
            var q2 = new TrueFalseQuestion("Inheritance", "Inheritance allows a class to acquire the properties and methods of another class.", 2, 2, new string[] { "True" });
            var q3 = new TrueFalseQuestion("Polymorphism", "Polymorphism allows objects of different types to be treated as objects of a common super type.", 2, 3, new string[] { "True" });
            var q4 = new TrueFalseQuestion("Abstraction", "Abstraction focuses on what an object does rather than how it does it.", 2, 4, new string[] { "True" });
            var q5 = new TrueFalseQuestion("OOP Concept", "C# does not support object-oriented programming.", 2, 5, new string[] { "False" });
            var q6 = new TrueFalseQuestion("Constructors", "A constructor in C# cannot have parameters.", 2, 6, new string[] { "False" });
            var q7 = new TrueFalseQuestion("Interfaces", "An interface can contain method implementations in C#.", 2, 7, new string[] { "False" });


            questionList.Add(q1);
            questionList.Add(q2);
            questionList.Add(q3);
            questionList.Add(q4);
            questionList.Add(q5);
            questionList.Add(q6);
            questionList.Add(q7);

            correctAnswers.Answers.Add(new Dictionary<int, string[]?> { { q1.QNum, q1.CorrectChoice } });
            correctAnswers.Answers.Add(new Dictionary<int, string[]?> { { q2.QNum, q2.CorrectChoice } });
            correctAnswers.Answers.Add(new Dictionary<int, string[]?> { { q3.QNum, q3.CorrectChoice } });
            correctAnswers.Answers.Add(new Dictionary<int, string[]?> { { q4.QNum, q4.CorrectChoice } });
            correctAnswers.Answers.Add(new Dictionary<int, string[]?> { { q5.QNum, q5.CorrectChoice } });
            correctAnswers.Answers.Add(new Dictionary<int, string[]?> { { q6.QNum, q6.CorrectChoice } });
            correctAnswers.Answers.Add(new Dictionary<int, string[]?> { { q7.QNum, q7.CorrectChoice } });


            var q8 = new MultipleChoiceQuestion("Encapsulation", "Which OOP principle is achieved by using access modifiers?", 3, 8,
                new string[] { "Encapsulation" },
                new string[] { "Polymorphism", "Encapsulation", "Inheritance", "Abstraction" });

            var q9 = new MultipleChoiceQuestion("Polymorphism", "Which keyword is used to achieve method overriding in C#?", 3, 9,
                new string[] { "override" },
                new string[] { "override", "virtual", "abstract", "sealed" });

            var q10 = new MultipleChoiceQuestion("Abstraction", "Which type cannot be instantiated but can have methods and properties?", 3, 10,
                new string[] { "Abstract class" },
                new string[] { "Abstract class", "Interface", "Sealed class", "Static class" });

            var q11 = new MultipleChoiceQuestion("Inheritance", "Which keyword is used to inherit from a base class in C#?", 3, 11,
                new string[] { "class" },
                new string[] { "inherit", "extends", "base", "class" });

            var q12 = new MultipleChoiceQuestion("Interfaces", "What does an interface contain in C#?", 3, 12,
                new string[] { "Method declarations without implementation" },
                new string[] { "Only fields", "Only methods", "Method declarations without implementation", "Constructors" });

            questionList.Add(q8);
            questionList.Add(q9);
            questionList.Add(q10);
            questionList.Add(q11);
            questionList.Add(q12);


            correctAnswers.Answers.Add(new Dictionary<int, string[]?> { { q8.QNum, q8.CorrectChoice } });
            correctAnswers.Answers.Add(new Dictionary<int, string[]?> { { q9.QNum, q9.CorrectChoice } });
            correctAnswers.Answers.Add(new Dictionary<int, string[]?> { { q10.QNum, q10.CorrectChoice } });
            correctAnswers.Answers.Add(new Dictionary<int, string[]?> { { q11.QNum, q11.CorrectChoice } });
            correctAnswers.Answers.Add(new Dictionary<int, string[]?> { { q12.QNum, q12.CorrectChoice } });

            var q13 = new MultipleChoiceQuestion("Encapsulation", "Which of the following ensure encapsulation in C#?", 3, 13,
    new string[] { "Access Modifiers", "Properties" },
    new string[] { "Access Modifiers", "Public Variables", "Properties", "Global Variables" });

            var q14 = new MultipleChoiceQuestion("Polymorphism", "Which of the following features enable polymorphism in C#?", 3, 14,
                new string[] { "Method Overloading", "Method Overriding" },
                new string[] { "Method Overloading", "Method Overriding", "Abstract Classes", "Interfaces" });

            var q15 = new MultipleChoiceQuestion("Abstraction", "How can abstraction be achieved in C#?", 3, 15,
                new string[] { "Abstract Classes", "Interfaces" },
                new string[] { "Static Classes", "Abstract Classes", "Sealed Classes", "Interfaces" });

            var q16 = new MultipleChoiceQuestion("Inheritance", "Which of the following can be inherited in C#?", 3, 16,
                new string[] { "Methods", "Properties" },
                new string[] { "Methods", "Fields", "Properties", "Constructors" });

            var q17 = new MultipleChoiceQuestion("Interfaces", "Which of the following statements are true about interfaces?", 3, 17,
                new string[] { "They contain method declarations without implementation", "A class can implement multiple interfaces" },
                new string[] { "They contain method declarations without implementation", "A class can inherit from multiple interfaces", "A class can implement multiple interfaces", "They have constructors" });

            questionList.Add(q13);
            questionList.Add(q14);
            questionList.Add(q15);
            questionList.Add(q16);
            questionList.Add(q17);


            correctAnswers.Answers.Add(new Dictionary<int, string[]?> { { q13.QNum, q13.CorrectChoice } });
            correctAnswers.Answers.Add(new Dictionary<int, string[]?> { { q14.QNum, q14.CorrectChoice } });
            correctAnswers.Answers.Add(new Dictionary<int, string[]?> { { q15.QNum, q15.CorrectChoice } });
            correctAnswers.Answers.Add(new Dictionary<int, string[]?> { { q16.QNum, q16.CorrectChoice } });
            correctAnswers.Answers.Add(new Dictionary<int, string[]?> { { q17.QNum, q17.CorrectChoice } });


            Console.WriteLine("\nAll Stored OOP Questions:\n");
            questionList.ReadQuestionsFromFile();


            Console.WriteLine(correctAnswers);


            Console.WriteLine("Select Exam Type:");
            Console.WriteLine("1. Practice Exam");
            Console.WriteLine("2. Final Exam");
            Console.Write("Enter choice (1 or 2): ");

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || (choice != 1 && choice != 2))
            {
                Console.Write("Invalid input! Please enter 1 or 2: ");
            }
            Subject oop = new Subject("ppo","ki");
            Exam exam;
            if (choice == 1)
                exam = new PracticeExam (30,5, oop);
            else
                exam = new FinalExam (50,9, oop);

            exam.ShowExam();

        }
    }
}
