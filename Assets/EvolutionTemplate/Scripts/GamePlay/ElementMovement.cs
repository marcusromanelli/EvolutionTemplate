using UnityEngine;


/// <summary>
/// A simple movement controller. Randomly picks a walkable place on the map and move the character to it.k
/// </summary>
[RequireComponent(typeof(EvolutionElement))]
public class ElementMovement : MonoBehaviour {
    const string FLOOR_TAG = "NotWalkable";

    private EvolutionElement element;
    private Vector3 _nextPosition;
    private float lastWanderTime;
    private Vector3 _lastKnownPosition;

    private ElementProperty Properties
    {
        get
        {
            return element.Properties;
        }
    }

    public void SetSelected()
    {
        _lastKnownPosition = transform.position;
    }

    public void UnSelect()
    {
        CheckPosition();
    }
    private void CheckPosition()
    {
        Collider2D[] foundRaycasts;
        bool isClear = CheckClearPosition(transform.position, out foundRaycasts);

        if(isClear)
        {
            SetStatus(ElementStatus.Idle);
            _nextPosition = transform.position;
            return;
        }

        bool foundNotWalkableFloor = false;
        foreach(Collider2D collider in foundRaycasts)
        {
            if(collider.CompareTag(FLOOR_TAG))
            {
                foundNotWalkableFloor = true;
                break;
            }
        }

        if(foundNotWalkableFloor)
        {
            SetStatus(ElementStatus.ReturningToPosition);
            return;
        }

        SetStatus(ElementStatus.Idle);
        _nextPosition = transform.position;
    }

    private void Awake()
    {
        element = GetComponent<EvolutionElement>();
    }

    private void Update()
    {
        RandomWalk();
    }

    private ElementStatus GetStatus()
    {
        return element.GetStatus();
    }
    private void SetStatus(ElementStatus status)
    {
        element.SetStatus(status);
    }

    private bool CanWander()
    {
        return GetStatus() == ElementStatus.Idle;
    }

    private bool FlyToPosition(Vector3 newPosition)
    {
        if(transform.position == newPosition)
        {
            return true;
        }

        return MoveToPosition(newPosition, Properties.FlySpeed);
    }

    private bool MoveToPosition(Vector3 newPosition, float speed)
    {
        if(transform.position == newPosition)
            return true;

        Vector3 nextPosition = Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);

        transform.position = nextPosition;

        return false;
    }

    private void CalculateNextPosition()
    {

        _nextPosition = Vector3.zero;
        bool foundNewPosition = false;

        while(!foundNewPosition)
        {
            _nextPosition = CalculateRandomPositionForMovement();

            foundNewPosition = CheckClearPosition(_nextPosition);
        }
    }
    private bool CheckClearPosition(Vector3 position, out Collider2D[] raycasts)
    {
        raycasts = Physics2D.OverlapBoxAll(position, Vector2.one * 0.1f, 0);

        if(raycasts.Length < 0)
        {
            return true;
        }
        else
        {
            bool foundNotWalkableFloor = false;
            foreach(Collider2D collider in raycasts)
            {
                if(collider.CompareTag(FLOOR_TAG))
                {
                    foundNotWalkableFloor = true;
                    break;
                }
            }

            return !foundNotWalkableFloor;
        }
    }
    private bool CheckClearPosition(Vector3 position)
    {
        Collider2D[] raycasts;

        return CheckClearPosition(position, out raycasts);
    }

    private Vector3 CalculateRandomPositionForMovement()
    {
        Vector3 nextPosition = transform.position;
        float radius = Properties.MovementRadius / 2;
        float randomX = nextPosition.x + Random.Range(-radius, radius);
        float randomY = nextPosition.y + Random.Range(-radius, radius);

        nextPosition.x = randomX;
        nextPosition.y = randomY;

        return nextPosition;
    }
    private void RandomWalk()
    {
        if(GetStatus() == ElementStatus.ReturningToPosition)
        {
            if(FlyToPosition(_lastKnownPosition))
                SetStatus(ElementStatus.Idle);

            return;
        }

        if(!CanWander())
            return;

        if((_nextPosition == Vector3.zero || transform.position == _nextPosition) && (Time.time - lastWanderTime) >= Properties.MinWanderAwait)
        {
            lastWanderTime = Time.time;
            CalculateNextPosition();
        }

        MoveToPosition(_nextPosition, Properties.WalkSpeed);
    }
}
