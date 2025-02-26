using ColdWind.Core.ModularCompositeRoot.Internal;

namespace ColdWind.Core.ModularCompositeRoot
{
    public abstract class MonoScript : BaseMonoScript
    {
        public void Construct() =>
            RunConstructorExecutor(() => Constructor());
        
        protected virtual void Constructor() { }
    }

    public abstract class MonoScript<T> : BaseMonoScript
    {
        public void Construct(T parameter) =>
            RunConstructorExecutor(() => Constructor(parameter));
        
        protected abstract void Constructor(T parameter);
    }

    public abstract class MonoScript<T1, T2> : BaseMonoScript
    {
        public void Construct(T1 parameter1, T2 parameter2) =>
            RunConstructorExecutor(() => Constructor(parameter1, parameter2));

        protected abstract void Constructor(T1 parameter1, T2 parameter2);
    }

    public abstract class MonoScript<T1, T2, T3> : BaseMonoScript
    {
        public void Construct(T1 parameter1, T2 parameter2, T3 parameter3) =>
            RunConstructorExecutor(() => Constructor(parameter1, parameter2, parameter3));

        protected abstract void Constructor(T1 parameter1, T2 parameter2, T3 parameter3);
    }

    public abstract class MonoScript<T1, T2, T3, T4> : BaseMonoScript
    {
        public void Construct(T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4) =>
            RunConstructorExecutor(() => Constructor(parameter1, parameter2, parameter3, parameter4));

        protected abstract void Constructor(T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4);
    }

    public abstract class MonoScript<T1, T2, T3, T4, T5> : BaseMonoScript
    {
        public void Construct(T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5) =>
            RunConstructorExecutor(() => Constructor(parameter1, parameter2, parameter3, parameter4, parameter5));

        protected abstract void Constructor(T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5);
    }
}
