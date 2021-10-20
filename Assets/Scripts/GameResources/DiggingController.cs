using DefaultNamespace;
using UnityEngine;

namespace GameResources
{
    public class DiggingController : MonoBehaviour
    {
        [SerializeField] private ResourcesGrid _grid;
        [SerializeField] private ResourcesController _resourcesController;

        private DiggingState _currentState;
        private ResourceItem _selectedResource;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cancel();
                return;
            }
            
            if (Input.GetMouseButtonDown(0) == false)
            {
                return;
            }

            Ray screenRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(screenRay, out RaycastHit hit) == false) return;
            
            switch (_currentState)
            {
                case DiggingState.None when TryGetResource(hit, out ResourceItem resource):
                case DiggingState.ResourceSelected when TryGetResource(hit, out resource):
                    SelectResource(resource);
                    break;
                case DiggingState.ResourceSelected when _selectedResource != null:
                    UseResource();
                    break;
                default:
                    Cancel();
                    break;
            }
        }

        private void Cancel()
        {
            _currentState = DiggingState.None;
            _selectedResource = null;
        }

        private void UseResource()
        {
            Resource resource = _selectedResource.Resource;
            if (_grid.TryDigging(_selectedResource) == false)
            {
                return;
            }
            
            //TODO determine the side and the place on board
            _resourcesController.OnResourceUsed(resource, BoardSide.City);
            Debug.Log($"Used resource {resource.Type}. Now there is {_resourcesController.GetResourcePoints(BoardSide.City)} resources");
        }

        private void SelectResource(ResourceItem resourceItem)
        {
            _selectedResource = resourceItem;
            Debug.Log($"Selected resource {resourceItem.Resource.Type}");
            _currentState = DiggingState.ResourceSelected;
        }

        private bool TryGetResource(RaycastHit hit, out ResourceItem item)
        {
            item = hit.collider.GetComponentInParent<ResourceItem>();
            return item != null && item.IsAvailableForDigging;
        }

        private enum DiggingState
        {
            None,
            ResourceSelected,
        }
    }
}