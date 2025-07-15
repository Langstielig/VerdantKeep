using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private int damage;

    public void SetDamage(int dmg)
    {
        damage = dmg;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if(enemy != null)
        {
            Debug.Log("Player Attack calls take damage method");
            enemy.TakeDamage(damage);
        }
    }
}
