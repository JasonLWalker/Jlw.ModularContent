using System;
using Jlw.ModularContent;
using Jlw.Utilities.Data.DbUtility;
using Jlw.Utilities.Testing;

namespace Jlw.Data.LocalizedContent.Tests
{
    public class LocalizedGroupDataItemRepositoryTestSchema : BaseModelSchema<ModularGroupDataItemRepository>
    {
        void InitConstructors()
        {
            AddConstructor(Public, new Type[] { typeof(IModularDbClient), typeof(string) });
        }

        void InitProperties()
        {
        }

        void InitFields()
        {
        }

        void InitInterfaces()
        {
            AddInterface(typeof(IModularGroupDataItemRepository));
            AddInterface(typeof(IModularDataRepositoryBase<IModularGroupDataItem, ModularGroupDataItem>));
        }

        public LocalizedGroupDataItemRepositoryTestSchema()
        {
            InitConstructors();
            InitProperties();
            InitFields();
            InitInterfaces();
        }


    }
}
