using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace partycli.Helpers
{
    public class SuccessResult<T> : IRequestResult<T>
    {
        public bool Success { get { return true; } }
        public T Result { get; private set; }
        public string ErrorMessage { get { return null; } }
        public SuccessResult(T result) { this.Result = result; }
    }
    public class FailedResult : IRequestResult<string>
    {
        public bool Success { get { return false; } }
        public string Result { get; private set; }
        public string ErrorMessage { get; private set; }
        public FailedResult(string result) { ErrorMessage = result; }
    }
}

