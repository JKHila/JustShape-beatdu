using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5;
	public float dashSpeed = 25;
	public GameObject dashSprite;
	public GameObject illusion;
    private GameObject sprite;

	private GameObject[] illusionList = new GameObject[20];
	private Vector3 previousPos;
	private float time = 0;
	private bool isCoolTime = false;
    //	public GameObject player;
	// Use this for initialization
	void Start () {
		sprite = transform.GetChild(0).gameObject;
		previousPos = transform.position;
        for (int i = 0; i < illusionList.Length ; i++){
			illusionList[i] = (GameObject)Instantiate(illusion, transform.position, transform.rotation);
			illusionList[i].SetActive(false);
        }
		StartCoroutine("showIllusion");
	}
	void OnTriggerEnter2D(Collider2D coll)
	{
        Debug.Log("hitted");
	}
	// Update is called once per frame
	void Update () {
		float mv = Input.GetAxis ("Vertical");
		float mh = Input.GetAxis ("Horizontal");

        //move
        //transform.position = new Vector3(transform.position.x + mh * speed, transform.position.y + mv * speed, transform.position.z);
		transform.Translate (Vector2.up * Time.smoothDeltaTime * speed * mv);
		transform.Translate (Vector2.right * Time.smoothDeltaTime * speed * mh);

		//rotation
		Vector3 moveDirection = transform.position - previousPos;
 
        if (moveDirection != Vector3.zero) 
		{
			float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
			sprite.transform.rotation = Quaternion.AngleAxis(angle - 90, sprite.transform.forward);
		}
        
        previousPos = transform.position;
 	 	
		//map clipping
		float posX = transform.position.x;
		float posY = transform.position.y;

		posX = Mathf.Clamp (posX, -8.7f, 8.7f);
		posY = Mathf.Clamp (posY, -4.7f, 4.7f);

		transform.position = new Vector3 (posX, posY, -1);

		//dash
		/* if (Input.GetKeyDown("space") && !isCoolTime){
			time = 0;
			if(time < 0.5f){
				transform.Translate (sprite.transform.up * Time.smoothDeltaTime * dashSpeed);
				Debug.Log(sprite.transform.up);
			}else{
				time = 0;
			}
		} */
		if (Input.GetKeyDown("space") && !isCoolTime)
        {
			Debug.Log("dash");
			time = 0;
			//dashSprite.transform.GetChild(1).localScale = new Vector2(0,0);
			dashSprite.transform.position = transform.position;
			dashSprite.SetActive(true);
			speed = dashSpeed;
			isCoolTime = true;
        }
		if(time > 0.1f) {
			speed = 5;
		}
		if(isCoolTime && time > 0.5f){
			isCoolTime = false;
			dashSprite.SetActive(false);
		}
		
		time += Time.smoothDeltaTime; 
		if(dashSprite.activeSelf)
			dashSprite.transform.GetChild(0).GetChild(0).localScale = new Vector3(time*70,time*70,time*70);
	}	
	IEnumerator showIllusion(){
		while(true){
			foreach(GameObject key in illusionList){
				if(!key.activeSelf){
					key.SetActive(true);
					key.transform.position = transform.position;
					key.transform.rotation = sprite.transform.rotation;
				}
				yield return new WaitForSeconds(0.02f);
			}
		}
	}
}
