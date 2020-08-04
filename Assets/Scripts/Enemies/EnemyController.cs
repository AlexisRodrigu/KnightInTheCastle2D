using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Variables de movimiento
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector2 direction = Vector2.right; //direcion de movimiento en X
    private bool isMoving = true;

    private IEnumerator moving;
    private IEnumerator waiting;

    [SerializeField] float movingTime;
    [SerializeField] float waitingTime;

    //PARAMETROS DE ANIMACIONES
    private Animator animator;
    readonly private int animMovementEnemyID = Animator.StringToHash("MoveX");
    readonly private int animAttackEnemyID = Animator.StringToHash("Attack");
    readonly private int animDieEnemyID = Animator.StringToHash("Die");

    [SerializeField] private float lifeEnemy;
    private float initialLifeEnemy = 50f;
    public float LifeEnemy { get => lifeEnemy; set => lifeEnemy = value; }

    //VariablesAI ATAQUE
    [Header("Variables ataque AI")]
    public Vector2 initialPosition;
    [SerializeField] private float minDistance;
    private float distance;
    [SerializeField] private float visionRadius;
    [SerializeField] private float stopDistance = 1.0f;
    [SerializeField] private GameObject player;
    private bool canPass;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        // moving = Moving(movingTime);
        // waiting = Waiting(waitingTime);
        // StartCoroutine(moving);
        // StartCoroutine(waiting);
        lifeEnemy = initialLifeEnemy;

        player = GameObject.FindGameObjectWithTag("Player");
        initialPosition = transform.position;
    }
    private void Update()
    {
        Death();
        Follow();
        Attack();
    }
    //CORRUTINA PARA LA ESPERA DEL ENEMIGO
    IEnumerator Waiting(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);

            if (isMoving)
            {
                StopCoroutine(moving);
                animator.SetFloat(animMovementEnemyID, 0);
            }
            else
            {
                spriteRenderer.flipX = FlipSprite;
                direction.x = direction.x > 0 ? -1 : 1;
                animator.SetFloat(animMovementEnemyID, direction.x);
                StartMoving();
            }
            isMoving = !isMoving;
        }
    }
    void StartMoving()
    {
        moving = Moving(movingTime);
        StartCoroutine(moving);
    }
    //CORRUTINA PARA EL MOVIMIENTO DEL ENEMIGO
    IEnumerator Moving(float waitTime)
    {
        while (true)
        {
            transform.Translate(direction * moveSpeed * Time.deltaTime);
            yield return new WaitForSeconds(waitTime);
        }
    }
    void Death()
    {
        if (lifeEnemy <= 0)
        {
            Destroy(gameObject);
        }
    }
    bool FlipSprite
    {
        get => direction.x > 0 ? true : false;
    }

    void Follow()
    {
        //Nuestro objetivo sera la posicion inicial
        Vector3 target = initialPosition;
        //SI LA DIRANCIA ES MENOR QUE EL RADIO DE VISION EL OBJETIVO SERA EL
        distance = Vector2.Distance(player.transform.position, transform.position);
        if (distance < visionRadius)
        {
            animator.SetFloat(animMovementEnemyID, 1); //Animacion
            target = player.transform.position;
        }
        //Movemos al enemigo al objetivo o target
        float fixedSpeed = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, fixedSpeed);
        Debug.DrawLine(transform.position, target, Color.red);
    }
    //Dibuja el rango de vision de mi enemigo a mi jugador o al que le quiero pegar
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, visionRadius);
        Gizmos.DrawLine(gameObject.transform.position, player.transform.position);
    }
    //Funcion atacar
    void Attack()
    {
        if (distance <= stopDistance)
        {
            moveSpeed = 0;
            animator.SetBool(animAttackEnemyID, true);
            Debug.Log("<color=red><b>" + "Ataco" + "</b></color>");
        }
        else
        {
            moveSpeed = 5;
            animator.SetBool(animAttackEnemyID, false);
        }
    }
    private void FlipEnemy()
    {
        if (distance < minDistance)
        {
            canPass = !canPass;
        }
        if (canPass)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }

}

