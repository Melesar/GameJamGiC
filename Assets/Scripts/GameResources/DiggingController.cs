using System;
using DefaultNamespace;
using DefaultNamespace.UI;
using UnityEngine;

namespace GameResources
{
    public class DiggingController : MonoBehaviour
    {
        [SerializeField] private ResourcesGrid _grid;
        [SerializeField] private ResourcesController _resourcesController;
        [SerializeField] private BuildTileClick _buildTileClick;
        [SerializeField] private SelectedResourceUI _selectedResourceUI;
        
        private DiggingState _currentState;
        public ResourceItem _selectedResource;

        private void Start()
        {
            _selectedResourceUI.SetSelectedResource(null);
        }

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
                case DiggingState.ResourceSelected when _selectedResource != null && _buildTileClick.selectedTile !=null:
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
            _selectedResourceUI.SetSelectedResource(null);
        }

        private void UseResource()
        {
            Resource resource = _selectedResource.Resource;
            float tileXPosition = _buildTileClick.selectedTile.transform.position.x;
            if (!Input.GetMouseButtonDown(0))
            {
                return;
            }


            if (_grid.TryDigging(_selectedResource) == false)
            {
                return;
            }

           
            if(tileXPosition <0)
                _resourcesController.OnResourceUsed(resource, BoardSide.City, tileXPosition);
            else
                _resourcesController.OnResourceUsed(resource, BoardSide.Nature, tileXPosition);
            _selectedResourceUI.SetSelectedResource(null);
        }

        private void SelectResource(ResourceItem resourceItem)
        {
            _selectedResource = resourceItem;
            _currentState = DiggingState.ResourceSelected;
            _selectedResourceUI.SetSelectedResource(_selectedResource.Resource);
        }

        private bool TryGetResource(RaycastHit hit, out ResourceItem item)
        {
            item = hit.collider.GetComponentInParent<ResourceItem>();
            if (item == null)
            {
                return false;
            }

            if (item.IsAvailableForDigging == false)
            {
                item.Blink();
                return false;
            }

            return true;
        }

        private enum DiggingState
        {
            None,
            ResourceSelected,
        }
    }
}