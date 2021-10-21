using System.Collections;
using UnityEngine;

namespace GameResources
{
    public class ResourceItem : MonoBehaviour
    {
        [SerializeField] private Transform _rig;
        [SerializeField] private Renderer _renderer;
        [SerializeField][ColorUsage(false, true)] private Color _highlightColor;
        
        public Resource Resource { get; private set; }
        
        public bool IsAvailableForDigging { get; set; }
        public (int, int) Position { get; set; }

        private Coroutine _blinking;
        
        public void InitResource(Resource resource)
        {
            _renderer.material = resource.Material;
            Resource = resource;
        }

        public void SetSize(Vector3 size)
        {
            _rig.localScale = size;
        }

        public void SetHighlight(bool isEnabled)
        {
            _renderer.material.SetColor("_EmissionColor", isEnabled ? _highlightColor : Color.black);
        }

        public void Blink()
        {
            if (_blinking != null)
            {
                StopCoroutine(_blinking);
                _blinking = null;
            }

            _blinking = StartCoroutine(BlinkCoroutine());
        }

        private IEnumerator BlinkCoroutine()
        {
            Color originalColor = _renderer.material.color;
            
            const float blinkTime = 0.3f;
            for (int i = 0; i < 3; i++)
            {
                float time = 0f;
                while (time < blinkTime)
                {
                    _renderer.material.color = Color.Lerp(originalColor, Color.red, time);
                    yield return null;
                    time += Time.deltaTime;
                }
                
                while (time > 0)
                {
                    _renderer.material.color = Color.Lerp(originalColor, Color.red, time);
                    yield return null;
                    time -= Time.deltaTime;
                }
            }

            _blinking = null;
        }
    }
}