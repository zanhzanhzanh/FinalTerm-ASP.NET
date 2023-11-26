namespace FinalTerm.Common.HandlingException {
    public class ApiException : Exception {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

        public ApiException(int errorCode, string errorMessage) {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }
    }
}
