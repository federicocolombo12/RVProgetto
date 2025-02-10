using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerPassato : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        

        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.F))
        {
            TransitionScript.instance.FadeOut(1);
            MySceneManager.instance.LoadNextScene("ScenaFinaleElettroshockPassato", LoadSceneMode.Single, () =>
            {
                TransitionScript.instance.FadeIn(1);
            });
            Destroy(gameObject);
        }
    }
}
