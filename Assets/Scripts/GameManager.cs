using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Canvas _gameCanvas;
        [SerializeField] private Canvas _tutorialCanvas;
        [SerializeField] private Canvas _gameEndCanvas;

        public event Action GameRestarts;
        public event Action GameEnded;

        private void Start()
        {
            _gameCanvas.enabled = false;
            _gameEndCanvas.enabled = false;
            StartGame();
        }

        public void StartGame()
        {
            _tutorialCanvas.enabled = false;
            _gameCanvas.enabled = true;
        }

        public void RestartGame()
        {
            _gameEndCanvas.enabled = false;
            _gameCanvas.enabled = true;
        }

        public void EndGame()
        {
            _gameCanvas.enabled = false;
            _gameEndCanvas.enabled = true;
            GameEnded.Invoke();
        }
    }
}