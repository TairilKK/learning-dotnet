using LearningTest.Model;

namespace LearningTest.Tests;

public class BmiCalculatorFacadeTests
{
    private const string OVERWEIGHT_SUMMARY = "You are a bit overweight";

    [Fact]
    public void GetResult_ForValidInputs_ReturnCorrectResult()
    {
        var bmiCalculatorFacade = new BmiCalculatorFacade(UnitSystem.Metric);

        double weight = 90;
        double height = 190;

        var result = bmiCalculatorFacade.GetResult(weight, height);

        Assert.Equal(24.93, result.Bmi);
        Assert.Equal(BmiClassification.Overweight, result.BmiClassification);
        Assert.Equal(OVERWEIGHT_SUMMARY, result.Summary);
    }
}