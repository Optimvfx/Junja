
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.GameLogic.Edit
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Tile/HexTiles/PlantTile")]
    public class PlantTile : Tile
    {
        [Range(0, 1000)]
        [SerializeField] private int _capacity;
        [Range(0, 1000)]
        [SerializeField] private int _stock;
        [Range(0, 1000)]
        [SerializeField] private int _growPerTick;

        private void OnValidate()
        {
            _capacity = Mathf.Max(_capacity, _stock);
        }

        public Plant GeneratePlant()
        {
            return new Plant(_capacity, _stock, _growPerTick);
        }
    }
}