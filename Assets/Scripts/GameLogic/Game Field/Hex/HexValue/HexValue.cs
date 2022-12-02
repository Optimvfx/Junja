using Game.Core;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameLogic
{
    public class HexValue<T> : IReadOnlyHexValue<T>, IClonable<HexValue<T>>
    {
        private readonly Dictionary<HexDirection.Direction, T> _directionToValue;

        public HexValue(T positiveR, T positiveS, T positiveQ, T negativeR, T negativeS, T negativeQ)
        {
            _directionToValue = GenerateDirectionToValue(positiveR, positiveS, positiveQ, negativeR, negativeS, negativeQ);
        }

        public T GetValue(HexDirection.Direction direction)
        {
            return _directionToValue[direction];
        }

        public void SetValue(HexDirection.Direction direction, T value)
        {
            _directionToValue[direction] = value;
        }

        public HexValue<T> Clone()
        {
            return new HexValue<T>(
                 GetValue(HexDirection.Direction.PositiveR),
                 GetValue(HexDirection.Direction.PositiveS),
                 GetValue(HexDirection.Direction.PositiveQ),
                 GetValue(HexDirection.Direction.NegativeR),
                 GetValue(HexDirection.Direction.NegativeS),
                 GetValue(HexDirection.Direction.NegativeQ)
                );
        }

        private Dictionary<HexDirection.Direction, T> GenerateDirectionToValue(T positiveR, T positiveS, T positiveQ, T negativeR, T negativeS, T negativeQ)
        {
            var directionToValue = new Dictionary<HexDirection.Direction, T>();

            directionToValue[HexDirection.Direction.PositiveR] = positiveR;
            directionToValue[HexDirection.Direction.PositiveS] = positiveS;
            directionToValue[HexDirection.Direction.PositiveQ] = positiveQ;

            directionToValue[HexDirection.Direction.NegativeR] = negativeR;
            directionToValue[HexDirection.Direction.NegativeS] = negativeS;
            directionToValue[HexDirection.Direction.NegativeQ] = negativeQ;

            return directionToValue;
        }
    }

    public interface IReadOnlyHexValue<T>
    {
        public T GetValue(HexDirection.Direction direction);
    }
}