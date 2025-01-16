using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
   
    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene()
    {
        
        
            MySceneManager.instance.LoadNextScene("PrimaScena", LoadSceneMode.Single, () =>
            {
                Debug.Log("PrimaScena caricata con successo!");
            });
        
    }
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
