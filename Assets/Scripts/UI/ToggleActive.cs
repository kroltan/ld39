using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    [RequireComponent(typeof(Toggle))]
    public class ToggleActive : MonoBehaviour {
        public RectTransform Target;

        [UsedImplicitly]
        private void Start() {
            GetComponent<Toggle>().onValueChanged.AddListener(Target.gameObject.SetActive);
        }
    }
}
