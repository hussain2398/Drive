using System.Security.Cryptography;
using UnityEngine;

public class Tire : MonoBehaviour {
    //private void Awake() {
    //    Debug.Log("Layer Index :: " + LayerMask.NameToLayer("Ground"));// returns 6 because Ground layer's index is 6th
    //    Debug.Log(1 << LayerMask.NameToLayer("Ground"));// returns 64 becuase 1<<6 gives 01000000 which is 64 in decimals and that 64 is stored in the layermask
    //}
    [Range(0, 10)]
    public float distance = 0;

    public Rigidbody rb;
    private void Awake() {
        rb = transform.parent.GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        RaycastHit hit;
        bool hitGround = false;
        Vector3 origin = transform.position;
        Vector3 direction = transform.TransformDirection(Vector3.down);
        float maxDistance = distance;
        LayerMask groundMask = 1 << LayerMask.NameToLayer("Ground");
        //LayerMask _groundMask = 1 << 6;

        if (Physics.Raycast(origin, direction, out hit, maxDistance, 1 << LayerMask.NameToLayer("Ground"))) {
            Debug.DrawRay(origin, direction.normalized * maxDistance, Color.green);
            Debug.Log(hit.transform.gameObject.layer);
            rb.transform.position = new Vector3(rb.transform.position.x, rb.transform.position.y - maxDistance, rb.transform.position.z);
        }
    }
}
