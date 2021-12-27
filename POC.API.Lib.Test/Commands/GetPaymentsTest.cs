using System;
using FluentAssertions;
using POC.API.Lib.Commands;
using Xunit;

namespace POC.API.Lib.Test.Commands
{
    public class GetPaymentsTest
    {
        [Fact]
        public void Given_WhenCreate_ThenCreate()
        {
            // Given

            // When
            Action action = () => new GetPayments();

            // Then
            action.Should().NotThrow();
        }
    }
}