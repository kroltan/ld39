using UniRx;
using UnityEngine;

public class Charger : MonoBehaviour {
	public float ChargePerSecond = 1;
	public FloatReactiveProperty ChargeLeft = new FloatReactiveProperty(5);

	public float Consume(float target) {
		var perSecond = ChargePerSecond * Time.deltaTime;
		var clamped = Mathf.Min(ChargeLeft.Value, perSecond, target);
		ChargeLeft.Value -= clamped;
		return clamped;
	}
}
