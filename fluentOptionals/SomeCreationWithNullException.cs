using System;

namespace FluentOptionals
{
    public class SomeCreationOfNullException : Exception
    {
        public SomeCreationOfNullException() : 
            base("Optional-Some cannot be created with null.")
        { }

        private SomeCreationOfNullException(string message) : base(message){ }

        public static SomeCreationOfNullException FromType<T>()
        {
            var type = typeof (T);
            var message = $"Optional-Some of Type '{type}' cannot be created with null.";

            return new SomeCreationOfNullException(message);
        } 
    }
}
