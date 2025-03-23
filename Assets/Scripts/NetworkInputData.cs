using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public struct NetworkInputData : INetworkInput
{
    public Vector2 movement; // Dữ liệu di chuyển
    public bool jumpPressed; // Dữ liệu nhảy
}
