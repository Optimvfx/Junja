namespace Game.Core
{
    public interface ITickHandler
    {
        void ApplayTick();
    }

    public interface ITickHandler<T>
    {
        void ApplayTick(T value);
    }
}
