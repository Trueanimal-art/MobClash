using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    public float speed = 5f; // Скорость движения

    void Update()
    {
        // Движение вверх (по оси Y)
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}
