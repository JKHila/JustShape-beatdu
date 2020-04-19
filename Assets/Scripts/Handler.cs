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
    public Dictionary<string,List<float>> delay = new Dictionary<string,List<float>>();
    public int spriteLayer = 0;
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
    
    public void readDelay(int n, string name){
        StreamReader sr = new StreamReader("Assets\\Delay\\Boss" + n + "\\" + name + ".txt");
        List<float> tp = new List<float>();
        string input = "";

        while (true){
            input = sr.ReadLine();
            if (input == null) { break; }
            tp.Add(float.Parse(input));
        }
        delay.Add(name,tp);
        sr.Close(); 
    }
    public void changeSceen(){
        SceneManager.LoadScene("Stage");
    }
}
