using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class recordTime : MonoBehaviour
{
    float time = 0;
    StreamWriter writer;
    // Start is called before the first frame update
    void Start()
    {
        writer = new StreamWriter("MyPath.txt", true);
        

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.smoothDeltaTime;
        if(Input.GetKeyDown(KeyCode.KeypadEnter)){
            writer.WriteLine(time.ToString());
            writer.Flush();
            Debug.Log(time);
            time = 0;
        }
        if(Input.GetKeyDown(KeyCode.Space)){
            writer.Close();
        }
    }
}
