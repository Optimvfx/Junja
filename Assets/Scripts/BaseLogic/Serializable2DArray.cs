using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.BaseLogic
{
    [Serializable]
    public class Serializable2DArray<T> : ISerializationCallbackReceiver
    {
        [SerializeField] private T[,] Value;
        [SerializeField] private int xLength, yLength;
        [SerializeField] private T[] serializedValue;

        public int Length => Value.Length;
        public int XLength => xLength;
        public int YLength => yLength;

        public Serializable2DArray(int xLength, int yLength, T defaultValue = default)
        {
            this.xLength = xLength;
            this.yLength = yLength;
            Value = new T[xLength, yLength];

            T[] values = GetValues();

            for (int i = 0; i < values.Length; i++)
            {
                values[i] = defaultValue;
            }

            Copy(values);

            OnBeforeSerialize();
        }

        public T Get(int xDimension, int yDimension)
        {
            ValidateDimensionIndexes(xDimension, yDimension);
            return Value[xDimension, yDimension];
        }

        public void Set(int xDimension, int yDimension, T value)
        {
            ValidateDimensionIndexes(xDimension, yDimension);
            Value[xDimension, yDimension] = value;
        }

        public void Copy(T[] reference)
        {
            if (reference == null)
            {
                throw new ArgumentNullException();
            }

            if (Length > reference.Length)
            {
                throw new ArgumentException();
            }

            for (int x = 0; x < xLength; x++)
            {
                for (int y = 0; y < yLength; y++)
                {
                    int refIndex = x * yLength + y;
                    Value[x, y] = reference[refIndex];
                }
            }
        }

        public T[] GetValues(int index)
        {
            index = Mathf.Clamp(index, 0, xLength);
            T[] result = new T[yLength];

            for (int y = 0; y < yLength; y++)
            {
                result[y] = Value[index, y];
            }

            return result;
        }

        public T[] GetValues()
        {
            List<T> result = new List<T>();

            for (int x = 0; x < xLength; x++)
            {
                result.AddRange(
                    GetValues(x)
                );
            }

            return result.ToArray();
        }

        public bool Contains(T value)
            => GetValues().Contains(value);

        public void Clear()
        {
            xLength = 0;
            yLength = 0;
            Value = new T[xLength, yLength];

            OnBeforeSerialize();
        }

        private void ValidateDimensionIndexes(int xDimension, int yDimension)
        {
            if (xDimension < 0 || yDimension < 0)
                throw new ArgumentException("[Serializable2DArray] Get: dimensions cannot be negative");

            if (xDimension > XLength || yDimension > YLength)
                throw new ArgumentException("[Serializable2DArray] Get: dimensions out of bounds");
        }

        #region Serialization

        public void OnBeforeSerialize()
        {
            serializedValue = GetValues();
        }

        public void OnAfterDeserialize()
        {
            Value = new T[xLength, yLength];

            if (serializedValue == null) return;

            Copy(serializedValue);
        }

        #endregion

    }
}