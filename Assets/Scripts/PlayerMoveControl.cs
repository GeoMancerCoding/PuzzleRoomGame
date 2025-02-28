using UnityEngine;
using System.Collections;
using System.Linq.Expressions;

public class PlayerMoveControl : MonoBehaviour
{
    public float walkingSpeed;
    public float gravity;
    public Camera playerCamera;
    public GameObject playerCapsule;
    public float lookSpeed;
    public float lookXLimit;
    public float YPOS;
    public float scale;
    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
    public bool canMove;
    public bool canLookAround;
    public Transform cameraPoint;
    InteractableVariables InterVaris;
    bool inInteractArea;

    private bool interacting = false;
    private bool canInteract = true;
    private bool inspecting = false;
    private Interactable nearbyInteractable;
    public Transform CameraT;
    private Pickup nearbyPickup;
    public Transform InspectPointT;
    public Transform InventoryPointT;
    public float LerpObjectToInspectPointDurationSecs = 1f;
    public float InspectionRotationSpeed = 2f;
    public float LerpObjectToInventoryDurationSecs = 1f;
    public GameObject FocusCamera;
    private Quaternion origLookRotBeforeInspection;
    public GameObject InspectionUICanvas;
    public UIScript uiScript;
    public PlayerVO PlayerVO;

    public AudioSource walk;
    public float cooldownTime = 2f;

    private bool inCooldown;

