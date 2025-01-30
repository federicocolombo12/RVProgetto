using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    // Start is called before the first frame update
    CorridoioEventTracker eventTracker;
    [SerializeField] private int eventIndex;
    private void Start()
    {
        eventTracker = FindObjectOfType<CorridoioEventTracker>();
    }
    private void OnTriggerEnter(Collider other)
    {
        eventTracker.corridoioEvents[eventIndex].Invoke();
        Destroy(gameObject);
    }
    
}
