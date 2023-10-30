using System;
using Jlw.ModularContent;
using Jlw.Utilities.Data.DbUtility;
using Jlw.Utilities.Testing;
using TRepo = Jlw.ModularContent.LocalizedContentTextRepository;

namespace Jlw.Data.LocalizedContent.Tests
{
    public class LocalizedContentTextRepositoryTestSchema : BaseModelSchema<TRepo>
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
