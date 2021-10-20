using UnityEngine;

namespace DefaultNamespace.Physics.Test
{
    public class TestPointsController : MonoBehaviour
    {
        public float CityPoints { get; private set; }
        public float NaturePoints { get; private set; }

        public void SetCityPoints(float points)
        {
            CityPoints = points;
        }

        public void SetNaturePoints(float points)
        {
            NaturePoints = points;
        }
    }
}