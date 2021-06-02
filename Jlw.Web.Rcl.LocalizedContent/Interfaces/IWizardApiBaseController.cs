using Jlw.Data.LocalizedContent;

namespace Jlw.Web.Rcl.LocalizedContent.Interfaces
{
    public interface IWizardApiBaseController
    {
        public object Index(IWizardModelBase model);
        public object SaveWizardPage(IWizardModelBase model);

    }
}