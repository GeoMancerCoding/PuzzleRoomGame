using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public Transform CameraT;
    private bool interacting = false;
    private bool canInteract = true;
    private bool freezeMovement = false;
    private Interactable nearbyInteractable;

    private void Update()
    {
        if (canInteract)
        {
            if (Input.GetKeyDown(KeyCode.E) && nearbyInteractable != null && nearbyInteractable.IsVisible(CameraT) && !interacting)
            {
                freezeMovement = true;
                CameraT.GetComponent<FirstPersonLook>().enabled = false;
                canInteract = false;
                interacting = true;
                _rigidbody.velocity = Vector3.zero;
                _rigidbody.angularVelocity = Vector3.zero;
                //nearbyInteractable.LerpCamToPos(this);
            }
            else if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space)) && interacting)
            {
                canInteract = false;
                nearbyInteractable.LerpCamToOrigPos();
            }
        }
    }

    public void EnableMovement()
    {
        freezeMovement = false;
    }

    public void EnableInteraction()
    {
        canInteract = true;
    }

    public void EnableLook()
    {
        CameraT.GetComponent<FirstPersonLook>().enabled = true;
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
    
    #region FPSMinimalContent
    public float speed = 5;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;

    Rigidbody _rigidbody;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    

    void Awake()
    {
        // Get the rigidbody on this.
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!freezeMovement)
        {
            // Update IsRunning from input.
            IsRunning = canRun && Input.GetKey(runningKey);

            // Get targetMovingSpeed.
            float targetMovingSpeed = IsRunning ? runSpeed : speed;
            if (speedOverrides.Count > 0)
            {
                targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
            }

            // Get targetVelocity from input.
            Vector2 targetVelocity = new Vector2(Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

            // Apply movement.
            _rigidbody.velocity = transform.rotation * new Vector3(targetVelocity.x, _rigidbody.velocity.y, targetVelocity.y);
        }

    }
    #endregion
}