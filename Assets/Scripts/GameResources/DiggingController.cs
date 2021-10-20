using UnityEngine;

namespace GameResources
{
    public class DiggingController : MonoBehaviour
    {
        [SerializeField] private ResourcesGrid _grid;
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(0) == false)
            {
                return;
            }

            Ray screenRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(screenRay, out RaycastHit hit) == false) return;
            
            var resourceItem = hit.collider.GetComponentInParent<ResourceItem>();
            if (resourceItem == null)
            {
                return;
            }
            
            _grid.Dig(resourceItem);
        }
    }
}