using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace help {
    public class Question {
        public string Title { get; set; }
        public List<string> Answers { get; set; }
        public int RightIndex { get; set; }
        public Question() { }
        public Question(string title, List<string> ans, int ind) {
            this.Title = title;
            this.Answers = ans;
            this.RightIndex = ind;


        }
       
    }
}
