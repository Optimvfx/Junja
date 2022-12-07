using Game.Core;
using UnityEngine;
using System;

namespace Game.GameLogic
{
    public class Plant : ReadOnlyPlant, ITickHandler<int>, ITickHandler
    {
        public Plant(int capacity, int stock, int growPerTick, HexValue<bool>? movePosible = null) : base(capacity, stock, growPerTick, movePosible)
        {
        }

        public new bool TryTake(int value)
        {
            return base.TryTake(value);
        }

        public new void Give(int value)
        {
            base.Give(value);
        }

        public void ApplayTick()
        {
            ApplayTick(1);
        }

        public new void SetMovePosible(HexDirection.Direction direction, bool posibleToMove)
        {
            base.SetMovePosible(direction, posibleToMove);
        }

        public void ApplayTick(int value)
        {
            if (value < 0)
                throw new ArgumentException("Unposible negative tick!");

            var grow = GrowPerTick * value;

            Give(grow);
        }
    }

    public class ReadOnlyPlant 
    {
        private HexValue<bool> _movePossible;

        public IReadOnlyHexValue<bool> MovePossibe => _movePossible;

        public int Capacity { get; private set; }
        public int Stock { get; private set; }

        public int GrowPerTick { get; private set; }

        public ReadOnlyPlant(int capacity, int stock, int growPerTick, HexValue<bool>? movePosible = null)
        {
            if (capacity < 0 || stock < 0 || growPerTick < 0)
                throw new ArgumentException();

            if (capacity < stock)
                throw new ArgumentException();

            if (movePosible == null)
                movePosible = new HexValue<bool>(false, false, false, false, false, false);

            _movePossible = movePosible.Clone();

            Capacity = capacity;
            Stock = stock;
            GrowPerTick = growPerTick;
        }

        protected bool TryTake(int value)
        {
            if (value < 0)
                throw new ArgumentException();

            var takeIsPosible = Stock >= value;

            if(takeIsPosible)
            {
                Stock -= value;
            }

            return takeIsPosible;
        }

        protected void Give(int value)
        {
            if (value < 0)
                throw new ArgumentException();

            Stock = Mathf.Min(Stock + value, Capacity);
        }

        protected void SetMovePosible(HexDirection.Direction direction, bool posibleToMove)
        {
            _movePossible.SetValue(direction, posibleToMove);
        }
    }
}
