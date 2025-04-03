namespace Company.Seif.PL.Services
{
    public interface ISingletonServices
    {
        public Guid Guid { get; set; }

        string GetGuid();
    }
}
