using UnityEngine;

public class Crystal : MonoBehaviour {
    void Update() {
        transform.Rotate(Vector3.up, Time.deltaTime * 180f, Space.World);
    }
}
