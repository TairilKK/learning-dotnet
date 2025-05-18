using FluentAssertions;
using LearningTest.Model;
using Moq;
using Xunit.Abstractions;

namespace LearningTest.Tests;

public class BmiCalculatorFacadeTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public BmiCalculatorFacadeTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    private const string UnderweightSummary = "You are underweight, you should put on some weight";
    private const string NormalSummary = "Your weight is normal, keep it up";
    private const string OverweightSummary = "You are a bit overweight";
    private const string ObesitySummary = "You should take care of your obesity";
    private const string ExtremeObesitySummary = "Your extreme obesity might cause health problems";

    [Theory]
    [InlineData(BmiClassification.Underweight, UnderweightSummary)]
    [InlineData(BmiClassification.Normal, NormalSummary)]
    [InlineData(BmiClassification.Overweight, OverweightSummary)]
    [InlineData(BmiClassification.Obesity, ObesitySummary)]
    [InlineData(BmiClassification.ExtremeObesity, ExtremeObesitySummary)]
    public void GetResult_ForValidInputs_ReturnCorrectSummary(BmiClassification bmiClassification, string expectedResult)
    {
        var bmiDeterminatorMock = new Mock<IBmiDeterminator>();
        bmiDeterminatorMock.Setup(m => m.DetermineBmi(It.IsAny<double>()))
            .Returns(bmiClassification);

        var bmiCalculatorFacade = new BmiCalculatorFacade(UnitSystem.Metric, bmiDeterminatorMock.Object);

        var result = bmiCalculatorFacade.GetResult(1, 1);

        _testOutputHelper.WriteLine($"For classification: {bmiClassification} the result is: {result.Summary}");
        result.Summary.Should().Be(expectedResult);
    }
}