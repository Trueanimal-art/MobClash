using UnityEngine;
using System.Collections;

public class CleanupArea : MonoBehaviour
{
    public Vector2 areaSize = new Vector2(5, 5); // Размер очищаемой области

    private void Start()
    {
        StartCoroutine(CleanupRoutine());
    }

    private IEnumerator CleanupRoutine()
    {
        while (true)
        {
            Cleanup();
            yield return new WaitForSeconds(0.5f); // Проверка каждые 0.5 секунды
        }
    }

    private void Cleanup()
    {
        Vector2 worldCenter = (Vector2)transform.position; // Центр области в мировых координатах
        Collider2D[] colliders = Physics2D.OverlapBoxAll(worldCenter, areaSize, 0);
        foreach (Collider2D col in colliders)
        {
            if (col.CompareTag("Mob")) // Проверяем тег объекта
            {
                Debug.Log($"Удалён объект: {col.gameObject.name}");
                Destroy(col.gameObject);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, areaSize);
    }
}
