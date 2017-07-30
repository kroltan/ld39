using System;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GameOver {
    [RequireComponent(typeof(Text))]
    public class LossReasonDisplay : MonoBehaviour {
        private Text _text;

        [UsedImplicitly]
        private void Start() {
            this.AssignComponent(out _text);
            SetText(GameManager.Instance.LossReason.Value);
            GameManager.Instance.LossReason.Subscribe(SetText);
        }

        private void SetText(LossReason? reason) {
            if (reason.HasValue) {
                switch (reason.Value) {
                    case LossReason.Caught:
                        _text.text = "The cops got ya!";
                        break;
                    case LossReason.Discharged:
                        _text.text = "You couldn't finish watching!";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else {
                _text.text = "";
            }
        }
    }
}
