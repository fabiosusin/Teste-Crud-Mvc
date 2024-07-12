namespace Models.Base.Output
{
    public class BaseOutputModel
    {
        public BaseOutputModel(bool scs) => Success = scs;
        public BaseOutputModel(bool scs, string msg)
        {
            Message = msg;
            Success = scs;
        }
        public BaseOutputModel(string msg)
        {
            Message = msg;
            Success = false;
        }

        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
