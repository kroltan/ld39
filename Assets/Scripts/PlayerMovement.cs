﻿using UnityEngine;

public class PlayerMovement : MonoBehaviour, IActorMovementSource {
    public Vector3 GetDirection() => new Vector3(
        Input.GetAxis("Horizontal"),
        0,
        Input.GetAxis("Vertical")
    );
}