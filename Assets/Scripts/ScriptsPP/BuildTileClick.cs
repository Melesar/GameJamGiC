using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameResources
{
    public class BuildTileClick : MonoBehaviour
    {
        [SerializeField] private DiggingController _diggingController;
        [SerializeField] private GameObject[] CityPrefabArray;
        [SerializeField] private GameObject[] NaturePrefabArray;

       private Renderer _prevTile;
        private Vector3 _selectedTilePos;
        public GameObject selectedTile;
        ResourceItem _selectedResource;

        

        // Update is called once per frame
        private void Update()
        {



            Ray screenRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(screenRay, out RaycastHit hit) == false) return;

            var buildTile = hit.collider.GetComponentInParent<BuildPoints>();
            
            if (buildTile == null)
            {
                return;
            }
            _selectedTilePos = buildTile.transform.position;
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
            if (!_selectedResource)
            {
                return;
            }
            _selectedResource = _diggingController._selectedResource;
            switch (_selectedResource.Resource.Type)
            {
                case ResourceType.Sand:
                   
                        Debug.Log("test");
                    break;


            }
            TileRenderer.material.color = Color.black;

        }
    }
}
