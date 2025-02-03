using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiTorcia : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Animator anim;
    void Start()
    {
        anim= GetComponent<Animator>();
    }

    // Update is called once per frame
    public void OnTorchFound()
    {
        anim.SetTrigger("TorchFound");
    }
}
