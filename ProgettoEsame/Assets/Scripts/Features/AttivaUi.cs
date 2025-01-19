using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttivaUi : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject canvasVicinanza;
    [SerializeField] private GameObject canvasVedi;
    [SerializeField] private GameObject canvasEsci;
    public bool vedi = false;
    // Update is called once per frame
    void Update()
    {
        
        UiManager.instance.AttivaVicinanza(canvasVicinanza, this.gameObject);
        if (!vedi)
        {
            canvasVedi.SetActive(false);
        }



    }
    public void Vedi()
    {

       UiManager.instance.AttivaVedi(canvasVedi);
        canvasEsci.SetActive(false);
        canvasVicinanza.SetActive(false);
        
    }
    public void Esci()
    {
        UiManager.instance.AttivaEsci(canvasEsci);
        canvasVedi.SetActive(false);
        canvasVicinanza.SetActive(false);
    }
    
}
