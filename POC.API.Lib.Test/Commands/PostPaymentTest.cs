using System;
using FluentAssertions;
using POC.API.Lib.Commands;
using POC.API.Lib.Model;
using POC.Domain.Enums;
using Xunit;

namespace POC.API.Lib.Test.Commands
{
    public class PostPaymentTest
    {
        [Fact]
        public void GivenPayment_WhenCreate_ThenShouldCreate()
        {
            // Given
            var payment = 
                new Payment (
                        "Name",
                        new Date(DateTime.Today, Frequency.Weekly),
                        10,
                        ContactMethods.Online,
                        new Comment("Text")
                    );

            // When
            var postPayment = new PostPayment(payment);

            // Then
            postPayment.Payment.Should().BeEquivalentTo(payment);
        }

        [Fact]
        public void GivenNullPayment_WhenCreate_ThenShouldNotThrow()
        {
            // Given

            // When
            Action action = () => new PostPayment(null);

            // Then
            action.Should().NotThrow();
        }
    }
}