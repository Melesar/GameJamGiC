using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private GameObject Tile;
    [SerializeField] private GameObject NatureTile;
    [SerializeField] private GameObject Underground;
    [SerializeField] private float size = 1f;
    [SerializeField] private float gridSizeX = 6;
    [SerializeField] private float gridSizeZ = 10;
    [SerializeField] private float moveInX = -10;
    [SerializeField] private float moveInZ = 5;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Vector3 bonusNaturePos;
    private List<GameObject> _spawnedObjects;

    private void Awake()
    {
        _spawnedObjects = new List<GameObject>();
        _gameManager.GameRestarts += OnGameRestarts;
    }

    private void OnDestroy()
    {
        _gameManager.GameRestarts -= OnGameRestarts;
    }

    private void Start()
    {
        GenerateGrid();
    }

    private void OnGameRestarts()
    {
        foreach (GameObject spawnedObject in _spawnedObjects)
        {
            Destroy(spawnedObject);
        }
        _spawnedObjects.Clear();
        
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        for (float x = moveInX; x < gridSizeX * size + moveInX; x += size)
        {
            GameObject baseBlock = Instantiate(Underground);
            baseBlock.transform.position = new Vector3(x, 0.1f, -4);
            baseBlock.transform.parent = gameObject.transform;
            _spawnedObjects.Add(baseBlock);
            for (float z = moveInZ; z < gridSizeZ * size + moveInZ; z += size)
            {
               

                GameObject block = Instantiate(Tile);

                if (x == bonusNaturePos.x && z == bonusNaturePos.z)
                {
                    block = Instantiate(NatureTile);
                }
                block.transform.position = new Vector3(x, 0.5f, z);
                block.transform.parent = gameObject.transform;
               
                if (x <=0)
                {
                    var tile = block.GetComponent<Renderer>();
                    tile.material.color = new Color32(255, 180, 0, 255);
                }
                _spawnedObjects.Add(block);
            }
        }
    }
}
