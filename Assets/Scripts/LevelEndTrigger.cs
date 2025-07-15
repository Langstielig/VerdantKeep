using UnityEngine;

public class LevelEndTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SetActive(false);
            Debug.Log("Enter level end trigger");
            UIManagerInGame.Instance.ShowVictoryScreen();
        }
    }
}
