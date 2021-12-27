using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using POC.API.Lib.Commands;
using POC.Domain.Repository;
using Xunit;

namespace POC.API.Lib.Test.Commands
{
    public class GetPaymentsHandlerTest
    {
        private Mock<IPaymentRepository> repo = new Mock<IPaymentRepository>();

        [Fact]
        public void GivenGetPayments_WhenHandle_ThenShouldNotThrow()
        {
            // Given
            var getPayments = new GetPayments();
            repo.Setup(x => x.GetPayments());
            var getPaymentsHandler = new GetPaymentsHandler(repo.Object);

            // When
            Func<Task> action = async () => await getPaymentsHandler.Handle(getPayments);

            // Then
            action.Should().NotThrowAsync();
        }

        [Fact]
        public void GivenNullGetPayments_WhenHandle_ThenShouldNotThrow()
        {
            // Given
            repo.Setup(x => x.GetPayments());
            var getPaymentsHandler = new GetPaymentsHandler(repo.Object);

            // When
            Func<Task> action = async () => await getPaymentsHandler.Handle(null);

            // Then
            action.Should().NotThrowAsync();
        }
    }
}