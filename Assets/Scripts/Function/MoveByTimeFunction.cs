using Game.BaseLogic;
using Game.Core;
using UnityEngine;

namespace Game.Functions
{
    public class MoveByTimeFunction : MonoBehaviour
    {
        [SerializeField] private PassedTimeCounterFactory _passedTimeCounterFactory;

        [Header("Move Config")]
        [SerializeField] private NormailzedVector3 _moveDirection;
        [SerializeField] private UFloat _moveSpeedBySecond;
        [SerializeField] private MoveUpdateType _moveType;
        [SerializeField] private bool _ignoreRotation;

        private IPassedTimeCounter _passedTimeCounter;

        private void Update()
        {
            if (_moveType == MoveUpdateType.Update)
                DoMove(GetPassedTimeCounter().PassedTime);
        }

        private void FixedUpdate()
        {
            if (_moveType == MoveUpdateType.FixedUpdate)
                DoMove(GetPassedTimeCounter().FixedPassedTime);
        }

        private void DoMove(UFloat passedTime)
        {
            var move = _moveDirection.Value * (_moveSpeedBySecond * passedTime);

            if (_ignoreRotation)
                transform.position += move;
            else
                transform.Translate(move);
        }

        private IPassedTimeCounter GetPassedTimeCounter()
        {
            if (_passedTimeCounter == null)
                _passedTimeCounter = _passedTimeCounterFactory.GetPassedTimeCounter();

            return _passedTimeCounter;
        }

        public enum MoveUpdateType
        {
            FixedUpdate,
            Update
        }
    }
}