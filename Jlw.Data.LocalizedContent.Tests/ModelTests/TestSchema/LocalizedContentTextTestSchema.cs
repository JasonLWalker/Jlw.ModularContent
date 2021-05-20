using System;
using System.Collections.Generic;
using Jlw.Utilities.Data;
using Jlw.Utilities.Testing;
using TModel = Jlw.Data.LocalizedContent.LocalizedContentText;

namespace Jlw.Data.LocalizedContent.Tests
{
    public class LocalizedContentTextTestSchema : BaseModelSchema<LocalizedContentText>
    {
        public override IEnumerable<InstanceMemberTestData<TModel>> InstanceMemberTestList
        {
            get
            {
                var groupKey = DataUtility.GenerateRandom<string>();
                var fieldKey = DataUtility.GenerateRandom<string>();

                var language = DataUtility.GenerateRandom<string>();
                var text = DataUtility.GenerateRandom<string>();

                var auditChangeType = DataUtility.GenerateRandom<string>();
                var auditChangeBy = DataUtility.GenerateRandom<string>();
                var auditChangeDate = DataUtility.GenerateRandom<DateTime>();
                var order = DataUtility.GenerateRandom<int>();

                var sut = new TModel(new
                {
                    GroupKey = groupKey,
                    FieldKey = fieldKey,
                    Language = language,
                    Text = text,
                    AuditChangeType = auditChangeType,
                    AuditChangeBy = auditChangeBy,
                    AuditChangeDate = auditChangeDate
                });

                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.GroupKey), groupKey);
                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.FieldKey), fieldKey);
                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.Language), language);
                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.Text), text);
                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.AuditChangeType), auditChangeType);
                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.AuditChangeBy), auditChangeBy);
                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.AuditChangeDate), auditChangeDate);
            }
        }


        protected void InitProperties()
        {
            AddProperty(typeof(string), "GroupKey", Public, Protected);
            AddProperty(typeof(string), "FieldKey", Public, Protected);
            AddProperty(typeof(string), "Language", Public, Protected);
            AddProperty(typeof(string), "Text", Public, Protected);
            AddProperty(typeof(string), "AuditChangeType", Public, Protected);
            AddProperty(typeof(string), "AuditChangeBy", Public, Protected);
            AddProperty(typeof(DateTime), "AuditChangeDate", Public, Protected);
        }

        public void InitInterfaces()
        {
            AddInterface(typeof(ILocalizedContentText));
        }

        public void InitFields()
        {

        }

        public void InitConstructors()
        {
            AddConstructor(Public, new Type[] { });
            AddConstructor(Public, new Type[] { typeof(object) });
        }

        public LocalizedContentTextTestSchema()
        {
            InitProperties();
            InitInterfaces();
            InitFields();
            InitConstructors();
        }

    }
}