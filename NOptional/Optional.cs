using System;
using System.ComponentModel;

namespace NOptional
{
    public class Optional
    {
        public static Optional<T> None<T>()
        {
            return new Optional<T>();
        }

        public static Optional<T> Some<T>(T value)
        {
            return new Optional<T>(value);
        }
    }

    public class Optional<T>
    {
        private readonly T _value;
        private readonly bool _filled;

        #region Constructor

        public Optional()
        {
            _filled = false;
        }

        public Optional(T value)
        {
            _filled = (value != null);
            _value = value;
        }

        #endregion

        public void Match(Action<T> some, Action none)
        {
            if (_filled)
                some(_value);
            else
                none();
        }

        public TReturn Match<TReturn>(Func<T, TReturn> some, Func<TReturn> none) 
            => _filled ? some(_value) : none();


        public void IfSome(Action<T> handle)
            => Match(handle, () => { });

        public void IfNone(Action handle)
            => Match(_ => { }, handle);

        public T IfNone(Func<T> handle)
            => Match(_ => _, handle);

        public T IfNone(T value)
            => Match(_ => _, () => value);

        public static implicit operator Optional<T>(T value) 
            => new Optional<T>(value);
    }
}