using FluentAssertions;
using LearningTest.AdditionalClassesForTests;

namespace LearningTest.Tests.AdditionalClassesForTests;

public class ValidatorTests
{

    private readonly List<List<DateRange>> _rangesList = new List<List<DateRange>>()
    {
        new List<DateRange>()
        {
            new DateRange(
                new DateTime(2025, 4, 1),
                new DateTime(2025, 4, 15)
            ),
            new DateRange(
                new DateTime(2025, 5, 1),
                new DateTime(2025, 5, 15)
            )
        },
        new List<DateRange>()
        {
            new DateRange(
                new DateTime(2025, 4, 15),
                new DateTime(2025, 4, 25)
            )
        },
        new List<DateRange>()
        {
            new DateRange(
                new DateTime(2025, 4, 8),
                new DateTime(2025, 4, 25)
            )
        },
        new List<DateRange>()
        {
            new DateRange(
                new DateTime(2025, 4, 12),
                new DateTime(2025, 4, 14)
            )
        }
    };

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void ValidateOverlapping_ForOverlappingDateRanges_ReturnsFalse(int index)
    {
        var ranges = _rangesList[index];

        var input = new DateRange(
            new DateTime(2025, 4, 10),
            new DateTime(2025, 4, 20)
        );

        var validator = new Validator();

        var res = validator.ValidateOverlapping(ranges, input);

        res.Should().BeFalse();
    }


    [Theory]
    [InlineData(0)]
    [InlineData(3)]
    public void ValidateOverlapping_ForNoneOverlappingDateRanges_ReturnsTrue(int index)
    {
        var ranges = _rangesList[index];

        var input = new DateRange(
            new DateTime(2025, 4, 16),
            new DateTime(2025, 4, 17)
        );

        var validator = new Validator();

        var res = validator.ValidateOverlapping(ranges, input);

        res.Should().BeTrue();
    }
}