using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int poolSize = 20;

    private GameObject[] bullets;

    void Awake()
    {
        bullets = new GameObject[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
            bullets[i] = Instantiate(bulletPrefab, transform);
            bullets[i].SetActive(false);
        }
    }

    public GameObject GetBullet()
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            if (!bullets[i].activeInHierarchy)
            {
                bullets[i].SetActive(true);
                return bullets[i];
            }
        }

        return null;
    }
}
