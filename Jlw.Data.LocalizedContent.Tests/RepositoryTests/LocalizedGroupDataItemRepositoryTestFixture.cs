using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Jlw.Utilities.Data.DbUtility;
using Jlw.Utilities.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TRepo = Jlw.Data.LocalizedContent.LocalizedGroupDataItemRepository;
using TModel = Jlw.Data.LocalizedContent.LocalizedGroupDataItem;

namespace Jlw.Data.LocalizedContent.Tests
{
    [TestClass]
    public class LocalizedGroupDataItemRepositoryTestFixture : BaseModelFixture<TRepo, LocalizedGroupDataItemRepositoryTestSchema>
    {
        [TestMethod]
        public void SqlParams_Should_Match_For_GetRecord()
        {
            var mockClient = new Mock<IModularDbClient>();
            var connStr = new DbConnectionStringBuilder();
            var input = new TModel(new { Id = 1 });
            var output = new TModel(new { Id = 1 });
            mockClient.Setup(m => m.GetConnectionBuilder(It.IsAny<string>())).Returns(connStr);
            mockClient.Setup(m => m.GetRecordObject<TModel>(It.IsAny<object>(), It.IsAny<string>(), It.IsAny<IRepositoryMethodDefinition>())).Returns(output);
            TRepo sut = new TRepo(mockClient.Object, "");
            var o = sut.GetRecord(input);

            mockClient.Verify(m => m.GetConnectionBuilder(It.IsAny<string>()));

            mockClient.Verify(m => m.GetRecordObject<TModel>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.Any(param => param.ParameterName == "@id"))));
            mockClient.Verify(m => m.GetRecordObject<TModel>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@language"))));
            mockClient.Verify(m => m.GetRecordObject<TModel>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@groupkey"))));
            mockClient.Verify(m => m.GetRecordObject<TModel>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@key"))));
            mockClient.Verify(m => m.GetRecordObject<TModel>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@value"))));
            mockClient.Verify(m => m.GetRecordObject<TModel>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@order"))));
            mockClient.Verify(m => m.GetRecordObject<TModel>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@description"))));
            mockClient.Verify(m => m.GetRecordObject<TModel>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@data"))));
            mockClient.Verify(m => m.GetRecordObject<TModel>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@auditchangeby"))));
            mockClient.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void SqlParams_Should_Match_For_GetAllRecords()
        {
            var mockClient = new Mock<IModularDbClient>();
            var connStr = new DbConnectionStringBuilder();
            //var input = new TModel(new { Id = 1 });
            IEnumerable<TModel> output = new []{ new TModel(new { Id = 1 }) };
            mockClient.Setup(m => m.GetConnectionBuilder(It.IsAny<string>())).Returns(connStr);
            mockClient.Setup(m => m.GetRecordList<TModel>(It.IsAny<object>(), It.IsAny<string>(), It.IsAny<IRepositoryMethodDefinition>())).Returns(output);
            TRepo sut = new TRepo(mockClient.Object, "");
            var o = sut.GetAllRecords();

            mockClient.Verify(m => m.GetConnectionBuilder(It.IsAny<string>()));
            mockClient.Verify(m => m.GetRecordList<TModel>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@id"))));
            mockClient.Verify(m => m.GetRecordList<TModel>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@id"))));
            mockClient.Verify(m => m.GetRecordList<TModel>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@language"))));
            mockClient.Verify(m => m.GetRecordList<TModel>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@groupkey"))));
            mockClient.Verify(m => m.GetRecordList<TModel>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@key"))));
            mockClient.Verify(m => m.GetRecordList<TModel>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@value"))));
            mockClient.Verify(m => m.GetRecordList<TModel>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@order"))));
            mockClient.Verify(m => m.GetRecordList<TModel>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@description"))));
            mockClient.Verify(m => m.GetRecordList<TModel>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@data"))));
            mockClient.Verify(m => m.GetRecordList<TModel>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@auditchangeby"))));
            mockClient.Verify(m => m.GetRecordList<TModel>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => !def.Parameters.Any())));
            mockClient.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void SqlParams_Should_Match_For_SaveRecord()
        {
            var mockClient = new Mock<IModularDbClient>();
            var connStr = new DbConnectionStringBuilder();
            var input = new TModel(new { Id = 1 });
            var output = new TModel(new { Id = 1 });
            mockClient.Setup(m => m.GetConnectionBuilder(It.IsAny<string>())).Returns(connStr);
            mockClient.Setup(m => m.GetRecordObject<TModel>(It.IsAny<object>(), It.IsAny<string>(), It.IsAny<IRepositoryMethodDefinition>())).Returns(output);

            TRepo sut = new TRepo(mockClient.Object, "");

            var o = sut.SaveRecord(input);

            mockClient.Verify(m => m.GetConnectionBuilder(It.IsAny<string>()));

            mockClient.Verify(m => m.GetRecordObject<TModel>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.Any(param => param.ParameterName == "@id"))));
            mockClient.Verify(m => m.GetRecordObject<TModel>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.Any(param => param.ParameterName == "@language"))));
            mockClient.Verify(m => m.GetRecordObject<TModel>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.Any(param => param.ParameterName == "@groupkey"))));
            mockClient.Verify(m => m.GetRecordObject<TModel>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.Any(param => param.ParameterName == "@key"))));
            mockClient.Verify(m => m.GetRecordObject<TModel>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.Any(param => param.ParameterName == "@value"))));
            mockClient.Verify(m => m.GetRecordObject<TModel>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.Any(param => param.ParameterName == "@order"))));
            mockClient.Verify(m => m.GetRecordObject<TModel>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.Any(param => param.ParameterName == "@description"))));
            mockClient.Verify(m => m.GetRecordObject<TModel>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.Any(param => param.ParameterName == "@data"))));
            mockClient.Verify(m => m.GetRecordObject<TModel>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.Any(param => param.ParameterName == "@auditchangeby"))));

