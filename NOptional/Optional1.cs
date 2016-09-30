using System;

namespace NOptional
{
    public class Optional<T1> : IOptional
    {
        private readonly T1 _value;

        public Optional()
        {
            IsSome = false;
        }

        public Optional(T1 value)
        {
            IsSome = (value != null);
            _value = value;
        }

        public static implicit operator Optional<T1>(T1 value) => Optional.From(value);

        public bool IsSome { get; }

        public bool IsNone => !IsSome;

        public void Match(Action<T1> some, Action none)
        {
            if (IsSome)
                some(_value);
            else
                none();
        }

        public TReturn Match<TReturn>(Func<T1, TReturn> some, Func<TReturn> none)
            => IsSome ? some(_value) : none();

        public Optional<TMapResult> Map<TMapResult>(Func<T1, TMapResult> mapper)
            => IsSome
                ? Optional.From(mapper(_value))
                : Optional.None<TMapResult>();

        public void IfSome(Action<T1> handle) => Match(handle, () => { });

        public void IfNone(Action handle) => Match(_ => { }, handle);

        public T1 ValueOr(Func<T1> handle) => Match(_ => _, handle);

        public T1 ValueOr(T1 value) => Match(_ => _, () => value);

        public Optional<T1, T2> Join<T2>(T2 valueToJoin)
        {
            return new Optional<T1, T2>(this, Optional.From<T2>(valueToJoin));
        }

        public Optional<T1, T2> Join<T2>(Optional<T2> optionalToJoin)
        {
            return new Optional<T1, T2>(this, optionalToJoin);
        }
    }
}