    public bool CarryingOneSnakePiece = false;
    public bool CarryingTwoSnakesPiece = false;
    public bool CarryingHeartPiece = false;
    public bool CarryingKey = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        transform.localScale = new Vector3(scale, scale, scale);
        canLookAround = true;
    }

    void Update()
    {
        if (canMove)
        {
            YPOS = transform.position.y;
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            float curSpeedX = canMove ? (walkingSpeed) * Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ? (walkingSpeed) * Input.GetAxis("Horizontal") : 0;
            float movementDirectionY = moveDirection.y;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);
            if (canMove && characterController.isGrounded == false)
            {
                moveDirection.y = movementDirectionY;
            }
            if (!characterController.isGrounded)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }
            characterController.Move(moveDirection * Time.deltaTime);
            if (canLookAround == true)
            {
                rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            }
            if (Input.GetKey(KeyCode.W))
            {
                if (!inCooldown)
                {
                    walk.Play();
                    StartCoroutine("walkDelay");
                }

            }
            if (Input.GetKey(KeyCode.A))
            {
                if (!inCooldown)
                {
                    walk.Play();
                    StartCoroutine("walkDelay");
                }

            }
            if (Input.GetKey(KeyCode.S))
            {
                if (!inCooldown)
                {
                    walk.Play();
                    StartCoroutine("walkDelay");
                }

            }
            if (Input.GetKey(KeyCode.D))                                                    
            {
                if (!inCooldown)
                {
                    walk.Play();
                    StartCoroutine("walkDelay");
                }

            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (canInteract)
        {
            if (Input.GetKeyDown(KeyCode.E) && nearbyInteractable != null && nearbyInteractable.IsVisible(CameraT) && !interacting)
            {
                playerCapsule.SetActive(false);
                canMove = false;
                canLookAround = false;
                canInteract = false;
                interacting = true;
                characterController.Move(Vector3.zero);
                nearbyInteractable.LerpCamToPos(this);
            }
            else if (Input.GetKeyDown(KeyCode.E) && nearbyPickup != null && nearbyPickup.IsVisible(CameraT) && !inspecting)
            {
                PickUpObject();
            }
            else if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space)) && interacting && !inspecting)
            {
                canInteract = false;
                nearbyInteractable.LerpCamToOrigPos();
            }
        }
    }

    public void PickUpObject()
    {
        if (nearbyPickup.ToBeInspected == true)
        {
            nearbyPickup.CanBePickedUp = false;
            inspecting = true;
            nearbyPickup.HideIndicator();
            canMove = false;
            canLookAround = false;
            canInteract = false;
            StartCoroutine(LerpObjectToInspectPoint());
            nearbyPickup.enabled = false;
        }
    }

    private IEnumerator LerpObjectToInspectPoint()
    {
        origLookRotBeforeInspection = CameraT.transform.rotation;
        InspectPointT.localPosition = new Vector3(
            InspectPointT.localPosition.x,
            InspectPointT.localPosition.y,
            nearbyPickup.InspectZDistance);
        Vector3 origPos = nearbyPickup.transform.position;
        Quaternion origRot = nearbyPickup.transform.rotation;
        float elapsedTime = 0f;
        while (elapsedTime < LerpObjectToInspectPointDurationSecs)
        {
            elapsedTime += Time.fixedDeltaTime;
            nearbyPickup.transform.position = Vector3.Lerp(origPos, InspectPointT.position, elapsedTime / LerpObjectToInspectPointDurationSecs);
            nearbyPickup.transform.rotation = Quaternion.Lerp(origRot, InspectPointT.rotation, elapsedTime / LerpObjectToInspectPointDurationSecs);
            CameraT.LookAt(nearbyPickup.transform.position);
            yield return null;
        }
        nearbyPickup.HideIndicator();
        nearbyPickup.transform.position = InspectPointT.position;
        nearbyPickup.transform.rotation = InspectPointT.rotation;
        CameraT.LookAt(InspectPointT.position);
        StartCoroutine(InspectObject());
        yield return null;
    }

    private IEnumerator InspectObject()
    {
        switch (nearbyPickup.gameObject.name)
        {
            case "Sethascope":
                PlayerVO.PlayStethoscopeVO();
                break;
            case "HeartPuzzlePiece":
                PlayerVO.PlayerHeartPuzzlePieceVO();
                break;
            case "OneSnakePuzzlePiece":
                PlayerVO.PlayOneSnakePuzzlePieceVO();
                break;
            case "TwoSnakesPuzzlePiece":
                PlayerVO.PlayTwoSnakesPuzzlePieceVO();
                break;
            case "RoundPills":
                PlayerVO.PlayPillBottleVO();
                break;
            case "Key":
                PlayerVO.PlayKeyVO();
                break;
            default:
                break;
        }
        InspectionUICanvas.SetActive(true);
        foreach (Renderer renderer in nearbyPickup.Renderers)
        {
            renderer.gameObject.layer = LayerMask.NameToLayer("Focused");
        }
        FocusCamera.SetActive(true);
        while (!Input.GetKeyDown(KeyCode.E))
        {
            nearbyPickup.transform.Rotate(
                new Vector3(
                    Input.GetAxis("Mouse Y"),
                    -Input.GetAxis("Mouse X"),
                    0) * Time.fixedDeltaTime * InspectionRotationSpeed);
            yield return null;
        }
        InspectionUICanvas.SetActive(false);
        foreach (Renderer renderer in nearbyPickup.Renderers)
        {
            renderer.gameObject.layer = LayerMask.NameToLayer("Default");
        }
        FocusCamera.SetActive(false);
        StartCoroutine(LerpObjectToInventory());
        yield return null;
    }

    private IEnumerator LerpObjectToInventory()
    {
        Vector3 origPos = nearbyPickup.transform.position;
        Quaternion origRot = nearbyPickup.transform.rotation;
        Quaternion lookRot = CameraT.transform.rotation;
        float elapsedTime = 0f;
        while (elapsedTime < LerpObjectToInventoryDurationSecs)
        {
            elapsedTime += Time.fixedDeltaTime;
            if (nearbyPickup == null)
            {
                break;
            }
            else
            {
                nearbyPickup.transform.position = Vector3.Lerp(origPos, InventoryPointT.position, elapsedTime / LerpObjectToInventoryDurationSecs);
                nearbyPickup.transform.rotation = Quaternion.Lerp(origRot, InventoryPointT.rotation, elapsedTime / LerpObjectToInventoryDurationSecs);
                CameraT.transform.rotation = Quaternion.Lerp(lookRot, origLookRotBeforeInspection, elapsedTime / LerpObjectToInventoryDurationSecs);
            }
            yield return null;
        }
        if (nearbyPickup != null)
        {
            if (nearbyPickup.gameObject.name == "HeartPuzzlePiece")
            {
                CarryingHeartPiece = true;
                uiScript.HeartPiece.SetActive(true);
            }
            else if (nearbyPickup.gameObject.name == "OneSnakePuzzlePiece")
            {
                CarryingOneSnakePiece = true;
                uiScript.OneSnakePiece.SetActive(true);
            }
            else if (nearbyPickup.gameObject.name == "TwoSnakesPuzzlePiece")
            {
                CarryingTwoSnakesPiece = true;
                uiScript.TwoSnakePiece.SetActive(true);
            }
            else if (nearbyPickup.gameObject.name == "Key")
            {
                CarryingKey = true;
            }
            Destroy(nearbyPickup.gameObject);
        }
        CameraT.transform.rotation = origLookRotBeforeInspection;
        canMove = true;
        canLookAround = true;
        canInteract = true;
        inspecting = false;
        yield return null;
    }
    
    public void EnableMovement()
    {
        canMove = true;
    }

    public void EnableInteraction()
    {
        canInteract = true;
    }

    public void EnableLook()
    {
        canLookAround = true;
    }

    public void StopInteracting()
    {
        interacting = false;
    }

    public void SetNearbyInteractable(Interactable interactable)
    {
        nearbyInteractable = interactable;
    }

    public void ClearNearbyInteractable()
    {
        nearbyInteractable = null;
    }

    public void SetNearbyPickup(Pickup pickup)
    {
        nearbyPickup = pickup;
    }

    public void ClearNearbyPickup()
    {
        nearbyPickup = null;
    }

    public void EnableCapsule()
    {
        playerCapsule.SetActive(true);
    }

    IEnumerator walkDelay()
    {
        inCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        inCooldown = false;
    }
}