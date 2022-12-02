using Game.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameLogic
{
    public class GameField : ReadOnlyGameField, ITickHandler<int>, ITickHandler
    {
        public GameField(HexArray<Plant> field) : base(field)
        {
        }

        public new bool TryAttack(HexVectorInt attackerPosition, HexDirection.Direction attackDirection)
        {
            return base.TryAttack(attackerPosition, attackDirection);
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

        public ReadOnlyPlant Get(HexVectorInt index)
        {
            return _field.Get(index);
        }

        public ReadOnlyGameField(HexArray<Plant> field)
        {
            _field = field.Clone();

            RecalculatePosibleMoves();
        }

        protected bool TryAttack(HexVectorInt attackerPosition, HexDirection.Direction attackDirection)
        {
            HexVectorInt atackablePosition = attackerPosition + HexDirection.ConvertDirectionToVector(attackDirection);

            if (_field.InBounds(attackerPosition) == false || _field.InBounds(atackablePosition) == false)
                return false;

            Plant attackerPlant = _field.Get(attackerPosition);
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

        private void RecalculatePosibleMoves()
        {
            foreach(var cell in _field.GetAllCells())
            {
                RecalculatePosiblePlantMoves(cell);
            }
        }

        private IEnumerator<HexVectorInt> GetPossibleMoves(ReadOnlyPlant plant)
        {
            foreach (var direction in HexDirection.GetAllPossibleDirections())
            {
                if (plant.MovePossibe.GetValue(direction))
                    yield return HexDirection.ConvertDirectionToVector(direction);
            }
        }

        private void RecalculatePosiblePlantMoves(HexCell<Plant> cell)
        {
            var movePosible = _field.Get(cell.Position).MovePossibe;
            var plant = cell.Value;

            foreach (var direction in HexDirection.GetAllPossibleDirections())
            {
                if (movePosible.GetValue(direction))
                {
                    HexVectorInt conectedCellPosition = cell.Position + HexDirection.ConvertDirectionToVector(direction);

                    if (_field.InBounds(conectedCellPosition))
                    {
                        ConnectOtherPlant(cell.Position, direction);
                    }
                    else
                    {
                        DisconnectPlant(plant, direction);
                    }
                }
            }
        }

        private void ConnectOtherPlant(HexVectorInt cellPosition, HexDirection.Direction direction)
        {
            HexVectorInt conectedCellPosition = cellPosition + HexDirection.ConvertDirectionToVector(direction);

            _field.Get(conectedCellPosition).SetMovePosible(HexDirection.Flip(direction), true);
        }

        private void DisconnectPlant(Plant plant, HexDirection.Direction direction)
        {
            plant.SetMovePosible(direction, false);
        }
    }
}