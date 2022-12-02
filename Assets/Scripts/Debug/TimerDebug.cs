using Game.Core;
using Game.BaseLogic;
using UnityEngine;

namespace Game.Debuging
{
    public class TimerDebug : MonoBehaviour
    {
        [SerializeField] private PassedTimeCounterFactory _timeCounterFactory;

        [SerializeField] private UFloat _cycleTime;

        private Timer _timer;

        private void Awake()
        {
            _timer = new Timer(_timeCounterFactory.GetPassedTimeCounter(), _cycleTime);
        }

        private void Update()
        {
            Debug.Log((int)_timer.CurrentTime);

            if (_timer.TryRestartTimer())
                Debug.Log("Restart");
        }
    }
}