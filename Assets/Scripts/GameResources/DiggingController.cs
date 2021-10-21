using DefaultNamespace;
using DefaultNamespace.UI;
using TMPro;
using UnityEngine;

namespace GameResources
{
    public class DiggingController : MonoBehaviour
    {
        [SerializeField] private ResourcesGrid _grid;
        [SerializeField] private ResourcesController _resourcesController;
        [SerializeField] private BuildTileClick _buildTileClick;
        [SerializeField] private SelectedResourceUI _selectedResourceUI;
        [SerializeField] private RewardAnimation _regularReward;
        [SerializeField] private RewardAnimation _shinyReward;
        [SerializeField] private float _shinyRewardThreshold;
        [SerializeField] private GameManager _gameManager;

        private bool _isGameRunning;
        private DiggingState _currentState;
        private ResourceItem _lastHighlightedResource;
        public ResourceItem _selectedResource;
        

        private void Awake()
        {
            _gameManager.GameStarted += () => _isGameRunning = true;
            _gameManager.GameEnded += () => _isGameRunning = false;
            _gameManager.GameRestarts += OnGameRestarts;
        }

        private void OnDestroy()
        {
            _gameManager.GameRestarts -= OnGameRestarts;
        }

        private void Start()
        {
            _selectedResourceUI.SetSelectedResource(null);
        }

        private void Update()
        {
            if (_isGameRunning == false)
            {
                return;
            }
            
            Ray screenRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(screenRay, out RaycastHit hit, 1000, ~0, QueryTriggerInteraction.Ignore) == false) return;
            if (TryGetResource(hit, out ResourceItem resourceItem) && resourceItem.IsAvailableForDigging)
            {
                if (_lastHighlightedResource != null)
                {
                    _lastHighlightedResource.SetHighlight(false);
                }

                resourceItem.SetHighlight(true);
                _lastHighlightedResource = resourceItem;
            }
            else if (_lastHighlightedResource != null)
            {
                _lastHighlightedResource.SetHighlight(false);
            }
            
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cancel();
                return;
            }
            
            if (Input.GetMouseButtonDown(0) == false)
            {
                return;
            }
            
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

            BoardSide boardSide = tileXPosition < 0 ? BoardSide.City : BoardSide.Nature;
            float receivedPoints = _resourcesController.OnResourceUsed(resource, boardSide, tileXPosition);
            if (boardSide == BoardSide.City)
            {
                RewardAnimation animationPrefab = receivedPoints >= _shinyRewardThreshold ? _shinyReward : _regularReward;
                RewardAnimation animationInstance = Instantiate(animationPrefab, _buildTileClick.selectedTile.transform.position, Quaternion.identity);
                animationInstance.SetPoints(receivedPoints);
            }
            
            _selectedResourceUI.SetSelectedResource(null);
        }

        private void SelectResource(ResourceItem resourceItem)
        {
            if (resourceItem.IsAvailableForDigging == false)
            {
                resourceItem.Blink();
                return;
            }
            
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
            return true;
        }

        private void OnGameRestarts()
        {
            Cancel();
            _lastHighlightedResource = null;
            _selectedResourceUI.SetSelectedResource(null);
            _isGameRunning = true;
        }

        private enum DiggingState
        {
            None,
            ResourceSelected,
        }
    }
}