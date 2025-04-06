using UnityEngine;

public class Deleteuncollectedobjects : MonoBehaviour
{
    public SpawningEnvironment spawner;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Potion"))
        {
            Destroy(collision.gameObject);
            spawner.TotalCountOfSpawnedObjects--;
        }
    }
}
