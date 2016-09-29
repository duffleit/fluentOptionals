using System;

namespace NOptional
{
    public static class Optional
    {
        public static Optional<T> None<T>()
        {
            return new Optional<T>();
        }

        public static Optional<T> Some<T>(T value)
        {
            if(value == null)
                throw new ArgumentNullException(nameof(value));
            return new Optional<T>(value);
        }
    }

    public class Optional<T>
    {
        private readonly T _value;

        #region Constructor

        public Optional()
        {
            IsSome = false;
        }

        public Optional(T value)
        {
            IsSome = (value != null);
            _value = value;
        }

        #endregion

        public bool IsSome { get; }
        public bool IsNone => !IsSome;

        public void Match(Action<T> some, Action none)
        {
            if (IsSome)
                some(_value);
            else
                none();
        }

        public TReturn Match<TReturn>(Func<T, TReturn> some, Func<TReturn> none) 
            => IsSome ? some(_value) : none();

        public Optional<TMapResult> Map<TMapResult>(Func<T, TMapResult> mapper)
            => IsSome
                ? new Optional<TMapResult>(mapper(_value)) 
                : new Optional<TMapResult>();

        #region Convenience Methods

        public void IfSome(Action<T1> handle) => Match(handle, () => { });

        public void IfNone(Action handle) => Match(_ => { }, handle);

        public T1 ValueOr(Func<T1> handle) => Match(_ => _, handle);

        public T1 ValueOr(T1 value) => Match(_ => _, () => value);

        #endregion




        public static implicit operator Optional<T>(T value) 
            => new Optional<T>(value);
    }
}