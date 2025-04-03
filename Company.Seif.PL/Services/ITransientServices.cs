namespace Company.Seif.PL.Services
{
    public interface ITransientServices
    {
        public Guid Guid { get; set; }

        string GetGuid();
    }
}
