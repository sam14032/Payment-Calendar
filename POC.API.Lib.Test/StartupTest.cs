
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using System;
using FluentAssertions;

namespace POC.API.Lib.Test
{
    public class StartupTest
    {
        private IServiceCollection _serviceCollection;
        private Startup _startup;
        private string connectionString = "DataSource=migration.db;mode=ReadWriteCreate;cache=shared";

        public StartupTest()
        {
            _serviceCollection = new ServiceCollection();
            _startup = new Startup(_serviceCollection, connectionString);
            _startup.ConfigureServices();
        }

        [Fact]
        public void GivenServiceCollectionAndConnectionString_WhenCreate_ThenShouldCreate()
        {
            // Given
            var serviceCollection = new ServiceCollection();

            // When
            Action action = () => new Startup(serviceCollection, connectionString);

            // Then
            action.Should().NotThrow();
        }

        [Fact]
        public void GivenServiceCollectionAndConnectionString_WhenConfigureServices_ThenShouldNotThrow()
        {
            // Given
            var serviceCollection = new ServiceCollection();
            var startup = new Startup(_serviceCollection, connectionString);

            // When
            Action action = () => startup.ConfigureServices();

            // Then
            action.Should().NotThrow();
        }
    }
}