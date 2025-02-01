using UnityEngine;

public class MultiplierGate : MonoBehaviour
{
    public int multiplier = 2; // Множитель юнитов

    void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, что объект имеет нужный тег и не является клоном
        if (other.CompareTag("Unit") && other.GetComponent<UnitCloneMarker>() == null)
        {
            MobController mobController = other.GetComponent<MobController>(); // Используем MobController
            if (mobController != null)
            {
                int clonesToSpawn = multiplier - 1; // Сколько клонов нужно создать

                // Умножаем множитель моба
                mobController.Multiply(multiplier);

                // Создаем клонов (без учета оригинала)
                for (int i = 0; i < clonesToSpawn; i++)
                {
                    // Создаем клон рядом с оригиналом
                    Vector3 spawnPosition = other.transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 0, 0);
                    GameObject clone = Instantiate(other.gameObject, spawnPosition, Quaternion.identity);
                    
                    // Добавляем маркер, чтобы клон не активировал ворота
                    clone.AddComponent<UnitCloneMarker>(); 

                    // Обновляем скорость клона (иначе он не двигается)
                    MobController cloneController = clone.GetComponent<MobController>();
                    if (cloneController != null)
                    {
                        cloneController.SetSpeed(mobController.GetSpeed()); // Передаем скорость от оригинала клону
                    }

                    // Логирование
                    Debug.Log($"Создан клон моба на позиции {spawnPosition}. Всего мобов теперь: {clonesToSpawn + 1}");
                }

                // Логирование для оригинального моба
                Debug.Log($"Моб прошел через ворота. Умножено на {multiplier}. Теперь их: {clonesToSpawn + 1}");
            }
        }
    }
}
