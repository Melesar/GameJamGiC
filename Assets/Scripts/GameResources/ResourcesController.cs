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

        public event Action ResourceUpdated;
        
        private Dictionary<BoardSide, float> _pointsMap;

        private void Awake()
        {
            _pointsMap = new Dictionary<BoardSide, float>
            {
                {BoardSide.City, 0},
                {BoardSide.Nature, 0}
            };
            _cityPointsUI.SetPoints(0);
        }
        
        //TODO handle different sides of the board
        public void OnResourceUsed(Resource resource, BoardSide side)
        {
            float points = side switch
            {
                BoardSide.City => resource.CityPoints,
                BoardSide.Nature => resource.NaturePoints,
                _ => throw new ArgumentOutOfRangeException(nameof(side), side, null)
            };
            
            _pointsMap[side] += points;
            ResourceUpdated?.Invoke();
            if (side == BoardSide.City)
            {
                _cityPointsUI.SetPoints(_pointsMap[side]);
            }
        }

        public float GetResourcePoints(BoardSide side)
        {
            return _pointsMap[side];
        }
    }
}