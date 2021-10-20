using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameResources
{
    public class BuildTileClick : MonoBehaviour
    {
        [SerializeField] private DiggingController _diggingController;
        public Vector3 mouseWorldPos;
        Renderer _prevTile;
        public GameObject selectedTile;


        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {



            Ray screenRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(screenRay, out RaycastHit hit) == false) return;

            var buildTile = hit.collider.GetComponentInParent<BuildPoints>();
            if (buildTile == null)
            {
                return;
            }

            selectedTile = hit.collider.transform.gameObject;
            Renderer TileRenderer;
            TileRenderer = buildTile.GetComponent<Renderer>();



            if (!_prevTile)
            {
                _prevTile = TileRenderer;
                TileRenderer.material.color = Color.red;
            }

            if (TileRenderer != _prevTile)
            {
                if (_prevTile.material.color == Color.red)
                    _prevTile.material.color = Color.white;
                _prevTile = null;

            }

            if (!Input.GetMouseButtonDown(0))
            {
                return;
            }
            TileRenderer.material.color = Color.black;

        }
    }
}
