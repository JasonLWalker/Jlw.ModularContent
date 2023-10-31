using System;
using Jlw.ModularContent;
using Jlw.Utilities.Data.DbUtility;
using Jlw.Utilities.Testing;

namespace Jlw.Data.LocalizedContent.Tests
{
    public class LocalizedContentTextRepositoryTestSchema : BaseModelSchema<ModularContentTextRepository>
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
            AddInterface(typeof(IModularContentTextRepository));
            AddInterface(typeof(IModularDataRepositoryBase<IModularContentText, ModularContentText>));
        }

        public LocalizedContentTextRepositoryTestSchema()
        {
            InitConstructors();
            InitProperties();
            InitFields();
            InitInterfaces();
        }


    }
}
