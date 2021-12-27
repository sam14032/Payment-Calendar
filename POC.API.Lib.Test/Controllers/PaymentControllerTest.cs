using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using POC.API.Lib.Controllers;
using POC.API.Lib.Model;
using POC.Domain.Enums;
using Xunit;

namespace POC.API.Lib.Test.Controllers
{
    public class PaymentControllerTest
    {
        private Mock<IMediator> _mediator = new Mock<IMediator>();

        [Fact]
        public async Task GivenPayment_WhenPost_ThenShouldCallSendOnce()
        {
            // Given
            PaymentController controller = new PaymentController(_mediator.Object);
            var payment = new Payment("Name", new Date(DateTime.Today, Frequency.Daily), 10, ContactMethods.Online, new Comment("Text"));
        
            // When
            _mediator.Setup(x => x.Send<Unit>(It.IsAny<IRequest<Unit>>(),It.IsAny<CancellationToken>()));

            await controller.Post(payment);
        
            // Then
            _mediator.Verify(x => x.Send<Unit>(It.IsAny<IRequest<Unit>>(),It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GivenNamePayment_WhenGet_ThenShouldCallSendWithPaymentReturnOnce()
        {
            // Given
            PaymentController controller = new PaymentController(_mediator.Object);
            var payment = new Payment("Name", new Date(DateTime.Today, Frequency.Daily), 10, ContactMethods.Online, new Comment("Text"));

            _mediator.Setup(x => x.Send<Payment>(It.IsAny<IRequest<Payment>>(),It.IsAny<CancellationToken>()));

            // When
            var result = await controller.Get("Name");

            // Then
            _mediator.Verify(x => x.Send<Payment>(It.IsAny<IRequest<Payment>>(),It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GivenNamePayment_WhenDelete_ThenShouldCallSendOnce()
        {
            // Given
            PaymentController controller = new PaymentController(_mediator.Object);
            var payment = new Payment("Name", new Date(DateTime.Today, Frequency.Daily), 10, ContactMethods.Online, new Comment("Text"));

            _mediator.Setup(x => x.Send<Unit>(It.IsAny<IRequest<Unit>>(),It.IsAny<CancellationToken>()));

            // When
            await controller.Delete("Name");

            // Then
            _mediator.Verify(x => x.Send<Unit>(It.IsAny<IRequest<Unit>>(),It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GivenNamePayment_WhenGet_ThenShouldCallSendWithPaymentsReturnOnce()
        {
            // Given
            PaymentController controller = new PaymentController(_mediator.Object);
            var payment = new Payment("Name", new Date(DateTime.Today, Frequency.Daily), 10, ContactMethods.Online, new Comment("Text"));

            _mediator.Setup(x => x.Send<List<Payment>>(It.IsAny<IRequest<List<Payment>>>(),It.IsAny<CancellationToken>()));

            // When
            var result = await controller.Get();

            // Then
            _mediator.Verify(x => x.Send<List<Payment>>(It.IsAny<IRequest<List<Payment>>>(),It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}