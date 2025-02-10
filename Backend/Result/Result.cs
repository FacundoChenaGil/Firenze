using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class Result<T>
    {
        private Result(T value)
        {
            Value = value;
            Errors = new List<Error>();
        }

        private Result(List<Error> errors)
        {
            Errors = errors;
            Value = default;
        }

        public T? Value { get; }
        public List<Error> Errors { get; }
        public bool IsSuccess => Errors.Count == 0;

        public static Result<T> Success(T value) => new Result<T>(value);
        public static Result<T> Failure(List<Error> errors) => new Result<T>(errors);

        public TResult Map<TResult>(Func<T, TResult> onSuccess, Func<List<Error>, TResult> onFailure)
        {
            return IsSuccess ? onSuccess(Value!) : onFailure(Errors);
        }


    }
}
