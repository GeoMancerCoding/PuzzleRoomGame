using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject Indicator;
    public Transform InteractableCamPosT;
    public Transform InteractableFocusPosT;
    public MonoBehaviour InteractableBehaviour;
    private PlayerMoveControl player;

    public float LerpDurationSecs = 1f;

    private string playerTag = "Player";
    private Vector3 origCamPos;
    private Quaternion origCamRot;

    public bool IsVisible(Transform cameraT)
    {
        Vector3 direction = (transform.position - cameraT.transform.position).normalized;
        return Vector3.Dot(cameraT.forward, direction) > 0;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == playerTag)
        {
            Indicator.SetActive(true);
            collider.gameObject.GetComponent<PlayerMoveControl>().SetNearbyInteractable(this);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == playerTag)
        {
            Indicator.SetActive(false);
            collider.gameObject.GetComponent<PlayerMoveControl>().ClearNearbyInteractable();
        }
    }

    public void LerpCamToPos(PlayerMoveControl _player)
    {
        player = _player;
        Indicator.SetActive(false);
        StartCoroutine(LerpCamToPosCoroutine(player));
    }

    private IEnumerator LerpCamToPosCoroutine(PlayerMoveControl player)
    {
        origCamPos = player.CameraT.position;
        origCamRot = player.CameraT.rotation;
        float elapsedTime = 0f;
        while (elapsedTime < LerpDurationSecs)
        {
            elapsedTime += Time.fixedDeltaTime;
            player.CameraT.position = Vector3.Lerp(origCamPos, InteractableCamPosT.position, elapsedTime / LerpDurationSecs);
            player.CameraT.rotation = Quaternion.Lerp(origCamRot, InteractableCamPosT.rotation, elapsedTime / LerpDurationSecs);
            yield return null;
        }
        player.CameraT.position = InteractableCamPosT.position;
        player.CameraT.rotation = InteractableCamPosT.rotation;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        InteractableBehaviour.enabled = true;
        player.EnableInteraction();
        yield return null;
    }

    public void LerpCamToOrigPos(bool DestroyOnFinish = false)
    {
        StartCoroutine(LerpCamToOrigPosCoroutine(DestroyOnFinish));
    }

    private IEnumerator LerpCamToOrigPosCoroutine(bool DestroyOnFinish = false)
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        InteractableBehaviour.enabled = false;
        float elapsedTime = 0f;
        while (elapsedTime < LerpDurationSecs)
        {
            elapsedTime += Time.fixedDeltaTime;
            player.CameraT.position = Vector3.Lerp(InteractableCamPosT.position, origCamPos, elapsedTime / LerpDurationSecs);
            player.CameraT.rotation = Quaternion.Lerp(InteractableCamPosT.rotation, origCamRot, elapsedTime / LerpDurationSecs);
            yield return null;
        }
        player.CameraT.position = origCamPos;
        player.CameraT.rotation = origCamRot;
        player.StopInteracting();
        player.EnableInteraction();
        player.EnableMovement();
        player.EnableLook();
        player.EnableCapsule();
        if (DestroyOnFinish)
        {
            Indicator.SetActive(false);
            Destroy(this);
        }
        else
        {
            Indicator.SetActive(true);
        }
        yield return null;
    }
}
