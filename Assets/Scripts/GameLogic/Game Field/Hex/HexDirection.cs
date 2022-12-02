using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.GameLogic
{
    public static class HexDirection
    {
        private static readonly IReadOnlyDictionary<Direction, Direction> _directionFlipConvertor = GenerateDirectionFlipConvetor();
        private static readonly IReadOnlyDictionary<Direction, HexVectorInt> _directionToVectorConvertor = GenerateDirectionToVectorConvetor();

        public static Direction Flip(Direction direction)
        {
            return _directionFlipConvertor[direction];
        }

        public static HexVectorInt ConvertDirectionToVector(Direction direction)
        {
            return _directionToVectorConvertor[direction];
        }

        public static IEnumerable<Direction> MultiDirectionToDirection(MultiDirection multiDirection)
        {
            if (multiDirection.HasFlag(MultiDirection.PositiveR))
                yield return Direction.PositiveR;

            if (multiDirection.HasFlag(MultiDirection.PositiveS))
                yield return Direction.PositiveS;

            if (multiDirection.HasFlag(MultiDirection.PositiveQ))
                yield return Direction.PositiveQ;

            if (multiDirection.HasFlag(MultiDirection.NegativeR))
                yield return Direction.NegativeR;

            if (multiDirection.HasFlag(MultiDirection.NegativeS))
                yield return Direction.NegativeS;

            if (multiDirection.HasFlag(MultiDirection.NegativeQ))
                yield return Direction.NegativeQ;
        }

        public static HexValue<bool> MultiDirectionToMovePosible(MultiDirection multiDirection)
        {
            var posiblePositiveR = multiDirection.HasFlag(MultiDirection.PositiveR);
            var posiblePositiveS = multiDirection.HasFlag(MultiDirection.PositiveS);
            var posiblePositiveQ = multiDirection.HasFlag(MultiDirection.PositiveQ);

            var posibleNegativeR = multiDirection.HasFlag(MultiDirection.NegativeR);
            var posibleNegativeS = multiDirection.HasFlag(MultiDirection.NegativeS);
            var posibleNegativeQ = multiDirection.HasFlag(MultiDirection.NegativeQ);

            return new HexValue<bool>(posiblePositiveR, posiblePositiveS, posiblePositiveQ, posibleNegativeR, posibleNegativeS, posibleNegativeQ);
        }

        public static IEnumerable<Direction> GetAllPossibleDirections()
        {
            return Enum.GetValues(typeof(Direction)).Cast<Direction>();
        }

        private static IReadOnlyDictionary<Direction, Direction> GenerateDirectionFlipConvetor()
        {
            var directionFlipConvertor = new Dictionary<Direction, Direction>();

            foreach(var direction in GetAllPossibleDirections())
            {
                directionFlipConvertor[direction] = (Direction)(-(int)direction);
            }

            return directionFlipConvertor;
        }

        private static IReadOnlyDictionary<Direction, HexVectorInt> GenerateDirectionToVectorConvetor()
        {
            var directionToVectorConvertor = new Dictionary<Direction, HexVectorInt>();

            directionToVectorConvertor[Direction.PositiveR] = HexVectorInt.PositiveR;
            directionToVectorConvertor[Direction.PositiveS] = HexVectorInt.PositiveS;
            directionToVectorConvertor[Direction.PositiveQ] = HexVectorInt.PositiveQ;

            directionToVectorConvertor[Direction.NegativeR] = HexVectorInt.NegativeR;
            directionToVectorConvertor[Direction.NegativeS] = HexVectorInt.NegativeS;
            directionToVectorConvertor[Direction.NegativeQ] = HexVectorInt.NegativeQ;

            return directionToVectorConvertor;
        }

        public enum Direction 
        {
            PositiveR = 1,
            PositiveS = 2,
            PositiveQ = 3,
            NegativeR = -1,
            NegativeS = -2,
            NegativeQ = -3
        }
        
        [Flags] public enum MultiDirection
        {
            PositiveR = 1,
            PositiveS = 2,
            PositiveQ = 4,
            NegativeR = 8,
            NegativeS = 16,
            NegativeQ = 32
        }
    }
}