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
        transform.rotation = Quaternion.Slerp(transform.rotation, gofollow.transform.rotation, speed * Time.deltaTime);
    }
}
