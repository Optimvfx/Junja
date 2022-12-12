using Game.Gamplay;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.GameLogic
{
    [CreateAssetMenu(fileName = "SingleTileSellector", menuName = "ScriptableObjects/TileSellector/SingleTileSellector", order = 1)]
    public class SingleTileSellector : ScriptableTileFactory
    {
        [SerializeField] private Tile _standart;
        [SerializeField] private Tile _emptyTile;

        public override Tile GenerateTile(HexTilemapRenderer.GameFieldPosition arguments)
        {
            if (arguments.GameField.Get(arguments.Position) == null)
                return _emptyTile;

            return _standart;
        }
    }
}