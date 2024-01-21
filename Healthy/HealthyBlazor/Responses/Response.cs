using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthy.Domain.Responses
{
    public class Response<T>
    {
        public IEnumerable<string> Errors { get; set; } = new List<string>();
        public string Message { get; set; } = string.Empty;
        public IEnumerable<T> Result { get; set; } 
    }
}
