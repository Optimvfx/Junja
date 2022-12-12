using Game.Core;
using Game.GameLogic.Edit;
using UnityEngine;

namespace Game.Gamplay
{
    public class GameIniter : MonoBehaviour
    {
        [SerializeField] private HexTilemapGameplay _hexTilemapGameplay;
        [SerializeField] protected HexTilemapRenderer _hexTilemapRenderer;
        [SerializeField] private HexTilemapChangmentRenderer _hexTilemapChangmentRenderer;

        protected void Init(GameLvlInfo lvlInfo)
        {
            _hexTilemapChangmentRenderer.Init(_hexTilemapGameplay, _hexTilemapRenderer);

            _hexTilemapRenderer.Init(lvlInfo.OffsetToTilemap);

            _hexTilemapGameplay.Init(lvlInfo);
        }
    }
}