using UnityEngine;

namespace GameResources
{
    public class ResourceItem : MonoBehaviour
    {
        [SerializeField] private Transform _rig;
        [SerializeField] private Renderer _renderer;
        
        public Resource Resource { get; private set; }
        
        public bool IsAvailableForDigging { get; set; }
        public (int, int) Position { get; set; }
        
        public void InitResource(Resource resource)
        {
            _renderer.material = resource.Material;
            Resource = resource;
        }

        public void SetSize(Vector3 size)
        {
            _rig.localScale = size;
        }
    }
}