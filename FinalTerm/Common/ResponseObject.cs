namespace FinalTerm.Common {
    public class ResponseObject<T> {
        public int Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public ResponseObject(int Status, string Message, T Data) {
            this.Status = Status;
            this.Message = Message;
            this.Data = Data;
        }
    }
}
