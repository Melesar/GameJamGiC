using TMPro;
using UnityEngine;

namespace DefaultNamespace.UI
{
    public class FinalPopulationDisplay : MonoBehaviour
    {
        [SerializeField] private UiSettings _uiSettings;
        [SerializeField] private TMP_Text _populationText;

        public void SetFinalPopulation(float population)
        {
            _populationText.text = (population * _uiSettings.ScoreDisplayMultiplier).ToString();
        }
    }
}