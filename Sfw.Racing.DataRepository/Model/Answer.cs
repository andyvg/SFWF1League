using Insight.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sfw.Racing.DataRepository.Model
{
    public class Answer
    {
        [RecordId]
        public int AnswerId { get; set; }
        [ParentRecordId]
        public int QuestionId { get; set; }
        public string AnswerText { get; set; }
        public int SortOrder { get; set; }
        public bool CorrectAnswer { get; set; }

    }
}
