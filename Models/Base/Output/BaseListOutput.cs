namespace Models.Base.Output
{
    public class BaseListOutputModel<T> : BaseOutputModel
    {
        public BaseListOutputModel(string msg) : base(msg) { }
        public BaseListOutputModel(IEnumerable<T> items) : base(true) => Items = items;

        public IEnumerable<T> Items { get; set; }
    }
}
