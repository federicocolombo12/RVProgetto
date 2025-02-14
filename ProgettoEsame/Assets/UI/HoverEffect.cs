using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Aggiungi questa libreria per gestire eventi UI

public class HoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Text buttonText; // Riferimento al testo del bottone
    public float fontSizeHover = 24f; // Dimensione del testo durante l'hover
    public float fontSizeNormal = 18f; // Dimensione normale del testo

    void Start()
    {
        if (buttonText == null)
            buttonText = GetComponentInChildren<Text>(); // Ottiene automaticamente il componente Text
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.fontSize = (int)fontSizeHover; // Aumenta la dimensione del testo
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.fontSize = (int)fontSizeNormal; // Ripristina la dimensione normale del testo
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Bottone cliccato!"); // Stampa un messaggio nella console
        // Qui puoi aggiungere altre azioni da eseguire al click
    }
}
