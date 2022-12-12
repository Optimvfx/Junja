using Game.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.GameLogic
{
    public class GameField : ReadOnlyGameField, IGameField
    {
        public GameField(HexArray<Plant> field) : base(field)
        {
        }

        public new bool TryMove(HexVectorInt moveStartPosition, HexDirection.Direction moveDirection)
        {
            return base.TryMove(moveStartPosition, moveDirection);
        }

        public void ApplayTick()
        {
            ApplayTick(1);
        }

        public new void ApplayTick(int value)
        {
            base.ApplayTick(value);
        }
    }

    public class ReadOnlyGameField : IReadOnlyHexArray<ReadOnlyPlant>
    {
        private readonly HexArray<Plant> _field;

        public int SizeR => _field.SizeR;

        public int SizeS => _field.SizeS;

        public int SizeQ => _field.SizeQ;

        public ReadOnlyGameField(HexArray<Plant> field)
        {
            _field = field.Clone();
        }

        public ReadOnlyPlant Get(HexVectorInt index)
        {
            return _field.Get(index);
        }

        public HexVectorInt GetSize()
        {
            return _field.GetSize();
        }

        public IEnumerable<HexCell<ReadOnlyPlant>> GetAllCells()
        {
            return _field.GetAllCells().Select(hexCell => new HexCell<ReadOnlyPlant>(hexCell.Position, hexCell.Value));
        }

        public IEnumerator<HexVectorInt> GetPossibleMoves(HexVectorInt position)
        {
            foreach (var direction in HexDirection.GetAllPossibleDirections())
            {
                if (IsMovePosible(position, direction))
                    yield return HexDirection.ConvertDirectionToVector(direction);
            }
        }

        public bool IsMovePosible(HexVectorInt startPosition, HexDirection.Direction direction)
        {
            var nextPosition = HexDirection.ConvertDirectionToVector(direction) + startPosition;

            if (_field.InBounds(nextPosition) == false)
                return false;

            return _field.Get(nextPosition) != null;
        }

        protected bool TryMove(HexVectorInt moveStartPosition, HexDirection.Direction moveDirection)
        {
            HexVectorInt atackablePosition = moveStartPosition + HexDirection.ConvertDirectionToVector(moveDirection);

            if (_field.InBounds(moveStartPosition) == false || _field.InBounds(atackablePosition) == false)
                return false;

            Plant attackerPlant = _field.Get(moveStartPosition);
            Plant attackkablePlant = _field.Get(atackablePosition);

            var unitsToAttack = Math.Min(attackerPlant.Stock, attackkablePlant.Stock);

            attackerPlant.TryTake(unitsToAttack);
            attackkablePlant.TryTake(unitsToAttack);

            return true;
        }

        protected void ApplayTick(int value)
        {
            if (value < 0)
                throw new ArgumentException("Unposible negative tick!");

            foreach(var cell in _field.GetAllCells())
            {
                cell.Value.ApplayTick(value);
            }
        }
    }
}