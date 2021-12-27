using Moq;
using POC.API.Lib.Commands;
using POC.API.Lib.Model;
using POC.Domain.Repository;
using DomainPayment = POC.Domain.PaymentAggregate.Payment;
using Xunit;
using System;
using System.Threading.Tasks;
using FluentAssertions;
using POC.Domain.Exceptions;
using POC.Domain.Enums;

namespace POC.API.Lib.Test.Commands
{
    public class PostPaymentHandlerTest
    {
        private Mock<IPaymentRepository> repo = new Mock<IPaymentRepository>();

        [Fact]
        public void GivenPostPaymentWithPayment_WhenHandle_ThenShouldNotThrow()
        {
            // Given
            var postPayment = 
                new PostPayment(
                    new Payment (
                        "Name",
                        new Date(DateTime.Today, Frequency.Weekly),
                        10,
                        ContactMethods.Online,
                        new Comment("Text")
                    )
                );
            repo.Setup(x => x.AddPayment(It.IsAny<DomainPayment>()));
            var postPaymentHandler = new PostPaymentHandler(repo.Object);

            // When
            Func<Task> action = async () => await postPaymentHandler.Handle(postPayment);

            // Then
            action.Should().NotThrowAsync();
        }

        [Fact]
        public void GivenPostPaymentWithNullPayment_WhenHandle_ThenShouldThrowLogicalExceptionWithMessage()
        {
            // Given
            var postPayment = new PostPayment(null);
            repo.Setup(x => x.AddPayment(It.IsAny<DomainPayment>()));
            var postPaymentHandler = new PostPaymentHandler(repo.Object);

            // When
            Func<Task> action = async () => await postPaymentHandler.Handle(postPayment);

            // Then
            action.Should().ThrowAsync<LogicalException>().WithMessage("There is no payment to process");
        }
    }
}