using TMPro;
using UnityEngine;

namespace DefaultNamespace.UI
{
    public class CityPointsUI : MonoBehaviour
    {
        [SerializeField] private int _displayScoreMultiplier;
        [SerializeField] private TMP_Text _pointsText;

        public void SetPoints(float points)
        {
            _pointsText.text = Mathf.RoundToInt(points * _displayScoreMultiplier).ToString();
        }
    }
}