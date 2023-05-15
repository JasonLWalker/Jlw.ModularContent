using System;
using System.Collections.Generic;
using Jlw.LocalizedContent;
using Jlw.Utilities.Data;
using Jlw.Utilities.Testing;
using TModel = Jlw.LocalizedContent.LocalizedGroupDataItem;

namespace Jlw.Data.LocalizedContent.Tests
{
    public class LocalizedGroupDataItemTestSchema : BaseModelSchema<TModel>
    {

        public override IEnumerable<InstanceMemberTestData<TModel>> InstanceMemberTestList
        {
            get
            {
                var id = DataUtility.GenerateRandom<long>();
                var language = DataUtility.GenerateRandom<string>();
                var groupKey = DataUtility.GenerateRandom<string>();
                var key = DataUtility.GenerateRandom<string>();
                var value = DataUtility.GenerateRandom<string>();
                var order = DataUtility.GenerateRandom<int>();
                var description = DataUtility.GenerateRandom<string>();
                var data = DataUtility.GenerateRandom<string>();
                var auditChangeType = DataUtility.GenerateRandom<string>();
                var auditChangeBy = DataUtility.GenerateRandom<string>();
                var auditChangeDate = DataUtility.GenerateRandom<DateTime>();

                var sut = new TModel(new
                {
                    Id = id,
                    GroupKey = groupKey,
                    Language = language,
                    Key = key,
                    Value = value,
                    Order = order,
                    Description = description,
                    Data = data,
                    AuditChangeType = auditChangeType,
                    AuditChangeBy = auditChangeBy,
                    AuditChangeDate = auditChangeDate
                });

                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.Id), id);
                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.Language), language);
                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.GroupKey), groupKey);
                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.Key), key);
                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.Value), value);
                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.Order), order);
                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.Description), description);
                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.Data), data);
                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.AuditChangeType), auditChangeType);
                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.AuditChangeBy), auditChangeBy);
                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.AuditChangeDate), auditChangeDate);
            }
        }


        protected void InitProperties()
        {
            AddProperty(typeof(long), "Id", Public, Protected);
            AddProperty(typeof(string), "Language", Public, Protected);
            AddProperty(typeof(string), "GroupKey", Public, Protected);
            AddProperty(typeof(string), "Key", Public, Protected);
            AddProperty(typeof(string), "Value", Public, Protected);
            AddProperty(typeof(int), "Order", Public, Protected);
            AddProperty(typeof(string), "Description", Public, Protected);
            AddProperty(typeof(string), "Data", Public, Protected);
            AddProperty(typeof(string), "AuditChangeType", Public, Protected);
            AddProperty(typeof(string), "AuditChangeBy", Public, Protected);
            AddProperty(typeof(DateTime), "AuditChangeDate", Public, Protected);
        }

        public void InitInterfaces()
        {
            AddInterface(typeof(ILocalizedGroupDataItem));
        }

        public void InitFields()
        {

        }

        public void InitConstructors()
        {
            AddConstructor(Public, new Type[] { });
            AddConstructor(Public, new Type[] { typeof(object) });
        }

        public LocalizedGroupDataItemTestSchema()
        {
            InitProperties();
            InitInterfaces();
            InitFields();
            InitConstructors();
        }

    }
}