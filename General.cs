using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace help {
    public class General {
        public string Title { get; set; }
        public List<Question> Questions { get; set; }
        public General() { 
            Questions = new List<Question>();
        }
        public void ShowQuestions() {
            Console.WriteLine($"Тема: {Title}");
            foreach (var question in Questions) {
                Console.WriteLine($"Вопрос: {question.Title}");
                Console.WriteLine("Варианты ответов:");
                foreach (var answer in question.Answers) {
                    Console.WriteLine(answer);
                }
                Console.WriteLine();
            }
        }



    }
}
