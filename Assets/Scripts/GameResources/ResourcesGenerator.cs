using UnityEngine;

namespace GameResources
{
    public class ResourcesGenerator : MonoBehaviour
    {
        [SerializeField] private ResourceMap _map;
        [SerializeField] private ResourceItem _resourcePrefab;
        [SerializeField] private Transform _anchor;
        [SerializeField] private Vector3 _blockSize;
        [SerializeField] private int _depth;

        private void Start()
        {
            GenerateResources();
        }

        private void GenerateResources()
        {
            (int width, int height) = _map.Size;
            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    Resource resource = _map[row, column];
                    Vector3 position = _anchor.position + new Vector3(_blockSize.x * column, -_blockSize.y * row);
                    for (int i = 0; i < _depth; i++)
                    {
                        position += new Vector3(0, 0, _blockSize.z);
                        ResourceItem item = Instantiate(_resourcePrefab, position, Quaternion.identity);
                        item.InitResource(resource);
                        item.SetSize(_blockSize);
                    }
                }
            }
        }
    }
    
}