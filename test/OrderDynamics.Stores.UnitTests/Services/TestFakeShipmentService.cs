using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Moq;
using Xunit;
using OrderDynamics.Stores.Web.Infrastructure.ApiClient;
using OrderDynamics.Stores.Web.Models;
using OrderDynamics.Stores.Web.Services;

namespace OrderDynamics.Stores.UnitTests.Services
{

    public class TestFakeShipmentService
    {
        [Fact]
        public void Ctor_NullApiClientFactory_ThrowsArgumentNullException() {
            //Arrange,Act,Assert
            Assert.Throws<ArgumentNullException>(() => new FakeShipmentService(null));
        }

        [Fact]
        public async void GetShipmentAsync_ShipmentDoesNotExist_ReturnsNull() {
            //Arrange
            var shipmentId = 1;
            var apiFactoryMock = new Mock<IApiClientFactory>();
            var apiClientMock = new Mock<IApiClient>();
            apiFactoryMock.Setup(f => f.GetApiClient()).Returns(apiClientMock.Object).Verifiable();

            var service = new FakeShipmentService(apiFactoryMock.Object);

            //Act
            var shipment = await service.GetShipmentAsync(shipmentId);

            //Assert
            Assert.Null(shipment);
            apiFactoryMock.Verify();
        }

        [Fact]
        public async void GetShipmentAsync_ShipmentExists_ReturnsShipment()
        {
            //Arrange
            var expectedShipment = new ShipmentModel() {Id = 1, Name = "Fake Shipment"};
            var apiFactoryMock = new Mock<IApiClientFactory>();
            var apiClientMock = new Mock<IApiClient>();
            apiFactoryMock.Setup(f => f.GetApiClient()).Returns(apiClientMock.Object).Verifiable();
            apiClientMock.Setup(c => c.GetAsync<ShipmentModel>(string.Format("FakeShipments/{0}", expectedShipment.Id))).Returns(Task.FromResult(expectedShipment)).Verifiable();

            var service = new FakeShipmentService(apiFactoryMock.Object);

            //Act
            var shipment = await service.GetShipmentAsync(expectedShipment.Id);

            //Assert
            Assert.StrictEqual(expectedShipment, shipment);
            apiFactoryMock.Verify();
            apiClientMock.Verify();
        }
    }
}
