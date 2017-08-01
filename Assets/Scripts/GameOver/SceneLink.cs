using System;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameOver {
    public class SceneLink : MonoBehaviour {
        public string Scene;
        public float Delay;
        public bool Async;

        public void Go() {
            var scene = Scene;
            if (string.IsNullOrWhiteSpace(scene)) {
                scene = GameManager.Instance.LostLevel;
            }
            Observable.Timer(TimeSpan.FromSeconds(Delay)).Subscribe(_ => {
                if (Async) {
                    SceneManager.LoadSceneAsync(scene);
                } else {
                    SceneManager.LoadScene(scene);
                }
            });
        }
    }
}