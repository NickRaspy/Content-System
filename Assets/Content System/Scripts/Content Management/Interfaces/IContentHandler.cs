namespace URIMP
{
    public interface IContentHandler
    {
        IContent LoadContent(string filePath);
        void SaveContent(IContent content, string filePath);
    }
}