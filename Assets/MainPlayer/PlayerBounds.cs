using UnityEngine;

public class PlayerBounds : MonoBehaviour
{
    private Camera cam;
    private SpriteRenderer sr;

    void Start()
    {
        cam = Camera.main;
        sr = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        Vector3 pos = transform.position;

        Vector3 min = cam.ViewportToWorldPoint(new Vector3(0f, 0f, 0f));
        Vector3 max = cam.ViewportToWorldPoint(new Vector3(1f, 1f, 0f));

        float halfWidth = sr.bounds.extents.x;
        float halfHeight = sr.bounds.extents.y;

        pos.x = Mathf.Clamp(pos.x, min.x + halfWidth, max.x - halfWidth);
        pos.y = Mathf.Clamp(pos.y, min.y + halfHeight, max.y - halfHeight);

        transform.position = pos;
    }
}
