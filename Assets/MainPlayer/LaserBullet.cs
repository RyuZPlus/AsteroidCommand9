using UnityEngine;

public class LaserBullet : MonoBehaviour
{
    [SerializeField] private float speed = 20f;

    [Header("Visual")]
    [SerializeField] private Sprite[] levelSprites;

    private int damage;
    private SpriteRenderer spriteRenderer;
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetLevel(int level)
    {
        damage = level;
        int index = Mathf.Clamp(level - 1, 0, levelSprites.Length - 1);
        spriteRenderer.sprite = levelSprites[index];
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
