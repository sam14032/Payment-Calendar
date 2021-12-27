using FluentAssertions;
using POC.API.Lib.Model;
using Xunit;

namespace POC.API.Lib.Test.Model
{
    public class CommentTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("Text")]
        public void GivenValidText_WhenCreate_ThenShouldCreate(string text)
        {
            // Given
        
            // When
            var comment = new Comment(text);
        
            // Then
            comment.Text.Should().Be(text);
        }
    }
}