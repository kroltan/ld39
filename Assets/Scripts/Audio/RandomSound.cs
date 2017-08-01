using JetBrains.Annotations;
using UnityEngine;

namespace Audio {
    [RequireComponent(typeof(AudioSource))]
    public class RandomSound : MonoBehaviour {
        public AudioClip[] Choices;

        private AudioSource _audio;

        [UsedImplicitly]
        private void Start() {
            this.AssignComponent(out _audio);
        }

        public void PlayRandom() {
            if (Choices.Length != 0) {
                var index = Random.Range(0, Choices.Length);
                _audio.clip = Choices[index];
            }
            _audio.Play();
        }
    }
}
