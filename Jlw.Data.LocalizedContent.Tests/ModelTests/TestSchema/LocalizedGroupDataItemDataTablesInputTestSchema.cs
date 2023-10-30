using System;
using System.Collections.Generic;
using Jlw.Utilities.Data;
using Jlw.Utilities.Data.DataTables;
using Jlw.Utilities.Testing;
using Newtonsoft.Json;
using TModel = Jlw.ModularContent.LocalizedGroupDataItemDataTablesInput;

namespace Jlw.Data.LocalizedContent.Tests
{
    public class LocalizedGroupDataItemDataTablesInputTestSchema : BaseModelSchema<TModel>
    {



        public override IEnumerable<InstanceMemberTestData<TModel>> InstanceMemberTestList
        {
            get
            {
                var id = DataUtility.GenerateRandom<long>();
                var groupKey = DataUtility.GenerateRandom<string>();
                var fieldKey = DataUtility.GenerateRandom<string>();
                var fieldType = DataUtility.GenerateRandom<string>();
                var parentKey = DataUtility.GenerateRandom<string>();
                var groupFilter = DataUtility.GenerateRandom<string>();
                var draw = DataUtility.GenerateRandom<int>();
                var start = DataUtility.GenerateRandom<int>();
                var length = DataUtility.GenerateRandom<int>();

                string json = "{";
                json += $"draw: {draw},start: {start},length: {length}";
                json += ", columns: [ {data: 'Id'} ], order: [{column:0, dir: 'asc'}],search: {value: 'some text', regex: false}";
                json += $", Id: {id}";
                json += $", FieldType: '{fieldType}'";
                json += $", FieldKey: '{fieldKey}'";
                json += $", ParentKey: '{parentKey}'";
                json += $", GroupKey: '{groupKey}'";
                json += $", GroupFilter: '{groupFilter}'";

                json += "}";

                var sut = JsonConvert.DeserializeObject<TModel>(json) ;

                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.Id), id);
//                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.FieldClass), fieldClass);
                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.draw), draw);
                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.start), start);
                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.length), length);
/*                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.DefaultLabel), defaultLabel);
                                                                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.WrapperClass), wrapperClass);
                                                                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.WrapperHtmlStart), wrapperHtmlStart);
                                                                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.WrapperHtmlEnd), wrapperHtmlEnd);
                                                                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.AuditChangeType), auditChangeType);
                                                                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.AuditChangeBy), auditChangeBy);
                                                                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.AuditChangeDate), auditChangeDate);
                                                                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.Order), order);*/
            }
        }


        protected void InitProperties()
        {
            AddProperty(typeof(long), "Id", Public, Public);
            AddProperty(typeof(string), "GroupKey", Public, Public);
            AddProperty(typeof(string), "FieldKey", Public, Public);
            //AddProperty(typeof(string), "FieldType", Public, Public);
            //AddProperty(typeof(string), "ParentKey", Public, Public);
            AddProperty(typeof(string), "GroupFilter", Public, Public);
            AddProperty(typeof(int), "draw", Public, Public);
            AddProperty(typeof(int), "start", Public, Public);
            AddProperty(typeof(int), "length", Public, Public);
            AddProperty(typeof(DataTablesInputSearch), "search", Public, Public);
            AddProperty(typeof(IEnumerable<DataTablesInputOrder>), "order", Public, Public);
            AddProperty(typeof(IEnumerable<DataTablesInputColumn>), "columns", Public, Public);
        }

        public void InitInterfaces()
        {
            AddInterface(typeof(IDataTablesInput));
        }

        public void InitFields()
        {

        }

        public void InitConstructors()
        {
            AddConstructor(Public, new Type[] { });
        }

        public LocalizedGroupDataItemDataTablesInputTestSchema()
        {
            InitProperties();
            InitInterfaces();
            InitFields();
            InitConstructors();
        }

    }
}