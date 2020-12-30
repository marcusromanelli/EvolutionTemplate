using System;
using UnityEngine;
using Zenject;

public interface IInputController : IInitializable, ITickable, IDisposable {
    Vector2 GetClickPosition();
}
