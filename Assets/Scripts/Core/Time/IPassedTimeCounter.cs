using UnityEngine;


namespace Game.Core
{
    public interface IPassedTimeCounter
    {
        public const float StandartTimeScale = 1;
        public const float PausedTimeScale = 0;

        UFloat PassedTime { get;}

        UFloat FixedPassedTime { get; }

        UFloat CurrentTime { get; }

        bool TimeStoped { get; }

        void StopTimeLine();

        void StartTimeLine();
    }
}
