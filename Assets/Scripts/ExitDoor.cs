using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    public Animation Anim;
    public BoxCollider doorCollider;

    private void OnEnable()
    {
        PlayerMoveControl player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMoveControl>();
        if (player.CarryingKey)
        {
            Anim.Play("OpenDoor");
            doorCollider.enabled = false;
        }
    }

    public void OnFinishOpeningDoor()
    {
        GetComponent<Interactable>().LerpCamToOrigPos(true);
        doorCollider.enabled = true;
    }
}
