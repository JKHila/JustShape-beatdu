using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Bullet heartBullet;
    public Bullet bullet;
    public Line line;
    public GameObject muzzle;
    public GameObject rot;
    public bool isRot;
     [Header("pattern1")]
    public float coolTime1;
    private float time1;
    
     [Header("pattern2")]
    public float coolTime2;
    private float time2;
    public float rotationSpeed;
    public float bullet2Speed;
     [Header("pattern3")]
    public float coolTime3;
    public float number3;
     [Header("pattern4")]


    private int spriteLayer = 0;
    private GameObject[] rotList = new GameObject[8];
    private GameObject[] muzzleList = new GameObject[8];

    // Start is called before the first frame update
    void Start()
    {
        rotList[0] = rot;
        muzzleList[0] = muzzle;
        for(int i = 1;i<8;i++){
            rotList[i] = (GameObject)Instantiate(rot.gameObject, transform.position, Quaternion.Euler(new Vector3(0, 0, i * 45)));
            rotList[i].transform.SetParent(transform);
            muzzleList[i] = rotList[i].transform.GetChild(0).gameObject;
        }
        StartCoroutine("pattern1");
        //StartCoroutine("pattern2");
        //StartCoroutine("pattern3");
        //StartCoroutine("pattern4");
    }

    // Update is called once per frame
    void Update()
    {
       /*  //pattern1
        if(time1 > coolTime1){
            pattern1();
            time1 = 0;
        }
        time1 += Time.smoothDeltaTime;
        
        //pattern2
        transform.GetChild(1).Rotate(Vector3.forward * Time.smoothDeltaTime * rotationSpeed);
        if(time2 > coolTime2){
            pattern2();
            time2 = 0;
        }
        time2 += Time.smoothDeltaTime; */
        if(isRot){
            for(int i = 0;i<8;i++){
                rotList[i].transform.Rotate(Vector3.forward * Time.smoothDeltaTime * rotationSpeed);
            }
        }
    }

    IEnumerator pattern1(){
        Vector3 vec = new Vector3(0,0,0);
        while(true){
            for (int i = 0; i < 8; i++){
                Instantiate(bullet.gameObject, muzzleList[i].transform.position, muzzleList[i].transform.rotation);//Quaternion.Euler(new Vector3(0, 0, i * 45)));
                //rot.transform.rotation = Quaternion.Euler(vec);
                //vec.z += 45;
                //tpObj.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = spriteLayer;
                //tpObj.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = spriteLayer+1;
            }
            vec.z += 1;
            spriteLayer++;
            yield return new WaitForSeconds(0.15f);
        }
    }
    IEnumerator pattern2(){
        while(true){
            GameObject tpObj = (GameObject)Instantiate(bullet.gameObject, muzzle.transform.position, transform.GetChild(1).rotation);
            tpObj.GetComponent<Bullet>().speed = bullet2Speed;
            yield return new WaitForSeconds(0.15f);
        }
    }
    IEnumerator pattern3(){
        while(true){
            Vector2 vec = new Vector2(-8.5f,0);
            for(int i = 0;i<number3;i++){
                GameObject tpObj = (GameObject)Instantiate(line.gameObject, vec, Quaternion.Euler(new Vector3(0, 0, -90)));
                vec.x += 2.3f;
                yield return new WaitForSeconds(coolTime3);
            }
            yield return new WaitForSeconds(3);
        }
    }
    IEnumerator pattern4(){
        float musicTime = 0;
        yield return new WaitForSeconds(musicTime + 1.1f);
        float originSpeed = heartBullet.GetComponent<Bullet>().speed;
        GameObject[] tpObj = new GameObject[8];
        for (int i = 0; i < 8; i++){
            tpObj[i] = (GameObject)Instantiate(heartBullet.gameObject, muzzleList[i].transform.position, muzzleList[i].transform.rotation);//Quaternion.Euler(new Vector3(0, 0, i * 45)));
            tpObj[i].GetComponent<Bullet>().speed = 0;
        }
        yield return new WaitForSeconds(musicTime + 1.02f);
        GetComponent<Animator>().SetTrigger("isStart");
        for (int i = 0; i < 8; i++){
            tpObj[i].GetComponent<Bullet>().speed = originSpeed;
        }
    }
}
