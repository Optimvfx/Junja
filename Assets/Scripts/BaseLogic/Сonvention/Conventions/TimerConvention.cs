using Game.Core;
using UnityEngine;

namespace Game.BaseLogic
{
    public class TimerConvention : IConvention
    {
        private Timer _timer;

        public bool IsPerformed => _timer.TryRestartTimer();

        public TimerConvention(UFloat cycleTimeInSeconds, IPassedTimeCounter timeCounter)
        {
            _timer = new Timer(timeCounter, cycleTimeInSeconds);
        }
    }
}