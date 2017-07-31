using Actors;
using UnityEngine;

public static class Utils {
    public const string LevelPrefabsPath = "Assets/Prefabs/Objects";

    public static GameObject GetPlayer() {
        return Object.FindObjectOfType<PlayerMovement>().gameObject;
    }

    public static void AssignComponent<T>(this MonoBehaviour self, out T property) {
        property = self.GetComponent<T>();
    }
}