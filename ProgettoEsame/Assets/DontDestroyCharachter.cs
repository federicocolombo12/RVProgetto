using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyCharachter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.instance.currentSceneName == "ScenaIniziale" || SceneManager.instance.currentSceneName == "Corridoio1")
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
