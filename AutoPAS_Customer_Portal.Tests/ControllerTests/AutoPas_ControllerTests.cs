using AutoFixture;
using AutoPAS_Customer_portal.Controllers;
using AutoPAS_Customer_portal.Models.Domain;
using AutoPAS_Customer_portal.Models.DTO;
using AutoPAS_Customer_portal.Service;
using Microsoft.AspNetCore.Mvc;

namespace AutoPAS_Customer_Portal.Tests.ControllerTests
{
    public class AutoPas_ControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<CustomerPortalInterface> _mockCustomerPortalInterface;
        private readonly CustomerPortalController _sut;

        public AutoPas_ControllerTests()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _mockCustomerPortalInterface = _fixture.Freeze<Mock<CustomerPortalInterface>>();
            _sut = new CustomerPortalController(_mockCustomerPortalInterface.Object);
        }

        //Tests For Login
        #region 
        //1 : Valid Login Credentials
        [Fact]
        public async Task Login_ReturnsOkResult_WithValidCredentials()
        {
            // Arrange
           
            var userName = _fixture.Create<string>();
            var password = _fixture.Create<string>();
            var expectedUser = _fixture.Create<PortalUser>();

            _mockCustomerPortalInterface
                .Setup(m => m.validateLogin(userName, password))
                .ReturnsAsync(expectedUser);

            // Act
            var result = await _sut.Login(userName, password);

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                  .Which.Value.Should().BeEquivalentTo(expectedUser);

            // Verify that validateLogin was called with the provided arguments
            _mockCustomerPortalInterface.Verify(m => m.validateLogin(userName, password), Times.Once);
        }

        //2 : Invalid Login Credentials
        [Fact]
        public async Task Login_ReturnsNotFound_WithInvalidCredentials()
        {
            // Arrange
            var userName = _fixture.Create<string>();
            var password = _fixture.Create<string>();

            _mockCustomerPortalInterface
                .Setup(m => m.validateLogin(userName, password))
                .ReturnsAsync((PortalUser)null);

            // Act
            var result = await _sut.Login(userName, password);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            // Verify that validateLogin was called with the provided arguments
            _mockCustomerPortalInterface.Verify(m => m.validateLogin(userName, password), Times.Once);
        }

        // 3 : Exception Handling
        [Fact]
        public async Task Login_ReturnsBadRequest_WhenExceptionOccurs()
        {
            // Arrange
            var userName = _fixture.Create<string>();
            var password = _fixture.Create<string>();
            var expectedExceptionMessage = "An error occurred during login.";

            _mockCustomerPortalInterface
                .Setup(m => m.validateLogin(userName, password))
                .ThrowsAsync(new Exception(expectedExceptionMessage));

            // Act
            var result = await _sut.Login(userName, password);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>()
                  .Which.Value.Should().Be(expectedExceptionMessage);

            // Verify that validateLogin was called with the provided arguments
            _mockCustomerPortalInterface.Verify(m => m.validateLogin(userName, password), Times.Once);
        }
        #endregion

        //Tests For AddPolicynumber
        #region
        //1: With Valid Data
        [Fact]
        public async Task AddPolicyNumber_ReturnsOkResult_WithValidData()
        {
            // Arrange
            var userId = _fixture.Create<Guid>();
            var policyNumber = _fixture.Create<int>();
            var chasisNumber = _fixture.Create<string>();
            var expectedUserPolicy = _fixture.Create<UserPloicyList>();

            _mockCustomerPortalInterface
                .Setup(m => m.validatePolicyNumber(policyNumber))
                .ReturnsAsync(_fixture.Create<Policy>());

            _mockCustomerPortalInterface
                .Setup(m => m.GetPolicyVehicleRecord(policyNumber))
                .ReturnsAsync(_fixture.Create<Policyvehicle>());

            _mockCustomerPortalInterface
                .Setup(m => m.validateChasisNumber(It.IsAny<string>(), chasisNumber))
                .ReturnsAsync(_fixture.Create<Vehicle>());

            _mockCustomerPortalInterface
                .Setup(m => m.AddPolicyNumberToPolicyList(userId, policyNumber))
                .ReturnsAsync(expectedUserPolicy);

            // Act
            var result = await _sut.AddPloicyNumber(userId, policyNumber, chasisNumber);

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                  .Which.Value.Should().BeEquivalentTo(expectedUserPolicy);

            // Verify method calls
            _mockCustomerPortalInterface.Verify(m => m.validatePolicyNumber(policyNumber), Times.Once);
            _mockCustomerPortalInterface.Verify(m => m.GetPolicyVehicleRecord(policyNumber), Times.Once);
            _mockCustomerPortalInterface.Verify(m => m.validateChasisNumber(It.IsAny<string>(), chasisNumber), Times.Once);
            _mockCustomerPortalInterface.Verify(m => m.AddPolicyNumberToPolicyList(userId, policyNumber), Times.Once);
        }

        //2 : Invalid PolicyNumber
        [Fact]
        public async Task AddPolicyNumber_ReturnsNotFound_NoPolicyNumber()
        {
            // Arrange
            var userId = _fixture.Create<Guid>();
            var policyNumber = _fixture.Create<int>();
            var chasisNumber = _fixture.Create<string>();

            // Simulate missing policy number
            _mockCustomerPortalInterface
                .Setup(m => m.validatePolicyNumber(policyNumber))
                .ReturnsAsync((Policy)null);

            // Act
            var result = await _sut.AddPloicyNumber(userId, policyNumber, chasisNumber);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>()
                .Which.Value.Should().Be("No Such PolicyNumber Exists"); 

            // Verify method call
            _mockCustomerPortalInterface.Verify(m => m.validatePolicyNumber(policyNumber), Times.Once);
            _mockCustomerPortalInterface.Verify(m => m.GetPolicyVehicleRecord(policyNumber), Times.Never);
            _mockCustomerPortalInterface.Verify(m => m.validateChasisNumber(It.IsAny<string>(), chasisNumber), Times.Never);
            _mockCustomerPortalInterface.Verify(m => m.AddPolicyNumberToPolicyList(userId, policyNumber), Times.Never);
        }

        //3: No record found in policyvehicle table for corresponding policyNumber
        [Fact]
        public async Task AddPolicyNumber_ReturnsNotFound_NoVehicleForPolicy()
        {
            // Arrange
            var userId = _fixture.Create<Guid>();
            var policyNumber = _fixture.Create<int>();
            var chasisNumber = _fixture.Create<string>();

            // Simulate missing vehicle record for policy
            _mockCustomerPortalInterface
                .Setup(m => m.validatePolicyNumber(policyNumber))
                .ReturnsAsync(_fixture.Create<Policy>());

            _mockCustomerPortalInterface
                .Setup(m => m.GetPolicyVehicleRecord(policyNumber))
                .ReturnsAsync((Policyvehicle)null);

            // Act
            var result = await _sut.AddPloicyNumber(userId, policyNumber, chasisNumber);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>()
                 .Which.Value.Should().Be("No Vehicle Found For Corresponding PolicyNumber");

            // Verify method calls
            _mockCustomerPortalInterface.Verify(m => m.validatePolicyNumber(policyNumber), Times.Once);
            _mockCustomerPortalInterface.Verify(m => m.GetPolicyVehicleRecord(policyNumber), Times.Once);
            _mockCustomerPortalInterface.Verify(m => m.validateChasisNumber(It.IsAny<string>(), chasisNumber), Times.Never);
            _mockCustomerPortalInterface.Verify(m => m.AddPolicyNumberToPolicyList(userId, policyNumber), Times.Never);
        }

        //4: ChasisNumber doesn't matches
        [Fact]
        public async Task AddPolicyNumber_ReturnsNotFound_NoVehicleForChasisNumber()
        {
            // Arrange
            var userId = _fixture.Create<Guid>();
            var policyNumber = _fixture.Create<int>();
            var chasisNumber = _fixture.Create<string>();

            // Simulate missing vehicle for chassis number
            _mockCustomerPortalInterface
                .Setup(m => m.validatePolicyNumber(policyNumber))
                .ReturnsAsync(_fixture.Create<Policy>());

            _mockCustomerPortalInterface
                .Setup(m => m.GetPolicyVehicleRecord(policyNumber))
                .ReturnsAsync(_fixture.Create<Policyvehicle>());

            _mockCustomerPortalInterface
                .Setup(m => m.validateChasisNumber(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync((Vehicle)null);

            // Act
            var result = await _sut.AddPloicyNumber(userId, policyNumber, chasisNumber);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>()
                  .Which.Value.Should().Be("ChasisNumber is not matches");

            // Verify method calls
            _mockCustomerPortalInterface.Verify(m => m.validatePolicyNumber(policyNumber), Times.Once);
            _mockCustomerPortalInterface.Verify(m => m.GetPolicyVehicleRecord(policyNumber), Times.Once);
            _mockCustomerPortalInterface.Verify(m => m.validateChasisNumber(It.IsAny<string>(), chasisNumber), Times.Once);
            _mockCustomerPortalInterface.Verify(m => m.AddPolicyNumberToPolicyList(userId, policyNumber), Times.Never);

        }

        //5 : Exception Handling
        [Fact]
        public async Task AddPolicyNumber_ReturnsBadRequest_WhenExceptionOccurs()
        {
            // Arrange
            var userId = _fixture.Create<Guid>();
            var policyNumber = _fixture.Create<int>();
            var chasisNumber = _fixture.Create<string>();
            var expectedExceptionMessage = "An error occurred during AddPolicyNumber.";

            // Simulate an exception during policy validation
            _mockCustomerPortalInterface
                .Setup(m => m.validatePolicyNumber(policyNumber))
                .ThrowsAsync(new Exception(expectedExceptionMessage));

            // Act
            var result = await _sut.AddPloicyNumber(userId, policyNumber, chasisNumber);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>()
                  .Which.Value.Should().Be(expectedExceptionMessage);

            // Verify method call
            _mockCustomerPortalInterface.Verify(m => m.validatePolicyNumber(policyNumber), Times.Once);
        }
        #endregion

        //Tests For GelAllPolicyNumbers
        #region
        //1 : With valid UserId
        [Fact]
        public async Task GetPolicyNumbers_ReturnsOkResult_WithValidUserId()
        {
            // Arrange
            var userId = _fixture.Create<Guid>();
            var policyNumbers = _fixture.CreateMany<UserPloicyList>().ToList();

            _mockCustomerPortalInterface
                .Setup(m => m.GetPolicyNumbersOfUser(userId))
                .ReturnsAsync(policyNumbers);

            // Act
            var result = await _sut.GetPolicyNumbers(userId);

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                  .Which.Value.Should().BeEquivalentTo(policyNumbers);

            // Verify method call
            _mockCustomerPortalInterface.Verify(m => m.GetPolicyNumbersOfUser(userId), Times.Once);
        }

        //2 : With Invalid UserId
        [Fact]
        public async Task GetPolicyNumbers_ReturnsNotFound_WithInvalidUserId()
        {
            // Arrange
            var userId = _fixture.Create<Guid>();

            _mockCustomerPortalInterface
                .Setup(m => m.GetPolicyNumbersOfUser(userId))
                .ReturnsAsync(new List<UserPloicyList>());

            // Act
            var result = await _sut.GetPolicyNumbers(userId);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>()
                  .Which.Value.Should().Be("There is No policyNumber added for corresponding user");

            // Verify method call
            _mockCustomerPortalInterface.Verify(m => m.GetPolicyNumbersOfUser(userId), Times.Once);
        }
        //3 : Exception Handling
        [Fact]
        public async Task GetPolicyNumbers_ReturnsBadRequest_WhenExceptionOccurs()
        {
            // Arrange
            var userId = _fixture.Create<Guid>();
            var expectedExceptionMessage = "An error occurred during GetPolicyNumbers.";

            // Simulate an exception during policy numbers retrieval
            _mockCustomerPortalInterface
                .Setup(m => m.GetPolicyNumbersOfUser(userId))
                .ThrowsAsync(new Exception(expectedExceptionMessage));

            // Act
            var result = await _sut.GetPolicyNumbers(userId);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>()
                  .Which.Value.Should().Be(expectedExceptionMessage);

            // Verify method call
            _mockCustomerPortalInterface.Verify(m => m.GetPolicyNumbersOfUser(userId), Times.Once);
        }
        #endregion

        //Tests For GetVehicleDetails
        #region
        //1 : when vehicleDetails is not empty
        [Fact]
        public async Task GetVehicleDetails_ReturnsOkResult_WithVehicleDetails()
        {
            // Arrange
            var policyNumber = _fixture.Create<int>();
            var vehicleDetails = _fixture.Create<VehicleDto>();

            _mockCustomerPortalInterface
                .Setup(m => m.GetVehicle(policyNumber))
                .ReturnsAsync(vehicleDetails);

            // Act
            var result = await _sut.GetVehicleDetails(policyNumber);

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                  .Which.Value.Should().BeEquivalentTo(vehicleDetails);

            // Verify method call
            _mockCustomerPortalInterface.Verify(m => m.GetVehicle(policyNumber), Times.Once);
        }

        //2 : Exception Handling
        [Fact]
        public async Task GetVehicleDetails_ReturnsBadRequest_WhenExceptionOccurs()
        {
            // Arrange
            var policyNumber = _fixture.Create<int>();
            var expectedExceptionMessage = "An error occurred during GetVehicleDetails.";

            // Simulate an exception during vehicle details retrieval
            _mockCustomerPortalInterface
                .Setup(m => m.GetVehicle(policyNumber))
                .ThrowsAsync(new Exception(expectedExceptionMessage));

            // Act
            var result = await _sut.GetVehicleDetails(policyNumber);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>()
                  .Which.Value.Should().Be(expectedExceptionMessage);

            // Verify method call
            _mockCustomerPortalInterface.Verify(m => m.GetVehicle(policyNumber), Times.Once);
        }
        #endregion

        //Tests For DeleteUserPolicy
        #region
        //1: If Deleted successfully
        [Fact]
        public async Task DeletePolicynumberFromUserPolicyList_ReturnsOkResult_WhenPolicyDeleted()
        {
            // Arrange
            var policyNumber = _fixture.Create<int>();

            _mockCustomerPortalInterface
                .Setup(m => m.DeletePolicyNumber(policyNumber))
                .ReturnsAsync("Deleted Successfully");

            // Act
            var result = await _sut.DeletePolicynumberFromUserPolicyList(policyNumber);

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                  .Which.Value.Should().Be("Deleted Successfully");

            // Verify method call
            _mockCustomerPortalInterface.Verify(m => m.DeletePolicyNumber(policyNumber), Times.Once);
        }

        //2: if not deleted
        [Fact]
        public async Task DeletePolicynumberFromUserPolicyList_ReturnsBadRequest_WhenPolicyDeletionFails()
        {
            // Arrange
            var policyNumber = _fixture.Create<int>();

            // Simulate policy deletion failure
            _mockCustomerPortalInterface
                .Setup(m => m.DeletePolicyNumber(policyNumber))
                .ReturnsAsync((string)null);

            // Act
            var result = await _sut.DeletePolicynumberFromUserPolicyList(policyNumber);

            // Assert
            result.Should().BeOfType<BadRequestResult>();

            // Verify method call
            _mockCustomerPortalInterface.Verify(m => m.DeletePolicyNumber(policyNumber), Times.Once);
        }

        //3: Exception Handling
        [Fact]
        public async Task DeletePolicynumberFromUserPolicyList_ReturnsBadRequest_WhenExceptionOccurs()
        {
            // Arrange
            var policyNumber = _fixture.Create<int>();
            var expectedExceptionMessage = "An error occurred during policy deletion.";

            // Simulate an exception during policy deletion
            _mockCustomerPortalInterface
                .Setup(m => m.DeletePolicyNumber(policyNumber))
                .ThrowsAsync(new Exception(expectedExceptionMessage));

            // Act
            var result = await _sut.DeletePolicynumberFromUserPolicyList(policyNumber);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>()
                  .Which.Value.Should().Be(expectedExceptionMessage);

            // Verify method call
            _mockCustomerPortalInterface.Verify(m => m.DeletePolicyNumber(policyNumber), Times.Once);
        }
        #endregion
    }


}
