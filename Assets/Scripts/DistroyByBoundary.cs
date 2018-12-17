
using UnityEngine;

public class DistroyByBoundary : MonoBehaviour {

	void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
}
