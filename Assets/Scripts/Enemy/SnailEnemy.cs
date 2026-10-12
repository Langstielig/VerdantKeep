using System.Collections;
using UnityEngine;

public class SnailEnemy : Enemy
{
    [SerializeField] private float leftBorder;
    [SerializeField] private float rightBorder;

    private bool isHiding = false;

    private SnailStates State
    {
        get { return (SnailStates)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    protected override int SetupStartHealth()
    {
        return 1;
    }

    protected override float SetupMoveSpeed()
    {
        return 5.0f;
    }

    protected override float SetupDetectionRange()
    {
        return 6.5f;
    }

    protected override int SetupDamage()
    {
        return 1;
    }

    private void Update()
    {
        if(!isHiding)
        {
            //ShowStatsInLog();
            //isHiding = true;
        }
    }

    protected override void Start()
    {
        base.Start();
        direction = transform.right;
    }

    private void FixedUpdate()
    {
        //Debug.Log("Distance is: " + Vector3.Distance(player.position, transform.position) + " is player in front: " + IsPlayerInFront());
        if (!isDying)
        {
            if (Vector3.Distance(player.position, transform.position) < DetectionRange && IsPlayerInFront())
            {
                Hide();
            }
            else
            {
                Move();
            }
        }
        Debug.Log(State);
    }

    private void ShowStatsInLog()
    {
        Debug.LogFormat("Snail: maxHealth = {0}, moveSpeed = {1}, detectionRange = {2}, damage = {3}", MaxHealth, MoveSpeed, DetectionRange, Damage);
    }

    private void Move()
    {
        State = SnailStates.walk;

        if(transform.position.x >= leftBorder && transform.position.x <= rightBorder) 
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, Time.deltaTime);
        }
        else
        {
            Debug.Log("Change direction");
            direction *= -1f;
            spriteRenderer.flipX = direction == transform.right;
            if (direction == transform.right)
            {
                transform.position = new Vector3(leftBorder, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(rightBorder, transform.position.y, transform.position.z);
            }
            Debug.Log("Position is: " + transform.position);
        }
    }

    private void Hide()
    {
        State = SnailStates.hide;
    }

    public override void TakeDamage(int damage)
    {
        if (State != SnailStates.hide)
        {
            base.TakeDamage(damage);
        }
    }

    protected override void Die()
    {
        Debug.Log("Die method");
        State = SnailStates.die;
        isDying = true;
        StartCoroutine(DieAnimation());
    }

    private IEnumerator DieAnimation()
    {
        Debug.Log("Die animation");

        yield return new WaitForSeconds(1f);

        base.Die();
    }
}

public enum SnailStates
{
    walk,
    hide,
    die
}
