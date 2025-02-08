using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    [SerializeField] private Paziente1ScriptNPC paziente1;
    [SerializeField] private Paziente2ScriptNPC paziente2;
    [SerializeField] private Paziente3ScriptNPC paziente3;
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            paziente1.SetShouldMoveToPosition(true);
            paziente2.SetShouldMoveToPosition(true);
            paziente3.SetShouldMoveToPosition(true);
        }
    }
}