using LearningTest.Calculator;

namespace LearningTest.Tests;

public class MetricBmiCalculatorTests
{
    public static IEnumerable<object[]> GetSampleData()
    {
        yield return new object[] {100, 170, 34.6};
        yield return new object[] {57, 170, 19.72};
        yield return new object[] {70, 170, 24.22};
        yield return new object[] {77, 160, 30.08};
        yield return new object[] {80, 190, 22.16};
        yield return new object[] {90, 190, 24.93};
    }
    [Theory]
    [MemberData(nameof(GetSampleData))]
    public void CalculateBmi_ForGivenWeightAndHeight_ReturnCorrectBmi(double weight, double height, double bmiResult)
    {
        var metricBmiCalculator = new MetricBmiCalculator();

        var result = metricBmiCalculator.CalculateBmi(weight, height);

        Assert.Equal(bmiResult, result);
    }

    [Theory]
    [JsonFileData("Data/MetricBmiCalculatorData.json")]
    public void CalculateBmi_ForInvalidArguments_ThrowsArgumentException(double weight, double height)
    {
        var metricBmiCalculator = new MetricBmiCalculator();

        Action action = () => metricBmiCalculator.CalculateBmi(weight, height);

        Assert.Throws<ArgumentException>(action);
    }
}