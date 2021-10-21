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
        [SerializeField] private Sprite _cityPreview;
        [SerializeField] private Sprite _naturePreview;

        public ResourceType Type => _type;
        public Material Material => _material;
        public Sprite Sprite => _sprite;
        public float CityPoints => _cityPoints;
        public float NaturePoints => _naturePoints;
        public Sprite CityPreview => _cityPreview;
        public Sprite NaturePreview => _naturePreview;
    }
}