using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInfermieria : MonoBehaviour
{
    CorridoioEventTracker eventTracker;
    [SerializeField] private int eventIndex;
    private void Start()
    {
        eventTracker = FindObjectOfType<CorridoioEventTracker>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
            {
            eventTracker.corridoioEvents[eventIndex].Invoke();
            Destroy(gameObject);
        }
    }
}
