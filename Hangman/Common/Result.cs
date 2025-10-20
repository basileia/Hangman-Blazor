namespace Hangman.Common
{
    public class Result<TValue, TError>
    {
        public TValue? Value { get; }
        public TError? Error { get; }
        public bool IsSuccess { get; }

        private Result(TValue value)
        {
            IsSuccess = true;
            Value = value;
            Error = default;
        }

        private Result(TError error)
        {
            IsSuccess = false;
            Value = default;
            Error = error;
        }

        public static Result<TValue, TError> Ok(TValue value) => new(value);
        public static Result<TValue, TError> Fail(TError error) => new(error);
    }
}
