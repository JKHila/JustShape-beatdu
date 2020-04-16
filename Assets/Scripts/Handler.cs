using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
public class Handler : MonoBehaviour
{
    private static Handler instance;
    public static Handler getInstance{
        get{
            if(instance == null){
                instance = FindObjectOfType<Handler>() as Handler;
            }
            return instance;
        }
    }
    private AudioSource audioData;
    public List<float> bounceDelay = new List<float>();
    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();
        //audioData.PlayScheduled(AudioSettings.dspTime + 2000);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void readBoundDelay(int n){
        StreamReader sr = new StreamReader("Assets\\Delay\\boss1BounceDelay.txt");

        string input = "";

        while (true){
            input = sr.ReadLine();
            if (input == null) { break; }
            bounceDelay.Add(float.Parse(input));
        }
        sr.Close(); 
    }
    public void changeSceen(){
        SceneManager.LoadScene("Stage");
    }
}
