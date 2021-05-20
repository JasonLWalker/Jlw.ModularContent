using System;
using Jlw.Utilities.Data.DbUtility;
using Jlw.Utilities.Testing;
using TRepo = Jlw.Data.LocalizedContent.LocalizedContentTextRepository;

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
            AddInterface(typeof(ILocalizedContentTextRepository));
            AddInterface(typeof(IModularDataRepositoryBase<ILocalizedContentText, LocalizedContentText>));
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
