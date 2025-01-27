using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torcia : MonoBehaviour
{
    public GameObject flashlight;
    public Transform flashlightTransform;

    [SerializeField] private bool on;
    [SerializeField] private bool off;
    [SerializeField] private float flickerTime = 2.5f;
    [SerializeField] private float elapsedTime;

    private bool independentMovement = false;
    private Camera mainCamera;
    private Vector3 defaultRotation = new Vector3(0f, 0f, 0f);

    void Start()
    {
        on = true;
        off = false;
        flashlight.SetActive(false);
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && on)
        {
            independentMovement = true;
            mainCamera.GetComponent<CameraController>().enabled = false;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            independentMovement = false;
            mainCamera.GetComponent<CameraController>().enabled = true;
            flashlightTransform.localRotation = Quaternion.Euler(defaultRotation);
        }

        if (!independentMovement)
        {
            flashlightTransform.localRotation = Quaternion.Euler(defaultRotation);
        }

        if (independentMovement && on)
        {
            float mouseX = Input.GetAxis("Mouse X") * 5f;
            float mouseY = -Input.GetAxis("Mouse Y") * 5f;
            flashlightTransform.Rotate(Vector3.up, mouseX, Space.World);
            flashlightTransform.Rotate(Vector3.right, mouseY, Space.World);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (off)
            {
                flashlight.SetActive(true);
                off = false;
                on = true;
                Debug.Log("Torcia accesa");
            }
            else if (on)
            {
                flashlight.SetActive(false);
                off = true;
                on = false;
                Debug.Log("Torcia spenta");
            }
        }
    }

    public void Flickering()
    {
        StartCoroutine(FlickeringLight());
    }

    IEnumerator FlickeringLight()
    {
        elapsedTime = 0f;
        while (elapsedTime < flickerTime)
        {
            yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
            flashlight.SetActive(!flashlight.activeSelf);
            elapsedTime += Time.deltaTime * 10;
        }
    }
}

