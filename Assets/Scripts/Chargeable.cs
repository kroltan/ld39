using System.Linq;
using Builder;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Chargeable : MonoBehaviour {
	public FloatReactiveProperty CurrentCharge = new FloatReactiveProperty(0);
	public FloatReactiveProperty MaximumCharge = new FloatReactiveProperty(1);
	public float DischargeRate = 0.1f;

	private float _lastFrameCharge;

	private IChargingCondition[] _conditions;
	private Animator _animator;

	public IObservable<float> PercentCharge => CurrentCharge
		.CombineLatest(MaximumCharge, (curr, max) => curr / max);

	[UsedImplicitly]
	private void Start() {
		_conditions = GetComponents<IChargingCondition>();
		this.AssignComponent(out _animator);
		_lastFrameCharge = CurrentCharge.Value;
		Observable.EveryFixedUpdate().Subscribe(_ => {
			_animator.SetBool("Charging", CurrentCharge.Value > _lastFrameCharge);
			_lastFrameCharge = CurrentCharge.Value;
		});
	}

	[UsedImplicitly]
	private void Update() {
		if (!LevelEditor.Instance.Editing) {
			CurrentCharge.Value -= DischargeRate * Time.deltaTime;
		}
	}

	[UsedImplicitly]
	private void OnTriggerStay(Collider other) {
		var charger = other.GetComponent<Charger>();
		if (charger == null || !CanCharge()) {
			return;
		}
		var needed = MaximumCharge.Value - CurrentCharge.Value;
		CurrentCharge.Value = Mathf.Min(
			MaximumCharge.Value,
			CurrentCharge.Value + charger.Consume(needed)
		);
	}

	private bool CanCharge() {
		return _conditions.All(c => c.CanCharge || !((MonoBehaviour) c).enabled);
	}
}
