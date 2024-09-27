using System.Collections;
using System.Collections.Generic;
using Autohand;
using UnityEngine;

public class NewtonSecondLawObjectController : MonoBehaviour
{
    [SerializeField] private NewtonSecondLawObject TopObject;    // Referencia al objeto de arriba
    [SerializeField] private NewtonSecondLawObject MiddleObject; // Referencia al objeto del medio
    [SerializeField] private NewtonSecondLawObject BottomObject; // Referencia al objeto de abajo

    [SerializeField] private Material _activeMaterial;  // Material cuando el objeto está activo
    [SerializeField] private Material _disableMaterial; // Material cuando el objeto está inactivo
    [SerializeField] private PhysicsGadgetSlider forceSlider;     // Palanca de fuerza
    [SerializeField] private PhysicsGadgetSlider massSlider;      // Palanca de masa
    [SerializeField] private PhysicsGadgetSlider accelerationSlider; // Palanca de aceleración

    private Rigidbody activeRigidbody;  // Rigidbody del objeto activo

    private float force;
    private float mass;
    private float acceleration;
    private int _activeObject = 0;  // Índice del objeto activo

    private void Start() {
        _activeObject = 0;  // Comenzar con el primer objeto como activo
        ActivateObject(_activeObject);  // Activar el objeto inicial

        // Inicializar los valores de las palancas
        force = forceSlider.GetValue();
        mass = massSlider.GetValue();
        acceleration = accelerationSlider.GetValue();
    }

    private void Update() {
        UpdateValuesFromSliders();  // Actualizar los valores de las palancas
        UpdatePhysics();            // Calcular y aplicar la física al objeto activo
    }

    // Activar un objeto basado en el índice (0 = Bottom, 1 = Middle, 2 = Top)
    public void ActivateObject(int objectIndex) {
        _activeObject = objectIndex;

        // Cambiar los materiales para reflejar el objeto activo
        if (objectIndex == 0) {
            SetObjectMaterial(BottomObject.gameObject, _activeMaterial);
            SetObjectMaterial(MiddleObject.gameObject, _disableMaterial);
            SetObjectMaterial(TopObject.gameObject, _disableMaterial);
            activeRigidbody = BottomObject.GetComponent<Rigidbody>();
        } else if (objectIndex == 1) {
            SetObjectMaterial(BottomObject.gameObject, _disableMaterial);
            SetObjectMaterial(MiddleObject.gameObject, _activeMaterial);
            SetObjectMaterial(TopObject.gameObject, _disableMaterial);
            activeRigidbody = MiddleObject.GetComponent<Rigidbody>();
        } else if (objectIndex == 2) {
            SetObjectMaterial(BottomObject.gameObject, _disableMaterial);
            SetObjectMaterial(MiddleObject.gameObject, _disableMaterial);
            SetObjectMaterial(TopObject.gameObject, _activeMaterial);
            activeRigidbody = TopObject.GetComponent<Rigidbody>();
        }
    }

    // Método para cambiar el material de un objeto
    private void SetObjectMaterial(GameObject obj, Material material) {
        obj.GetComponent<MeshRenderer>().material = material;
    }

    // Obtener los valores actuales de las palancas
    private void UpdateValuesFromSliders() {
        force = forceSlider.GetValue();         // Valor de la palanca de Fuerza (N)
        mass = massSlider.GetValue();           // Valor de la palanca de Masa (m)
        acceleration = accelerationSlider.GetValue(); // Valor de la palanca de Aceleración (a)
    }

    // Aplicar las físicas al objeto activo
    private void UpdatePhysics() {
        if (activeRigidbody != null) {
            activeRigidbody.mass = Mathf.Max(mass, 0.1f); // Evitar que la masa sea cero
            if (mass > 0) {
                acceleration = force / mass;  // Calcular la aceleración usando F = ma
            }

            // Aplicar la fuerza al objeto activo
            activeRigidbody.AddForce(Vector3.right * acceleration);
        }
    }

    public void ChangeForceStep(int add) {
        force = add;
        Debug.Log("[NewtonSecondLawObjectController] - ForceStepAdd = " + add.ToString());
    }
    public void ChangeMassStep(int add) {
        mass = add;
        Debug.Log("[NewtonSecondLawObjectController] - MassStepAdd = " + add.ToString());
    }
    public void ChangeAceleration(int add) {
        acceleration = add;
        Debug.Log("[NewtonSecondLawObjectController] - AcelerationStepAdd = " + add.ToString());
    }
}
