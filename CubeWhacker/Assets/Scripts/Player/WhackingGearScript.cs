using UnityEngine;

public class WhackingGearScript : MonoBehaviour
{
    [SerializeField]
    private LayerMask hitMask;

    private bool isActive;

    private void OnTriggerEnter(Collider coll)
    {
        if (isActive && (hitMask.value | (1 << coll.gameObject.layer)) == hitMask.value)
        {
            var hit = coll.GetComponent<CombatEntity>();
            if (hit != null)
            {
                hit.Hit();
            }
        }
    }

    public void SetGearActive(bool val)
    {
        isActive = val;
    }
}