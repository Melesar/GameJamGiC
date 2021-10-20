using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace GameResources
{
    public class ResourcesController : MonoBehaviour
    {
        private Dictionary<BoardSide, float> _pointsMap;

        private void Awake()
        {
            _pointsMap = new Dictionary<BoardSide, float>
            {
                {BoardSide.City, 0},
                {BoardSide.Nature, 0}
            };
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
        }

        public float GetResourcePoints(BoardSide side)
        {
            return _pointsMap[side];
        }
    }
}