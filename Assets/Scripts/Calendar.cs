using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calendar : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;
    public LayerMask CalendarTargetLayerMask;
    public float TimeUntilColorStartsToChangeSecs = 3f;
    public float ColorChangeDurationSecs = 4f;
    private float elapsedTime = 0f;
    private float elapsedColorChangeTime = 0f;
    public Renderer March17thRenderer;
    public Transform PaintingT;
    public Interactable SafeInteractable;

    public AudioSource clock;
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, CalendarTargetLayerMask))
        {
            elapsedTime += Time.fixedDeltaTime;
            if (elapsedTime > TimeUntilColorStartsToChangeSecs)
            {
                elapsedColorChangeTime += Time.fixedDeltaTime;
                March17thRenderer.material.color = new Color(
                    March17thRenderer.material.color.r,
                    March17thRenderer.material.color.g,
                    March17thRenderer.material.color.b,
                    elapsedColorChangeTime / ColorChangeDurationSecs);
                if (elapsedColorChangeTime > ColorChangeDurationSecs)
                {
                    GetComponent<Interactable>().LookAtPointOfInterest(PaintingT, true);
                    SafeInteractable.CanBeInteractedWith = true;
                    SafeInteractable.GetComponent<Animation>().Play("MovePainting");
                    clock.Play();
                }
            }
        }
        else
        {
            elapsedTime = 0f;
            elapsedColorChangeTime = 0f;
        }
    }
}
