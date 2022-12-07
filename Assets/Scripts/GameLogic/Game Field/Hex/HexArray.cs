using Game.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameLogic
{
    public class HexArray<T> : IReadOnlyHexArray<T>, IClonable<HexArray<T>>
    {
        public const int AxisXArrayDimension = 0;
        public const int AxisYArrayDimension = 1;

        private readonly IHexPositionToCellPositionConvertor _hexPositionToCellPositionConvertor;

        private T[,] _array;

        public int SizeR => GetSize().R;
        public int SizeS => GetSize().S;
        public int SizeQ => GetSize().Q;

        public int SizeX => _array.GetLength(AxisXArrayDimension);
        public int SizeY => _array.GetLength(AxisYArrayDimension);
      

        public HexArray(HexVectorInt size, IHexPositionToCellPositionConvertor hexPositionToCellPositionConvetor)
        {
            _hexPositionToCellPositionConvertor = hexPositionToCellPositionConvetor;

            Init(size);
        }

        public HexArray(int r, int s, int q, IHexPositionToCellPositionConvertor hexPositionToCellPositionConvetor)
        {
            _hexPositionToCellPositionConvertor = hexPositionToCellPositionConvetor;

            Init(new HexVectorInt(r, s, q));
        }

        public void Set(HexVectorInt index, T value)
        {
            if (!InBounds(index))
                throw GetOutOfBoundsException(index);

            var cellPosition = _hexPositionToCellPositionConvertor.HexToCell(index);

            _array[cellPosition.x, cellPosition.y] = value;
        }

        public T Get(HexVectorInt index)
        {
            if (!InBounds(index))
                throw GetOutOfBoundsException(index);

            var cellPosition = _hexPositionToCellPositionConvertor.HexToCell(index);

            return _array[cellPosition.x, cellPosition.y];
        }

        public bool InBounds(int r, int s, int q)
        {
            return InBounds(new HexVectorInt(r, s, q));
        }

        public bool InBounds(HexVectorInt vector)
        {
            var cellPosition = _hexPositionToCellPositionConvertor.HexToCell(vector);

            return cellPosition.x >= 0 && cellPosition.y >= 0 && cellPosition.x < SizeX && cellPosition.y < SizeY;
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
            var clone = new HexArray<T>(SizeR, SizeS, SizeQ, _hexPositionToCellPositionConvertor);

            foreach(var cell in GetAllCells())
            {
                clone.Set(cell.Position, cell.Value);
            }

            return clone;
        }

        public HexVectorInt GetSize()
        {
            return _hexPositionToCellPositionConvertor.CellToHex(new Vector2Int(SizeX, SizeY));
        }

        private Exception GetOutOfBoundsException(HexVectorInt vector)
        {
            var cellIndex = _hexPositionToCellPositionConvertor.HexToCell(vector);

            return new IndexOutOfRangeException($"R: {vector.R}, S: {vector.S}, Q: {vector.Q}. Vector: {cellIndex}  \n  Size: R: {SizeR}, S: {SizeS}, Q: {SizeQ} X: {SizeX} Y: {SizeY}.");
        }

        private void Init(HexVectorInt size)
        {
            var vector2Size = _hexPositionToCellPositionConvertor.HexToCell(size);

            if (vector2Size.x < 0 || vector2Size.y < 0)
                throw new ArgumentException();

            _array = new T[vector2Size.x, vector2Size.y];
        }
    }

    public interface IHexPositionToCellPositionConvertor
    {
        HexVectorInt CellToHex(Vector2Int cellPosition);

        Vector2Int HexToCell(HexVectorInt hexPosition);
    }

    public interface IReadOnlyHexArray<T>
    {
        HexVectorInt GetSize();

        public T Get(HexVectorInt index);
    }
}