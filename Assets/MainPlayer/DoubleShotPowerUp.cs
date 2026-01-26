using UnityEngine;

public class DoubleShotPowerUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger with: " + other.name);
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                Debug.Log("Llamando a Doubleshot");
                player.ActivateDoubleShot();
            }

            Destroy(gameObject);
        }
    }
}
