using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class my_player_control : MonoBehaviour
{
    public PlayerController playerControl;
    Vector3 moveDirection = Vector3.zero;
    public CharacterController rb;
    public float speed = 5f;
    // Start is called before the first frame update
    public InputAction move;
    public InputAction aim;
    public CharacterController cont;
    public Camera cam;
    public Animator anim;

    private int _animIDSpeed;
    private int _animIDGrounded;
    private int _animIDJump;
    private int _animIDFreeFall;
    private int _animIDMotionSpeed;


    private RaycastHit hit;
    private int layerMask;

    public bool Grounded = true;

    private void OnEnable()
    {
        move = playerControl.Player.Move;
        aim = playerControl.Player.Look;
        aim.Enable();
        move.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        aim.Disable();
    }
    private void Awake()
    {
        playerControl = new PlayerController();
        layerMask = LayerMask.GetMask("mousePlane");
    }
    void Start()
    {
        AssignAnimationIDs();
        rb = GetComponent<CharacterController>();
        anim.SetBool(_animIDGrounded, Grounded);
    }

    // Update is called once per frame
    void Update()
    {

        moveDirection = move.ReadValue<Vector2>();

        Vector2 mousePosition = aim.ReadValue<Vector2>();
        Vector3 projectedMousePosition = cam.ScreenToWorldPoint(mousePosition);
        Ray ray = cam.ScreenPointToRay(mousePosition);
        Vector3 mouseWorldPosition = new Vector3(projectedMousePosition.x,projectedMousePosition.y,projectedMousePosition.z);
        Debug.Log(projectedMousePosition);
        if (Physics.Raycast(ray, out hit, layerMask))
        {
            mouseWorldPosition = hit.point;
        }

        // Calculate the difference between the positions
        
        var positionVector = mouseWorldPosition - transform.position;
        
        // Match the new y value to the object's Y value.
        // This ensures that the rotation is calculated only with the X and Z
        // I would love to know why this is happening... but I didn't find anything in my initial research
        positionVector.y = transform.position.y;

        // Now we calculate the rotation
        var targetRotation = Quaternion.LookRotation(positionVector);

        // FYI, if your object's final rotation is off by 90 degrees, you can do the following
        // I think it has to do with what the system thinks "forward" is, and which way your model is facing by default.
        // So you can either fix it in your model, or add/subtract 90 degrees
        // Note that **multiplying** Quaternions together effectively **combines** them
        // var targetRotation = Quaternion.LookRotation(positionVector) * Quaternion.Euler(0, -90, 0);

        // And smoothly transition to the new angle using Slerp
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 100 * Time.deltaTime);

    }
    private void FixedUpdate()
    {
        rb.Move(new Vector3(moveDirection.x * (speed/1000), 0, moveDirection.y * (speed/1000)));
        anim.SetFloat(_animIDSpeed, Mathf.Abs(Mathf.Abs(moveDirection.x * speed) + Mathf.Abs(moveDirection.y * speed) + Mathf.Abs(moveDirection.z * speed)));
        
    }
    private void AssignAnimationIDs()
    {
        _animIDSpeed = Animator.StringToHash("Speed");
        _animIDGrounded = Animator.StringToHash("Grounded");
        _animIDJump = Animator.StringToHash("Jump");
        _animIDFreeFall = Animator.StringToHash("FreeFall");
        _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
    }
}
