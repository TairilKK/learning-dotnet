using FluentAssertions;
using LearningTest.AdditionalClassesForTests;

namespace LearningTest.Tests.AdditionalClassesForTests;

public class ValidatorTests
{
   public static IEnumerable<object[]> GetNoneOverlappingRangeList()
    {
        yield return new object[] {new List<DateRange>()
        {
            new DateRange(
                new DateTime(2025, 4, 1),
                new DateTime(2025, 4, 15)
            ),
            new DateRange(
                new DateTime(2025, 5, 1),
                new DateTime(2025, 5, 15)
            )
        }};

        yield return new object[] {new List<DateRange>()
        {
            new DateRange(
                new DateTime(2025, 4, 12),
                new DateTime(2025, 4, 14)
            )
        }};

    }
    [Theory]
    [ClassData(typeof(ValidatorTestData))]
    public void ValidateOverlapping_ForOverlappingDateRanges_ReturnsFalse(List<DateRange> ranges)
    {

        var input = new DateRange(
            new DateTime(2025, 4, 10),
            new DateTime(2025, 4, 20)
        );

        var validator = new Validator();

        var res = validator.ValidateOverlapping(ranges, input);

        res.Should().BeFalse();
    }


    [Theory]
    [MemberData(nameof(GetNoneOverlappingRangeList))]
    public void ValidateOverlapping_ForNoneOverlappingDateRanges_ReturnsTrue(List<DateRange> ranges)
    {

        var input = new DateRange(
            new DateTime(2025, 4, 16),
            new DateTime(2025, 4, 17)
        );

        var validator = new Validator();

        var res = validator.ValidateOverlapping(ranges, input);

        res.Should().BeTrue();
    }
}