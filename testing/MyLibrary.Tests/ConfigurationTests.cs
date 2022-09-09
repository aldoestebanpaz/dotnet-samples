using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace MyLibrary.Tests
{
    public class ConfigurationTests
    {
        [Fact]
        public void GetConnectionString_WhenInvoked_ShouldReturnTheExpectedValue()
        {
            // arrange
            Mock<IEnvironmentWrapper> mockEnvironmentWrapper = new ();

            mockEnvironmentWrapper.Setup(x => x.GetVariable(It.Is<string>(x => x == "DB_DIALECT"))).Returns("").Verifiable();
            mockEnvironmentWrapper.Setup(x => x.GetVariable(It.Is<string>(x => x == "DB_DRIVER"))).Returns("").Verifiable();
            mockEnvironmentWrapper.Setup(x => x.GetVariable(It.Is<string>(x => x == "DB_USERNAME"))).Returns("foo").Verifiable();
            mockEnvironmentWrapper.Setup(x => x.GetVariable(It.Is<string>(x => x == "DB_PASSWORD"))).Returns("bar").Verifiable();
            mockEnvironmentWrapper.Setup(x => x.GetVariable(It.Is<string>(x => x == "DB_HOST"))).Returns("").Verifiable();
            mockEnvironmentWrapper.Setup(x => x.GetVariable(It.Is<string>(x => x == "DB_PORT"))).Returns("").Verifiable();
            mockEnvironmentWrapper.Setup(x => x.GetVariable(It.Is<string>(x => x == "DB_NAME"))).Returns("mydb").Verifiable();

            // act
            Configuration configuration = new (mockEnvironmentWrapper.Object);

            // assert
            mockEnvironmentWrapper.Verify(x => x.GetVariable(It.IsAny<string>()), Times.Exactly(7));
            Assert.Equal("mysql://foo:bar@localhost:3306/mydb", configuration.GetConnectionString());
        }

        [Fact]
        public void GetConnectionString_WhenInvokedWithFullConfig_ShouldReturnTheExpectedValue()
        {
            // arrange
            Mock<IEnvironmentWrapper> mockEnvironmentWrapper = new();

            mockEnvironmentWrapper.Setup(x => x.GetVariable(It.Is<string>(x => x == "DB_DIALECT"))).Returns("mysql").Verifiable();
            mockEnvironmentWrapper.Setup(x => x.GetVariable(It.Is<string>(x => x == "DB_DRIVER"))).Returns("").Verifiable();
            mockEnvironmentWrapper.Setup(x => x.GetVariable(It.Is<string>(x => x == "DB_USERNAME"))).Returns("foo").Verifiable();
            mockEnvironmentWrapper.Setup(x => x.GetVariable(It.Is<string>(x => x == "DB_PASSWORD"))).Returns("bar").Verifiable();
            mockEnvironmentWrapper.Setup(x => x.GetVariable(It.Is<string>(x => x == "DB_HOST"))).Returns("myhost").Verifiable();
            mockEnvironmentWrapper.Setup(x => x.GetVariable(It.Is<string>(x => x == "DB_PORT"))).Returns("3306").Verifiable();
            mockEnvironmentWrapper.Setup(x => x.GetVariable(It.Is<string>(x => x == "DB_NAME"))).Returns("mydb").Verifiable();

            // act
            Configuration configuration = new(mockEnvironmentWrapper.Object);

            // assert
            mockEnvironmentWrapper.Verify(x => x.GetVariable(It.IsAny<string>()), Times.Exactly(7));
            Assert.Equal("mysql://foo:bar@myhost:3306/mydb", configuration.GetConnectionString());
        }
    }
}
