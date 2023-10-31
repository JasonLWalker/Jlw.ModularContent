namespace Jlw.ModularContent
{
    public interface IModularWizardApiBaseController
    {
        public object Index(IModularWizardModelBase model);
        public object SaveWizardPage(IModularWizardModelBase model);

    }
}