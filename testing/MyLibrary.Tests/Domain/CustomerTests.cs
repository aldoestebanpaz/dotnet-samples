using MyLibrary.Domain;
using System.Globalization;

namespace MyLibrary.Tests.Domain
{
    public class CustomerTests
    {
        [Fact]
        public void Customer_WhenCreated_ShouldHaveExpectedValues()
        {
            // arrange
            var name = "aldo";
            var email = "apaz@noemail.com";

            // act
            Customer customer = new(name, email);

            // assert
            Assert.Equal(name, customer.Name);
            Assert.Equal(email, customer.Email);
            Assert.Equal(0, customer.Id);
        }

        [Theory]
        [InlineData("aldo", "apaz@noemail.com")]
        [InlineData("ella", "ella@ella.com")]
        [InlineData("foo", "foo@bar.com")]
        public void Customers_WhenCreated_ShouldHaveExpectedValues(string name, string email)
        {
            // act
            Customer customer = new(name, email);

            // assert
            Assert.Equal(name, customer.Name);
            Assert.Equal(email, customer.Email);
            Assert.Equal(0, customer.Id);
        }

        [Fact]
        public void Customer_WhenCreatedWithCustomDate_ShouldHaveExpectedValues()
        {
            // arrange
            var name = "aldo";
            var email = "apaz@noemail.com";
            DateTime currDateTime = DateTime.ParseExact("2022/08/30 13:26:15", "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture);

            // act
            Customer customer = new(name, email) { CreatedDate = currDateTime };

            // assert
            Assert.Equal(name, customer.Name);
            Assert.Equal(email, customer.Email);
            Assert.Equal(currDateTime, customer.CreatedDate);
        }
    }
}
