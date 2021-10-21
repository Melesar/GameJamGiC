using UnityEngine;
using TMPro;

public class BoardNumberPoints : MonoBehaviour
{
    [SerializeField] private TMP_Text NumberRef;
    void Start()
    {
        NumberRef.enabled = gameObject.transform.position.x < 0;
        float tilePosx = Mathf.Round( Mathf.Abs (gameObject.transform.position.x));
        NumberRef.SetText("X {0}", tilePosx);
    }

    
}
