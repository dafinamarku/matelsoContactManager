using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class Result<R>
    {
        private readonly bool _hasError;
        private readonly string _message;
        private readonly R _result;

        public Result(R result, ResponseType responseType)
        {
            switch (responseType)
            {
                case ResponseType.Success:
                    _message = responseType.ToString();
                    _hasError = false;
                    break;
                case ResponseType.Failure:
                    _message = "Failed to store/get data.";
                    _hasError = true;
                    break;
                case ResponseType.NotFound:
                    _message = "No record was found";
                    _hasError = false;
                    break;
            }
            _result = result;
        }

        public Result(R result, Exception ex)
        {
            _result = result;
            _hasError = true;
            _message = ex.Message + " | StackTrace: " + ex.StackTrace;
        }
        public bool HasError { get { return _hasError;} }
        public string Message { get { return _message;} }
        public R ResultValue { get { return _result; } }
    }
    public enum ResponseType
    {
        Success,
        Failure,
        NotFound
    }
}
