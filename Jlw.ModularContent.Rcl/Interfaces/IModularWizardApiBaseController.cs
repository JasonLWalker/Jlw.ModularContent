namespace Jlw.ModularContent
{
    public interface IModularWizardApiBaseController
    {
        public object Index(IWizardModelBase model);
        public object SaveWizardPage(IWizardModelBase model);

    }
}