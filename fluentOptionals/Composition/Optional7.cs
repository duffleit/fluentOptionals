using System;

namespace FluentOptionals.Composition
{
    public struct Optional<T1, T2, T3, T4, T5, T6, T7> : 
        IEquatable<Optional<T1, T2, T3, T4, T5, T6, T7>>
    {
        private readonly Optional<T1> _o1;
        private readonly Optional<T2> _o2;
        private readonly Optional<T3> _o3;
        private readonly Optional<T4> _o4;
        private readonly Optional<T5> _o5;
        private readonly Optional<T6> _o6;
        private readonly Optional<T7> _o7;

        internal Optional(Optional<T1> o1, Optional<T2> o2, Optional<T3> o3, Optional<T4> o4, Optional<T5> o5, Optional<T6> o6, Optional<T7> o7)
        {
            _o1 = o1;
            _o2 = o2;
            _o3 = o3;
            _o4 = o4;
            _o5 = o5;
            _o6 = o6;
            _o7 = o7;
        }

        public bool IsSome => _o1.IsSome && _o2.IsSome && _o3.IsSome && _o4.IsSome && _o5.IsSome && _o6.IsSome && _o7.IsSome;
        public bool IsNone => !IsSome;
        
        public void Match(Action<T1, T2, T3, T4, T5, T6, T7> some, Action none)
        {
            if (IsSome)
                some(_o1.Value, _o2.Value, _o3.Value, _o4.Value, _o5.Value, _o6.Value, _o7.Value);
            else
                none();
        }

        public TReturn Match<TReturn>(Func<T1, T2, T3, T4, T5, T6, T7, TReturn> some, Func<TReturn> none)
            => IsSome 
                ? some(_o1.Value, _o2.Value, _o3.Value, _o4.Value, _o5.Value, _o6.Value, _o7.Value)
                : none();

        public void IfSome(Action<T1, T2, T3, T4, T5, T6, T7> handle)
            => Match(handle, () => { });

        public void IfNone(Action handle)
            => Match((o1, o2, o3, o4, o5, o6, o7) => { }, handle);

        #region Equals
        
        public bool Equals(Optional<T1, T2, T3, T4, T5, T6, T7> other)
        {
            return _o1.Equals(other._o1) && _o2.Equals(other._o2) && _o2.Equals(other._o3) && _o2.Equals(other._o4) && _o2.Equals(other._o5) && _o2.Equals(other._o6) && _o2.Equals(other._o7);
        }

        #endregion
    }
}