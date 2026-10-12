using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int maxHealth = 2; 
    protected int currentHealth;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private int damage = 2;

    protected bool isPlayerDetected;
    protected bool isDying = false;

    protected Vector3 direction;

    protected Transform player;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;

    protected int MaxHealth => maxHealth;
    protected float MoveSpeed => moveSpeed;
    protected float DetectionRange => detectionRange;
    protected int Damage => damage;

    protected virtual int SetupStartHealth() => maxHealth;
    protected virtual float SetupMoveSpeed() => moveSpeed;
    protected virtual float SetupDetectionRange() => detectionRange;
    protected virtual int SetupDamage() => damage;

    protected virtual void Start()
    {
        maxHealth = SetupStartHealth();
        moveSpeed = SetupMoveSpeed();
        detectionRange = SetupDetectionRange();
        damage = SetupDamage();

        player = FindAnyObjectByType<Player>().transform;

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("Tag is " +  collision.gameObject.tag);
        if(collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if(player != null)
            {
                Debug.Log("Player take damage");
                player.TakeDamage(damage);
            }
        }
    }

    public virtual void TakeDamage(int damage)
    {
        if (!isDying)
        {
            currentHealth -= damage;
            Debug.Log("Enemy took damage");

            if(currentHealth > 0)
            { 
                //сделать мигание
            }
            else
            {
                Die();
            }
        }
    }

    protected bool IsPlayerInFront()
    {
        float directionToPlayer = player.position.x - transform.position.x;
        bool isFacingRight = direction == transform.right;

        return (directionToPlayer > 0 && isFacingRight) || (directionToPlayer < 0 && !isFacingRight);
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
