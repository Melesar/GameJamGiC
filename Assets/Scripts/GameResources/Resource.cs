using UnityEngine;

namespace GameResources
{
    [CreateAssetMenu(fileName = nameof(Resource), menuName = "Game/Resource")]
    public class Resource : ScriptableObject
    {
        [SerializeField] private ResourceType _type;
        [SerializeField] private Material _material;
        [SerializeField] private Sprite _sprite;

        public ResourceType Type => _type;
        public Material Material => _material;
        public Sprite Sprite => _sprite;
    }
}