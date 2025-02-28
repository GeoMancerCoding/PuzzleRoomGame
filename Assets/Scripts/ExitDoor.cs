using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    public Animation Anim;
    public BoxCollider collider;

    private void OnEnable()
    {
        PlayerMoveControl player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMoveControl>();
        if (player.CarryingKey)
        {
            Anim.Play("OpenDoor");
            collider.enabled = false;
        }
    }

    public void OnFinishOpeningDoor()
    {
        GetComponent<Interactable>().LerpCamToOrigPos(true);
        collider.enabled = true;
    }
}
