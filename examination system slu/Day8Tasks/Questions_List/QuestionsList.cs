using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Day8Tasks.Questions;

namespace Day8Tasks.Questions_List
{
    public class QuestionList
    {
        private string fileName;
        private List<BasicQuestions> questions;

        public QuestionList(string listIdentifier)
        {
            fileName = $@"F:\\ITI 9months\\C#\\Day7\\Day8 logfile\\{listIdentifier}_questions.txt";
            questions = new List<BasicQuestions>(15);
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
                            writer.WriteLine(tfQuestion);
                        }
                        else if (question is MultipleChoiceQuestion mcQuestion)
                        {
                            writer.WriteLine(mcQuestion);
                        }


                    }
                }
            }
            catch (Exception)
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
            catch (Exception)
            {
                Console.WriteLine($"Error while reading from file ");
            }
        }



        public void RemoveAllQuestions()
        {
            questions.Clear();  
            File.WriteAllText(fileName, string.Empty);  
            Console.WriteLine("All questions removed successfully.");
        }

    }

}
