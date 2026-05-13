using UnityEngine;

public class Crystal : MonoBehaviour {
    static Crystal instance;
    public int hitpoints;

    void Awake() {
        instance = this;
    }

    public static void TakeDamage(int damage) {
        instance.hitpoints -= damage;
    }
}
