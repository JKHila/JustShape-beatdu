using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : Boss
{
    [Header("ownPattern6")]
    public Bullet bomb;
    public float bullet6Speed;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        StartCoroutine("ownPattern1");
        StartCoroutine(MoveTo(new Vector2(6,0)));
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
     IEnumerator ownPattern1(){
        float musicTime = 0;
        yield return new WaitForSeconds(musicTime + 1.1f);
        float originSpeed = heartBullet.GetComponent<Bullet>().speed;
        GameObject[] tpObj = new GameObject[8];
        for (int i = 0; i < 8; i++){
            tpObj[i] = (GameObject)Instantiate(heartBullet.gameObject, muzzleList[i].transform.position, muzzleList[i].transform.rotation);//Quaternion.Euler(new Vector3(0, 0, i * 45)));
            tpObj[i].GetComponent<Bullet>().speed = 0;
        }
        yield return new WaitForSeconds(musicTime + 1.02f);
        StartCoroutine("bounce");
        StartCoroutine("ownPattern6");
        //GetComponent<Animator>().SetTrigger("isStart");
        for (int i = 0; i < 8; i++){
            tpObj[i].GetComponent<Bullet>().speed = originSpeed;
        }
    }
    IEnumerator ownPattern6(){
        float angle = 65;
        int count = 0;
        while(true){
            foreach(float b in Handler.getInstance.bounceDelay){
                yield return new WaitForSeconds(b);
                if(count++ % 2 == 0){
                    GameObject tp = (GameObject)Instantiate(bomb.gameObject, new Vector2(9.5f,0f), Quaternion.Euler(new Vector3(0, 0, angle)));//Quaternion.Euler(new Vector3(0, 0, i * 45)));
                    tp.GetComponent<Bullet>().speed = bullet6Speed;
                    tp.GetComponent<Bomb>().explosionTime = b;
                    if(angle > 100){
                        angle = 65;
                    }else{
                        angle = 110;
                    }
                }
            }
        }
    }
}
