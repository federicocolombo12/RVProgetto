using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttivaUi : MonoBehaviour
{
    // Start is called before the first frame update

   
    public bool vedi = false;
    // Update is called once per frame
    void Update()
    {
        
        UiManager.instance.AttivaVicinanza(this.gameObject);
     



    }
    public void Vedi()
    {

       UiManager.instance.AttivaVedi();
        
        
        
    }
    public void Esci()
    {
        UiManager.instance.AttivaEsci();
      
    }
    
}
