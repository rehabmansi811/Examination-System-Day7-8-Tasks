using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day8Tasks.Questions
{
    public abstract class BasicQuestions : IQuestions
    {
        string body;
        string header;
        int marks;
        int qNum;

        public BasicQuestions(string _header, string _body, int _marks, int _qNum)
        {
            header = _header;
            body = _body;
            marks = _marks;
            qNum = _qNum;
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

        public int QNum { get =>  qNum ; set =>  qNum=value; }
    }
}
