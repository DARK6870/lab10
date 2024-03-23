using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab10
{
    public class QuestionModel
    {
        public int CategoryId { get; set; }
        public string? CorrectAnswer { get; set; }
        public string? ImageUrl { get; set; }
    }
}
