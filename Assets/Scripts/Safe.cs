using UnityEngine;

public class Safe : MonoBehaviour
{
    public string FourDigitCode;
    private string enteredCode;
    public Animation Anim;
    public LayerMask SafeButtonLayerMask;
    public Material DefaultMat;
    public Material HighlightMat;
    public Material PressedMat;

    private Ray ray;
    private RaycastHit hit;
    private Collider lastButtonCollider;
    private bool pressingButton = false;

    private void Update()
    {
        if (!pressingButton)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, SafeButtonLayerMask))
            {
                if (lastButtonCollider != null && lastButtonCollider != hit.collider)
                {
                    lastButtonCollider.GetComponent<Renderer>().material = DefaultMat;
                    lastButtonCollider = null;
                }
                if (lastButtonCollider == null)
                {
                    lastButtonCollider = hit.collider;
                    lastButtonCollider.GetComponent<Renderer>().material = HighlightMat;
                }
                if (Input.GetMouseButtonDown(0))
                {
                    lastButtonCollider.GetComponent<Renderer>().material = PressedMat;
                    Anim.Play(lastButtonCollider.name);
                    pressingButton = true;
                }
            }
            else if (lastButtonCollider != null)
            {
                lastButtonCollider.GetComponent<Renderer>().material = DefaultMat;
                lastButtonCollider = null;
            }
        }
    }

    public void OnFinishedButtonPress()
    {
        lastButtonCollider.GetComponent<Renderer>().material = DefaultMat;
        enteredCode += lastButtonCollider.name;
        if (enteredCode.Length == 4)
        {
            if (enteredCode == FourDigitCode)
            {
                Anim.Play("CorrectCode");
            }
            else
            {
                Anim.Play("IncorrectCode");
            }
        }
        else
        {
            pressingButton = false;
        }
    }

    public void OnFinishedIncorrectCode()
    {
        enteredCode = "";
        pressingButton = false;
    }

    public void OnFinishedCorrectCode()
    {
        Anim.Play("Open");
        GetComponent<Interactable>().LerpCamToOrigPos(true);
        enabled = false;
    }
}
