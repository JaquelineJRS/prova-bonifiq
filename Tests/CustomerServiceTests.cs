using Moq;
using ProvaPub.Entities;
using ProvaPub.Interfaces.Repositories;
using ProvaPub.Interfaces.Rules;
using ProvaPub.Results;
using ProvaPub.Services;
using Xunit;

namespace ProvaPub.Tests
{
    public class CustomerServiceTests
    {
        private readonly Mock<ICustomerRepository> _customerRepoMock;
        private readonly List<Mock<IPurchaseRule>> _rulesMocks;
        private readonly CustomerService _service;

        public CustomerServiceTests()
        {
            _customerRepoMock = new Mock<ICustomerRepository>();
            _rulesMocks = new List<Mock<IPurchaseRule>>();

            var defaultRuleTrue = new Mock<IPurchaseRule>();
            defaultRuleTrue.Setup(r => r.IsSatisfiedAsync(It.IsAny<Customer>(), It.IsAny<decimal>()))
                       .ReturnsAsync(true);

            _rulesMocks.Add(defaultRuleTrue);

            _service = new CustomerService(
                _customerRepoMock.Object,
                new List<IPurchaseRule> { defaultRuleTrue.Object }
            );
        }

        [Fact]
        public async Task Should_Throw_When_CustomerId_IsInvalid()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
                _service.CanPurchase(0, 50));
        }

        [Fact]
        public async Task Should_Throw_When_PurchaseValue_IsInvalid()
        {
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
                _service.CanPurchase(1, 0));
        }

        [Fact]
        public async Task Should_Throw_When_Customer_Not_Found()
        {
            _customerRepoMock.Setup(r => r.GetByIdWithOrdersAsync(It.IsAny<int>()))
                             .ReturnsAsync((Customer?)null);

            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _service.CanPurchase(1, 50));
        }

        [Fact]
        public async Task Should_Return_False_When_Any_Rule_Fails()
        {
            var customer = new Customer { Id = 1 };
            _customerRepoMock.Setup(r => r.GetByIdWithOrdersAsync(1))
                             .ReturnsAsync(customer);

            var failingRule = new Mock<IPurchaseRule>();
            failingRule.Setup(r => r.IsSatisfiedAsync(customer, 50))
                       .ReturnsAsync(false);

            var serviceWithFailingRule = new CustomerService(
                _customerRepoMock.Object,
                new List<IPurchaseRule> { failingRule.Object }
            );

            var result = await serviceWithFailingRule.CanPurchase(1, 50);

            Assert.False(result);
        }

        [Fact]
        public async Task Should_Return_True_When_All_Rules_Pass()
        {
            var customer = new Customer { Id = 1 };
            _customerRepoMock.Setup(r => r.GetByIdWithOrdersAsync(1))
                             .ReturnsAsync(customer);

            var rule1 = new Mock<IPurchaseRule>();
            rule1.Setup(r => r.IsSatisfiedAsync(customer, 50))
                 .ReturnsAsync(true);

            var rule2 = new Mock<IPurchaseRule>();
            rule2.Setup(r => r.IsSatisfiedAsync(customer, 50))
                 .ReturnsAsync(true);

            var serviceWithMultipleRules = new CustomerService(
                _customerRepoMock.Object,
                new List<IPurchaseRule> { rule1.Object, rule2.Object }
            );

            var result = await serviceWithMultipleRules.CanPurchase(1, 50);

            Assert.True(result);
        }

        [Fact]
        public void Should_Return_Paged_Customers()
        {
            var pagedResult = new PagedResult<Customer>
            {
                HasNext = true,
                TotalCount = 1,
                Items = new List<Customer> { new Customer { Id = 1, Name = "Test" } }
            };

            _customerRepoMock.Setup(r => r.GetPaged(1))
                             .Returns(pagedResult);

            var result = _service.ListCustomers(1);

            Assert.Equal(1, result.TotalCount);
            Assert.Equal("Test", result.Items[0].Name);
        }
    }
}
