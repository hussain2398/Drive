using System.Security.Cryptography;
using UnityEngine;

public class Tire : MonoBehaviour {
    //private void Awake() {
    //    Debug.Log("Layer Index :: " + LayerMask.NameToLayer("Ground"));// returns 6 because Ground layer's index is 6th
    //    Debug.Log(1 << LayerMask.NameToLayer("Ground"));// returns 64 becuase 1<<6 gives 01000000 which is 64 in decimals and that 64 is stored in the layermask
    //}
    [Range(0, 10)]
    public float rayDistance = 0;
    [Range(0, 10)]
    public float suspensionRestDistance = 1;
    [Range(0, 50)]
    public float springStrength = 5;
    [Range(0, 50)]
    public float springDamper = 5;
    public Rigidbody carRigidbody;
    private void Awake() {
        carRigidbody = transform.parent.GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        float suspensionRestDistance = this.suspensionRestDistance;
        float springStrength = this.springStrength;
        float springDamper = this.springDamper;
        RaycastHit hit;
        Vector3 origin = transform.position;
        Vector3 direction = transform.TransformDirection(Vector3.down);
        float maxDistance = rayDistance;
        Ray tireRay = new Ray(origin, direction);
        LayerMask groundMask = 1 << LayerMask.NameToLayer("Ground");
        //LayerMask _groundMask = 1 << 6;

        bool rayDidHit = Physics.Raycast(tireRay, out hit, maxDistance, 1 << LayerMask.NameToLayer("Ground"));

        if (rayDidHit) {
            Debug.DrawRay(origin, direction.normalized * maxDistance, Color.green);
            // world space direction of the spring force
            Vector3 springDir = transform.up;
            // world space velocity of this tire
            Vector3 tireWorldVelocity = carRigidbody.GetPointVelocity(transform.position);
            // calculate offset from the raycast
            float offset = suspensionRestDistance - maxDistance;
            // calculate velocity along the spring direction
            // note that springDirection is a unity vector so this returns the magnitude of tire world velocity
            // as projected onto the spring direction
            float vel = Vector3.Dot(springDir, tireWorldVelocity);
            // calculate magnitude of the damped spring force
            float force = (offset * springStrength) - (vel * springDamper);
            // apply force at the location of this tire, in the direction of the suspension
            carRigidbody.AddForceAtPosition(springDir * force, transform.position);

            //this.force = springDir * force;
            //Debug.Log(hit.transform.gameObject.layer);
        }


    }
    //----------------------------------
    //public Rigidbody rb;
    //public Vector3 forcePoint; // The point where force is applied (local or world)
    //public Vector3 force; // The force vector

    //private void OnDrawGizmos() {
    //    if (carRigidbody == null) return;

    //    Vector3 worldPoint = carRigidbody.transform.TransformPoint(transform.position);
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawLine(worldPoint, worldPoint + force); // Draw force vector
    //    DrawArrowHead(worldPoint + force, force.normalized * 0.5f); // Draw arrowhead
    //}

    //private void DrawArrowHead(Vector3 position, Vector3 direction) {
    //    Vector3 right = Quaternion.Euler(0, 20, 0) * -direction;
    //    Vector3 left = Quaternion.Euler(0, -20, 0) * -direction;
    //    Gizmos.DrawLine(position, position + right * 0.2f);
    //    Gizmos.DrawLine(position, position + left * 0.2f);
    //}
}
