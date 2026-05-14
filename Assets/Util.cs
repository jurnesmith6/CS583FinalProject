using UnityEngine;

public class Util {
    public static float NormalDist(float mean, float sd) {
        return mean + sd * Mathf.Sqrt(-2.0f * Mathf.Log(Mathf.Max(Random.value, 1e-7f))) * Mathf.Cos(2.0f * Mathf.PI * Random.value);
    }
}
