using GameResources;
using UnityEngine;

namespace DefaultNamespace.Physics
{
    public class PlaneTilt : MonoBehaviour
    {
        [SerializeField] private Transform _tiltPoint;
        [SerializeField] private Transform _planeTransform;
        [SerializeField] private ResourcesController _resourcesController;
        [SerializeField] private float _tiltVelocity;

        private float _targetTilt;
        private float _currentTilt;

        private void Update()
        {
            float newTilt = Mathf.MoveTowardsAngle(_currentTilt, _targetTilt, 5f);
            float delta = newTilt - _currentTilt;
            if (Mathf.Approximately(delta, 0))
            {
                return;
            }
            
            _planeTransform.RotateAround(_tiltPoint.position, Vector3.forward, delta);
            _currentTilt = newTilt;
        }

        private void Awake()
        {
            _resourcesController.ResourceUpdated += OnResourceUpdated;
        }

        private void OnDestroy()
        {
            _resourcesController.ResourceUpdated -= OnResourceUpdated;
        }

        private void OnResourceUpdated()
        {
            float balance = _resourcesController.GetResourceBalance();
            _targetTilt = _tiltVelocity * balance;
        }
    }
}