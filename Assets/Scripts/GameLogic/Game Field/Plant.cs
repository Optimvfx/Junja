using Game.Core;
using UnityEngine;
using System;

namespace Game.GameLogic
{
    public class Plant : ReadOnlyPlant, ITickHandler<int>, ITickHandler
    {
        public Plant(int capacity, int stock, int growPerTick) : base(capacity, stock, growPerTick)
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
        public int Capacity { get; private set; }
        public int Stock { get; private set; }

        public int GrowPerTick { get; private set; }

        public ReadOnlyPlant(int capacity, int stock, int growPerTick)
        {
            if (capacity < 0 || stock < 0 || growPerTick < 0)
                throw new ArgumentException();

            if (capacity < stock)
                throw new ArgumentException();

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
    }
}
