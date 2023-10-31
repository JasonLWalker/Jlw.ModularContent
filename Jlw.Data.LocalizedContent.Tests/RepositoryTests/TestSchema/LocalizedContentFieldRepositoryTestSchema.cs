using System;
using Jlw.ModularContent;
using Jlw.Utilities.Data.DbUtility;
using Jlw.Utilities.Testing;

namespace Jlw.Data.LocalizedContent.Tests
{
    public class LocalizedContentFieldRepositoryTestSchema : BaseModelSchema<ModularContentFieldRepository>
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
