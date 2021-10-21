using GameResources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UI
{
    public class SelectedResourceUI : MonoBehaviour
    {
        [SerializeField] private GameObject _holder;
        [SerializeField] private TMP_Text _resourceNameText;
        [SerializeField] private TMP_Text _pointsValue;
        [SerializeField] private Image _cityImage;
        [SerializeField] private Image _natureImage;
        [SerializeField] private UiSettings _uiSettings;

        public void SetSelectedResource(Resource resource)
        {
            _holder.SetActive(resource != null);
            if (resource == null) return;
            
            _resourceNameText.text = resource.Type.ToString();
            _pointsValue.text = $"{(resource.CityPoints * _uiSettings.ScoreDisplayMultiplier).ToString()} POINTS";
            _cityImage.sprite = resource.CityPreview;
            _natureImage.sprite = resource.NaturePreview;
        }
    }
}