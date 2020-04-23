using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePattern : MonoBehaviour
{
    [Header("Worm")]
    public bool wormPattern;
    public float wormSpeed;
    public float wormRange;
    private Vector2 firstPosition;

    [Header("Clump")]
    public bool clumpPattern;
    public float clumpRange;
    public float clumpSpeed;
    private Vector2 firstScale;
    private float tmpScale;
    private float clumpTime;

    // Start is called before the first frame update
    void Start()
    {
        firstPosition = transform.GetChild(0).localPosition;
        firstScale = transform.localScale;
        tmpScale = firstScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(wormPattern){
            if((wormSpeed > 0 && transform.GetChild(0).localPosition.x > firstPosition.x + wormRange) || (wormSpeed < 0 && transform.GetChild(0).localPosition.x <= firstPosition.x - wormRange))
                wormSpeed *= -1;

		    transform.GetChild(0).Translate (Vector2.up * Time.smoothDeltaTime * wormSpeed);
        }

        if(clumpPattern){
            if((clumpSpeed > 0 && transform.localScale.y > firstScale.y + clumpRange) || (clumpSpeed < 0 && transform.localScale.y < firstScale.y - clumpRange)){
                clumpSpeed *= -1;
            }

            tmpScale += clumpRange*clumpSpeed;
		    transform.localScale = new Vector2(tmpScale,tmpScale);
        }
    }
}
