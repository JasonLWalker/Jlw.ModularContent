namespace Jlw.LocalizedContent
{
    public interface IWizardApiBaseController
    {
        public object Index(IWizardModelBase model);
        public object SaveWizardPage(IWizardModelBase model);

    }
}