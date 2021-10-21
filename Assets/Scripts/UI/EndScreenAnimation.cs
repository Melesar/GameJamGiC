using System;
using UnityEngine;

namespace DefaultNamespace.UI
{
    public class EndScreenAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private GameManager _gameManager;

        private void Awake()
        {
            _gameManager.GameEnded += OnGameEnded;
        }

        private void OnDestroy()
        {
            _gameManager.GameEnded -= OnGameEnded;
        }

        private void OnGameEnded()
        {
            _animator.SetTrigger("End");
        }
    }
}