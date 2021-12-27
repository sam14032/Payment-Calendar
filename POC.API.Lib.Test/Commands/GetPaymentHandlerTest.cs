using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using POC.API.Lib.Commands;
using POC.Domain.Exceptions;
using POC.Domain.Repository;
using Xunit;

namespace POC.API.Lib.Test.Commands
{
    public class GetPaymentHandlerTest
    {
        private Mock<IPaymentRepository> repo = new Mock<IPaymentRepository>();

        [Fact]
        public void GivenGetPaymentWithName_WhenHandle_ThenShouldNotThrow()
        {
            // Given
            var getPayment = new GetPayment("name");
            repo.Setup(x => x.GetPayment(It.IsAny<string>()));
            var getPaymentHandler = new GetPaymentHandler(repo.Object);

            // When
            Func<Task> action = async () => await getPaymentHandler.Handle(getPayment);

            // Then
            action.Should().NotThrowAsync();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void GivenInvalidGetPayment_WhenHandle_ThenShouldThrowLogicalExceptionWithMessage(string name)
        {
            // Given
            var getPayment = new GetPayment(name);
            var getPaymentHandler = new GetPaymentHandler(repo.Object);

            // When
            Func<Task> action = async () => await getPaymentHandler.Handle(getPayment);

            // Then
            action.Should().ThrowAsync<LogicalException>().WithMessage("Payment name is missing");
        }
    }
}