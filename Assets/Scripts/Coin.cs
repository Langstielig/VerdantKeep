using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private AudioClip collectSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            UIManagerInGame.Instance.AddCoin();

            SFXPlayer.Instance.PlaySFX(collectSound);
            Destroy(gameObject);
        }
    }
}
