using UnityEngine;

public class CarController : MonoBehaviour {
    [SerializeField] private GameObject carBody;
    [SerializeField] private Rigidbody carRB;
    [SerializeField] private Transform FRW;
    [SerializeField] private Transform FLW;
    [SerializeField] private Transform RRW;
    [SerializeField] private Transform RLW;

    private void Awake() {
        carRB = carBody.GetComponent<Rigidbody>();
    }
    private void FixedUpdate() {

        RaycastHit hit;

        //if(Physics.Raycast())
    }
}
