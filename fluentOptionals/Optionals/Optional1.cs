using System;
using System.Collections.Generic;

namespace FluentOptionals
{
    public struct Optional<T1> :
        IComparable<Optional<T1>>,
        IComparable<T1>,
        IEquatable<Optional<T1>>,
        IEquatable<T1>
    {
        internal Optional(T1 value)
        {
            if (value == null)
                throw SomeCreationWithNullException.FromType<T1>();

            _isSome = true;
            Value = value;
        }

        public static implicit operator Optional<T1>(T1 value) => Optional.From(value);
        
        private readonly bool _isSome;
        internal readonly T1 Value;

        public bool IsSome => _isSome;
        public bool IsNone => !_isSome;

        public void Match(Action<T1> some, Action none)
        {
            if (IsSome)
                some(Value);
            else
                none();
        }

        public TReturn Match<TReturn>(Func<T1, TReturn> some, Func<TReturn> none)
            => IsSome ? some(Value) : none();

        public void IfSome(Action<T1> handle) => Match(handle, () => { });

        public void IfNone(Action handle) => Match(_ => { }, handle);

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
        {
            return IsNone && other.IsNone || IsSome && other.IsSome && EqualityComparer<T1>.Default.Equals(Value, other.Value);
        }

        public bool Equals(T1 other) 
            => !IsNone && EqualityComparer<T1>.Default.Equals(Value, other);

        #endregion
    }
}