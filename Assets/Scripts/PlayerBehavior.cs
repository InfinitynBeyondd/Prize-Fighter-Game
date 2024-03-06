using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    // TO DO: Jump, Basic Movement, Grapple, Double Jump, Grounded Punch, Diving Punch, Wall Jump

    [Header("Player Movement")]
    private float movementSpeed; // Player's movement speed.
    private float horizontalInput; // Player's horizontal input.
    private float verticalInput; // Player's vertical input.
    private Vector3 moveDirection; // Vector3 determining where player is moving.
    private Rigidbody rB; // Player's rigidbody.

    [Header("Ground Check")]    
    public LayerMask groundLayer; // Layer mask determining what is a ground layer.
    private bool isGrounded; // Bool that says when the player is on the ground. 
    private float groundDrag; // The amount of drag experienced on the ground.

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space; // Jump Key is set to Spacebar by default, but it can be changed in the inspector.

    [Header("References")]
    public Transform player;
    public Transform playerCharacter;
    public Transform orientation; // The orientation or direction of the player.
    private float rotationSpeed = 10f; // Speed determining player's rotation.
    private GameManager gM; // Script reference.
    [SerializeField] Animator m_Animator; // Player animator.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // FixedUpdate is called once per physics update    
    void FixedUpdate()
    {

    }

    // PlayerInput determines when players are inputting movement
    void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal"); // Sets the player's horizontal move input.
        verticalInput = Input.GetAxisRaw("Vertical"); // Sets the player's vertical move input.
    }

    void PlayerMovement() 
    {

    }

    void Jump() 
    {

    }

    void Grapple() 
    {

    }

    void Attack() 
    {

    }

}
