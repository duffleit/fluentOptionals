using System;

namespace fluentOptionals
{
    public class Optional<T1, T2, T3, T4> : 
        IOptional,
        IEquatable<Optional<T1, T2, T3, T4>>
    {
        private readonly Optional<T1> _o1;
        private readonly Optional<T2> _o2;
        private readonly Optional<T3> _o3;
        private readonly Optional<T4> _o4;

        internal Optional(Optional<T1> o1, Optional<T2> o2, Optional<T3> o3, Optional<T4> o4)
        {
            _o1 = o1;
            _o2 = o2;
            _o3 = o3;
            _o4 = o4;
        }

        public bool IsSome => _o1.IsSome && _o2.IsSome && _o3.IsSome && _o4.IsSome;
        public bool IsNone => !IsSome;
        
        public void Match(Action<T1, T2, T3, T4> some, Action none)
        {
            if (IsSome)
                some(_o1.Value, _o2.Value, _o3.Value, _o4.Value);
            else
                none();
        }

        public TReturn Match<TReturn>(Func<T1, T2, T3, T4, TReturn> some, Func<TReturn> none)
            => IsSome 
                ? some(_o1.Value, _o2.Value, _o3.Value, _o4.Value)
                : none();

        public void IfSome(Action<T1, T2, T3, T4> handle)
            => Match(handle, () => { });

        public void IfNone(Action handle)
            => Match((o1, o2, o3, o4) => { }, handle);

        public Optional<T1, T2, T3, T4, T5> Join<T5>(T5 valueToJoin)
            => new Optional<T1, T2, T3, T4, T5>(_o1, _o2, _o3, _o4, Optional.From<T5>(valueToJoin));

        public Optional<T1, T2, T3, T4, T5> Join<T5>(Optional<T5> optionalToJoin)
            => new Optional<T1, T2, T3, T4, T5>(_o1, _o2, _o3, _o4, optionalToJoin);

        #region Equals

        public bool Equals(Optional<T1, T2, T3, T4> other) 
            => _o1.Equals(other._o1) && _o2.Equals(other._o2) && _o2.Equals(other._o3) && _o2.Equals(other._o4);

        #endregion
    }
}