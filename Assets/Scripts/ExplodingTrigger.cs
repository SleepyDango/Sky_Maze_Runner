using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using static UnityEditor.PlayerSettings;

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
    [SerializeField] private int _damage = 100;
    [SerializeField] private float _explodingForce;

    public UnityEvent Exploding;
    public GameObject wfxPrefab;
    public Transform spawnPoint;

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
        GameObject effectInstance = Instantiate(wfxPrefab, spawnPoint.position, spawnPoint.rotation);
        Destroy(gameObject);
        var victims = Physics.OverlapSphere(
            transform.position, _explodingRadius);
        foreach (var victim in victims)
        {
            if (victim.TryGetComponent<Health>(out var health))
            {
                health.TakeDamage(_damage); // Example damage value
            }
            if (victim.attachedRigidbody != null)
            {
                victim.attachedRigidbody.AddExplosionForce(
                    _explodingForce, transform.position,
                    _explodingRadius, 0f, ForceMode.Impulse);
            }
        }
    }
}