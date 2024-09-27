using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleModePlayer : MonoBehaviour
{
    private float speed = 10f;

    private Rigidbody rigidBody;

    Vector3 movement;
    Vector3 rotation;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rotation.x += Input.GetAxis("Mouse X");
        rotation.y += Input.GetAxis("Mouse Y");
        movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        transform.localRotation = Quaternion.Euler(-rotation.y, rotation.x, 0);
        rigidBody.velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed;
    }
}
