namespace TeamConnect.Helpers
{
    public class ServerResponse<TSuccess, TError>
    {
        public TSuccess SuccessResult { get; set; }

        public TError ErrorResult { get; set; }

        public bool IsSuccess { get; set; }

        public int Code { get; set; }
    }
}
