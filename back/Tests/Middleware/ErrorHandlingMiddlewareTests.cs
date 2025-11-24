using Api.Middlewares;
using Aplication.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Middleware
{
    [TestClass]
    public class ErrorHandlingMiddlewareTests
    {
        private  Mock<ILogger<ErrorHandlingMiddleware>> _logger;
        private Mock<IWebHostEnvironment> _envMock;
        private DefaultHttpContext _context;

        [TestInitialize]
        public void SetUp()
        {
            _logger = new Mock<ILogger<ErrorHandlingMiddleware>>();
            _envMock = new Mock<IWebHostEnvironment>();

            _context = new DefaultHttpContext();
            _context.Response.Body = new MemoryStream();
        }
        public async Task<string> GetErrorObject()
        {
            _context.Response.Body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(_context.Response.Body);
            var responseText = await reader.ReadToEndAsync();
            return responseText;
        }
        [TestMethod]
        public async Task Invoke_ProductionEnvWithUnhandledException_Returns500AndGenericMessage()
        {
            // Arrange
            _envMock.Setup(x => x.EnvironmentName).Returns("Production"); 

            var middleware = new ErrorHandlingMiddleware(
                next: (inner_context) => throw new Exception("Test exception"),
                logger: _logger.Object,
                env: _envMock.Object
            );

            // Act
            await middleware.Invoke(_context);

            // Assert
            var responseText = await GetErrorObject();

            Assert.AreEqual(500, _context.Response.StatusCode);
            Assert.AreEqual("application/json", _context.Response.ContentType);
            StringAssert.Contains(responseText, "An internal server error occurred");
        }
        [TestMethod]
        public async Task Invoke_DevelopmentEnvWithUnhandledException_Returns500ErrorAndSpecificMessage()
        {
            // Arrange
            _envMock.Setup(x => x.EnvironmentName).Returns("Development");

            var middleware = new ErrorHandlingMiddleware(
                next: (inner_context) => throw new Exception("Test exception"),
                logger: _logger.Object,
                env: _envMock.Object
            );

            // Act
            await middleware.Invoke(_context);

            // Assert
            var responseText = await GetErrorObject();

            Assert.AreEqual(500, _context.Response.StatusCode);
            Assert.AreEqual("application/json", _context.Response.ContentType);
            StringAssert.Contains(responseText, "Test exception");
        }
        [TestMethod]
        public async Task Invoke_DevelopmentEnvWithValidationException_Returns400ErrorAndSpecificMessage()
        {
            // Arrange
            _envMock.Setup(x => x.EnvironmentName).Returns("Development");

            var middleware = new ErrorHandlingMiddleware(
                next: (inner_context) => throw new ValidationException("Test exception"),
                logger: _logger.Object,
                env: _envMock.Object
            );

            // Act
            await middleware.Invoke(_context);

            // Assert
            var responseText = await GetErrorObject();

            Assert.AreEqual(400, _context.Response.StatusCode);
            Assert.AreEqual("application/json", _context.Response.ContentType);
            StringAssert.Contains(responseText, "Test exception");
        }
        [TestMethod]
        public async Task Invoke_DevelopmentEnvWithUnauthorizedAccessException_Returns401ErrorAndSpecificMessage()
        {
            // Arrange
            _envMock.Setup(x => x.EnvironmentName).Returns("Development");

            var middleware = new ErrorHandlingMiddleware(
                next: (inner_context) => throw new UnauthorizedAccessException("Test exception"),
                logger: _logger.Object,
                env: _envMock.Object
            );

            // Act
            await middleware.Invoke(_context);

            // Assert
            var responseText = await GetErrorObject();

            Assert.AreEqual(401, _context.Response.StatusCode);
            Assert.AreEqual("application/json", _context.Response.ContentType);
            StringAssert.Contains(responseText, "Test exception");
        }

        [TestMethod]
        public async Task Invoke_DevelopmentEnvWithKeyNotFoundException_Returns404ErrorAndSpecificMessage()
        {
            // Arrange
            _envMock.Setup(x => x.EnvironmentName).Returns("Development");

            var middleware = new ErrorHandlingMiddleware(
                next: (inner_context) => throw new KeyNotFoundException("Test exception"),
                logger: _logger.Object,
                env: _envMock.Object
            );

            // Act
            await middleware.Invoke(_context);

            // Assert
            var responseText = await GetErrorObject();

            Assert.AreEqual(404, _context.Response.StatusCode);
            Assert.AreEqual("application/json", _context.Response.ContentType);
            StringAssert.Contains(responseText, "Test exception");
        }
        [TestMethod]
        public async Task Invoke_ProductiontEnvWithKeyNotFoundException_Returns404ErrorAndSpecificMessage()
        {
            // Arrange
            _envMock.Setup(x => x.EnvironmentName).Returns("Production");

            var middleware = new ErrorHandlingMiddleware(
                next: (inner_context) => throw new KeyNotFoundException("Test exception"),
                logger: _logger.Object,
                env: _envMock.Object
            );

            // Act
            await middleware.Invoke(_context);

            // Assert
            var responseText = await GetErrorObject();

            Assert.AreEqual(404, _context.Response.StatusCode);
            Assert.AreEqual("application/json", _context.Response.ContentType);
            StringAssert.Contains(responseText, "Test exception");
        }
    }
}
