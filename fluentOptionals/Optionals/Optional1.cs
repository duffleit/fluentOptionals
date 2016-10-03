using System;
using System.Collections.Generic;

namespace FluentOptionals
{
    public class Optional<T1> :
        IOptional,
        IComparable<Optional<T1>>,
        IComparable<T1>,
        IEquatable<Optional<T1>>,
        IEquatable<T1>
    {
        internal Optional()
        {
            IsSome = false;
        }

        internal Optional(T1 value)
        {
            if (value == null)
                throw SomeCreationWithNullException.FromType<T1>();

            IsSome = true;
            Value = value;
        }

        public static implicit operator Optional<T1>(T1 value) => Optional.From(value);

        internal T1 Value { get; }
        
        public bool IsSome { get; }

        public bool IsNone => !IsSome;

        public void Match(Action<T1> some, Action none)
        {
            if (IsSome)
                some(Value);
            else
                none();
        }

        public TReturn Match<TReturn>(Func<T1, TReturn> some, Func<TReturn> none)
            => IsSome ? some(Value) : none();

        public void MatchSome(Action<T1> handle) => Match(handle, () => { });

        public void MatchNone(Action handle) => Match(_ => { }, handle);

        public Optional<TMapResult> Map<TMapResult>(Func<T1, TMapResult> mapper)
            => IsSome
                ? Optional.From(mapper(Value))
                : Optional.None<TMapResult>();

        public Optional<T1> Shift(Func<T1, bool> condition)
        {
            if (IsNone) return this;
            return (condition(Value)) ? Optional.None<T1>() : this;
        }

        public T1 ValueOr(Func<T1> handle) => Match(_ => _, handle);

        public T1 ValueOr(T1 value) => Match(_ => _, () => value);

        public T1 ValueOrThrow(Exception exception)
        {
            if (IsNone) throw exception;
            return Value;
        }

        public Optional<T1, T2> Join<T2>(T2 valueToJoin) 
            => new Optional<T1, T2>(this, Optional.From(valueToJoin));

        public Optional<T1, T2> Join<T2>(T2 valueToJoin, Func<T2, bool> condition)
            => new Optional<T1, T2>(this, Optional.From(valueToJoin, condition));

        public Optional<T1, T2> Join<T2>(Optional<T2> optionalToJoin) 
            => new Optional<T1, T2>(this, optionalToJoin);

        #region Compare/Equals

        public int CompareTo(Optional<T1> other) 
            => IsNone && other.IsNone
                ? 0 : IsSome && other.IsSome
                    ? Comparer<T1>.Default.Compare(Value, other.Value)
                    : IsSome ? -1 : 1;


        public int CompareTo(T1 other)
            => IsNone
                ? -1 : Comparer<T1>.Default.Compare(Value, other);


        public bool Equals(Optional<T1> other) 
            => IsNone && other.IsNone || IsSome && other.IsSome && EqualityComparer<T1>.Default.Equals(Value, other.Value);

        public bool Equals(T1 other) 
            => !IsNone && EqualityComparer<T1>.Default.Equals(Value, other);

        #endregion
    }
}