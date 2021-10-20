using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GameObject Tile;
    public float size = 1f;
    public float gridSizeX = 6;
    public float gridSizeZ = 10;
    public float moveInX = -10;
    public float moveInZ = 5;
    GameObject _block;

    private void Start()
    {
        for (float x = moveInX; x < gridSizeX; x += size)
        {
            for (float z = moveInZ; z < gridSizeZ; z += size)
            {
               
                _block = Instantiate(Tile);
                _block.transform.position = new Vector3(x, 0.5f, z);
                _block.transform.parent = gameObject.transform;
            }
        }
    }
}
