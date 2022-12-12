using UnityEngine;

namespace Game.GameLogic
{
    [System.Serializable]
    public struct HexCell<T>
    {
        [SerializeField] private HexVectorInt _position;
        [SerializeField] private T _value;

        public HexVectorInt Position => _position;
        public T Value => _value;

        public HexCell(HexVectorInt position, T value)
        {
            _position = position;
            _value = value;
        }
    }
}