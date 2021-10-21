using UnityEngine;

namespace GameResources
{
    public class BuildTileClick : MonoBehaviour
    {
        [SerializeField] private DiggingController _diggingController;
        [SerializeField] private GameObject[] CityPrefabArray;
        [SerializeField] private GameObject[] NaturePrefabArray;
        [SerializeField] private GameObject Ground;
        [SerializeField] private GameObject _spawnEffect;

       private Renderer _prevTile;
        private Vector3 _selectedTilePos;
        public GameObject selectedTile;
        ResourceItem _selectedResource;
        Color _previnitialColor;
        // Update is called once per frame
        private void Update()
        {

            Ray screenRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(screenRay, out RaycastHit hit) == false) return;

            var buildTile = hit.collider.tag;

            if (buildTile != "BoardTile")
            {
                return;
            }
            
            selectedTile = hit.collider.transform.gameObject;
            _selectedTilePos = selectedTile.transform.position;
            TileColorChange();

            if (!Input.GetMouseButtonDown(0))
            {
                return;
            }
            if (!_diggingController._selectedResource)
            {
                return;
            }
            _selectedResource = _diggingController._selectedResource;
            ChangeTilie();
        }

        void ChangeTilie()
        {
            GameObject tile;
            if (_selectedTilePos.x < 0)
                tile = Instantiate(CityPrefabArray[(int) _selectedResource.Resource.Type]);
            else
                tile = Instantiate(NaturePrefabArray[(int) _selectedResource.Resource.Type]);

            tile.transform.position = _selectedTilePos;
            Destroy(Instantiate(_spawnEffect, _selectedTilePos, Quaternion.LookRotation(Vector3.up)), 5f);

            tile.transform.parent = Ground.transform;
            tile.transform.rotation = selectedTile.transform.rotation;
            Destroy(selectedTile);
        }

        void TileColorChange()
        {
            Renderer TileRenderer;
            
            TileRenderer = selectedTile.GetComponent<Renderer>();

            if (!_prevTile)
            {
                _prevTile = TileRenderer;
                _previnitialColor = _prevTile.material.color;
                TileRenderer.material.color = Color.white;
            }

            if (TileRenderer != _prevTile)
            {
                if (_prevTile.material.color == Color.white)
                    _prevTile.material.color = _previnitialColor;
                _prevTile = null;
                

            }
        }
    }
}
