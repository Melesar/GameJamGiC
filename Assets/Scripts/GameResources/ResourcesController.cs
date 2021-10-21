using System;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.UI;
using UnityEngine;

namespace GameResources
{
    public class ResourcesController : MonoBehaviour
    {
        [SerializeField] private CityPointsUI _cityPointsUI;
        [SerializeField] private GameManager _gameManager;
        
        private Dictionary<BoardSide, float> _pointsMap;

        private void Awake()
        {
            _gameManager.GameRestarts += OnGameRestarts;
        }

        private void OnDestroy()
        {
            _gameManager.GameRestarts -= OnGameRestarts;
        }

        private void Start()
        {
            ResetResources();
        }

        public float OnResourceUsed(Resource resource, BoardSide side, float bonusMultiplayer)
        {
            float points = side switch
            {
                BoardSide.City => resource.CityPoints,
                BoardSide.Nature => resource.NaturePoints,
                _ => throw new ArgumentOutOfRangeException(nameof(side), side, null)
            };

            float pointsToAdd = points * Mathf.Round(Mathf.Abs(bonusMultiplayer));
            _pointsMap[side] += pointsToAdd;
            
            if (side == BoardSide.City)
            {
                _cityPointsUI.SetPoints(_pointsMap[side]);
            }

            return pointsToAdd;
        }

        public float GetResourcePoints(BoardSide side)
        {
            return _pointsMap[side];
        }

        private void ResetResources()
        {
            _pointsMap = new Dictionary<BoardSide, float>
            {
                {BoardSide.City, 0},
                {BoardSide.Nature, 0}
            };
            _cityPointsUI.SetPoints(0);
        }

        private void OnGameRestarts()
        {
            ResetResources();
        }
    }
}