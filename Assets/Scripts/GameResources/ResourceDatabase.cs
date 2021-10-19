using System.Collections.Generic;
using UnityEngine;

namespace GameResources
{
    [CreateAssetMenu(fileName = nameof(ResourceDatabase), menuName = "Game/Resource database")]
    public class ResourceDatabase : ScriptableObject
    {
        [SerializeField] private List<Resource> _resources;

        public IReadOnlyList<Resource> Resources => _resources;

        public Resource GetResourceByType(ResourceType type)
        {
            return _resources.Find(r => r.Type == type);
        }
    }
}