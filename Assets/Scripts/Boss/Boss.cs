using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Boss : MonoBehaviour
{
    public int bossNum = 1;
    public Bullet heartBullet;
    public Bullet[] bullet;
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


    protected GameObject[] rotList = new GameObject[8];
    protected GameObject[] muzzleList = new GameObject[8];
    protected int sessionStatus = 1;
    private float[] bounceDelay;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rotList[0] = rot;
        muzzleList[0] = muzzle;
        for(int i = 1;i<8;i++){
            rotList[i] = (GameObject)Instantiate(rot.gameObject, transform.position, Quaternion.Euler(new Vector3(0, 0, i * 45)));
            rotList[i].transform.SetParent(transform);
            muzzleList[i] = rotList[i].transform.GetChild(0).gameObject;
        }

        Handler.getInstance.readDelay(bossNum, "BounceDelay");
        //StartCoroutine("pattern2");
        //StartCoroutine("pattern3");
        //StartCoroutine("pattern4");
    }

    // Update is called once per frame
    protected virtual void Update()
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
    protected virtual IEnumerator patternParser(List<string> tp){
        Debug.Log("parent"+tp[0]);
        if(tp[0].Contains("own")) yield break;
        for(int i = 0;i<tp.Count;i++){
            //yield return new WaitForSeconds(delay);
            switch(tp[0]){
                case "pattern1" : StartCoroutine(pattern1(bool.Parse(tp[++i]),bool.Parse(tp[++i]),float.Parse(tp[++i])));break;
                case "pattern9" : StartCoroutine(pattern9(bullet[int.Parse(tp[++i])].gameObject,new Vector2(float.Parse(tp[++i]),float.Parse(tp[++i])),float.Parse(tp[++i])));break;
                //default : StartCoroutine(tp[0]);break;
            }
        }
        yield return null;
    }
    /* protected virtual IEnumerator patternParser(List<List<string>> lst, List<float> delay){
        for(int j = 0;j<lst.Count;j++){
            List<string> tp = lst[j];
            for(int i = 0;i<lst[j].Count;i++){
                yield return new WaitForSeconds(delay[j]);
                if(!tp[0].Contains("own")){
                    switch(tp[0]){
                        case "pattern1" : StartCoroutine(pattern1(bool.Parse(tp[++i]),bool.Parse(tp[++i]),float.Parse(tp[++i])));break;
                        case "pattern9" : StartCoroutine(pattern9(bullet[int.Parse(tp[++i])].gameObject,new Vector2(float.Parse(tp[++i]),float.Parse(tp[++i])),float.Parse(tp[++i])));break;
                        //default : StartCoroutine(tp[0]);break;
                    }
                }
            }
        }
        yield return null;
    } */
    protected IEnumerator pattern1(bool isRot, bool isOnce, float size){
        Vector3 vec = new Vector3(0,0,0);
        this.isRot = isRot;
        do{
            for (int i = 0; i < 8; i++){
                GameObject tp = Instantiate(bullet[0].gameObject, muzzleList[i].transform.position, muzzleList[i].transform.rotation) as GameObject;//Quaternion.Euler(new Vector3(0, 0, i * 45)));
                tp.GetComponent<Bullet>().setSize(size);
                //rot.transform.rotation = Quaternion.Euler(vec);
                //vec.z += 45;
                //tpObj.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = spriteLayer;
                //tpObj.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = spriteLayer+1;
            }
            vec.z += 1;
            yield return new WaitForSeconds(0.15f);}
        while(!isOnce);
        yield return null;
    }
    protected IEnumerator pattern2(){
        while(true){
            GameObject tpObj = Instantiate(bullet[0].gameObject, muzzle.transform.position, transform.GetChild(1).rotation) as GameObject;
            tpObj.GetComponent<Bullet>().speed = bullet2Speed;
            yield return new WaitForSeconds(0.15f);
        }
    }
    protected IEnumerator pattern3(){
        while(true){
            Vector2 vec = new Vector2(-8.5f,0);
            for(int i = 0;i<number3;i++){
                GameObject tpObj = Instantiate(line.gameObject, vec, Quaternion.Euler(new Vector3(0, 0, -90))) as GameObject;
                vec.x += 2.3f;
                yield return new WaitForSeconds(coolTime3);
            }
            yield return new WaitForSeconds(3);
        }
    }
    protected IEnumerator pattern9(GameObject line, Vector2 position, float angle){
        GameObject tp = Instantiate(line.gameObject, position, Quaternion.Euler(new Vector3(0,0,angle))) as GameObject;
        yield return null;
    }

    protected IEnumerator bounce(){
        while(true){
            foreach(float b in Handler.getInstance.delay["BounceDelay"]){
                yield return new WaitForSeconds(b);
                GetComponent<Animator>().SetTrigger("bounce");
            }
        }
        /* for(int i = 0;i<Handler.getInstance.bounceDelay.Length;i++){
            GetComponent<Animator>().SetTrigger("bounce");
        } */
    }
    protected IEnumerator MoveTo(Vector2 toPos, float speed){
        float count = 0;
        Vector2 wasPos = transform.position;
        while(true){
            count += Time.smoothDeltaTime * speed;
            transform.position = Vector2.Lerp(wasPos,toPos,count);
            
            if(transform.position.Equals(toPos)){
                break;
            }
            yield return null;
        }
    }
}
