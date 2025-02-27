using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
    public bool CanBePickedUp = true;
    public bool ToBeInspected = true;
    public float InspectZDistance = 0.5f;
    public GameObject Indicator;
    public BoxCollider boxCollider;
    private string playerTag = "Player";
    public Renderer[] Renderers;
    public RawImage PuzzleImage;

    private void OnTriggerEnter(Collider collider)
    {
        if (CanBePickedUp && collider.gameObject.tag == playerTag)
        {
            Indicator.SetActive(true);
            collider.gameObject.GetComponent<PlayerMoveControl>().SetNearbyPickup(this);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (CanBePickedUp && collider.gameObject.tag == playerTag)
        {
            Indicator.SetActive(false);
            collider.gameObject.GetComponent<PlayerMoveControl>().ClearNearbyPickup();
        }
    }

    public bool IsVisible(Transform cameraT)
    {
        Vector3 direction = (transform.position - cameraT.transform.position).normalized;
        return Vector3.Dot(cameraT.forward, direction) > 0;
    }

    public void Enable()
    {
        CanBePickedUp = true;
        //Indicator.SetActive(true);
        GetComponent<Collider>().enabled = true;
    }

    public void HideIndicator()
    {
        Indicator.SetActive(false);
    }
}
