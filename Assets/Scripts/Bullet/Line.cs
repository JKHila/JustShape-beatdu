using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : Bullet
{
    public float maxWidth;
    public float term;
    public float direction;
    //1: right
    //-1: left
    private Color tmpCol;
    private float tmpWidth = 0;
    private GameObject sprite;
    private int status = 0;
    //0: ready
    //1: hit
    //2: disappear

    // Start is called before the first frame update
    protected override void Start()
    {
		sprite = transform.GetChild(0).gameObject;
        tmpCol = sprite.GetComponent<SpriteRenderer>().color;
        tmpCol.a = 0f;
        sprite.GetComponent<SpriteRenderer>().color = tmpCol;

        sprite.transform.localScale = new Vector2(sprite.transform.localScale.x,0);
    }

    // Update is called once per frame
    protected override void Update()
    {
        if(status == 0 || status == 1){
            if(status == 0){
                tmpCol.a += Time.smoothDeltaTime / term;
                sprite.GetComponent<SpriteRenderer>().color = tmpCol;
            }

            if(sprite.transform.localScale.y < maxWidth){
                tmpWidth += Time.smoothDeltaTime * maxWidth / term;
                sprite.transform.localScale = new Vector2(sprite.transform.localScale.x,tmpWidth);
            }else{
                StartCoroutine ("Shoot");
                status = 1;
            }
        }

        if(status == 2){
            tmpWidth -= Time.smoothDeltaTime * maxWidth / term * 5;
            sprite.transform.localScale = new Vector2(sprite.transform.localScale.x,tmpWidth);
            //transform.Translate (Vector2.right * Time.smoothDeltaTime * 10000 * direction);
            tmpCol.a -= Time.smoothDeltaTime / term * 5;
            sprite.GetComponent<SpriteRenderer>().color = tmpCol;
        }
    }

    IEnumerator Shoot(){
        sprite.transform.localScale = new Vector2(sprite.transform.localScale.x,maxWidth / 1.2f);
        tmpWidth = maxWidth / 1.2f;
        sprite.GetComponent<SpriteRenderer>().color = new Color(255,255,255);
        yield return new WaitForSeconds(0.2f);
        sprite.GetComponent<SpriteRenderer>().color = tmpCol;
        yield return new WaitForSeconds(0.2f);
        status = 2;
    }
}
