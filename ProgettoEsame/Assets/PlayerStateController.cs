using UnityEngine;
using System.Collections;

public interface IPlayerState
{
    void EnterState(PlayerStateController player);
    void UpdateState(PlayerStateController player);
    void ExitState(PlayerStateController player);
}

public class PlayerStateController : MonoBehaviour
{
    private IPlayerState currentState;
    public Camera playerCamera;
    public FirstPersonController firstPersonController;
    public GameObject standingPos;


    private void Start()
    {
        // Imposta la posizione della telecamera in modo che sia bassa
        playerCamera.transform.position = new Vector3(playerCamera.transform.position.x, 1f, playerCamera.transform.position.z);

        // Imposta la rotazione della telecamera in modo che guardi verso l'alto
        playerCamera.transform.rotation = Quaternion.Euler(90, 0, 0);

        // Imposta lo stato iniziale
        SetState(new InBedState());
    }

    private void Update()
    {
        // Aggiorna lo stato corrente
        currentState?.UpdateState(this);
    }

    public void SetState(IPlayerState newState)
    {
        currentState?.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }

    // Metodi per cambiare stato
    public void GoToBed()
    {
        SetState(new InBedState());
    }

    public void StandUp()
    {
        SetState(new StandingState());
    }

    public void TalkToNPC()
    {
        SetState(new TalkingState());
    }

    public IEnumerator AnimateCameraPosition(Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = playerCamera.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            playerCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        playerCamera.transform.position = targetPosition;
    }
}

// InBedState.cs
public class InBedState : IPlayerState
{
    public void EnterState(PlayerStateController player)
    {
        Debug.Log("Player is now in bed.");
        player.firstPersonController.cameraCanMove = true; // Permetti la rotazione della telecamera
        player.firstPersonController.playerCanMove = false; // Disabilita il movimento del giocatore
        player.playerCamera.transform.rotation = Quaternion.Euler(-90, 0, 0); // Imposta la camera a guardare verso l'alto
    }

    public void UpdateState(PlayerStateController player)
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        player.playerCamera.transform.Rotate(-mouseY, mouseX, 0);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            player.StandUp();
        }
    }

    public void ExitState(PlayerStateController player)
    {
        player.firstPersonController.playerCanMove = true; // Abilita il movimento del giocatore
    }
}

public class StandingState : IPlayerState
{
    public void EnterState(PlayerStateController player)
    {
        Debug.Log("Player is now standing.");

        if (player.standingPos != null)
        {
            // Sposta il FirstPersonController alla posizione del GameObject "standing pos"
            player.firstPersonController.transform.position = player.standingPos.transform.position;
        }
        else
        {
            Debug.LogWarning("GameObject 'standing pos' non assegnato.");
        }

        player.firstPersonController.playerCanMove = true; // Abilita il movimento del giocatore
        player.StartCoroutine(player.AnimateCameraPosition(new Vector3(player.playerCamera.transform.position.x, 1.8f, player.playerCamera.transform.position.z), 1f)); // Anima la posizione della telecamera
    }

    public void UpdateState(PlayerStateController player)
    {
        // Implementa qui eventuali aggiornamenti specifici per lo stato in piedi
    }

    public void ExitState(PlayerStateController player)
    {
        // Implementa qui la logica per uscire dallo stato in piedi, se necessario
    }
}



// TalkingState.cs
public class TalkingState : IPlayerState
{
    public void EnterState(PlayerStateController player)
    {
        Debug.Log("Player is now talking to an NPC.");
        player.firstPersonController.playerCanMove = false; // Disabilita il movimento del giocatore
    }

    public void UpdateState(PlayerStateController player)
    {
        // Implementa qui la logica per gestire la conversazione con un NPC
    }

    public void ExitState(PlayerStateController player)
    {
        player.firstPersonController.playerCanMove = true; // Riabilita il movimento del giocatore
    }
}