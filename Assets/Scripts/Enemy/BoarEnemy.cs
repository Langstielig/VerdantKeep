using System.Collections;
using UnityEngine;

public class BoarEnemy : Enemy
{
    [SerializeField] private float runSpeed;
    [SerializeField] private float collisionTime;

    private bool isColliding = false;

    private BoarStates State
    {
        get { return (BoarStates)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    protected override int SetupStartHealth()
    {
        return 2;
    }

    protected override float SetupMoveSpeed()
    {
        return 3.0f;
    }

    protected override float SetupDetectionRange()
    {
        return 5f;
    }

    protected override int SetupDamage()
    {
        return 1;
    }

    protected override void Start()
    {
        base.Start();
        direction = transform.right;
    }

    private void FixedUpdate()
    {
        if(!isDying && !isColliding)
        {
            if (Vector3.Distance(player.position, transform.position) < DetectionRange && IsPlayerInFront())
            {
                Run();
            }
            else
            {
                Walk();
            }
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if(collision.gameObject.CompareTag("Rock"))
        {
            if (State == BoarStates.walk)
            {
                ChangeDirection(collision.transform.position.x);
            }
            else if(State == BoarStates.run)
            {
                Collide(collision.transform.position.x);
            }
        }
    }

    private void Walk()
    {
        Debug.Log("Boar is moving");

        State = BoarStates.walk;
        Move(MoveSpeed);
    }

    private void Move(float speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, Time.deltaTime * speed);
    }

    private void ChangeDirection(float border)
    {
        direction *= -1f;
        spriteRenderer.flipX = direction == transform.right;
        //transform.position = new Vector3(border, transform.position.y, transform.position.z);
    }

    private void Run()
    {
        State = BoarStates.run;
        Move(runSpeed);
    }

    private void Collide(float border)
    {
        Debug.Log("Boar collies");

        isColliding = true;
        State = BoarStates.idle;

        StartCoroutine(CollideAnimation(border));
    }

    protected override void Die()
    {
        State = BoarStates.die;
        isDying = true;
        StartCoroutine(DieAnimation());
    }

    private IEnumerator DieAnimation()
    {
        Debug.Log("Die animation");

        yield return new WaitForSeconds(1f);

        base.Die();
    }

    private IEnumerator CollideAnimation(float border)
    {
        Debug.Log("Idle animation");

        yield return new WaitForSeconds(collisionTime);

        isColliding = false;
        ChangeDirection(border);
    }
}

public enum BoarStates
{
    idle,
    walk,
    run,
    die
}
