using Game.Core;
using UnityEngine;

namespace Game.BaseLogic
{
    [CreateAssetMenu(fileName = "TimeConvention", menuName = "ScriptableObjects/Conventions/TimeConvention", order = 1)]
    public class ScriptableTimeConvention : ScriptableConvention
    {
        [SerializeField] private UFloat _cycleTimeInSeconds;

        [SerializeField] private PassedTimeCounterFactory _timeCounterFactory;

        public override IConvention GetConvention()
        {
            return new TimerConvention(_cycleTimeInSeconds, _timeCounterFactory.GetPassedTimeCounter());
        }
    }
}
