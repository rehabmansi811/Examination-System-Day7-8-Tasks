using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day8Tasks.Questions
{
//    public enum TrueFalse
//    {
//        False = 0,
//        True = 1,
//    }
    public class TrueFalseQuestion : BasicQuestions
    {
        public static readonly string[] choices = new string[] { "False", "True" };
        string[]? correct;

        public TrueFalseQuestion(string _header, string _body, int _marks,int _qnum, string[] _correct)
            : base(_header, _body, _marks,_qnum)
        {
            if (_correct.Length == 1)
                if ((_correct[0] == "False") || (_correct[0] == "True"))
                {
                    correct = _correct;
                }
                else {
                    correct = new string[] { "null" } ;
                    Console.WriteLine("correct choice must be only true or false"); }
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
            return $"Question Number: {QNum} ,Question Mark: {Marks} \n" +
                   $"{Body}\n" +
                   $"{string.Join("\n", Choices)}\n" +
                   $"Correct choices: {string.Join(" , ", correct)}\n" +
                   $"-----------------------------------------------";
        }
    }
}
