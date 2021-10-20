using UnityEngine;

namespace GameResources
{
    [CreateAssetMenu(fileName = nameof(Resource), menuName = "Game/Resource")]
    public class Resource : ScriptableObject
    {
        [SerializeField] private ResourceType _type;
        [SerializeField] private Material _material;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private float _cityPoints;
        [SerializeField] private float _naturePoints;

        public ResourceType Type => _type;
        public Material Material => _material;
        public Sprite Sprite => _sprite;
        public float CityPoints => _cityPoints;
        public float NaturePoints => _naturePoints;
    }
}