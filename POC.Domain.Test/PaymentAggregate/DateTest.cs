using System;
using FluentAssertions;
using POC.Domain.Enums;
using POC.Domain.PaymentAggregate;
using Xunit;

namespace POC.Domain.Test.PaymentAggregate
{
    public class DateTest
    {
        [Fact]
        public void GivenValidParams_WhenCreate_ThenShouldCreate()
        {
            // Given
            DateTime nextPaymentDate = DateTime.Now;
            Frequency frequency = Frequency.Weekly;
        
            // When
            var date = new Date(nextPaymentDate, frequency);
        
            // Then
            date.NextPaymentDate.Should().Be(nextPaymentDate);
            date.PaymentFrequency.Should().Be(frequency);
        }
    }
}