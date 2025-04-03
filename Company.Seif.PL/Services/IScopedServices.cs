namespace Company.Seif.PL.Services
{
    public interface IScopedServices
    {
        public Guid Guid { get; set; }

        string GetGuid();
    }
}
