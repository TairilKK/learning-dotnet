namespace LearningTest.AdditionalClassForTests
{
    public static class StringHelper
    {
        public static string ReverseWords(string str)
        {
            var splitWords = str.Split(' ');
            var reversedWords = splitWords.Reverse();
            var result = string.Join(' ', reversedWords);
            return result;
        }

        public static bool IsPalindrome(string str)
        {
            return str.SequenceEqual(str.Reverse());
        }

    }
}
