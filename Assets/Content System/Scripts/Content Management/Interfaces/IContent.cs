namespace URIMP
{
    public interface IContent
    {
        string Id { get; }
        string Name { get; }
        void Load();
        void Save();
        void Delete();
    }
}