using System;
using DefaultNamespace;
using UnityEngine;

public class WaterCollision : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Resources")
        {
            _gameManager.EndGame();
        }
    }
}
