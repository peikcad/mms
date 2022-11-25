namespace peikcad.mms.domain.model.overlord
{
    public interface IOverlordContext
    {
        public string IID { get; internal set; }
        
        public string Name { get; internal set; }
        
        public DateTime BirthDate { get; internal set; }
    }
}