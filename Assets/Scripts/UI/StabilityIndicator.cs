using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UI
{
    public class StabilityIndicator : MonoBehaviour
    {
        [SerializeField] private Image _cityIndicator;
        [SerializeField] private Image _natureIndicator;

        public void SetPoints(float cityPoints, float naturePoints)
        {
            float total = cityPoints + naturePoints;
            _cityIndicator.fillAmount = cityPoints / total;
            _natureIndicator.fillAmount = naturePoints / total;
        }
    }
}