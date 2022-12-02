using UnityEngine;

namespace Game.Core
{
    public class Timer
    {
        private IPassedTimeCounter _passedTimeCounter;
        private UFloat _cycleTimeInSeconds;

        private UFloat _lastCycleStartTime;

        public float CurrentTime => _passedTimeCounter.CurrentTime;

        public bool IsCycleFinished => _lastCycleStartTime + _cycleTimeInSeconds < CurrentTime;

        public Timer(IPassedTimeCounter passedTimeCounter, UFloat cycleTimeInSeconds)
        {
            _passedTimeCounter = passedTimeCounter;
            _cycleTimeInSeconds = cycleTimeInSeconds;

            _lastCycleStartTime = CurrentTime;
        }

        public void RestartTimer()
        {
            _lastCycleStartTime = CurrentTime;
        }

        public bool TryRestartTimer()
        {
            var isCycleFinished = IsCycleFinished;

            if (isCycleFinished)
                _lastCycleStartTime = CurrentTime;

            return isCycleFinished;
        }
    }
}
    