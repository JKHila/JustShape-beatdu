using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public bool isRot;
    public float rotationSpeed;
    private float tmpSize;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        tmpSize = 1.2f;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //move
		transform.Translate (Vector2.up * Time.smoothDeltaTime * speed);

        //destroy
        if(transform.position.x < -10 || transform.position.x > 10 || transform.position.y > 6 || transform.position.y < -6){
            Destroy(this.gameObject);
        }

        //start effect
        tmpSize -= Time.smoothDeltaTime * 2;
        if(tmpSize < 0){
            tmpSize = 0;
        }
        transform.GetChild(0).GetChild(0).localScale = new Vector2(tmpSize,tmpSize);
        
        //rotation
        if(isRot){
            transform.Rotate(Vector3.forward * Time.smoothDeltaTime * rotationSpeed);
        }
    }
}
