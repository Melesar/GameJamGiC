using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterColision : MonoBehaviour
{
    public bool endOfGame;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Resources")
        {
            Destroy(collision.gameObject);
            endOfGame = true;
        }
    }
}
