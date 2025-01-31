using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Vector3 vectOffset;
    [SerializeField] GameObject gofollow;
    [SerializeField] float speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        vectOffset = transform.position - gofollow.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = gofollow.transform.position + vectOffset;
        Quaternion targetRotation = Quaternion.Euler(gofollow.transform.eulerAngles.x, gofollow.transform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
       
        // Forza l'asse Z della rotazione a zero
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
    }
}
