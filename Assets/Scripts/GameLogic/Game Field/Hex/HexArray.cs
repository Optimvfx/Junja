using Game.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GameLogic
{
    [System.Serializable]
    public class HexArray<T> : IReadOnlyHexArray<T>, IClonable<HexArray<T>>
    {
        public const int AxisXArrayDimension = 0;
        public const int AxisYArrayDimension = 1;

        private IHexPositionToCellPositionConvertor _hexPositionToCellPositionConvertor;

        private T[][] _array;

        public int SizeR => GetSize().R;
        public int SizeS => GetSize().S;
        public int SizeQ => GetSize().Q;

        public int SizeX
        {
            get
            {
                return _array.Length;
            }
        }

        public int SizeY
        {
            get
            {
                if (SizeX == 0)
                    return 0;

                return _array[0].Length;
            }
        }

        public HexArray(Vector2Int size, IHexPositionToCellPositionConvertor hexPositionToCellPositionConvetor)
        {
            Init(hexPositionToCellPositionConvetor.CellToHex(size), hexPositionToCellPositionConvetor);
        }

        public HexArray(HexVectorInt size, IHexPositionToCellPositionConvertor hexPositionToCellPositionConvetor)
        {
            Init(size, hexPositionToCellPositionConvetor);
        }

        public HexArray(int r, int s, int q, IHexPositionToCellPositionConvertor hexPositionToCellPositionConvetor)
        {
            Init(new HexVectorInt(r, s, q), hexPositionToCellPositionConvetor);
        }

        public void Set(HexVectorInt index, T value)
        {
            if (!InBounds(index))
                throw GetOutOfBoundsException(index);

            var cellPosition = _hexPositionToCellPositionConvertor.HexToCell(index);

            _array[cellPosition.x][cellPosition.y] = value;
        }

        public T Get(HexVectorInt index)
        {
            if (!InBounds(index))
                throw GetOutOfBoundsException(index);

            var cellPosition = _hexPositionToCellPositionConvertor.HexToCell(index);

            return _array[cellPosition.x][cellPosition.y];
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
            for (int x = 0; x < SizeX; x++)
            {
                for (int y = 0; y < SizeY; y++)
                {
                    var hexPosition = _hexPositionToCellPositionConvertor.CellToHex(new Vector2Int(x, y));
                    var value = Get(hexPosition);

                    yield return new HexCell<T>(hexPosition, value);
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

        private void Init(HexVectorInt size, IHexPositionToCellPositionConvertor hexPositionToCellPositionConvetor)
        {
            _hexPositionToCellPositionConvertor = hexPositionToCellPositionConvetor;


            var vector2Size = _hexPositionToCellPositionConvertor.HexToCell(size);

            if (vector2Size.x < 0 || vector2Size.y < 0)
                throw new ArgumentException();


            _array = new T[vector2Size.x][];

            for(int x = 0; x < _array.Length; x++)
            {
                _array[x] = new T[vector2Size.y];
            }
        }
    }

    public interface IHexPositionToCellPositionConvertor
    {
        HexVectorInt CellToHex(Vector2Int cellPosition);

        Vector2Int HexToCell(HexVectorInt hexPosition);
    }

    public interface IReadOnlyHexArray<T>
    {
        public T Get(HexVectorInt index);

        HexVectorInt GetSize();

        public IEnumerable<HexCell<T>> GetAllCells();
    }
}