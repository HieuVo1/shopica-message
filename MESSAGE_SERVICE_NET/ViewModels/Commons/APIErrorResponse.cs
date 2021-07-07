using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MESSAGE_SERVICE_NET.ViewModels.Commons
{
    public class APIErrorResponse<T>:APIResponse<T>
    {
        public APIErrorResponse(string message, int code)
        {
            IsSuccess = false;
            Message = message;
            Code = code;
        }

        public APIErrorResponse(int code)
        {
            IsSuccess = false;
            Code = code;
        }
    }
}
