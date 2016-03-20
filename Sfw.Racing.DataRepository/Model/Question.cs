using Insight.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sfw.Racing.DataRepository.Model
{
    public class Question
    {
        [RecordId]
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Points { get; set; }
        public int SortOrder { get; set; }
        [ChildRecords]
        public IList<Answer> Answers { get; set; }

    }
}
