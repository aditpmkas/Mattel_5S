using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class SimpleFPController : MonoBehaviour
{
    [Header("MOUSE LOOK")]
    public Vector2 mouseSensitivity = new Vector2(20, 20);
    public Vector2 verticalLookLimit = new Vector2(-85, 85);
    public float smooth = 0.05f;

    private float xRot;
    private Camera cam;

    [Header("MOVEMENT")]
    public bool physicsController = false;
    public float walkSpeed = 1;
    public float runSpeed = 3;
    //public float jumpForce = 2;
    private float speed = 1;

#if ENABLE_INPUT_SYSTEM
    [Header("CONTROLS")]
    public Key forward = Key.W;
    public Key backward = Key.S;
    public Key strafeLeft = Key.A;
    public Key strafeRight = Key.D;
    public Key run = Key.LeftShift;
    //public Key jump = Key.Space;
#else
[Header("CONTROLS")]
    public KeyCode forward = KeyCode.W;
    public KeyCode backward = KeyCode.S;
    public KeyCode strafeLeft = KeyCode.A;
    public KeyCode strafeRight = KeyCode.D;
    public KeyCode run = KeyCode.LeftShift;
    //public KeyCode jump = KeyCode.Space;
#endif

    [Header("SIGHT")]
    public bool sight = false;
    public GameObject sightPrefab;

    //private Rigidbody rb;

    public bool hideCursor = false;

    private bool forwardMove = false;
    private bool backwardMove = false;
    private bool leftMove = false;
    private bool rightMove = false;

    private CharacterController controller;

    private void OnDisable()
    {
        Cursor.visible = true;
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = GetComponentInChildren<Camera>();
        //rb = GetComponent<Rigidbody>();

        if (hideCursor)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (sight)
        {
            GameObject sightObj = Instantiate(sightPrefab);
            sightObj.transform.SetParent(transform.parent);
        }
    }

    void Update()
    {
        CameraLook();

        PlayerMove();
    }

    float refVelX;
    float refVelY;
    float xRotSmooth;
    float yRotSmooth;

    void CameraLook()
    {
#if ENABLE_INPUT_SYSTEM
        float mouseX = Mouse.current.delta.x.ReadValue() * Time.deltaTime * mouseSensitivity.x;
        float mouseY = Mouse.current.delta.y.ReadValue() * Time.deltaTime * mouseSensitivity.y;
#else
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity.x * 10;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity.y * 10;
#endif

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, verticalLookLimit.x, verticalLookLimit.y);

        xRotSmooth = Mathf.SmoothDamp(xRotSmooth, xRot, ref refVelX, smooth);
        yRotSmooth = Mathf.SmoothDamp(yRotSmooth, mouseX, ref refVelY, smooth);

        cam.transform.localEulerAngles = new Vector3(xRotSmooth, 0, 0);
        transform.Rotate(Vector3.up * yRotSmooth);
    }

    void PlayerMove()
    {
#if ENABLE_INPUT_SYSTEM

        if (Keyboard.current[run].isPressed)
        {
            speed = runSpeed;
        }
        else
        {
            speed = walkSpeed;
        }

        if (Keyboard.current[forward].isPressed)
        {
            forwardMove = true;
        }
        else
        {
            forwardMove = false;
        }

        if (Keyboard.current[backward].isPressed)
        {
            backwardMove = true;
        }
        else
        {
            backwardMove = false;
        }

        if (Keyboard.current[strafeLeft].isPressed)
        {
            leftMove = true;
        }
        else
        {
            leftMove = false;
        }
        if (Keyboard.current[strafeRight].isPressed)
        {
            rightMove = true;
        }
        else
        {
            rightMove = false;
        }
#else
        if (Input.GetKey(run))
        {
            speed = runSpeed;
        }
        else
        {
            speed = walkSpeed;
        }

        if (Input.GetKey(forward))
        {
            forwardMove = true;
        }
        else
        {
            forwardMove = false;
        }

        if (Input.GetKey(backward))
        {
            backwardMove = true;
        }
        else
        {
            backwardMove = false;
        }

        if (Input.GetKey(strafeLeft))
        {
            leftMove = true;
        }
        else
        {
            leftMove = false;
        }
        if (Input.GetKey(strafeRight))
        {
            rightMove = true;
        }
        else
        {
            rightMove = false;
        }
#endif
    }

    private void FixedUpdate()
    {
        if (forwardMove)
        {
            controller.Move(controller.transform.forward * speed * 0.01f);
        }

        if (backwardMove)
        {
            controller.Move(controller.transform.forward * -speed * 0.01f);
        }

        if (leftMove)
        {
            controller.Move(controller.transform.right * -speed * 0.01f);
        }
        if (rightMove)
        {
            controller.Move(controller.transform.right * speed * 0.01f);
        }

        if (controller.isGrounded) return;

        if(Physics.SphereCast(transform.position, controller.radius, -transform.up, out RaycastHit hitInfo, 10, -1, QueryTriggerInteraction.Ignore))
        {
            transform.position = new Vector3(transform.position.x, hitInfo.point.y + controller.height / 2 + controller.skinWidth, transform.position.z);
        }
    }
}
