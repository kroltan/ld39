using JetBrains.Annotations;
using UniRx;
using UnityEngine;

namespace UI {
    public class ChargeDisplay : MonoBehaviour {
        public Chargeable Target;
        public RectTransform Bar;

        [UsedImplicitly]
        private void Update() {
            if (Target == null) {
                Target = Utils.GetPlayer().GetComponent<Chargeable>();
            }
            var percent = Target.PercentCharge.Value;
            var scale = Bar.localScale;
            scale.x = percent;
            Bar.localScale = scale;
        }
    }
}