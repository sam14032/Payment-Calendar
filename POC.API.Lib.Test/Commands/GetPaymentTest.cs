using FluentAssertions;
using POC.API.Lib.Commands;
using Xunit;

namespace POC.API.Lib.Test.Commands
{
    public class GetPaymentTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("Candy")]
        public void GivenValidName_WhenCreate_ThenCreate(string name)
        {
            // Given

            // When
            var getPayment = new GetPayment(name);

            // Then
            getPayment.Name.Should().Be(name);
        }
    }
}