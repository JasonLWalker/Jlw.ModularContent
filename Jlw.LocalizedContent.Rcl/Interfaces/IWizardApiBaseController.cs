namespace Jlw.ModularContent
{
    public interface IWizardApiBaseController
    {
        public object Index(IWizardModelBase model);
        public object SaveWizardPage(IWizardModelBase model);

    }
}