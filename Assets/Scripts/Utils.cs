using UnityEngine;

public static class Utils {
    public static void AssignComponent<T>(this MonoBehaviour self, out T property) {
        property = self.GetComponent<T>();
    }
}