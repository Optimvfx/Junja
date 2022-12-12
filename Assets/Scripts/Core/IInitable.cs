namespace Game.Core
{
    public interface IInitable
    {
        void Init();
    }

    public interface IInitable<T>
    {
        void Init(T argument);
    }

    public interface IInitable<T, T1>
    {
        void Init(T argument, T1 argumentOne);
    }

    public interface IInitable<T, T1, T2>
    {
        void Init(T argument, T1 argumentOne, T2 argumenTtwo);
    }
}
