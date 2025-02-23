using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day8Tasks.Questions
{
    public interface IQuestions
    {
        string Body { get; set; }
        string Header { get; set; }
        int Marks { get; set; }
        int QNum {  get; set; }
    }
}
