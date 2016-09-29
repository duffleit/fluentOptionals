using System;

namespace NOptional
{
    public class Optional<T1, T2>
    {
        private readonly Optional<T1> _o1;
        private readonly Optional<T2> _o2;

        public Optional(Optional<T1> o1, Optional<T2> o2)
        {
            _o1 = o1;
            _o2 = o2;
        }

        public bool IsSome => _o1.IsSome && _o2.IsSome;
        public bool IsNone => !IsSome;
        
        public void Match(Action<T1, T2> some, Action none)
        {
            if (IsSome)
                some(_o1.ValueOr(default(T1)), _o2.ValueOr(default(T2)));
            else
                none();
        }

        public TReturn Match<TReturn>(Func<T1, T2, TReturn> some, Func<TReturn> none)
            => IsSome 
                ? some(_o1.ValueOr(default(T1)), _o2.ValueOr(default(T2)))
                : none();

        public void IfSome(Action<T1, T2> handle)
            => Match(handle, () => { });

        public void IfNone(Action handle)
            => Match((o1, o2) => { }, handle);

        //#region Join

        ////public Optional<T1, T2, T3> Join<T3>(T3 valueToJoin)
        ////{
        ////    return new Optional<T1, T2, T3>(_o1, _o2, new Optional<T3>(valueToJoin));
        ////}

        ////public Optional<T1, T2, T3> Join<T3>(Optional<T3> optionalToJoin)
        ////{
        ////    return new Optional<T1, T2, T3>(_o1, _o2, optionalToJoin);
        ////}

        //#endregion 
    }
}