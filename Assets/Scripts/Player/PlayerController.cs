using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    protected PlayerSystemInput playerSystemInput;
    private Vector2 inputVector;  ///Vectpr del input de movimiento

    private SpriteRenderer spriteRenderer;
    //Variables de salto
    [SerializeField] protected bool isGrounded;
    [SerializeField] protected bool canJump;
    [SerializeField] private bool jump;
    [SerializeField] private float JumpSpeed;

    //Variables de movimiento
    [SerializeField] private float speed; //Velocidad del jugador
    [SerializeField] private bool isPlayerMovement;
    [SerializeField] private bool isAlive;

    [SerializeField] private float health;
    [SerializeField] private float initialHealth = 100;
     public float Health { get => health; set => health = value; }

    //PARAMETROS DE LAS ANIMACIONES
    private Animator animator;
    readonly int animMovementID = Animator.StringToHash("MoveX");
    readonly int animAttackOneID = Animator.StringToHash("AttackOne");
    readonly int animAttackTwoID = Animator.StringToHash("AttackTwo");
    readonly int animDeathID = Animator.StringToHash("Death");
    readonly int animJumpID = Animator.StringToHash("Jump");

    [SerializeField] private float damageOfSword;

    public float DamageOfSword { get => damageOfSword; }
  

    Rigidbody2D rigidbodyPlayer;
    [SerializeField] private float climbSpeed = 5.0f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerSystemInput = GetComponent<PlayerSystemInput>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbodyPlayer = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        health = initialHealth;
    }
    void FixedUpdate()
    {
        Move();
        
    }
    void Update()
    {
        Climb();
    }

    void Move()
    {
        if (isPlayerMovement && isAlive)
        {
            Vector2 move = playerSystemInput.Movement;
            inputVector = new Vector2(move.x, 0);
            transform.Translate(inputVector * speed * Time.deltaTime, 0);
            animator.SetFloat(animMovementID, inputVector.x);

            if (inputVector.x < 0)
            {
                spriteRenderer.flipX = true;
                animator.SetFloat(animMovementID, (inputVector.x) * -1); //Multiplicamos por -1 la animacion para no hacer un moonwalker
            }
            else if (inputVector.x > 0)
            {
                spriteRenderer.flipX = false;
            }
        }
        else speed = 0;
    }
    public void AttackOne(bool attackOne)
    {
        if (attackOne == true) animator.SetTrigger(animAttackOneID);

    }
    public void AttackTwo(bool attackTwo)
    {
        if (attackTwo == true) animator.SetTrigger(animAttackTwoID);
    }
    public void Death()
    {
        if (health <= 0)
        {
            animator.SetBool(animDeathID, true);
            FindObjectOfType<GameManager>().ProcessPlayerDeath();
        }
    }

    public void Jump()
    {
        // // if (playerSystemInput.JumpInput && isGrounded)
        // //     readyToJump = true;

        // // if (isGrounded)
        // // {
        // //     if (playerSystemInput.JumpInput && readyToJump)
        // //     {
        // //         Vector2 jumpVelocityToAdd = new Vector2(0f, JumpSpeed);
        // //         rb.velocity += jumpVelocityToAdd;
        // //         isGrounded = false;
        // //         readyToJump = false;
        // //          animator.SetBool("Jump", true);
        // //     }
        // // }
        // if (isGrounded)
        //     canJump = true;

        // // if (canJump && isGrounded)
        // //     jump = true;

        // if (jump && canJump)
        // {
        //     isGrounded = false;
        //      animator.SetBool("Jump", true);
        // }
        // else{
        //      canJump = false;
        //      animator.SetBool("Jump", false);
        // }
        // //     Vector2 jumpVelocityToAdd = new Vector2(0f, JumpSpeed);
        // //     rb.velocity += jumpVelocityToAdd;
        // //     animator.SetBool("Jump", true);
        // //     readyToJump = false;
        // // }else{
        // //     isGrounded= true;
        // //     animator.SetBool("Jump", false);
        // // }
        // Debug.Log("<color=red><b>" + "Brincas" + "</b></color>");
    }
    public void Climb()
    {
//         Debug.Log("<color=red><b>" + "subeaas" + "</b></color>");
        if (!rigidbodyPlayer.IsTouchingLayers(LayerMask.GetMask("Climbing"))) { return; }
        Vector2 climbVelocity = new Vector2 (rigidbodyPlayer.velocity.x, 1 * climbSpeed);
        rigidbodyPlayer.velocity = climbVelocity;
        Debug.Log("<color=red><b>" + "subes" + "</b></color>");
    }


}

