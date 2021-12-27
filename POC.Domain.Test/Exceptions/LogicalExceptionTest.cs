using System;
using FluentAssertions;
using POC.Domain.Exceptions;
using Xunit;

namespace POC.Domain.Test.Exceptions
{
    public class LogicalExceptionTest
    {
        [Fact]
        public void Given_WhenThrowLogicalException_ThenShouldHaveLogicalExceptionMessage()
        {
            // Given
        
            // When
            var ex = new LogicalException();
        
            // Then
            ex.Message.Should().Be(nameof(LogicalException));
        }

        [Fact]
        public void GivenExceptionMessage_WhenThrowLogicalException_ThenShouldHaveExceptionMessage()
        {
            // Given
            var msg = "Exception Message";
        
            // When
            var ex = new LogicalException(msg);
        
            // Then
            ex.Message.Should().Be(msg);
        }
    }
}