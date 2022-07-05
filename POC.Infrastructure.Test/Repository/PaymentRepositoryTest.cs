using POC.Infrastructure.Context;
using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using POC.Infrastructure.Repository;
using DomainPayment = POC.Domain.PaymentAggregate.Payment;
using DomainDate = POC.Domain.PaymentAggregate.Date;
using DomainComment = POC.Domain.PaymentAggregate.Comment;
using System;
using POC.Domain.Enums;
using System.Threading.Tasks;
using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace POC.Infrastructure.Test.Repository
{
    public class PaymentRepositoryTest : IDisposable
    {
        private readonly DbConnection _connection;
        private PaymentContext paymentContext;
        private PaymentRepository paymentRepository;

        public PaymentRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<PaymentContext>().UseSqlite(CreateInMemoryDatabase()).Options;

            _connection = RelationalOptionsExtension.Extract(options).Connection;

            paymentContext = new PaymentContext(options);
            paymentContext.Database.EnsureDeleted();
            paymentContext.Database.EnsureCreated();

            paymentRepository = new PaymentRepository(paymentContext);
        }

        [Fact]
        public async Task GivenDomainPayment_WhenAddPayment_ThenShouldAddPayment()
        {
            // Given
            var domainPayment =
                new DomainPayment(
                    "name",
                    new DomainDate(
                        DateTime.Today,
                        Frequency.Daily
                    ),
                    10,
                    ContactMethods.Online,
                    new DomainComment("comment")
                );

            // When
            Func<Task> action = async () => await paymentRepository.AddPayment(domainPayment);

            // Then
            await action.Should().NotThrowAsync();
        }

        [Fact]
        public async Task GivenExistingPaymentWithNewDate_WhenAddPayment_ThenShouldUpdateDate()
        {
            // Given
            var date = new DomainDate(DateTime.Today, Frequency.Daily);
            var domainPayment =
                new DomainPayment(
                    "name",
                    date,
                    10,
                    ContactMethods.Online,
                    new DomainComment("comment")
                );

            await paymentRepository.AddPayment(domainPayment);
            var payments = await paymentRepository.GetPayments();
            payments[0].Date.Should().BeEquivalentTo(date);

            var newDate = new DomainDate(DateTime.MaxValue, Frequency.Weekly);
            domainPayment.Date = newDate;

            // When
            await paymentRepository.AddPayment(domainPayment);

            // Then
            payments = await paymentRepository.GetPayments();
            payments.Count.Should().Be(1);
            payments[0].Date.Should().BeEquivalentTo(newDate);
        }

        [Fact]
        public async Task GivenExistingPaymentWithNewComment_WhenAddPayment_ThenShouldUpdateComment()
        {
            // Given
            var comment = new DomainComment("comment");
            var domainPayment =
                new DomainPayment(
                    "name",
                    new DomainDate(DateTime.Today, Frequency.Daily),
                    10,
                    ContactMethods.Online,
                    comment
                );

            await paymentRepository.AddPayment(domainPayment);
            var payments = await paymentRepository.GetPayments();
            payments[0].Comment.Should().BeEquivalentTo(comment);

            var newComment = new DomainComment("new comment");
            domainPayment.Comment = newComment;

            // When
            await paymentRepository.AddPayment(domainPayment);

            // Then
            payments = await paymentRepository.GetPayments();
            payments.Count.Should().Be(1);
            payments[0].Comment.Should().BeEquivalentTo(newComment);
        }

        [Fact]
        public async Task GivenExistingPaymentWithNewAmountAndNewContactMethod_WhenAddPayment_ThenShouldUpdateAmountAndContactMethod()
        {
            // Given
            var domainPayment =
                new DomainPayment(
                    "name",
                    new DomainDate(DateTime.Today, Frequency.Daily),
                    10,
                    ContactMethods.Online,
                    new DomainComment("comment")
                );

            await paymentRepository.AddPayment(domainPayment);

            var newAmount = 50.56;
            var newContactMethod = ContactMethods.Phone;

            domainPayment.Amount = newAmount;
            domainPayment.ContactMethod = newContactMethod;

            // When
            await paymentRepository.AddPayment(domainPayment);

            // Then
            var payments = await paymentRepository.GetPayments();
            payments.Count.Should().Be(1);
            payments[0].Amount.Should().Be(newAmount);
            payments[0].ContactMethod.Should().Be(newContactMethod);
        }

        [Fact]
        public async Task GivenOneExistingPayment_WhenGetPayments_ThenShouldReturnOnePayment()
        {
            // Given
            var domainPayment =
                new DomainPayment(
                    "name",
                    new DomainDate(
                        DateTime.Today,
                        Frequency.Daily
                    ),
                    10,
                    ContactMethods.Online,
                    new DomainComment("comment")
                );
            await paymentRepository.AddPayment(domainPayment);

            // When
            var payments = await paymentRepository.GetPayments();

            // Then
            payments.Count.Should().Be(1);
            payments[0].Should().BeEquivalentTo(domainPayment);
        }

        [Fact]
        public async Task GivenTwoExistingPayment_WhenGetPayments_ThenShouldReturnTwoPayment()
        {
            // Given
            var payment1 = CreateDomainPayment("name1");
            await paymentRepository.AddPayment(payment1);

            var payment2 = CreateDomainPayment("name2");
            await paymentRepository.AddPayment(payment2);

            // When
            var payments = await paymentRepository.GetPayments();

            // Then
            payments.Count.Should().Be(2);
            payments[0].Should().BeEquivalentTo(payment1);
            payments[1].Should().BeEquivalentTo(payment2);
        }

        [Fact]
        public async Task GivenNoPayment_WhenGetPayments_ThenShouldReturnNoPayment()
        {
            // Given

            // When
            var payments = await paymentRepository.GetPayments();

            // Then
            payments.Count.Should().Be(0);
        }
        
        [Fact]
        public async Task GivenExistingPaymentWithPoutineName_WhenGetPayment_ThenShouldReturnPaymentWithPoutineName()
        {
            // Given
            var name = "Poutine";
            var domainPayment =
                new DomainPayment(
                    name,
                    new DomainDate(
                        DateTime.Today,
                        Frequency.Daily
                    ),
                    10,
                    ContactMethods.Online,
                    new DomainComment("comment")
                );
            await paymentRepository.AddPayment(domainPayment);

            // When
            var payment = await paymentRepository.GetPayment(name);

            // Then
            payment.Should().BeEquivalentTo(domainPayment);
        }
        
        [Fact]
        public async Task GivenExistingPaymentWithEmptyComment_WhenGetPayment_ThenShouldReturnPaymentWithEmptyComment()
        {
            // Given
            var name = "Poutine";
            var domainPayment =
                new DomainPayment(
                    name,
                    new DomainDate(
                        DateTime.Today,
                        Frequency.Daily
                    ),
                    10,
                    ContactMethods.Online,
                    new DomainComment(string.Empty)
                );
            await paymentRepository.AddPayment(domainPayment);

            // When
            var payment = await paymentRepository.GetPayment(name);

            // Then
            payment.Should().BeEquivalentTo(domainPayment);
        }
        
        [Fact]
        public async Task GivenNoPayment_WhenGetPayment_ThenShouldReturnNoPayment()
        {
            // Given
            var paymentName = "name";

            // When
            var payment = await paymentRepository.GetPayment(paymentName);

            // Then
            payment.Should().BeNull();
        }

        [Fact]
        public async Task GivenExistingPayment_WhenDeletePayment_ThenShouldReturnZero()
        {
            // Given
            var name = "Poutine";
            var domainPayment =
                new DomainPayment(
                    name,
                    new DomainDate(
                        DateTime.Today,
                        Frequency.Daily
                    ),
                    10,
                    ContactMethods.Online,
                    new DomainComment("comment")
                );
            await paymentRepository.AddPayment(domainPayment);

            var payment = await paymentRepository.GetPayment(name);
            payment.Should().BeEquivalentTo(domainPayment);

            // When
            var result = await paymentRepository.DeletePayment(name);
        
            // Then
            payment = await paymentRepository.GetPayment(name);
            payment.Should().BeNull();
            result.Should().Be(0);
        }
        
        [Fact]
        public async Task GivenInexistingPayment_WhenDeletePayment_ThenShouldReturn404()
        {
            // Given

            // When
            var result = await paymentRepository.DeletePayment("name");
        
            // Then
            result.Should().Be(404);
        }

        private DomainPayment CreateDomainPayment(string name)
        {
            return new DomainPayment(
                name,
                new DomainDate(
                    DateTime.Today,
                    Frequency.Daily
                ),
                10,
                ContactMethods.Online,
                new DomainComment("comment")
            );
        }
        
        private static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");

            connection.Open();

            return connection;
        }

        public void Dispose() => _connection.Dispose();
    }
}