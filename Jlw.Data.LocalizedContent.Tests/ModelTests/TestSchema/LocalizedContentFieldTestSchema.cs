using System;
using System.Collections.Generic;
using Jlw.Utilities.Data;
using Jlw.Utilities.Testing;
using TModel = Jlw.Data.LocalizedContent.LocalizedContentField;

namespace Jlw.Data.LocalizedContent.Tests
{
    public class LocalizedContentFieldTestSchema : BaseModelSchema<TModel>
    {



        public override IEnumerable<InstanceMemberTestData<TModel>> InstanceMemberTestList
        {
            get
            {
                var id = DataUtility.GenerateRandom<long>();
                var groupKey = DataUtility.GenerateRandom<string>();
                var fieldKey = DataUtility.GenerateRandom<string>();
                var fieldType = DataUtility.GenerateRandom<string>();
                var fieldData = DataUtility.GenerateRandom<string>();
                var fieldClass = DataUtility.GenerateRandom<string>();
                var parentKey = DataUtility.GenerateRandom<string>();
                var defaultLabel = DataUtility.GenerateRandom<string>();
                var wrapperClass = DataUtility.GenerateRandom<string>();
                var wrapperHtmlStart = DataUtility.GenerateRandom<string>();
                var wrapperHtmlEnd = DataUtility.GenerateRandom<string>();
                var auditChangeType = DataUtility.GenerateRandom<string>();
                var auditChangeBy = DataUtility.GenerateRandom<string>();
                var auditChangeDate = DataUtility.GenerateRandom<DateTime>();
                var order = DataUtility.GenerateRandom<int>();
                var groupFilter = DataUtility.GenerateRandom<string>();
                var language = DataUtility.GenerateRandom<string>();

                var sut = new TModel(new
                {
                    Id = id,
                    GroupKey = groupKey,
                    FieldKey = fieldKey,
                    FieldType = fieldType,
                    FieldData = fieldData,
                    FieldClass = fieldClass,
                    ParentKey = parentKey,
                    DefaultLabel = defaultLabel,
                    WrapperClass = wrapperClass,
                    WrapperHtmlStart = wrapperHtmlStart,
                    WrapperHtmlEnd = wrapperHtmlEnd,
                    AuditChangeType = auditChangeType,
                    AuditChangeBy = auditChangeBy,
                    AuditChangeDate = auditChangeDate,
                    Order = order,
                    GroupFilter = groupFilter,
                });

                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.Id), id);
                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.GroupKey), groupKey);
                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.GroupFilter), groupFilter);
                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.FieldKey), fieldKey);
                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.FieldType), fieldType);
                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.FieldData), fieldData);
                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.FieldClass), fieldClass);
                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.ParentKey), parentKey);
                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.DefaultLabel), defaultLabel);
                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.WrapperClass), wrapperClass);
                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.WrapperHtmlStart), wrapperHtmlStart);
                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.WrapperHtmlEnd), wrapperHtmlEnd);
                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.AuditChangeType), auditChangeType);
                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.AuditChangeBy), auditChangeBy);
                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.AuditChangeDate), auditChangeDate);
                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.Order), order);
            }
        }


        protected void InitProperties()
        {
            AddProperty(typeof(long), "Id", Public, Protected);
            AddProperty(typeof(string), "GroupKey", Public, Protected);
            AddProperty(typeof(string), "FieldKey", Public, Protected);
            AddProperty(typeof(string), "FieldType", Public, Protected);
            AddProperty(typeof(string), "FieldData", Public, Protected);
            AddProperty(typeof(string), "FieldClass", Public, Protected);
            AddProperty(typeof(string), "ParentKey", Public, Protected);
            AddProperty(typeof(string), "DefaultLabel", Public, Protected);
            AddProperty(typeof(string), "WrapperClass", Public, Protected);
            AddProperty(typeof(string), "WrapperHtmlStart", Public, Protected);
            AddProperty(typeof(string), "WrapperHtmlEnd", Public, Protected);
            AddProperty(typeof(string), "AuditChangeType", Public, Protected);
            AddProperty(typeof(string), "AuditChangeBy", Public, Protected);
            AddProperty(typeof(DateTime), "AuditChangeDate", Public, Protected);
            AddProperty(typeof(int), "Order", Public, Protected);
            AddProperty(typeof(string), "GroupFilter", Public, Public);
        }

        public void InitInterfaces()
        {
            AddInterface(typeof(ILocalizedContentField));
        }

        public void InitFields()
        {

        }

        public void InitConstructors()
        {
            AddConstructor(Public, new Type[] { });
            AddConstructor(Public, new Type[] { typeof(object) });
        }

        public LocalizedContentFieldTestSchema()
        {
            InitProperties();
            InitInterfaces();
            InitFields();
            InitConstructors();
        }

    }
}