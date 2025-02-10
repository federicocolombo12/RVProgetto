using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChiudiPortaElettroshock : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] DoorFinale doorFinale;
    void Start()
    {
        doorFinale = FindObjectOfType<DoorFinale>();
    }
    private void OnTriggerEnter(Collider other)
    {
        doorFinale.ChiudiPorta();
    }
}
