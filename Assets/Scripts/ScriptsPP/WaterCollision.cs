using System;
using DefaultNamespace;
using UnityEngine;

public class WaterCollision : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag != "Resources")
        {
            _gameManager.EndGame();
        }
    }
}
