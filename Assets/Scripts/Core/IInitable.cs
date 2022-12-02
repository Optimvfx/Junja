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
}
