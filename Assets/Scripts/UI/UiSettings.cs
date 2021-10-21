using UnityEngine;

namespace DefaultNamespace.UI
{
    [CreateAssetMenu(menuName = "Game/UI settings", fileName = nameof(UiSettings))]
    public class UiSettings : ScriptableObject
    {
        [SerializeField] private float _scoreDisplayMultiplier;
        
        public float ScoreDisplayMultiplier => _scoreDisplayMultiplier;
    }
}