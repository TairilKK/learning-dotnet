using LearningTest.Model;

namespace LearningTest.Tests;

public class BmiDeterminatorTests
{
    [Theory]
    [InlineData(BmiClassification.Underweight,8.0)]
    [InlineData(BmiClassification.Underweight,10.0)]
    [InlineData(BmiClassification.Underweight,13.0)]
    [InlineData(BmiClassification.Underweight,18.0)]
    [InlineData(BmiClassification.Normal,19.0)]
    [InlineData(BmiClassification.Normal,20.0)]
    [InlineData(BmiClassification.Normal,23.0)]
    [InlineData(BmiClassification.Normal,24.0)]
    [InlineData(BmiClassification.Overweight,25.0)]
    [InlineData(BmiClassification.Overweight,25.2)]
    [InlineData(BmiClassification.Overweight,27.9)]
    [InlineData(BmiClassification.Overweight,29.8)]
    [InlineData(BmiClassification.Obesity,29.9)]
    [InlineData(BmiClassification.Obesity,31.9)]
    [InlineData(BmiClassification.Obesity,30.9)]
    [InlineData(BmiClassification.Obesity,34.9)]
    [InlineData(BmiClassification.ExtremeObesity,35.9)]
    [InlineData(BmiClassification.ExtremeObesity,36.9)]
    public void DetermineBmi_ForGivenBmi_ReturnsCorrectClassification(BmiClassification expectedResult, double bmi)
    {
        var bmiDeterminator = new BmiDeterminator();

        var result = bmiDeterminator.DetermineBmi(bmi);

        Assert.Equal(expectedResult, result);
    }
}