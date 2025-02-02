using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchFlicker : MonoBehaviour
{
    public Light torchLight; // Riferimento alla luce
    public float minWaitTime = 0.1f; // Tempo minimo tra accensioni/spegnimenti
    public float maxWaitTime = 0.5f; // Tempo massimo tra accensioni/spegnimenti
    public float longWaitTime = 3f;  // Tempo lungo tra un ciclo e l'altro

    void Start()
    {
        StartCoroutine(FlickerEffect());
    }

    private IEnumerator FlickerEffect()
    {
        while (true)
        {
            for (int i = 0; i < Random.Range(3, 6); i++) // Numero di lampeggi casuale
            {
                torchLight.enabled = !torchLight.enabled; // Spegne/accende la luce
                yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime)); // Tempo casuale tra lampeggi
            }

            torchLight.enabled = false; // Spegne la luce per un intervallo più lungo
            yield return new WaitForSeconds(longWaitTime);
        }
    }
}