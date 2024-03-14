using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    // TO DO: Jump, Basic Movement, Grapple, Double Jump, Grounded Punch, Diving Punch, Wall Jump

    [Header("Player Movement")]
    public float movementSpeed = 15f; // Player's movement speed.
    public float defaultMovementSpeed = 15f; // Default movement speed that will be used to determine movement speed calculations.
    private float horizontalInput; // Player's horizontal input.
    private float verticalInput; // Player's vertical input.
    private Vector3 moveDirection; // Vector3 determining where player is moving.
    private Rigidbody rB; // Player's rigidbody.

    public bool freeze = false; //To stop the player when they grapple
    public bool activeGrapple = false; //To see if the player is grappling at the moment

    [Header("Ground Check")]
    [SerializeField] Transform groundCheck; // Empty GameObject at player's feet that casts a sphere, detecting if the ground layer is stood on.
    public LayerMask groundLayer; // Layer mask determining what is a ground layer.    
    public float gCR = .35f; // Ground Check Radius

    public bool IsGrounded() // Bool that says when the player is on a ground layer- a small sphere is cast at the player's feet to determine if they're standing on solid ground.
    {        
        return Physics.CheckSphere(groundCheck.position, gCR, groundLayer);
    }

    [SerializeField] private float groundDrag = 3f; // The amount of drag experienced when moving on the ground.

    [Header("Air Variables")]
    public float jumpForce = 7.5f; // Force behind a player's jump.
    [SerializeField] private float airMultiplier = 0.2f; // Float that slows players down midair.
    [SerializeField] private int airTimeJumps;
    [SerializeField] private int airTimeJumpsMax;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space; // Jump Key is set to Spacebar by default, but it can be changed in the inspector.
    public KeyCode attackKey = KeyCode.LeftShift; // Attack Key is set to LShift by default, but it can be changed in the inspector.

    [Header("References")]
    public Transform player;
    public Transform playerCharacter;
    public Transform orientation; // The orientation or direction of the player.
    private float rotationSpeed = 10f; // Speed determining player's rotation.
    private GameManager gM; // Script reference to GameManager.
    private GravityScalePhysX gSPX; // Script reference to GravityScalePhysX.
    [SerializeField] Animator m_Animator; // Player animator.
    public RespawnController rC;

    // Start is called before the first frame update
    void Start()
    {
        rB = GetComponent<Rigidbody>(); // Gets the rigidbody.
        rB.freezeRotation = true; // Freeze player's rotation so they don't tilt or fall over.

        // Makes the cursor invisible on screen (will be used in finished release only, not during debugging).
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

        airTimeJumpsMax = 1;        

        gM = GameObject.Find("GameManager").GetComponent<GameManager>(); // Get reference to GameManager script.
        gSPX = GetComponent<GravityScalePhysX>(); // Get reference to GravityScalePhysX script.        
    }

    // Update is called once per frame
    void Update()
    {

        PlayerInput();

        // If the player is on the ground, set the drag on their RB to the groundDrag variable.
        if (IsGrounded() && !activeGrapple)
        {
            rB.drag = groundDrag;
            airTimeJumps = 0;
        }

        // If player is midair, they have no drag when moving.
        else if (!IsGrounded())
        {
            rB.drag = rB.drag;
        }

        if (freeze)
        {
            rB.velocity = Vector3.zero;
        }
    }

    // FixedUpdate is called once per physics update    
    void FixedUpdate()
    {
        PlayerDirection();
        PlayerMovement();
    }

    // PlayerInput determines when players are inputting movement
    void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal"); // Sets the player's horizontal move input.
        verticalInput = Input.GetAxisRaw("Vertical"); // Sets the player's vertical move input.

        // Inputs used to switch between idle animation and moving animation.
        bool hasHorizontalInput = !Mathf.Approximately(horizontalInput, 0f);
        bool hasVerticalInput = !Mathf.Approximately(verticalInput, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool("IsWalking", isWalking);

        // If the player clicked the jump key and they are grounded OR if the player is not grounded but hasn't used a jump midair, let them jump.
        // Pressing the Jump Key will take away one of the jumps the player can do.
        // if (Input.GetKeyDown(jumpKey) && (IsGrounded() || (!IsGrounded() && airTimeJumps < airTimeJumpsMax)))
        if (Input.GetKeyDown(jumpKey) && (IsGrounded() || airTimeJumps < airTimeJumpsMax))
        {
            Jump(); // Makes the player jump.
            
        }

    }

    void PlayerMovement()
    {
        if (activeGrapple) return; //This stops player movement whenever the grapple is happening

        // This Vector3 determines the forward direction of the camera, allowing the player to move in the same direction as the camera.
        // If the camera rotates 180 degress, the player can still press the forward key to go forwards because that is the camera's orientation.
        Vector3 camForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // Set the player's move direction to their input times the camera's transform. This makes the player's movement based on the directon the camera is facing.
        // Doing this ensures that the player is always going in the direction the camera is facing.
        // When the player clicks the key to move right, they are moving right on the screen rather than in the world because their movement is based on the direction the camera is facing.
        moveDirection = camForward * verticalInput + Camera.main.transform.right * horizontalInput;

        if (IsGrounded())
        {
            // Add force to the player's rigidbody by taking the normalized version of their move direction and multiplying it to adjust the speed.
            rB.AddForce(moveDirection.normalized * movementSpeed * 10f, ForceMode.Acceleration);
        }
        else if (!IsGrounded())
        {
            // Add force to the player's rigidbody by taking the normalized version of their move direction and multiplying it to adjust the speed.
            // Multiply by an airMultiplier to determine how much the player can move in the air.
            rB.AddForce(moveDirection.normalized * movementSpeed * 10f * airMultiplier, ForceMode.Acceleration);
        }
    }

    // Limit the player's speed so they can't go too fast.
    void SpeedLimiter()
    {
        // Check to find the velocity the player is traveling at.
        Vector3 flatVelocity = new Vector3(rB.velocity.x, 0f, rB.velocity.z);

        // If the player's velocity is higher than the moveSpeed, limit it to the moveSpeed.
        if (flatVelocity.magnitude > movementSpeed)
        {
            // Set the limited version of the player's velocity to the normalzied version of their current velocity times their moveSpeed.
            Vector3 limitedVelocity = flatVelocity.normalized * movementSpeed;

            // Set the player's rigidbody velocity to the new limited velocity so they aren't going quicker than allowed.
            rB.velocity = new Vector3(limitedVelocity.x, rB.velocity.y, limitedVelocity.z);
        }
    }

    // Function to set the player's direction.
    void PlayerDirection()
    {
        // Calculate the direction from the player to the camera in order to determine which direction is forward.
        Vector3 viewDirection = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);

        // If the viewDirection vector does not equal zero, normalize it. Doing this helps prevent clutter of messages in the console saying the vector equals zero.
        if (viewDirection != Vector3.zero)
        {
            orientation.forward = viewDirection.normalized;
        }
        else
        {
            // After doing the above if statement, the player does not seem rotate. Therefore, use the camera's direction when the viewDirection equals zero.
            orientation.forward = Camera.main.transform.forward;
        }

        // If the player's input direction is not zero, set their forward direction smoothly and use the rotation speed to determine how quickly that occurs.
        if (moveDirection != Vector3.zero)
        {
            playerCharacter.forward = Vector3.Slerp(playerCharacter.forward, moveDirection.normalized, Time.deltaTime * rotationSpeed);
        }
    }

    void Jump()
    {
        // Make sure the player's y velocity is at zero so they always jump the exact same height.
        rB.velocity = new Vector3(rB.velocity.x, 0f, rB.velocity.z);

        if (airTimeJumps < airTimeJumpsMax)
        {
            // Perform the player's jump by adding upwards force to their rigidbody.
            rB.AddForce(transform.up * jumpForce, ForceMode.VelocityChange);
        }        

        airTimeJumps++;

    }

    /* void Grapple()
    {

    }

    void Attack()
    {

    } */

    public Vector3 CalculateJumpVelocity(Vector3 startPoint, Vector3 endPoint, float trajectoryHeight)
    {
        float gravity = Physics.gravity.y;
        float displacementY = endPoint.y - startPoint.y;
        Vector3 displacementXZ = new Vector3(endPoint.x - startPoint.x, 0f, endPoint.z - startPoint.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * trajectoryHeight);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * trajectoryHeight / gravity)
            + Mathf.Sqrt(2 * (displacementY - trajectoryHeight) / gravity));

        return velocityXZ + velocityY;
    }

    public void JumpToPosition(Vector3 targetPosition, float trajectoryHeight)
    {
        activeGrapple = true;

        velocityToSet = CalculateJumpVelocity(transform.position, targetPosition, trajectoryHeight);
        Invoke(nameof(SetVelocity), 0.1f);

        Invoke(nameof(ResetRestrictions), 3f);
    }

    private Vector3 velocityToSet;
    private void SetVelocity()
    {
        enableMovementOnNextTouch = true;
        rB.velocity = velocityToSet * 4 ;
    }
    private bool enableMovementOnNextTouch;

    private void OnCollisionEnter(Collision collision)
    {
        if (enableMovementOnNextTouch)
        {
            enableMovementOnNextTouch = false;
            ResetRestrictions();

            GetComponent<Grappling>().StopGrapple();
        }        

    }

    // Player Controller will detect when a checkpoint is crossed, then it will set the respawn coordinates to that checkpoint'a position.
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Checkpoint")) 
        {
            rC.pathToRespawn = other.transform.position;
            Debug.Log("CHECKPOINT CROSSED - Respawn position has been set to: " + other.transform.position);
        }
    }

    private void ResetRestrictions() { activeGrapple = false; }

}