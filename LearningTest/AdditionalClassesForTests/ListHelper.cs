namespace LearningTest.AdditionalClassesForTests
{
    public static class ListHelper
    {
        public static List<int> FilterOddNumber(List<int> listOfNumbers)
        {
            var result = new List<int>();
            foreach (var n in listOfNumbers)
            {
                if (n % 2 != 0)
                {
                    result.Add(n);
                }
            }
            return result;
        }
    }
}
