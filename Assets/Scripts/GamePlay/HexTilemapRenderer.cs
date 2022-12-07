using Game.GameLogic;
using UnityEngine;

namespace Game.Gamplay
{
    public class HexTilemapRenderer : MonoBehaviour
    {
        [SerializeField] HexTilemap _cellTileMap;

        public void Render(ReadOnlyGameField gameField, Vector2Int offset)
        {
            ClearMaps();

            
        }

        private void RenderByTileFactory(HexTilemap renderTarget, ITileFactory<GameFieldPosition> tileFactory)
        {

        }

        private void ClearMaps()
        {
            _cellTileMap.Clear();
        }

        public class GameFieldPosition
        {
            public readonly ReadOnlyGameField GameField;

            public readonly HexVectorInt Position;

            public GameFieldPosition(ReadOnlyGameField gameField, HexVectorInt position)
            {
                GameField = gameField;
                Position = position;
            }
        }
    }
}