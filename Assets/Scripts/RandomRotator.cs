using UnityEngine;

public class RandomRotator : MonoBehaviour {

    private Rigidbody _rigidBody;
    public float tumble;

	// Use this for initialization
	void Start () {
        _rigidBody = GetComponent<Rigidbody>();
        _rigidBody.angularVelocity = Random.insideUnitSphere * tumble;
	}
	
}
