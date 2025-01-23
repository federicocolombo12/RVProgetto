using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrozzellaMove : MonoBehaviour
{
   [SerializeField] float moveDuration = 5f; // Durata in secondi del movimento
    [SerializeField] float speed = 5f; // Velocità di movimento
    // Metodo per avviare il movimento
    public void Move()
    {
        StartCoroutine(MoveCoroutine());
    }

    // Coroutine per gestire il movimento temporizzato
    IEnumerator MoveCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Movimento terminato
        Debug.Log("Movimento terminato!");
    }
}
