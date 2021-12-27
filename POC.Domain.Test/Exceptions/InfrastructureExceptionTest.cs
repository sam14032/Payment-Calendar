using System;
using FluentAssertions;
using POC.Domain.Exceptions;
using Xunit;

namespace POC.Domain.Test.Exceptions
{
    public class InfrastructureExceptionTest
    {
        [Fact]
        public void Given_WhenThrowInfrastructureException_ThenShouldHaveInfrastuctureExceptionMessage()
        {
            // Given
        
            // When
            var ex = new InfrastructureException();
        
            // Then
            ex.Message.Should().Be(nameof(InfrastructureException));
        }

        [Fact]
        public void GivenExceptionMessage_WhenThrowInfrastructureException_ThenShouldHaveExceptionMessage()
        {
            // Given
            var msg = "Exception Message";
        
            // When
            var ex = new InfrastructureException(msg);
        
            // Then
            ex.Message.Should().Be(msg);
        }
    }
}