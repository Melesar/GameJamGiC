using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTileClick : MonoBehaviour
{
    public Vector3 mouseWorldPos;
    Renderer _prevTile;
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

       

        Ray screenRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(screenRay, out RaycastHit hit) == false) return;

        var buildTile = hit.collider.GetComponentInParent<BuildPoints>();
        if (buildTile == null)
        {
            return;
        }
        

            Renderer TileRenderer;
            TileRenderer = buildTile.GetComponent<Renderer>();
            TileRenderer.material.color = Color.red;

        if (!_prevTile)
            _prevTile = TileRenderer;

        if(TileRenderer != _prevTile)
        {
            _prevTile.material.color = Color.white;
            _prevTile = null;

        }
    }
}
