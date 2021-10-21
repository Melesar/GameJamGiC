using System;
using DefaultNamespace;
using UnityEngine;

namespace GameResources
{
    public class ResourcesGrid : MonoBehaviour
    {
        [SerializeField] private ResourceMap _map;
        [SerializeField] private ResourceItem _resourcePrefab;
        [SerializeField] private Transform _anchor;
        [SerializeField] private Vector3 _blockSize;
        [SerializeField] private GameManager _gameManager;

        private ResourceItem[] _grid;

        public bool TryDigging(ResourceItem item)
        {
            if (item.IsAvailableForDigging == false)
            {
                return false;
            }

            (int x, int y) = item.Position;
            (int width, int height) = _map.Size;
            for (int row = Mathf.Max(y - 1, 0); row <= Mathf.Min(y + 1, height - 1); row++)
            {
                for (int column = Mathf.Max(x - 1, 0); column <= Mathf.Min(x + 1, width - 1); column++)
                {
                    ResourceItem gridItem = _grid[row * width + column];
                    if (gridItem != null)
                    {
                        gridItem.IsAvailableForDigging = true;
                    }
                }
            }
            
            Destroy(item.gameObject);
            _grid[y * width + x] = null;
            
            return true;
        }

        private void Awake()
        {
            _gameManager.GameRestarts += OnGameRestarts;
        }

        private void Start()
        {
            _grid = GenerateResources();
        }

        private void OnDestroy()
        {
            _gameManager.GameRestarts -= OnGameRestarts;
        }

        private ResourceItem[] GenerateResources()
        {
            (int width, int height) = _map.Size;
            var grid = new ResourceItem[width * height];
            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    Resource resource = _map[row, column];
                    Vector3 position = _anchor.position + new Vector3(_blockSize.x * column, -_blockSize.y * row);
                    ResourceItem item = SpawnResource(resource, position);
                    item.IsAvailableForDigging = row == 0;
                    item.Position = (column, row);
                    grid[row * width + column] = item;
                }
            }

            return grid;
        }

        private ResourceItem SpawnResource(Resource resource, Vector3 position)
        {
            ResourceItem item = Instantiate(_resourcePrefab, position, Quaternion.identity);
            item.InitResource(resource);
            item.SetSize(_blockSize);
            return item;
        }

        private void OnGameRestarts()
        {
            foreach (ResourceItem resourceItem in _grid)
            {
                Destroy(resourceItem.gameObject);
            }

            _grid = GenerateResources();
        }
    }
}