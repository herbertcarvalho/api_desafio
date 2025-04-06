namespace Tests.Helpers
{
    public static class CustomAssert
    {
        public static void Contains(string actual, List<string> expectedSubstring)
            => Assert.IsTrue(expectedSubstring.Contains(actual));
    }
}
