using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCelle : MonoBehaviour
{
    // Start is called before the first frame update
    CorridoioEventTracker eventTracker;
    [SerializeField] private int eventIndex;
    private void Start()
    {
        eventTracker = FindObjectOfType<CorridoioEventTracker>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.F))
        {
            eventTracker.corridoioEvents[eventIndex].Invoke();
            Destroy(gameObject);
        }
       
        
    }
}
