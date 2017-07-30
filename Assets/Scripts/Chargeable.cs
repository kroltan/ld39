using System.Linq;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;

public class Chargeable : MonoBehaviour {
	public FloatReactiveProperty CurrentCharge = new FloatReactiveProperty(0);
	public FloatReactiveProperty MaximumCharge = new FloatReactiveProperty(1);
	public float DischargeRate = 0.1f;

	private IChargingCondition[] _conditions;

	public IObservable<float> PercentCharge => CurrentCharge
		.CombineLatest(MaximumCharge, (curr, max) => curr / max);

	[UsedImplicitly]
	private void Start() {
		_conditions = GetComponents<IChargingCondition>();
	}

	[UsedImplicitly]
	private void Update() {
		CurrentCharge.Value -= DischargeRate * Time.deltaTime;
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
