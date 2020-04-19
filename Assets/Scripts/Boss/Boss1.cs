using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : Boss
{
    [Header("session1")]
    private List<List<string>> session1Order = new List<List<string>>{
        new List<string>{"pattern9","2","0","-4","90"},
        //new List<string>{"pattern1","false","true","0.5"},
        new List<string>{"pattern9","2","-5","0","180"},
        new List<string>{"pattern9","2","5","0","180"},
        new List<string>{"pattern9","2","0","0","180"},
        new List<string>{"ownPattern7","4","90"},
        new List<string>{"pattern9","2","0","0","270"},
        new List<string>{"ownPattern2"},
        };
    
    [Header("ownPattern6")]
    public Bullet bomb;
    public float bullet6Speed;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        Handler.getInstance.readDelay(base.bossNum, "Session1");
        //session1 start
        base.isRot = false;
        StartCoroutine(session1());

        StartCoroutine("ownPattern1");
        //StartCoroutine("ownPattern2");
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    //protected override IEnumerator patternParser(List<List<string>> lst, List<float> delay){
    protected override IEnumerator patternParser(List<string> tp){
        StartCoroutine(base.patternParser(tp));
        if(!tp[0].Contains("own")) yield break;
        for(int i = 0;i<tp.Count;i++){
            //yield return new WaitForSeconds(delay);
            switch(tp[0]){
                case "ownPattern7" : Debug.Log("pattern7"); StartCoroutine(ownPattern7(float.Parse(tp[++i]),float.Parse(tp[++i])));break;
                default : StartCoroutine(tp[0]);break;
            }
        }
        yield return null;
    }
    IEnumerator session1(){
        for(int i = 0;i<session1Order.Count;i++){
            yield return new WaitForSeconds(Handler.getInstance.delay["Session1"][i]);
            StartCoroutine(patternParser(session1Order[i]));
        }
    }
    IEnumerator ownPattern1(){
        float musicTime = 0;
        yield return new WaitForSeconds(musicTime + 1.1f);
        float originSpeed = heartBullet.GetComponent<Bullet>().speed;
        GameObject[] tpObj = new GameObject[8];
        for (int i = 0; i < 8; i++){
            tpObj[i] = Instantiate(heartBullet.gameObject, muzzleList[i].transform.position, muzzleList[i].transform.rotation) as GameObject;//Quaternion.Euler(new Vector3(0, 0, i * 45)));
            tpObj[i].GetComponent<Bullet>().speed = 0;
        }
        yield return new WaitForSeconds(musicTime + 1.02f);
        StartCoroutine("bounce");
        StartCoroutine("ownPattern6");
        StartCoroutine(MoveTo(new Vector2(6,0),2));
        for (int i = 0; i < 8; i++){
            tpObj[i].GetComponent<Bullet>().speed = originSpeed;
        }

        yield return new WaitForSeconds(0.5f);
        

    }
    IEnumerator ownPattern2(){
        for (int i = 0; i < 30; i++){
            GameObject tp = Instantiate(bullet[5].gameObject, new Vector2(0,-5.0f), Quaternion.Euler(new Vector3(0, 0, Random.Range(-90,90)))) as GameObject;//;
            tp.transform.GetChild(0).rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(-60,60)));
            yield return new WaitForSeconds(0.03f);
        }
    }
    IEnumerator ownPattern6(){
        float angle = 65;
        int count = 0;
        while(true){
            foreach(float b in Handler.getInstance.delay["BounceDelay"]){
                yield return new WaitForSeconds(b);
                if(count++ % 2 == 0){
                    GameObject tp = Instantiate(bomb.gameObject, new Vector2(9.5f,0f), Quaternion.Euler(new Vector3(0, 0, angle))) as GameObject;//Quaternion.Euler(new Vector3(0, 0, i * 45)));
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
    IEnumerator ownPattern7(float position, float angle){
        GameObject tp = Instantiate(bullet[3].gameObject, new Vector2(9.5f,position), bullet[3].transform.rotation) as GameObject;
        yield return new WaitUntil(() => tp.transform.position.x <= 9.0f);
        for(int i = 0;i<10;i++){
            tp = Instantiate(bullet[4].gameObject, new Vector2(9.5f,position), bullet[3].transform.rotation) as GameObject;
            yield return new WaitUntil(() => tp.transform.position.x <= 9.0f);
        }
    }
    
}
