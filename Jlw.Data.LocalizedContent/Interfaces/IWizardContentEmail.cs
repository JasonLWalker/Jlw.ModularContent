namespace Jlw.Data.LocalizedContent
{
    public interface IWizardContentEmail
    {
        string GroupKey { get; set; }
        string Subject { get; set; }
        string Body { get; set; }
        object FormData { get; set; }
    }
}