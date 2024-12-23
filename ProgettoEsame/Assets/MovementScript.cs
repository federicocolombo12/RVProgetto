using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    // Speed of the object
    [SerializeField] float speed = 5f;
    
    bool moving = true;
    Animator characterAnim;

    // Start is called before the first frame update
    void Start()
    {
        characterAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        moving = !characterAnim.GetCurrentAnimatorStateInfo(0).IsName("Idle");
        if (moving) {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        
    }
   

}
