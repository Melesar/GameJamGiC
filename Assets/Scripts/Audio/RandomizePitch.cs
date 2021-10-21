using UnityEngine;

namespace DefaultNamespace.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class RandomizePitch : MonoBehaviour
    {
        [SerializeField] private Vector2 _pitchRange;
        
        private AudioSource _source;

        private void Awake()
        {
            _source = gameObject.GetComponent<AudioSource>();
        }

        private void Start()
        {
            _source.pitch = Mathf.Clamp(Random.Range(_pitchRange.x, _pitchRange.y), -3, 3);
        }
    }
}