using System;
using FluentAssertions;
using POC.API.Lib.Model;
using POC.Domain.Enums;
using Xunit;

namespace POC.API.Lib.Test.Model
{
    public class DateTest
    {
        [Fact]
        public void GivenValidParams_WhenCreate_ThenShouldCreate()
        {
            // Given
            var date = DateTime.Now;
            var frequency = Frequency.Weekly;
        
            // When
            var result = new Date(date, frequency);
        
            // Then
            result.NextPaymentDate.Should().Be(date);
            result.PaymentFrequency.Should().Be(frequency);
        }
    }
}