using Game.GameLogic;
using TMPro;
using UnityEngine;

namespace Game.Gamplay
{
    [RequireComponent(typeof(TMP_Text))]
    public class PlantInfoRenderer : MonoBehaviour
    {
        private TMP_Text _renderText;

        public void Render(ReadOnlyPlant renderingPlant)
        {
            var plantInfo = GeneratePlantInfo(renderingPlant);

            GetRendererText().text = plantInfo;
        }

        private string GeneratePlantInfo(ReadOnlyPlant plant)
        {
            var capacity = plant.Capacity;
            var stock = plant.Stock;

            return $"{stock}";
        }

        private TMP_Text GetRendererText()
        {
            if (_renderText == null)
                _renderText = GetComponent<TMP_Text>();

            return _renderText;
        }
    }
}