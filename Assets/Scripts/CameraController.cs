using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace DefaultNamespace
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _cameraXValue;
        [SerializeField] private float _cameraZOffset;
        [SerializeField] private float _cameraYOffset;
        [SerializeField] private float _lowestY;
        [SerializeField] private float _furthestZ;
        [SerializeField] private float _curvatureRadius;
        [SerializeField] private Transform _transform;
        [SerializeField] [Range(0f, 1f)] private float _initialPosition;

        private Vector3 _lowest;
        private Vector3 _transitionStart;
        private Vector3 _transitionEnd;
        private Vector3 _furthest;

        private Vector3 _lastMousePosition;

        private Quaternion _lookFront;
        private Quaternion _lookTop;

        private float _frontFraction;
        private float _curvatureFraction;
        private float _topFraction;

        private float _currentLerp;

        private void Update()
        {
            if (TryGetMoveDirection(out int direction) == false)
            {
                return;
            }
            
            float offset = direction * _moveSpeed * Time.deltaTime;
            _currentLerp = Mathf.Clamp(_currentLerp + offset, 0f, 1f);
            
            BuildPath();
            LerpPath();
        }

        private void Start()
        {
            _currentLerp = _initialPosition;
            BuildPath();
            LerpPath();
        }

        private void LerpPath()
        {
            if (_currentLerp < _frontFraction)
            {
                _transform.position = Vector3.Lerp(_lowest, _transitionStart, _currentLerp / _frontFraction);
                _transform.rotation = _lookFront;
            }
            else if (_currentLerp < _frontFraction + _curvatureFraction)
            {
                float t = Mathf.InverseLerp(_frontFraction, _curvatureFraction + _frontFraction, _currentLerp);
                _transform.position = Vector3.Slerp(_transitionStart, _transitionEnd, t);
                _transform.rotation = Quaternion.Slerp(_lookFront, _lookTop, t);
            }
            else
            {
                float t = Mathf.InverseLerp(_curvatureFraction + _frontFraction, 1f, _currentLerp);
                _transform.position = Vector3.Lerp(_transitionEnd, _furthest, t);
                _transform.rotation = _lookTop;
            }
            
        }

        private void BuildPath()
        {
            _lowest = new Vector3(_cameraXValue, _lowestY, -_cameraZOffset);
            _transitionStart = new Vector3(_cameraXValue, _cameraYOffset - _curvatureRadius, -_cameraZOffset);
            _transitionEnd = new Vector3(_cameraXValue, _cameraYOffset, _curvatureRadius - _cameraZOffset);
            _furthest = new Vector3(_cameraXValue, _cameraYOffset, _furthestZ);
            
            _lookFront = Quaternion.LookRotation(Vector3.forward, Vector3.up);
            _lookTop = Quaternion.LookRotation(Vector3.down, Vector3.forward);

            float frontDistance = Vector3.Distance(_lowest, _transitionStart);
            float curvatureDistance = 0.25f * Mathf.PI * _curvatureRadius;
            float topDistance = Vector3.Distance(_transitionEnd, _furthest);
            float totalDistance = frontDistance + curvatureDistance + topDistance;

            _frontFraction = frontDistance / totalDistance;
            _curvatureFraction = curvatureDistance / totalDistance;
            _topFraction = topDistance / totalDistance;
            
            Assert.AreApproximatelyEqual(1f, _frontFraction + _curvatureFraction + _topFraction);
        }

        private bool TryGetMoveDirection(out int direction)
        {
            Vector3 mousePosition = Input.mousePosition;
            
            if (Input.GetKey(KeyCode.W) || MouseMoveDirection(mousePosition) > 0)
            {
                direction = 1;
            }
            else if (Input.GetKey(KeyCode.S) || MouseMoveDirection(mousePosition) < 0)
            {
                direction = -1;
            }
            else
            {
                direction = 0;
            }

            _lastMousePosition = mousePosition;

            int MouseMoveDirection(Vector3 position)
            {
                return Input.GetMouseButton(1) ? Math.Sign((position - _lastMousePosition).y) : 0;
            }

            return direction != 0;
        }

        private void OnDrawGizmos()
        {
            var radius = 1f;
            Gizmos.DrawSphere(new Vector3(_cameraXValue, _lowestY, -_cameraZOffset), radius);
            Gizmos.DrawSphere(new Vector3(_cameraXValue, _cameraYOffset - _curvatureRadius, -_cameraZOffset), radius);
            Gizmos.DrawSphere(new Vector3(_cameraXValue, _cameraYOffset, _curvatureRadius - _cameraZOffset), radius);
            Gizmos.DrawSphere(new Vector3(_cameraXValue, _cameraYOffset, _furthestZ), radius);
        }
    }
}