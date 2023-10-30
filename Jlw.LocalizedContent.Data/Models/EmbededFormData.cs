using System.Collections.Generic;

namespace Jlw.ModularContent
{
    public class EmbededFormData : WizardFormData
    {
        public EmbededFormData(IWizardContentField baseField, string formKey, string screenName, IEnumerable<WizardContentField> fieldData, WizardButtonData editButton = null) : base(formKey, screenName, fieldData, editButton)
        {
            FieldKey = baseField.FieldKey;
            ParentKey = baseField.ParentKey;
            FieldData = baseField.FieldData;
            AuditChangeBy = baseField.AuditChangeBy;
            AuditChangeDate = baseField.AuditChangeDate;
            AuditChangeType = baseField.AuditChangeType;

            if (!string.IsNullOrWhiteSpace(baseField.DefaultLabel)) DefaultLabel = baseField.DefaultLabel;
            if (!string.IsNullOrWhiteSpace(baseField.FieldClass)) FieldClass = baseField.FieldClass;

            FieldType = baseField.FieldType;
            Id = baseField.Id;
            if (!string.IsNullOrWhiteSpace(baseField.Label)) Label = baseField.Label;
            
            Order = baseField.Order;
            
            if (!string.IsNullOrWhiteSpace(baseField.WrapperClass)) WrapperClass = baseField.WrapperClass;
            if (!string.IsNullOrWhiteSpace(baseField.WrapperHtmlEnd)) WrapperHtmlEnd = baseField.WrapperHtmlEnd;
            if (!string.IsNullOrWhiteSpace(baseField.WrapperHtmlStart)) WrapperHtmlStart = baseField.WrapperHtmlStart;
            
            GroupKey = baseField.GroupKey;
            GroupFilter = baseField.GroupFilter;
        }
    }
}