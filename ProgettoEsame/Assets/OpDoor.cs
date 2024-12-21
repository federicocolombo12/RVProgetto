using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Animator doorAnimator;

    // Start is called before the first frame update
    void Start()
    {
        if (doorAnimator == null)
        {
            doorAnimator = GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // 1 è il tasto destro del mouse
        {
            OpenTheDoor();
        }
    }

    void OpenTheDoor()
    {
        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger("DoorTrigger");
        }
    }
}