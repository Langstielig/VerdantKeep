using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class Player : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int currentHealth;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private float maxHeightOfJump = 4f;
    [SerializeField] private int damage;
    [SerializeField] private float deathBorder;

    [Header("GroundCheck")]
    private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    private float groundCheckRadius = 0.1f;
    private bool isGrounded = true;

    private float? startHeightOfJump = null;
    private float currentHeightOfJump;

    private bool isInvincible = false;
    private float invincibilityDuration = 1.5f;
    private bool isAttacking = false;
    private bool isDying = false;

    [Header("Controls")]
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private Button leftAttackButton;
    [SerializeField] private Button rightAttackButton;

    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private PlayerAttack attackHitbox;

    private States State
    {
        get { return (States)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        attackHitbox = GetComponentInChildren<PlayerAttack>();
        if(attackHitbox != null)
        {
            Debug.Log("Attack Hitbox");
            attackHitbox.gameObject.SetActive(false);
        }

        groundLayer = LayerMask.GetMask("Ground");
    }

    private void Start()
    {
        currentHealth = maxHealth;

        leftAttackButton.onClick.AddListener(() => Attack(false));
        rightAttackButton.onClick.AddListener(() => Attack(true));
    }

    private void FixedUpdate()
    {
        CheckGround();

        if (!isAttacking && !isDying)
        {
            if (isGrounded)
            {
                currentHeightOfJump = 0f;
                startHeightOfJump = null;

                //Debug.Log("State idle");
                State = States.idle;
            }

            if (joystick.Horizontal != 0)
            {
                Move();
            }

            if (joystick.Vertical > 0.5f && currentHeightOfJump < maxHeightOfJump)
            {
                Jump();
            }

            if(transform.position.y < deathBorder)
            {
                Die();
            }    
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Coin"))
        {
            Debug.Log("Enter coin collision");
            UIManagerInGame.Instance.AddCoin();
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(isAttacking)
        {
            if(collision.gameObject.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(damage);
            }
        }
    }

    private void Move()
    {
        if(isGrounded)
        {
            //Debug.Log("State run");
            State = States.run;
        }

        Vector3 direction = transform.right * joystick.Horizontal;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, moveSpeed * Time.deltaTime);

        spriteRenderer.flipX = direction.x < 0.0f;
    }

    private void Jump()
    {
        if(State != States.jumpStart && State != States.jump)
        {
            Debug.Log("State jumpStart");
            State = States.jumpStart;
        }
        else if(State == States.jumpStart)
        {
            Debug.Log("State jump");
            State = States.jump;
        }

        if(currentHeightOfJump == 0f && startHeightOfJump == null)
        {
            startHeightOfJump = transform.position.y;
        }

        rigidbody.linearVelocity = Vector2.up * jumpForce;
        currentHeightOfJump = (float)(transform.position.y - startHeightOfJump);
    }

    private void Attack(bool isRight)
    {
        if(isRight)
        {
            Debug.Log("Attack is right");
        }
        else
        {
            Debug.Log("Attack is left");
        }

        if (isGrounded && !isAttacking)
        {
            if(attackHitbox != null)
            {
                attackHitbox.SetDamage(damage);
                attackHitbox.gameObject.SetActive(true);
            }

            isAttacking = true;
            State = States.attack;
            spriteRenderer.flipX = !isRight;
            FlipAttackHitbox();
            StartCoroutine(AttackAnimation());
        }
    }

    public void TakeDamage(int damage)
    {
        if(isInvincible)
        {
            return;
        }

        currentHealth -= damage;
        UIManagerInGame.Instance.UpdateHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvincibilityCoroutine());
        }
    }

    private void FlipAttackHitbox()
    {
        if (attackHitbox != null)
        {
            float currentPosition = attackHitbox.gameObject.transform.localPosition.x;
            
            if((spriteRenderer.flipX && currentPosition > 0) ||
                (!spriteRenderer.flipX && currentPosition < 0))
            {
                currentPosition *= -1;
            }

            //Debug.Log("direction is " + isRight + "flipX is " + spriteRenderer.flipX + ", attack hitbox position x is " + currentPosition.x *  direction);

            Vector3 loclPosition = attackHitbox.gameObject.transform.localPosition;
            attackHitbox.gameObject.transform.localPosition = new Vector3(currentPosition, loclPosition.y, loclPosition.z);
        }
    }

    private void Die()
    {
        State = States.die;
        isDying = true;

        StartCoroutine(DieAnimation());
    }

    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;

        float elapsed = 0f;
        bool visible = true;

        while (elapsed < invincibilityDuration)
        {
            visible = !visible;
            if(spriteRenderer != null)
            {
                spriteRenderer.color = visible ? Color.white : new Color(1, 1, 1, 0.3f);
            }

            yield return new WaitForSeconds(0.15f);
            elapsed += 0.15f;
        }

        if(spriteRenderer != null)
        {
            spriteRenderer.color = Color.white;
        }

        isInvincible = false;
    }

    private IEnumerator AttackAnimation()
    {
        yield return new WaitForSeconds(0.4f);
        isAttacking = false;
        attackHitbox.gameObject.SetActive(false);
    }

    private IEnumerator DieAnimation()
    {
        yield return new WaitForSeconds(1f);

        UIManagerInGame.Instance.ShowDeathScreen();
        gameObject.SetActive(false);
    }

    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }
}

public enum States
{
    idle,
    run,
    jumpStart,
    jump,
    attack,
    die
}
