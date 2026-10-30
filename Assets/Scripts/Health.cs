using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealthPoint = 100;

    private int _healthPoint;

    public bool IsDead => _healthPoint <= 0;

    private void Start() => _healthPoint = _maxHealthPoint;

    public void TakeDamage(int damage)
    {
        if (IsDead) return;

        _healthPoint -= damage;
        if (IsDead)
        {
            Die();
        }
    }

    private void Die() => Destroy(gameObject);
}
