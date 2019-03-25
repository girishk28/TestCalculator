using Calculator;
using Moq;
using NUnit.Framework;

namespace SimpleCalculator.Tests
{
    /// <summary>
    /// Unit Tests for all Calculator Operations
    /// </summary>
    [TestFixture]
    public class SimpleCalculatorTests
    {

        [TestCase(2, 2, 4)]
        [TestCase(-5, 5, 0)]
        [TestCase(-3, -3, -6)]
        public void Add_Always_ReturnsExpectedResult(int a, int b, int expected)
        {
            //Setup
            var mockCalculator = new Mock<IDiagnostics>();
            var mockDummyDiagnostics = new Mock<IDummyDiagnostics>();
            mockCalculator.Setup(x => x.AddToConsole(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()));

            ISimpleCalculator simpleCalculator = new Calculator.SimpleCalculator(mockCalculator.Object,
                mockDummyDiagnostics.Object);
            //Act
            var result = simpleCalculator.Add(a, b);
            //Verify
            Assert.AreEqual(result, expected, "The Add functionality is not working correctly.");
            // Check to see if the calculators add method diagnostics AddToConsole method once
            mockCalculator.Verify(x => x.AddToConsole(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);
            //Check if AddToConsole was called with the expected value
            mockCalculator.Verify(x => x.AddToConsole(a, b, result), Times.Once);
        }

        [TestCase(2, 2, 0)]
        [TestCase(-2, 2, -4)]
        [TestCase(-2, -2, -0)]
        public void Subtract_Always_ReturnsExpectedResult(int a, int b, int expected)
        {
            //Setup
            var mockCalculator = new Mock<IDiagnostics>();
            var mockDummyDiagnostics = new Mock<IDummyDiagnostics>();
            mockCalculator.Setup(x => x.SubtractToConsole(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()));

            ISimpleCalculator simpleCalculator = new Calculator.SimpleCalculator(mockCalculator.Object,
                mockDummyDiagnostics.Object);

            //Act
            var result = simpleCalculator.Subtract(a, b);
            //Verify
            Assert.AreEqual(result, expected, "The Substract functionality is not working correctly.");
            // Check to see if the calculators add method diagnostics AddToConsole method once
            mockCalculator.Verify(x => x.SubtractToConsole(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()),
                Times.Once);
            //Check if AddToConsole was called with the expected value
            mockCalculator.Verify(x => x.SubtractToConsole(a, b, result), Times.Once);
        }

        [TestCase(2, 2, 4)]
        [TestCase(-2, 2, -4)]
        [TestCase(-2, -2, 4)]
        public void Multiply_Always_ReturnsExpectedResult(int a, int b, int expected)
        {
            //Setup
            var mockCalculator = new Mock<IDiagnostics>();
            var mockDummyDiagnostics = new Mock<IDummyDiagnostics>();
            mockCalculator.Setup(x => x.MultiplyToConsole(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()));

            ISimpleCalculator simpleCalculator = new Calculator.SimpleCalculator(mockCalculator.Object,
                mockDummyDiagnostics.Object);

            //Act
            var result = simpleCalculator.Multiply(a, b);
            //Verify
            Assert.AreEqual(result, expected, "The Multiply functionality is not working correctly.");
            // Check to see if the calculators add method diagnostics AddToConsole method once
            mockCalculator.Verify(x => x.MultiplyToConsole(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()),
                Times.Once);
            //Check if AddToConsole was called with the expected value
            mockCalculator.Verify(x => x.MultiplyToConsole(a, b, result), Times.Once);
        }

        [TestCase(2, 2, 1)]
        [TestCase(-2, 2, -1)]
        [TestCase(-2, -2, 1)]
        [TestCase(9, 3, 3)]
        public void Division_Always_ReturnsExpectedResult(int a, int b, int expected)
        {

            //Setup
            var mockCalculator = new Mock<IDiagnostics>();
            var mockDummyDiagnostics = new Mock<IDummyDiagnostics>();
            mockCalculator.Setup(x => x.DivideToConsole(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()));

            ISimpleCalculator simpleCalculator = new Calculator.SimpleCalculator(mockCalculator.Object,
                mockDummyDiagnostics.Object);

            //Act
            var result = simpleCalculator.Divide(a, b);
            //Verify
            Assert.AreEqual(result, expected, "The Divide functionality is not working correctly.");
            // Check to see if the calculators add method diagnostics AddToConsole method once
            mockCalculator.Verify(x => x.DivideToConsole(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()),
                Times.Once);
            //Check if AddToConsole was called with the expected value
            mockCalculator.Verify(x => x.DivideToConsole(a, b, result), Times.Once);
        }
    }
}
