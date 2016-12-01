using System;

namespace FluentOptionals.Composition
{
    public struct Optional<T1, T2> : 
        IEquatable<Optional<T1, T2>>
    {
        private readonly Optional<T1> _o1;
        private readonly Optional<T2> _o2;

        internal Optional(Optional<T1> o1, Optional<T2> o2)
        {
            _o1 = o1;
            _o2 = o2;
        }

        public bool IsSome => _o1.IsSome() && _o2.IsSome();
        public bool IsNone => !IsSome;
        
        public void Match(Action<T1, T2> some, Action none)
        {
            if (IsSome)
                some(_o1.Value, _o2.Value);
            else
                none();
        }

        public TReturn Match<TReturn>(Func<T1, T2, TReturn> some, Func<TReturn> none)
            => IsSome 
                ? some(_o1.Value, _o2.Value)
                : none();

        public void IfSome(Action<T1, T2> handle)
            => Match(handle, () => { });

        public void IfNone(Action handle)
            => Match((o1, o2) => { }, handle);

        public Optional<T1, T2, T3> Join<T3>(T3 valueToJoin)
            => new Optional<T1, T2, T3>(_o1, _o2, Optional.From(valueToJoin));

        public Optional<T1, T2, T3> Join<T3>(T3 valueToJoin, Func<T3, bool> condition)
            => new Optional<T1, T2, T3>(_o1, _o2, Optional.From(valueToJoin, condition));

        public Optional<T1, T2, T3> Join<T3>(Optional<T3> optionalToJoin)
            => new Optional<T1, T2, T3>(_o1, _o2, optionalToJoin);

        #region Equals

        public bool Equals(Optional<T1, T2> other)
        {
            return _o1.Equals(other._o1) && _o2.Equals(other._o2);
        }

        #endregion
    }
}