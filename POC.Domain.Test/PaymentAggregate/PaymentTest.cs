using Xunit;
using POC.Domain.PaymentAggregate;
using FluentAssertions;
using System;
using POC.Domain.Enums;

namespace POC.Domain.Test.PaymentAggregate
{
    public class PaymentTest
    {
        Comment comment;
        Date date;

        public PaymentTest()
        {
            comment = new Comment("comment");
            date = new Date(DateTime.Now, Frequency.Daily);
        }

        [Fact]
        public void GivenValidParams_WhenCreate_ThenShouldCreate()
        {
            // Given
            var name = "name";
            var amount = 10.5;
            var contactMethod = ContactMethods.Online;

            // When
            var payment = new Payment(name, date, amount, contactMethod, comment);

            // Then
            payment.Name.Should().Be(name);
            payment.Date.Should().BeEquivalentTo(date);
            payment.Amount.Should().Be(amount);
            payment.ContactMethod.Should().Be(contactMethod);
            payment.Comment.Should().BeEquivalentTo(comment);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void GivenInvalidName_WhenCreate_ThenShouldThrowException(string name)
        {
            // Given
            var amount = 10.5;
            var contactMethod = ContactMethods.Phone;

            // When
            Action action = () => new Payment(name, date, amount, contactMethod, comment);

            // Then
            action.Should().Throw<Exception>();
        }

        [Fact]
        public void GivenAmountBelowZero_WhenCreate_ThenShouldThrowException()
        {
            // Given
            var name = "name";
            var amount = -1;
            var contactMethod = ContactMethods.Phone;

            // When
            Action action = () => new Payment(name, date, amount, contactMethod, comment);

            // Then
            action.Should().Throw<Exception>();
        }
    }
}