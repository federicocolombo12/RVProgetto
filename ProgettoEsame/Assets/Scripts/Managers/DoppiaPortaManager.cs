using UnityEngine;
using System.Collections;

public class DoppiaPortaController : MonoBehaviour
{
    public Transform portaSinistra;
    public Transform portaDestra;

    private bool isOpen = false;
    private bool isAnimating = false;

    private Vector3 sinistraAperta = new Vector3(0, 50, 0);
    private Vector3 sinistraChiusa = Vector3.zero;

    private Vector3 destraAperta = new Vector3(0, 120, 0);
    private Vector3 destraChiusa = Vector3.zero;

    public float speed = 2f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isAnimating) // Tasto per aprire/chiudere la porta
        {
            ToggleDoor();
        }
    }

    public void ToggleDoor()
    {
        if (!isAnimating)
        {
            isOpen = !isOpen;
            StopAllCoroutines();
            StartCoroutine(MuoviPorta());
        }
    }

    private IEnumerator MuoviPorta()
    {
        isAnimating = true;
        float elapsedTime = 0;
        Vector3 sinistraTarget = isOpen ? sinistraAperta : sinistraChiusa;
        Vector3 destraTarget = isOpen ? destraAperta : destraChiusa;

        Vector3 sinistraStart = portaSinistra.localEulerAngles;
        Vector3 destraStart = portaDestra.localEulerAngles;

        while (elapsedTime < speed)
        {
            elapsedTime += Time.deltaTime;
            portaSinistra.localEulerAngles = Vector3.Lerp(sinistraStart, sinistraTarget, elapsedTime / speed);
            portaDestra.localEulerAngles = Vector3.Lerp(destraStart, destraTarget, elapsedTime / speed);
            yield return null;
        }

        portaSinistra.localEulerAngles = sinistraTarget;
        portaDestra.localEulerAngles = destraTarget;
        isAnimating = false;
    }
}
