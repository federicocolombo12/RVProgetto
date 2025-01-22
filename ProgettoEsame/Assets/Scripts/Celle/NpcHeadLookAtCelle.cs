using UnityEngine.Animations.Rigging;
using UnityEngine;

public class NpcHeadLookAtCelle : MonoBehaviour
{
    [SerializeField] private Rig rig;
    [SerializeField] private Transform target;
    [SerializeField] private float maxDistance = 2.0f; // Distanza massima per guardare il giocatore
    public bool isLooking;
    private const float threshold = 0.01f; // Soglia per considerare la transizione completata

    void Update()
    {
        float targetWeight = isLooking ? 1.0f : 0.0f;
        float lerpSpeed = 2.0f;

        // Aggiorna il peso del rig
        rig.weight = Mathf.Lerp(rig.weight, targetWeight, Time.deltaTime * lerpSpeed);

        // Controlla se il peso ha raggiunto il valore target
        if (Mathf.Abs(rig.weight - targetWeight) < threshold)
        {
            rig.weight = targetWeight; // Imposta esattamente il valore target per precisione
            if (isLooking && targetWeight == 1.0f)
            {
                // La transizione verso il "guardare" è completata
                isLooking = false;
            }
        }
    }

    public void LookAtPosition(Vector3 lookAtPosition)
    {
        float distance = Vector3.Distance(transform.position, lookAtPosition);
        if (distance <= maxDistance)
        {
            isLooking = true; // Avvia la transizione
            target.position = lookAtPosition;
        }
    }
}
