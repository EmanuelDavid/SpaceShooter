
using UnityEngine;

public class Mover : MonoBehaviour {

    private Rigidbody _rigidBody;

    public float Speed = 10;
	// Use this for initialization
	void Start () {
        _rigidBody = GetComponent<Rigidbody>();
        _rigidBody.velocity = transform.forward * Speed;
    }
}
