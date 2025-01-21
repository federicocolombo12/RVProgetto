using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCelleInteractionManager : MonoBehaviour
{
    public static NpcCelleInteractionManager instance;
    public Camera playerCamera;
    public float interactionDistance = 2f;
    public bool paziente0Interaction = false;
    public bool paziente2Interaction = false;
    public bool paziente3Interaction = false;
    private Paziente0Script paziente0Script;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        paziente0Script = FindObjectOfType<Paziente0Script>();
        if (paziente0Script == null)
        {
            Debug.LogError("Paziente0Script non trovato nella scena!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactionDistance))
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("NPC"))
                {
                    Debug.Log("Giocatore ha interagito con un NPC");
                    LookAtPlayer(hit.transform);

                    if (hit.transform.CompareTag("Paziente0"))
                    {
                        Debug.Log("Giocatore ha interagito con il Paziente 0");
                        paziente0Interaction = true;
                    }
                    else if (hit.transform.CompareTag("Paziente2"))
                    {
                        Debug.Log("Giocatore ha interagito con il Paziente 2");
                        paziente2Interaction = true;
                    }
                    else if (hit.transform.CompareTag("Paziente3"))
                    {
                        Debug.Log("Giocatore ha interagito con il Paziente 3");
                        paziente3Interaction = true;
                    }
                }
            }
        }
    }



    private void LookAtPlayer(Transform npcTransform)
    {
        Vector3 direction = (playerCamera.transform.position - npcTransform.position).normalized;
        direction.y = 0; // Mantieni la rotazione solo sull'asse Y
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        npcTransform.rotation = lookRotation;
    }
}
