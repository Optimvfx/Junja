using Game.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameLogic
{
    public class HexArray<T> : IReadOnlyHexArray<T>, IClonable<HexArray<T>>
    {
        public const int AxisRArrayDimension = 0;
        public const int AxisSArrayDimension = 1;
        public const int AxisQArrayDimension = 2;

        private T[,,] _array;

        public int SizeR => _array.GetLength(AxisRArrayDimension);
        public int SizeS => _array.GetLength(AxisSArrayDimension);
        public int SizeQ => _array.GetLength(AxisQArrayDimension);

        public HexArray(HexVectorInt size)
        {
            if (size.R < 0 || size.S < 0 || size.Q < 0)
                throw new ArgumentException();

            _array = new T[size.R, size.S, size.Q];
        }

        public HexArray(int r, int s, int q)
        {
            if (r < 0 || s < 0 || q < 0)
                throw new ArgumentException();

            _array = new T[r, s, q];
        }

        public void Set(HexVectorInt index, T value)
        {
            if (!InBounds(index.R, index.S, index.Q))
                throw GetOutOfBoundsException(index.R, index.S, index.Q);

            _array[index.R, index.S, index.Q] = value;
        }

        public T Get(HexVectorInt index)
        {
            if (!InBounds(index.R, index.S, index.Q))
                throw GetOutOfBoundsException(index.R, index.S, index.Q);

            return _array[index.R, index.S, index.Q];
        }

        public bool InBounds(HexVectorInt vector)
        {
            return InBounds(vector.R, vector.S, vector.Q);
        }

        public bool InBounds(int r, int s, int q)
        {
            return MathExtenstions.InBounds(r, 0, SizeR)
                   && MathExtenstions.InBounds(s, 0, SizeS)
                   && MathExtenstions.InBounds(q, 0, SizeQ);
        }

        public IEnumerable<HexCell<T>> GetAllCells()
        {
            for (int r = 0; r < SizeR; r++)
            {
                for (int s = 0; s < SizeS; s++)
                {
                    for (int q = 0; q < SizeQ; q++)
                    {
                        HexVectorInt cellPosition = new HexVectorInt(r, s, q);

                        yield return new HexCell<T>(cellPosition, Get(cellPosition));
                    }
                }
            }
        }

        public HexArray<T> Clone()
        {
            var clone = new HexArray<T>(SizeR, SizeS, SizeQ);

            foreach(var cell in GetAllCells())
            {
                clone.Set(cell.Position, cell.Value);
            }

            return clone;
        }

        private Exception GetOutOfBoundsException(int r, int s, int q)
        {
            return new IndexOutOfRangeException($"R: {r}, S: {s}, Q: {q}. \n  Size: R: {SizeR}, S: {SizeS}, Q: {SizeQ}.");
        }

    }

    public interface IReadOnlyHexArray<T>
    {
        public int SizeR { get; }
        public int SizeS { get; }
        public int SizeQ { get; }

        public T Get(HexVectorInt index);
    }

    public struct HexCell<T>
    {
        public readonly HexVectorInt Position;
        public readonly T Value;

        public HexCell(HexVectorInt position, T value)
        {
            Position = position;
            Value = value;
        }
    }
}