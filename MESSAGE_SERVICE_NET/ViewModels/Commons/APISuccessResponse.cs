using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MESSAGE_SERVICE_NET.ViewModels.Commons
{
    public class APISuccessResponse<T>:APIResponse<T>
    {
        public APISuccessResponse( T data, int code)
        {
            IsSuccess = true;
            Code = code;
            Data = data;
        }

        public APISuccessResponse(int code)
        {
            IsSuccess = true;
            Code = code;
        }
    }
}
