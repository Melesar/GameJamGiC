using GameResources;
using UnityEngine;

namespace DefaultNamespace.Physics
{
    public class PlaneTilt : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private ResourcesController _resourcesController;
        [SerializeField] private PhysicsSettings _physicsSettings;

        private void FixedUpdate()
        {
            float cityPoints = _resourcesController.GetResourcePoints(BoardSide.City);
            float naturePoints = _physicsSettings.NatureInitialPoints + _resourcesController.GetResourcePoints(BoardSide.Nature);

            float cityForce = cityPoints * _physicsSettings.PointsToForceCurve.Evaluate(cityPoints);
            float natureForce = naturePoints * _physicsSettings.PointsToForceCurve.Evaluate(naturePoints);
            
            _rigidbody.AddForceAtPosition(Vector3.down * cityForce, _rigidbody.position + Vector3.left * _physicsSettings.PressureDistance);
            _rigidbody.AddForceAtPosition(Vector3.down * natureForce, _rigidbody.position + Vector3.right * _physicsSettings.PressureDistance);
        }
    }
}