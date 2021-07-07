using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MESSAGE_SERVICE_NET.ViewModels.Commons
{
    public class APIResponse<T>
    {
        public int Code { get; set; }
        public bool  IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
