using OneOf;

namespace PriceHubble.Client.Results
{
    public class ServiceResult<TSuccessResult> : OneOfBase<
        TSuccessResult,
        ServerError,
        UnexpectedError>
    {
        protected ServiceResult(OneOf<TSuccessResult, ServerError,  UnexpectedError> val)
            : base(val)
        {

        }

        public bool IsSuccess => IsT0;
        public bool IsServerError => IsT1;
        public bool IsUnexpectedError => IsT2;

        public TSuccessResult AsSuccess => AsT0;
        public ServerError AsServerError => AsT1;
        public UnexpectedError AsUnexpectedError => AsT2;

        public static implicit operator ServiceResult<TSuccessResult>(TSuccessResult successValue)
        {
            return new ServiceResult<TSuccessResult>(successValue);
        }

        public static implicit operator ServiceResult<TSuccessResult>(ServerError serverError)
        {
            return new ServiceResult<TSuccessResult>(serverError);
        }

        public static implicit operator ServiceResult<TSuccessResult>(UnexpectedError unexpectedError)
        {
            return new ServiceResult<TSuccessResult>(unexpectedError);
        }

        public static ServiceResult<TSuccessResult> WithSuccess(TSuccessResult successValue) => successValue;
        public static ServiceResult<TSuccessResult> WithServerError(ServerError serverError) => serverError;
        public static ServiceResult<TSuccessResult> WithUnexpectedError(UnexpectedError uexpectedError) => uexpectedError;
    }
}