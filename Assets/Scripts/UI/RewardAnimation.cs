using TMPro;
using UnityEngine;

namespace DefaultNamespace.UI
{
    public class RewardAnimation : MonoBehaviour
    {
        [SerializeField] private TMP_Text _numberText;
        [SerializeField] private float _animationDuration;

        private Camera _camera;

        public void SetPoints(float points)
        {
            _numberText.text = points.ToString();
        }

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Start()
        {
            Destroy(gameObject, _animationDuration);
        }

        private void Update()
        {
            transform.forward = _camera.transform.forward;
        }
    }
}