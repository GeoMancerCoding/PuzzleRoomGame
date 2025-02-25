using UnityEngine;

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
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.E) && (inInteractArea || canLookAround == false))
        {
            if (canLookAround == true)
            {
                canLookAround = false;
                canMove = false;
                playerCamera.transform.position = InterVaris.CameraPoint.position;
                playerCamera.transform.rotation = InterVaris.CameraPoint.rotation;
                playerCapsule.SetActive(false);
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
            else
            {
                canMove = true;
                canLookAround = true;
                playerCapsule.SetActive(true);
                playerCamera.transform.position = cameraPoint.position;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PuzzleInteractable")
        {
            inInteractArea = true;
            InterVaris = other.GetComponent<InteractableVariables>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PuzzleInteractable")
        {
            inInteractArea = false;
            InterVaris = null;
        }
    }
}