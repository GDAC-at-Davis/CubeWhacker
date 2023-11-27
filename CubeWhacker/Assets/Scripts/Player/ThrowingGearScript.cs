using System;
using UnityEngine;

/// <summary>
/// Throwing gear script that explodes on impact
/// </summary>
public class ThrowingGearScript : MonoBehaviour
{
    [Header("Dependencies"), SerializeField]
    private Rigidbody rb;

    [SerializeField] private GearPositioningScript positioningScript;

    [SerializeField]
    private LayerMask explosionHitMask;

    [SerializeField]
    private LayerMask collisionMask;

    [SerializeField] private VFXScript explosionVFX;

    [Header("Stats"), SerializeField]
    private float speed;

    [SerializeField] private float spinSpeed;

    [SerializeField] private float explosionRadius;

    private bool isThrown;

    private void Awake()
    {
        rb.isKinematic = true;
    }

    public void Setup()
    {
        positioningScript.UpdatePosition();
    }

    public void Throw(Vector3 direction)
    {
        // Remove parent
        transform.SetParent(null);

        // Enable physics
        rb.isKinematic = false;
        rb.velocity = direction.normalized * speed;
        rb.angularVelocity = Quaternion.AngleAxis(1, Vector3.Cross(direction, Vector3.up).normalized).eulerAngles.normalized * spinSpeed;

        isThrown = true;
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (isThrown && (collisionMask.value | (1 << coll.gameObject.layer)) == collisionMask.value)
        {
            // Explode
            Explode();
        }
    }

    private void Explode()
    {
        // AOE scan
        var hits = Physics.OverlapSphere(transform.position, explosionRadius, explosionHitMask);

        foreach (var hit in hits)
        {
            var combatEntity = hit.GetComponentInParent<CombatEntity>();
            if (combatEntity != null)
            {
                combatEntity.Hit();
            }
        }

        Instantiate(explosionVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        // Draw AOE radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}