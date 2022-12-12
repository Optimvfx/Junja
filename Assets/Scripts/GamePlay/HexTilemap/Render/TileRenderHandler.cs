using Game.GameLogic;
using UnityEngine;

namespace Game.Gamplay
{
    public abstract class TileRenderHandler : MonoBehaviour
    {
        public abstract void RenderTile(ReadOnlyGameField gameField, HexVectorInt position, HexVectorInt tileMapOffset);
    }
}