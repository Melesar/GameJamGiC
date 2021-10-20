using UnityEngine;

namespace DefaultNamespace.Physics.Test
{
    public class TestPhysicsController : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private TestPointsController _pointsController;
        [SerializeField] private float _advancedNaturePoints;
        [SerializeField] private AnimationCurve _pointsToTorqueCurve;
        [SerializeField] private float _pressureDistance;

        private void FixedUpdate()
        {
            float cityPoints = _pointsController.CityPoints;
            float naturePoints = _pointsController.NaturePoints + _advancedNaturePoints;

            float cityTorque = _pointsToTorqueCurve.Evaluate(cityPoints);
            float natureTorque = _pointsToTorqueCurve.Evaluate(naturePoints);
            
            _rigidbody.AddForceAtPosition(Vector3.down * cityTorque, _rigidbody.position + Vector3.left * _pressureDistance);
            _rigidbody.AddForceAtPosition(Vector3.down * natureTorque, _rigidbody.position + Vector3.right * _pressureDistance);
        }
    }
}