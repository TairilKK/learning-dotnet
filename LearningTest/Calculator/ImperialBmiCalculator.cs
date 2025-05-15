namespace LearningTest.Calculator;

public class ImperialBmiCalculator : IBmiCalculator
{
    public double CalculateBmi(double weight, double height)
    {
        if (weight <= 0)
            throw new ArgumentException("Weight is not a valid number");

        if (height <= 0)
            throw new ArgumentException("Height is not a valid number");

        var bmi = 703 * weight / Math.Pow((height / 100.0), 2);
        return Math.Round(bmi, 2);
    }
}