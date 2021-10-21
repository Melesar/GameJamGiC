using UnityEngine;

namespace GameResources
{
    [CreateAssetMenu(fileName = nameof(ResourceMap), menuName = "Game/Resource map")]
    public class ResourceMap : ScriptableObject
    {
        [SerializeField] private int _width;
        [SerializeField] private int _height;
        [SerializeField] private Resource[] _resources;

        public (int, int) Size => (_width, _height);

        public Resource this[int row, int column] => _resources[row * _width + column];
    }
}