using Game.Gamplay;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.GameLogic
{
    public abstract class ScriptableTileFactory : ScriptableObject, ITileFactory<HexTilemapRenderer.GameFieldPosition>
    {
        public abstract Tile GenerateTile(HexTilemapRenderer.GameFieldPosition arguments);
    }
}