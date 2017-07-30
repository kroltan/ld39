using JetBrains.Annotations;
using UnityEngine;
using UniRx;

[RequireComponent(typeof(Chargeable))]
public class LoseWhenDischarged : MonoBehaviour {
	public string LoseScene;

	[UsedImplicitly]
	private void Start () {
		GetComponent<Chargeable>().CurrentCharge
			.First(charge => charge <= 0)
			.Subscribe(charge => GameManager.Instance.Lose(LossReason.Discharged));
	}
}
