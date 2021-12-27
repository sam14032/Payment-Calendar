using FluentAssertions;
using POC.API.Lib.Commands;
using Xunit;

namespace POC.API.Lib.Test.Commands
{
    public class DeletePaymentTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("Candy")]
        public void GivenValidName_WhenCreate_ThenCreate(string name)
        {
            // Given

            // When
            var deletePayment = new DeletePayment(name);

            // Then
            deletePayment.Name.Should().Be(name);
        }
    }
}