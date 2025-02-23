using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day8Tasks.Questions
{
    public class MultipleChoiceQuestion : BasicQuestions
    {
        string[] choices;
        string[] correct;

        public MultipleChoiceQuestion(string _header, string _body, int _marks,int _qnum ,string[] _correct, string[] _choices)
            : base(_header, _body, _marks, _qnum)
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
            return $"Question Number: {QNum} , Question Mark: {Marks}\n" +
                   $"{Body}\n" +
                   $"{string.Join("\n", Choices)}\n" +
                   $"Correct choices: {string.Join(", ", correct)}\n" +
                   $"-----------------------------------------------";
        }
    }
}
