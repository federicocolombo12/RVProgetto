using System.Collections;
using UnityEngine;

public class Fulmine : MonoBehaviour
{
    private Light lightningLight;
    private bool doppio = true;

    void Start()
    {
        lightningLight = GetComponent<Light>();
        lightningLight.enabled = false;
        StartCoroutine(ActivateAndDeactivate());
    }

    private IEnumerator ActivateAndDeactivate()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5, 15));
            lightningLight.enabled = true;
            yield return new WaitForSeconds(0.2f);
            lightningLight.enabled = false;
            if (doppio) { 
                yield return new WaitForSeconds(Random.Range(0.01f, 0.3f));
                lightningLight.enabled = true;
                yield return new WaitForSeconds(0.2f);
                lightningLight.enabled = false;
            }
            doppio = !doppio;
        }
    }

}


