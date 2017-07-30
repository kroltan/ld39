using JetBrains.Annotations;
using UniRx;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ChargerStatusSprite : MonoBehaviour {
    public Charger Charger;
    public Sprite CanCharge;
    public Sprite CannotCharge;

    [UsedImplicitly]
    private void Start() {
        var sprite = GetComponent<SpriteRenderer>();
        Charger.ChargeLeft
            .Select(charge => charge > 0)
            .Subscribe(canCharge => {
                sprite.sprite = canCharge ? CanCharge : CannotCharge;
            });
    }
}