using UnityEngine;
using Zenject;

/// <summary>
/// Yeah. This happened. This controller exists sorely to watch the pop spawn animation to end.
/// </summary>
[RequireComponent(typeof(Animation))]
public class ElementPoop : MonoBehaviour {

    private Animation _animation;

    private void Awake()
    {
        _animation = GetComponent<Animation>();
    }


    private void Update()
    {
        CheckAnimationCompleted();
    }

    private void CheckAnimationCompleted()
    {
        if(_animation.isPlaying)
            return;

        Destroy(this.gameObject);
    }

    public class Factory : PlaceholderFactory<ElementPoop> {
    }
}
