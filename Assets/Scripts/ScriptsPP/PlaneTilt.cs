using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneTilt : MonoBehaviour
{
    public static PlaneTilt Instance { get; private set; }
    public GameObject Plane;
    public bool ChangeTiltPoint;
    public float tiltValue =0;
    public float tiltScale =1;
    float _actualAngle;
    float _tiltVelocityScale;

    private float PointsValue;
    public float PointsCounter
    {
        get { return PointsValue; }
        set
        {
            PointsValue += value;


        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        gameObject.transform.eulerAngles = new Vector3(0, 0, tiltValue);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tiltValue = PointsValue * tiltScale;
        if (_actualAngle < tiltValue)
        {
            _actualAngle = gameObject.transform.eulerAngles.z;
            _tiltVelocityScale = (tiltValue - _actualAngle)/tiltValue;
            gameObject.transform.eulerAngles = new Vector3(0, 0, _actualAngle + Time.deltaTime*_tiltVelocityScale);

        }

        if (ChangeTiltPoint)
        {
            gameObject.transform.position = new Vector3(-8, 0, 0);
            Plane.transform.position = new Vector3(0, 0, 0);
        }
            
    }
}
