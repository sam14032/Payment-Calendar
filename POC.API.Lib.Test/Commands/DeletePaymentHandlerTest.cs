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
    public class DeletePaymentHandlerTest
    {
        private Mock<IPaymentRepository> repo = new Mock<IPaymentRepository>();

        [Fact]
        public void GivenDeletePaymentWithName_WhenHandle_ThenShouldNotThrow()
        {
            // Given
            var deletePayment = new DeletePayment("name");
            repo.Setup(x => x.DeletePayment(It.IsAny<string>()));
            var deletePaymentHandler = new DeletePaymentHandler(repo.Object);

            // When
            Func<Task> action = async () => await deletePaymentHandler.Handle(deletePayment);

            // Then
            action.Should().NotThrowAsync();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void GivenInvalidDeletePayment_WhenHandle_ThenShouldThrowLogicalExceptionWithMessage(string name)
        {
            // Given
            var deletePayment = new DeletePayment(name);
            var deletePaymentHandler = new DeletePaymentHandler(repo.Object);

            // When
            Func<Task> action = async () => await deletePaymentHandler.Handle(deletePayment);

            // Then
            action.Should().ThrowAsync<LogicalException>().WithMessage("Payment name is missing");
        }
    }
}