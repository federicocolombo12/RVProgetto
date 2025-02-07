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
            MySceneManager.instance.LoadNextScene("ScenaFinaleElettroshockPassato", LoadSceneMode.Single, () =>
            {
                
            });
            Destroy(gameObject);
        }
    }
}
