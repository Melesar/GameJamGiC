using System;
using UnityEngine;

namespace DefaultNamespace.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public sealed class EndGameSplash : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _gameManager.GameEnded += OnGameEnded;
        }

        private void OnDestroy()
        {
            _gameManager.GameEnded -= OnGameEnded;
        }

        private void OnGameEnded()
        {
            _audioSource.Play();
        }
    }
}