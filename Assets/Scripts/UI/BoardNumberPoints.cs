using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoardNumberPoints : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
       TMP_Text NumberRef = GetComponent<TMP_Text>();
        float tilePosx = Mathf.Round( Mathf.Abs (gameObject.transform.position.x));
        NumberRef.SetText("X {0}", tilePosx);
    }

    
}
