using LearningTest.Calculator;

namespace LearningTest.Tests;

public class MetricBmiCalculatorTests
{
    [Theory]
    [InlineData(100, 170, 34.6)]
    [InlineData(57, 170, 19.72)]
    [InlineData(70, 170, 24.22)]
    [InlineData(77, 160, 30.08)]
    [InlineData(80, 190, 22.16)]
    [InlineData(90, 190, 24.93)]
    public void CalculateBmi_ForGivenWeightAndHeight_ReturnCorrectBmi(double weight, double height, double bmiResult)
    {
        var metricBmiCalculator = new MetricBmiCalculator();

        var result = metricBmiCalculator.CalculateBmi(weight, height);

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
        var metricBmiCalculator = new MetricBmiCalculator();

        Action action = () => metricBmiCalculator.CalculateBmi(weight, height);

        Assert.Throws<ArgumentException>(action);
    }
}