using UnityEditor;
using UnityEngine;

/// <summary>
/// Simple script that handles offsetting gear based on a "reference" hand's position.
/// Assumes that this script is on the root gameObject of the gear, and the gear is a child of the actual hand.
/// </summary>
public class GearPositioningScript : MonoBehaviour
{
    [SerializeField]
    private Transform handReferencePoint;
    
    [ContextMenu("Update Position")]
    private void OnValidate()
    {
        // Makes sure the gear is positioned correctly when viewing in the editor
        UpdatePosition();
    }

    private void Awake()
    {
        // We don't want the hand reference point to be visible in the actual game
        handReferencePoint.gameObject.SetActive(false);
        UpdatePosition();
    }

    public void UpdatePosition()
    {
        // Prevents changing the position of a prefab asset (which doesn't have a parent)
        if (transform.parent == null)
        {
            return;
        }

        var offset = handReferencePoint.localPosition;
        transform.localPosition = -offset;
        transform.localRotation = Quaternion.identity;
    }

    private void OnDrawGizmos()
    {
        // Draws the origin, useful when editing prefabs
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.1f);
    }
}