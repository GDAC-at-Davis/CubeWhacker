using UnityEngine;

/// <summary>
/// Simple target dummy style enemy
/// </summary>
public class BoxEnemy : MonoBehaviour
{
    [Header("Dependencies"), SerializeField]
    private CombatEntity combatEntity;

    [SerializeField] private VFXScript deathVFX;

    private void Awake()
    {
        combatEntity.OnHit += OnHit;
        EnemySpawner.EnemyCount++;
    }

    private void OnDestroy()
    {
        combatEntity.OnHit -= OnHit;
        EnemySpawner.EnemyCount--;
    }

    private void OnHit()
    {
        Destroy(gameObject);
        Instantiate(deathVFX, transform.position, Quaternion.identity);
        ScoreCounter.Instance.AddScore(1);
    }
}