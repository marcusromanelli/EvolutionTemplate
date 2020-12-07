using System;
using UnityEngine;
using Zenject;

public class InputController : IInitializable, ITickable, IDisposable {
    public void Dispose()
    {
    }

    public void Initialize()
    {
    }

    public void Tick()
    {
    }

    public Vector2 GetClickPosition()
    {
#if !UNITY_EDITOR && (UNITY_IPHONE || UNITY_ANDROID)
        return Input.GetTouch(0).position;
#endif

        return Input.mousePosition;
    }
}
