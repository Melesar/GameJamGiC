using Cinemachine;
using UnityEngine;

namespace DefaultNamespace
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private CinemachineVirtualCamera _camera;

        private CinemachineTrackedDolly _dolly;

        private void Update()
        {
            int direction;
            if (Input.GetKey(KeyCode.S))
            {
                direction = -1;
            }
            else if (Input.GetKey(KeyCode.W))
            {
                direction = 1;
            }
            else
            {
                return;
            }

            float position = _dolly.m_PathPosition;
            float offset = direction * _moveSpeed * Time.deltaTime;
            _dolly.m_PathPosition = Mathf.Clamp(position + offset, 0, 1);
        }

        private void Awake()
        {
            _dolly = _camera.GetCinemachineComponent<CinemachineTrackedDolly>();
        }
    }
}