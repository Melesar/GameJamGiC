using UnityEngine;

namespace DefaultNamespace.Physics
{
    [CreateAssetMenu(fileName = nameof(PhysicsSettings), menuName = "Game/Physics settings")]
    public class PhysicsSettings : ScriptableObject
    {
        [SerializeField] private float _natureInitialPoints;
        [SerializeField] private AnimationCurve _pointsToForceCurve;
        [SerializeField] private float _pressureDistance;

        public float NatureInitialPoints => _natureInitialPoints;
        public AnimationCurve PointsToForceCurve => _pointsToForceCurve;
        public float PressureDistance => _pressureDistance;
    }
}