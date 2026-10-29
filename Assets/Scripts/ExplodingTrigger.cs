using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public enum ChasingState
{
    Idle,
    Explode,
}

public class ExplodingTrigger : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private NavMeshObstacle _navMeshObstacle;
    [SerializeField] private float _explodingRadius = 5f;

    public UnityEvent Exploding;

    private void Update()
    {
        if (_target == null) return;

        var distance = Vector3.Distance(transform.position, _target.position);

        if (distance < _explodingRadius)
        {
            Explode();
        }
    }

    public void Explode()
    {
        print("Explode !!!");
        Destroy(gameObject);
    }
}