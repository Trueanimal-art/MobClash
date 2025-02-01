using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    public GameObject unitPrefab;  // Префаб юнита
    public Transform spawnPoint;   // Ссылка на объект SpawnPoint
    public float unitSpeed = 5f;   // Скорость движения юнита

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // Спаун по нажатию
        {
            SpawnUnit();
        }
    }

    void SpawnUnit()
    {
        // Спауним юнита
        GameObject unit = Instantiate(unitPrefab, spawnPoint.position, Quaternion.identity);

        // Устанавливаем скорость сразу после спауна
        Rigidbody2D rb = unit.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Устанавливаем начальное движение, если нужно
            rb.velocity = new Vector2(unitSpeed, 0);  // Двигаемся только по оси X
        }

        // Включаем коллайдер сразу, без задержки
        Collider2D col = unit.GetComponent<Collider2D>();
        if (col != null)
        {
            col.enabled = true;  // Включаем коллайдер сразу
        }

        // Убедимся, что моб не застревает в базе, если она рядом
        // Отключим физику и столкновения временно, если есть необходимость
        // Rigidbody2D r = unit.GetComponent<Rigidbody2D>();
        // if (r != null) r.isKinematic = true;  // Временно отключаем физику
    }
}
