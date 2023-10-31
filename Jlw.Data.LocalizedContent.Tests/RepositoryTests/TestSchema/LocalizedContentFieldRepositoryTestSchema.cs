using System;
using Jlw.ModularContent;
using Jlw.Utilities.Data.DbUtility;
using Jlw.Utilities.Testing;
using TRepo = Jlw.ModularContent.LocalizedContentFieldRepository;

namespace Jlw.Data.LocalizedContent.Tests
{
    public class LocalizedContentFieldRepositoryTestSchema : BaseModelSchema<TRepo>
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
            AddInterface(typeof(IModularContentFieldRepository));
            AddInterface(typeof(IModularDataRepositoryBase<IModularContentField, ModularContentField>));
        }

        public LocalizedContentFieldRepositoryTestSchema()
        {
            InitConstructors();
            InitProperties();
            InitFields();
            InitInterfaces();
        }


    }
}
