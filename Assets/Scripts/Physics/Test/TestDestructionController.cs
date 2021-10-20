using System;
using UnityEngine;

namespace DefaultNamespace.Physics.Test
{
    public class TestDestructionController : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetMouseButtonDown(0) == false)
            {
                return;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (UnityEngine.Physics.Raycast(ray, out RaycastHit hit, 1000, LayerMask.GetMask("Water")))
            {
                Destroy(hit.collider.gameObject);
            }
        }
    }
}