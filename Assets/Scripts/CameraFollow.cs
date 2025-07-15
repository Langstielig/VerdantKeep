using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;

    private Vector3 position;

    private void Awake()
    {
        if(!player)
        {
            player = FindAnyObjectByType<Player>().transform;
        }
    }

    private void FixedUpdate()
    {
        position = player.position;
        position.z = -10f;

        transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime);
    }
}
