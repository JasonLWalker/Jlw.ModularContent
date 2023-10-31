using System;
using Jlw.ModularContent;
using Jlw.Utilities.Data.DbUtility;
using Jlw.Utilities.Testing;
using TRepo = Jlw.ModularContent.LocalizedGroupDataItemRepository;

namespace Jlw.Data.LocalizedContent.Tests
{
    public class LocalizedGroupDataItemRepositoryTestSchema : BaseModelSchema<TRepo>
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
            AddInterface(typeof(IModularDataRepositoryBase<IModularGroupDataItem, LocalizedGroupDataItem>));
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
