using LearningTest.Calculator;

namespace LearningTest.Tests;

public class ImperialBmiCalculatorTests
{
    [Theory]
    [InlineData(154, 71, 21.48)]
    [InlineData(125.66, 66.93, 19.72)]
    [InlineData(154.32, 66.93, 24.22)]
    [InlineData(169.76, 62.99, 30.08)]
    [InlineData(176.37, 74.80, 22.16)]
    [InlineData(198.42, 74.80, 24.93)]

    public void CalculateBmi_ForGivenWeightAndHeight_ReturnCorrectBmi(double weight, double height, double bmiResult)
    {
        var imperialBmiCalculator = new ImperialBmiCalculator();

        var result = imperialBmiCalculator.CalculateBmi(weight, height);

        Assert.Equal(bmiResult, result);
    }

    [Theory]
    [InlineData(0, 190)]
    [InlineData(-5, 150)]
    [InlineData(-15, 150)]
    [InlineData(150, 0)]
    [InlineData(150, -1)]
    [InlineData(150, -19)]
    [InlineData(0, 0)]
    public void CalculateBmi_ForInvalidArguments_ThrowsArgumentException(double weight, double height)
    {
        var imperialBmiCalculator = new ImperialBmiCalculator();

        Action action = () => imperialBmiCalculator.CalculateBmi(weight, height);

        Assert.Throws<ArgumentException>(action);
    }
}