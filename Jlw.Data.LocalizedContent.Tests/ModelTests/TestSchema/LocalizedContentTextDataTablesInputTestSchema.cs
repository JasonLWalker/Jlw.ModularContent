using System;
using System.Collections.Generic;
using Jlw.Utilities.Data;
using Jlw.Utilities.Data.DataTables;
using Jlw.Utilities.Testing;
using Newtonsoft.Json;
using TModel = Jlw.LocalizedContent.LocalizedContentTextDataTablesInput;

namespace Jlw.Data.LocalizedContent.Tests
{
    public class LocalizedContentTextDataTablesInputTestSchema : BaseModelSchema<TModel>
    {



        public override IEnumerable<InstanceMemberTestData<TModel>> InstanceMemberTestList
        {
            get
            {
                var language = DataUtility.GenerateRandom<string>();
                var groupKey = DataUtility.GenerateRandom<string>();
                var fieldKey = DataUtility.GenerateRandom<string>();
                var parentKey = DataUtility.GenerateRandom<string>();
                var groupFilter = DataUtility.GenerateRandom<string>();
                var draw = DataUtility.GenerateRandom<int>();
                var start = DataUtility.GenerateRandom<int>();
                var length = DataUtility.GenerateRandom<int>();

                string json = "{";
                json += $"draw: {draw},start: {start},length: {length}";
                json += ", columns: [ {data: 'Id'} ], order: [{column:0, dir: 'asc'}],search: {value: 'some text', regex: false}";
                json += $", Language: '{language}'";
                json += $", FieldKey: '{fieldKey}'";
                json += $", ParentKey: '{parentKey}'";
                json += $", GroupKey: '{groupKey}'";
                json += $", GroupFilter: '{groupFilter}'";

                json += "}";

                var sut = JsonConvert.DeserializeObject<TModel>(json) ;

                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.Language), language);
                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.GroupKey), groupKey);
                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.GroupFilter), groupFilter);
                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.FieldKey), fieldKey);
                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.ParentKey), parentKey);
                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.draw), draw);
                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.start), start);
                yield return new InstanceMemberTestData<TModel>(sut, nameof(sut.length), length);
            }
        }


        protected void InitProperties()
        {
            AddProperty(typeof(string), "Language", Public, Public);
            AddProperty(typeof(string), "GroupKey", Public, Public);
            AddProperty(typeof(string), "FieldKey", Public, Public);
            AddProperty(typeof(string), "ParentKey", Public, Public);
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

        public LocalizedContentTextDataTablesInputTestSchema()
        {
            InitProperties();
            InitInterfaces();
            InitFields();
            InitConstructors();
        }

    }
}