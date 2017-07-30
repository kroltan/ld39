using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameOver {
    public class SceneLink : MonoBehaviour {
        public string Scene;

        public void Go() {
            var scene = Scene;
            if (string.IsNullOrWhiteSpace(scene)) {
                scene = GameManager.Instance.LostLevel;
            }
            SceneManager.LoadScene(scene);
        }
    }
}