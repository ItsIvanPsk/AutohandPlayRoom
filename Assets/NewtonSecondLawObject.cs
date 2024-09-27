using UnityEngine;

public class NewtonSecondLawObject : MonoBehaviour
{
    [SerializeField] private float _distance = 2.0f;  // Distancia máxima antes de invertir dirección
    private Vector3 _startPosition;  // Posición inicial del objeto
    private bool movingLeft = true;  // Controlar la dirección del movimiento
    private float _speed;  // Velocidad del objeto, que será asignada por el controlador

    private void Start() {
        _startPosition = transform.position;  // Guardar la posición inicial
    }

    private void Update() {
        // Mover el objeto
        MoveObject();
    }

    // Método para establecer la velocidad desde el controlador
    public void SetSpeed(float speed) {
        _speed = speed;
    }

    private void MoveObject() {
        // Verificar si ha alcanzado la distancia máxima desde la posición inicial en el eje X
        if (movingLeft && Mathf.Abs(transform.position.x - _startPosition.x) >= _distance) {
            movingLeft = false;  // Cambiar dirección a la derecha
        } else if (!movingLeft && Mathf.Abs(transform.position.x - _startPosition.x) <= 0.01f) {
            movingLeft = true;  // Cambiar dirección a la izquierda
        }

        // Mover el objeto en la dirección actual en el eje X (izquierda o derecha)
        Vector3 direction = movingLeft ? Vector3.left : Vector3.right;
        transform.Translate(direction * _speed * Time.deltaTime);
    }
}
