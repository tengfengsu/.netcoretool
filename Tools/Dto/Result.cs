using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Tools.Dto
{
    public class Result
    {
        public ResultCode Code { get; set; }
        public string Message { get; set; }

        public Result(ResultCode code, string message = null)
        {
            Code = code;
            Message = message;
        }

        public static Result<TData> FromData<TData>(TData data)
        {
            return new Result<TData>(ResultCode.Success, "", data);
        }
        public static Result FromSuccess(string message = null)
        {
            return new Result(ResultCode.Success, message);
        }
    }

    public class Result<TData> : Result
    {
        public TData Data { get; set; }

        public Result(ResultCode code, string message = null, TData data = default) : base(code, message)
        {
            Data = data;
        }
    }

    public enum ResultCode
    {
        Success = 200,
    }
}
