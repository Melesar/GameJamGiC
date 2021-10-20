using System;
using GameResources;
using TMPro;
using UnityEngine;

namespace DefaultNamespace.UI
{
    public class SelectedResourceUI : MonoBehaviour
    {
        [SerializeField] private GameObject _holder;
        [SerializeField] private TMP_Text _resourceNameText;
        [SerializeField] private TMP_Text _pointsValue;

        public void SetSelectedResource(Resource resource)
        {
            _holder.SetActive(resource != null);
            if (resource != null)
            {
                _resourceNameText.text = resource.Type.ToString();
                _pointsValue.text = $"{resource.CityPoints.ToString()} POINTS";
            }
        }
    }
}