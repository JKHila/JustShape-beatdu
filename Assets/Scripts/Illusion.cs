using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Illusion : MonoBehaviour
{
    private Color tmpCol;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tmpCol.a -= Time.smoothDeltaTime * 5;
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = tmpCol;

        if(tmpCol.a <= 0){
            gameObject.SetActive(false);
        }
    }

    private void OnEnable() {
        tmpCol = transform.GetChild(0).GetComponent<SpriteRenderer>().color;
        tmpCol.a = 1.0f;
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = tmpCol;
    }
}
