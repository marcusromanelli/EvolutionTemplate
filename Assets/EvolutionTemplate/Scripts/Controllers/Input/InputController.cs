using UnityEngine;

public class InputController : IInputController {
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
