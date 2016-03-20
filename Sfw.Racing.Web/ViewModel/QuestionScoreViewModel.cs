using Sfw.Racing.DataRepository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sfw.Racing.Web.ViewModel
{
    public class QuestionScoreViewModel
    {
        public QuestionScoreViewModel()
        {
            Question = new Question();
            QuestionPoints = new QuestionPoints();
        }

        public Question Question { get; set; }
        public QuestionPoints QuestionPoints { get; set; }
    }
}