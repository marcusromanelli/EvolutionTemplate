using System;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public struct ElementComponents {
    [SerializeField]
    private Transform _elementPivot;

    public Transform ElementPivot
    {
        get
        {
            return _elementPivot;
        }
    }

    [SerializeField]
    private Rigidbody2D _rigidbody2D;

    public Rigidbody2D Rigidbody
    {
        get
        {
            return _rigidbody2D;
        }
    }
}
