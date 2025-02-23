using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Day8Tasks.Questions;

namespace Day8Tasks.Correct_Answers
{
    public class CorrectAnswers
    {
        private List<Dictionary<int, string[]?>> answers = new List<Dictionary<int, string[]?>>();

        public List<Dictionary<int, string[]?>> Answers
        {
            get { return answers; }
            set { answers = value; }
        }


        public override string ToString()
        {
            string result = "Correct Answers:\n";

            foreach (var dict in answers)
            {
                foreach (var entry in dict)
                {
                    result += $"Question ID: {entry.Key}, Correct Answers: {string.Join(", ", entry.Value)}\n";
                }
            }

            return result.Trim();
        }
    }
}
