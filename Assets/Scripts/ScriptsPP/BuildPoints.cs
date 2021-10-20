using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPoints : MonoBehaviour
{

    public float pointScale;
    public float tileType;
    float _tilePosition;
    bool _builded;
    // Start is called before the first frame update
    void Start()
    {
        _builded = false;
        _tilePosition = gameObject.transform.position.x; // Uwzglêdnic obrot
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(tileType != 0 && !_builded)
        {
            PlaneTilt.Instance.PointsCounter = pointScale * tileType * _tilePosition;
            _builded = true;
        }

        Debug.Log(gameObject.transform.position.x);
    }
}
