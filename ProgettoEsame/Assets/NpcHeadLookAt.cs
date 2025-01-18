using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
public class NpcHeadLookAt : MonoBehaviour
{
    [SerializeField] private Rig rig;
    [SerializeField] private Transform target;
    [SerializeField] private bool isLooking;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float targetWeight = isLooking ? 1.0f : 0.0f;
        float lerpSpeed = 2.0f;
        rig.weight = Mathf.Lerp(rig.weight, targetWeight, Time.deltaTime * lerpSpeed);
        
    }
    public void LookAtPosition(Vector3 lookAtPosition)
    {
        isLooking = true;
        target.position = lookAtPosition;
    }
}
