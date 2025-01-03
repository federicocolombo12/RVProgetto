using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerifyCollision : MonoBehaviour
{
    [SerializeField] Animator characterAnim;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision detected");
        characterAnim=other.GetComponent<Animator>();
        characterAnim.SetTrigger("ReachedPoint");
    }
}
