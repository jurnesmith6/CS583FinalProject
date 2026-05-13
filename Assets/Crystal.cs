using UnityEngine;

public class Crystal : MonoBehaviour {
    public static int hitpoints;

    public static void TakeDamage(int damage) {
        hitpoints -= damage;
    }
}
