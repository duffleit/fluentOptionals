using System;
using System.Collections.Generic;
using FluentOptionals.Composition;

namespace FluentOptionals
{
    #region factory methods

    public static class Optional
    {
        public static Optional<T> None<T>()
        {
            return new Optional<T>();
        }

        public static Optional<T> Some<T>(T value)
        {
            return new Optional<T>(value);
        }

        public static Optional<T> From<T>(T value)
        {
            return value == null ? None<T>() : Some(value);
        }

        public static Optional<T> From<T>(T value, Func<T, bool> condition)
        {
            if (value == null) return None<T>();

            return condition(value) ? Some(value) : None<T>();
        }

        public static Optional<T> From<T>(T? nullable) where T : struct
        {
            return nullable?.ToSome() ?? None<T>();
        }
    }

    #endregion

    public struct Optional<T> :
        IComparable<Optional<T>>,
        IComparable<T>,
        IEquatable<Optional<T>>,
        IEquatable<T>
    {
        internal Optional(T value)
        {
            if (value == null)
                throw SomeCreationOfNullException.FromType<T>();

            _isSome = true;
            _value = value;
        }

        public static implicit operator Optional<T>(T value) => Optional.From(value);

        private readonly bool _isSome;
        private readonly T _value;

        public void Match(Action<T> some, Action none)
        {
            if (_isSome)
                some(_value);
            else
                none();
        }

        public TReturn Match<TReturn>(Func<T, TReturn> some, Func<TReturn> none)
            => _isSome ? some(_value) : none();

        public void IfSome(Action<T> handle) => Match(handle, () => { });

        public void IfNone(Action handle) => Match(_ => { }, handle);

        public T ValueOr(Func<T> handle) => Match(_ => _, handle);

        public T ValueOr(T value) => Match(_ => _, () => value);

        public T ValueOrThrow(Exception exception)
        {
            if (!_isSome) throw exception;
            return _value;
        }

        public Optional<T, T2> Join<T2>(T2 valueToJoin)
            => new Optional<T, T2>(this, Optional.From(valueToJoin));

        public Optional<T, T2> Join<T2>(T2 valueToJoin, Func<T2, bool> condition)
            => new Optional<T, T2>(this, Optional.From(valueToJoin, condition));

        public Optional<T, T2> Join<T2>(Optional<T2> optionalToJoin)
            => new Optional<T, T2>(this, optionalToJoin);

        #region Compare/Equals

        public int CompareTo(Optional<T> other)
            => !_isSome && !other._isSome
                ? 0 : _isSome && other._isSome
                    ? Comparer<T>.Default.Compare(_value, other._value)
                    : _isSome ? -1 : 1;

        public int CompareTo(T other)
            => !_isSome ? -1 : Comparer<T>.Default.Compare(_value, other);

        public bool Equals(Optional<T> other) 
            => !_isSome && !other._isSome || _isSome && other._isSome && EqualityComparer<T>.Default.Equals(_value, other._value);

        public bool Equals(T other)
            => _isSome && EqualityComparer<T>.Default.Equals(_value, other);

        #endregion
    }
}