using UnityEngine;

namespace DefaultNamespace
{
    public class WaterRenderQueue : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _renderer;

        private void Start()
        {
            _renderer.material.renderQueue -= 1;
        }
    }
}