using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sfw.Racing.DataRepository.Model
{
    public class Response<T> 
    {
        public bool Success { get; set; }
        public StatusCode Status { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
    }
}