            mockClient.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void SqlParams_Should_Match_For_DeleteRecord()
        {
            var mockClient = new Mock<IModularDbClient>();
            var connStr = new DbConnectionStringBuilder();
            var input = new TModel(new { Id = 1 });
            var output = new TModel(new { Id = 1 });
            mockClient.Setup(m => m.GetConnectionBuilder(It.IsAny<string>())).Returns(connStr);
            mockClient.Setup(m => m.GetRecordObject<TModel>(It.IsAny<object>(), It.IsAny<string>(), It.IsAny<IRepositoryMethodDefinition>())).Returns(output);

            TRepo sut = new TRepo(mockClient.Object, "");

            var o = sut.DeleteRecord(input);

            mockClient.Verify(m => m.GetConnectionBuilder(It.IsAny<string>()));

            mockClient.Verify(m => m.GetRecordObject<TModel>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.Any(param => param.ParameterName == "@id"))));
            mockClient.Verify(m => m.GetRecordObject<TModel>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@language"))));
            mockClient.Verify(m => m.GetRecordObject<TModel>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@groupkey"))));
            mockClient.Verify(m => m.GetRecordObject<TModel>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@key"))));
            mockClient.Verify(m => m.GetRecordObject<TModel>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@value"))));
            mockClient.Verify(m => m.GetRecordObject<TModel>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@order"))));
            mockClient.Verify(m => m.GetRecordObject<TModel>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@description"))));
            mockClient.Verify(m => m.GetRecordObject<TModel>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.All(param => param.ParameterName != "@data"))));
            mockClient.Verify(m => m.GetRecordObject<TModel>(It.IsAny<object>(), It.IsAny<string>(), It.Is<IRepositoryMethodDefinition>(def => def.Parameters.Any(param => param.ParameterName == "@auditchangeby"))));

            mockClient.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void SqlParams_Should_Throw_NotImplementedException_For_UpdateRecord()
        {
            var mockClient = new Mock<IModularDbClient>();
            var connStr = new DbConnectionStringBuilder();
            var input = new TModel(new { Id = 1 });
            var output = new TModel(new { Id = 1 });
            mockClient.Setup(m => m.GetConnectionBuilder(It.IsAny<string>())).Returns(connStr);
            mockClient.Setup(m => m.GetRecordObject<TModel>(It.IsAny<object>(), It.IsAny<string>(), It.IsAny<IRepositoryMethodDefinition>())).Returns(output);

            TRepo sut = new TRepo(mockClient.Object, "");

            Assert.ThrowsException<NotImplementedException>(() => sut.UpdateRecord(input));
        }

        [TestMethod]
        public void SqlParams_Should_Throw_NotImplementedException_For_InsertRecord()
        {
            var mockClient = new Mock<IModularDbClient>();
            var connStr = new DbConnectionStringBuilder();
            var input = new TModel(new { Id = 1 });
            var output = new TModel(new { Id = 1 });
            mockClient.Setup(m => m.GetConnectionBuilder(It.IsAny<string>())).Returns(connStr);
            mockClient.Setup(m => m.GetRecordObject<TModel>(It.IsAny<object>(), It.IsAny<string>(), It.IsAny<IRepositoryMethodDefinition>())).Returns(output);

            TRepo sut = new TRepo(mockClient.Object, "");

            Assert.ThrowsException<NotImplementedException>(() => sut.InsertRecord(input));
        }


    }
}
