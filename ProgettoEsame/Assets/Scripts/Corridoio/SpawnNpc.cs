using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNpc : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject npcPrefab;
    public void InstantiateNpc()
    {
        Instantiate(npcPrefab, transform.position, transform.rotation);
    }
}
