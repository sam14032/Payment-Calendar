using System;
using FluentAssertions;
using POC.API.Lib.Model;
using POC.Domain.Enums;
using Xunit;

namespace POC.API.Lib.Test.Model
{
    public class PaymentTest
    {
        [Fact]
        public void GivenPayment_WhenToDomain_ThenReturnEquivalent()
        {
            // Given
            var payment =
                new Payment(
                    "Name",
                    new Date(DateTime.Today, Frequency.Weekly),
                    10,
                    ContactMethods.Online,
                    new Comment("Text")
                );

            // When
            var domainPayment = payment.ToDomain();

            // Then
            domainPayment.Name.Should().Be(payment.Name);
            domainPayment.Date.NextPaymentDate.Should().Be(payment.Date.NextPaymentDate);
            domainPayment.Date.PaymentFrequency.Should().Be(payment.Date.PaymentFrequency);
            domainPayment.Amount.Should().Be(payment.Amount);
            domainPayment.ContactMethod.Should().Be(payment.ContactMethod);
            domainPayment.Comment.Text.Should().Be(payment.Comment.Text);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("name")]
        public void GivenValidName_WhenCreate_ThenShouldCreate(string name)
        {
            // Given

            // When
            var payment =
                new Payment(
                    name,
                    new Date(DateTime.Today, Frequency.Weekly),
                    10,
                    ContactMethods.Online,
                    new Comment("Text")
                );

            // Then
            payment.Name.Should().Be(name);
        }

        [Fact]
        public void GivenNullDate_WhenCreate_ThenShouldCreateWithNullDate()
        {
            // Given
        
            // When
            var payment =
                new Payment(
                    "name",
                    null,
                    10,
                    ContactMethods.Online,
                    new Comment("Text")
                );

            // Then
            payment.Date.Should().BeNull();
        }

        [Theory]
        [InlineData(double.MaxValue)]
        [InlineData(double.MinValue)]
        [InlineData(0)]
        public void GivenValidAmount_WhenCreate_ThenReturnAmount(double amount)
        {
            // Given
        
            // When
            var payment =
                new Payment(
                    "name",
                    new Date(DateTime.Today, Frequency.Weekly),
                    amount,
                    ContactMethods.Online,
                    new Comment("Text")
                );

            // Then
            payment.Amount.Should().Be(amount);
        }

        [Fact]
        public void GivenNullComment_WhenCreate_ThenReturnNullComment()
        {
            // Given
            var payment =
                new Payment(
                    "name",
                    new Date(DateTime.Today, Frequency.Weekly),
                    10,
                    ContactMethods.Online,
                    null
                );

            // Then
            payment.Comment.Should().BeNull();
        }
    }
}