using System;

namespace TeamConnect.Helpers
{
    public class OperationResult
    {
        #region -- Public properties --

        public bool IsSuccess { get; private set; }

        public string Message { get; private set; }

        public string ErrorName { get; private set; }

        public Exception Exception { get; private set; }

        #endregion

        #region -- Public helpers --

        public void SetSuccess()
        {
            SetResult(true, null, null, null);
        }

        public void SetFailure()
        {
            SetResult(false, null, null, null);
        }

        public void SetFailure(string message)
        {
            SetResult(false, null, message, null);
        }

        public void SetError(string errorName, string message, Exception exception = null)
        {
            SetResult(false, errorName, message, exception);
        }

        #endregion

        #region -- Private helpers --

        private void SetResult(bool isSuccess, string errorName, string message, Exception exception)
        {
            IsSuccess = isSuccess;
            ErrorName = errorName;
            Message = message;
            Exception = exception;
        }

        #endregion
    }

    public class OperationResult<T> : OperationResult
    {
        public T Result { get; private set; }

        #region -- Public properties --

        public void SetSuccess(T result)
        {
            Result = result;
            SetSuccess();
        }

        public void SetFailure(T result)
        {
            Result = result;
            SetFailure();
        }

        #endregion
    }
}
