namespace NOptional.Tests
{
    public static class OptionalTestHelper
    {
        public static bool IsSome<T>(this Optional<T> optional)
        {
            bool someHandleCalled = false, noneHandleCalled = false;

            optional.Match(
                some: _ => someHandleCalled = true,
                none: () => noneHandleCalled = true);

            return 
                someHandleCalled && !noneHandleCalled;
        }

        public static bool IsNone<T>(this Optional<T> optional)
        {
            bool someHandleCalled = false, noneHandleCalled = false;

            optional.Match(
                some: _ => someHandleCalled = true,
                none: () => noneHandleCalled = true);

            return
                !someHandleCalled && noneHandleCalled;
        }
    }
}