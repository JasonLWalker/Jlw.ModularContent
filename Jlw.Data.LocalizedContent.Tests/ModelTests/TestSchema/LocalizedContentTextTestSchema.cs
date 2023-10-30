using System;
using System.Collections.Generic;
using Jlw.ModularContent;
using Jlw.Utilities.Data;
using Jlw.Utilities.Testing;

namespace Jlw.Data.LocalizedContent.Tests
{
    public class LocalizedContentTextTestSchema : BaseModelSchema<ModularContentText>
    {
        public override IEnumerable<InstanceMemberTestData<ModularContentText>> InstanceMemberTestList
        {
            get
            {
                var groupKey = DataUtility.GenerateRandom<string>();
                var fieldKey = DataUtility.GenerateRandom<string>();
                var parentKey = DataUtility.GenerateRandom<string>();

                var language = DataUtility.GenerateRandom<string>();
                var text = DataUtility.GenerateRandom<string>();

                var auditChangeType = DataUtility.GenerateRandom<string>();
                var auditChangeBy = DataUtility.GenerateRandom<string>();
                var auditChangeDate = DataUtility.GenerateRandom<DateTime>();
                var order = DataUtility.GenerateRandom<int>();

                var sut = new ModularContentText(new
                {
                    GroupKey = groupKey,
                    FieldKey = fieldKey,
                    ParentKey = parentKey,
                    Language = language,
                    Text = text,
                    AuditChangeType = auditChangeType,
                    AuditChangeBy = auditChangeBy,
                    AuditChangeDate = auditChangeDate
                });

                yield return new InstanceMemberTestData<ModularContentText>(sut, nameof(sut.GroupKey), groupKey);
                yield return new InstanceMemberTestData<ModularContentText>(sut, nameof(sut.FieldKey), fieldKey);
                yield return new InstanceMemberTestData<ModularContentText>(sut, nameof(sut.ParentKey), parentKey);
                yield return new InstanceMemberTestData<ModularContentText>(sut, nameof(sut.Language), language);
                yield return new InstanceMemberTestData<ModularContentText>(sut, nameof(sut.Text), text);
                yield return new InstanceMemberTestData<ModularContentText>(sut, nameof(sut.AuditChangeType), auditChangeType);
                yield return new InstanceMemberTestData<ModularContentText>(sut, nameof(sut.AuditChangeBy), auditChangeBy);
                yield return new InstanceMemberTestData<ModularContentText>(sut, nameof(sut.AuditChangeDate), auditChangeDate);
            }
        }


        protected void InitProperties()
        {
            AddProperty(typeof(string), "GroupKey", Public, Protected);
            AddProperty(typeof(string), "FieldKey", Public, Protected);
            AddProperty(typeof(string), "ParentKey", Public, Protected);
            AddProperty(typeof(string), "Language", Public, Protected);
            AddProperty(typeof(string), "Text", Public, Protected);
            AddProperty(typeof(string), "AuditChangeType", Public, Protected);
            AddProperty(typeof(string), "AuditChangeBy", Public, Protected);
            AddProperty(typeof(DateTime), "AuditChangeDate", Public, Protected);
        }

        public void InitInterfaces()
        {
            AddInterface(typeof(IModularContentText));
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