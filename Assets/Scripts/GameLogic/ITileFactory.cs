using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.GameLogic
{
    public interface ITileFactory<T>
    {
        Tile GenerateTile(T arguments);
    }
}
