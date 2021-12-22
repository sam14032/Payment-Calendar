using System;
using FluentAssertions;
using POC.Domain.PaymentAggregate;
using Xunit;

namespace POC.Domain.Test.PaymentAggregate
{
    public class CommentTest
    {
        [Fact]
        public void GivenValidText_WhenCreate_ThenShouldCreate()
        {
            // Given
            var text = "text";
        
            // When
            var comment = new Comment(text);
        
            // Then
            comment.Text.Should().Be(text);
            comment.Id.Should().Be(0);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void GivenNullOrEmptyText_WhenCreate_ThenShouldThrowException(string text)
        {
            // Given
        
            // When
            Action action = () => new Comment(text);
        
            // Then
            action.Should().Throw<Exception>();
        }
    }
}