using System;

namespace fluentOptionals
{
    public class SomeCreationWithNullException : Exception
    {
        public SomeCreationWithNullException() : 
            base("Optional-Some cannoat be created with null.")
        { }

        private SomeCreationWithNullException(string message) : base(message){ }

        public static SomeCreationWithNullException FromType<T>()
        {
            var type = typeof (T);
            var message = $"Optional-Some of Type '{type}' cannoat be created with null.";

            return new SomeCreationWithNullException(message);
        } 
    }
}