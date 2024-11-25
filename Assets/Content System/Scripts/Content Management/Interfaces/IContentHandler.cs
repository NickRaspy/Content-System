namespace URIMP
{
    public interface IContentHandler
    {
        IContent LoadContent(string filePath);
        void SaveContent(IContent content, string filePath);
        void SaveSubcontent(ISubcontent subcontent, string filePath);
        void EditContent(IContent previousContent, IContent newContent, string filePath);
        void EditSubcontent(ISubcontent previousSubcontent, ISubcontent newSubcontent, string filePath);
    }
}