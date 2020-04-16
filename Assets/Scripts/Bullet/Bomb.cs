using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Bullet
{
    public Bullet bullet;
    public float explosionTime;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        StartCoroutine(explosion());
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    IEnumerator explosion(){
        yield return new WaitForSeconds(explosionTime);
        for(int i = 0;i<16;i++){
            GameObject tp = (GameObject)Instantiate(bullet.gameObject, transform.position, Quaternion.Euler(new Vector3(0, 0, i * 22.5f)));
            tp.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
        }
        Destroy(this.gameObject);
    }
}
