using System;
using UnityEngine;

namespace GameResources
{
    public class ResourcesGrid : MonoBehaviour
    {
        [SerializeField] private ResourceMap _map;
        [SerializeField] private ResourceItem _resourcePrefab;
        [SerializeField] private Transform _anchor;
        [SerializeField] private Vector3 _blockSize;

        private ResourceItem[,] _grid;

        public event Action ResourceDug;

        public bool TryDigging(ResourceItem item)
        {
            if (item.IsAvailableForDigging == false)
            {
                return false;
            }

            (int row, int column) = item.Position;
            for (int i = Mathf.Max(row - 1, 0); i <= Mathf.Min(row + 1, _map.Size.Item2 - 1); i++)
            {
                for (int j = Mathf.Max(column - 1, 0); j <= Mathf.Min(column + 1, _map.Size.Item1 - 1); j++)
                {
                    ResourceItem gridItem = _grid[i, j];
                    if (gridItem != null)
                    {
                        gridItem.IsAvailableForDigging = true;
                    }
                }
            }
            
            Destroy(item.gameObject);
            _grid[row, column] = null;
            ResourceDug?.Invoke();
            
            return true;
        }
        
        private void Start()
        {
            _grid = GenerateResources();
        }

        private ResourceItem[,] GenerateResources()
        {
            (int width, int height) = _map.Size;
            var grid = new ResourceItem[width, height];
            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    Resource resource = _map[row, column];
                    Vector3 position = _anchor.position + new Vector3(_blockSize.x * column, -_blockSize.y * row);
                    ResourceItem item = SpawnResource(resource, position);
                    item.IsAvailableForDigging = row == 0;
                    item.Position = (row, column);
                    grid[row, column] = item;
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
    }
    
}