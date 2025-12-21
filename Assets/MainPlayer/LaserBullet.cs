using UnityEngine;

public class LaserBullet : MonoBehaviour
{
    [SerializeField] private float speed = 20f;

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
