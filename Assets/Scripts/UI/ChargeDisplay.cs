using JetBrains.Annotations;
using UniRx;
using UnityEngine;

namespace UI {
    public class ChargeDisplay : MonoBehaviour {
        public Chargeable Target;
        public RectTransform Bar;

        [UsedImplicitly]
        private void Start() {
            Target.PercentCharge.Subscribe(percent => {
                var scale = Bar.localScale;
                scale.x = percent;
                Bar.localScale = scale;
            });
        }
    }
}