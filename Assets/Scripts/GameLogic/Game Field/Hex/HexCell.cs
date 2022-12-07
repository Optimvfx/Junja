namespace Game.GameLogic
{
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