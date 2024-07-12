using DTO.Interface;
using Models.Base.Output;

namespace Models.Base.Output
{
    public class DAOActionResultOutput : BaseOutputModel
    {
        public DAOActionResultOutput(bool scs) : base(scs) { }

        public DAOActionResultOutput(string msg) : base(msg) { }
        public DAOActionResultOutput(IBaseData data) : base(true) => Data = data;

        public IBaseData Data { get; set; }

    }
}
