using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Canvas _gameCanvas;
        [SerializeField] private Canvas _startCanvas;
        [SerializeField] private Canvas _tutorialCanvas;
        [SerializeField] private Canvas _gameEndCanvas;

        public event Action GameStarted;
        public event Action GameRestarts;
        public event Action GameEnded;

        private bool _isGameOver;

        private void Start()
        {
            _isGameOver = false;
            _startCanvas.enabled = true;
            _tutorialCanvas.enabled = false;
            _gameCanvas.enabled = false;
            _gameEndCanvas.enabled = false;
        }

        public void GoToTutorial()
        {
            _startCanvas.enabled = false;
            _tutorialCanvas.enabled = true;
        }

        public void StartGame()
        {
            _isGameOver = false;
            _startCanvas.enabled = false;
            _tutorialCanvas.enabled = false;
            _gameCanvas.enabled = true;
            GameStarted?.Invoke();
        }

        public void RestartGame()
        {
            _isGameOver = false;
            _gameEndCanvas.enabled = false;
            _gameCanvas.enabled = true;
            GameRestarts?.Invoke();
        }

        public void EndGame()
        {
            if (_isGameOver)
            {
                return;
            }

            _isGameOver = true;
            _gameCanvas.enabled = false;
            _gameEndCanvas.enabled = true;
            GameEnded?.Invoke();
        }
    }
}