using Cinemachine.Utility;
using StarterAssets;
using UnityEngine;

/// <summary>
/// State machine that manages player actions from a top level
/// </summary>
public class PlayerManager : MonoBehaviour
{
    [Header("Dependencies"), SerializeField]
    private ThirdPersonController moveController;

    [SerializeField] private StarterAssetsInputs input;
    [SerializeField] private Animator anim;
    [SerializeField] private PlayerAnimEventHandler animEventHandler;

    [Header("Throwing Config"), SerializeField]
    private ThrowingGearScript throwGear;

    [SerializeField] private Transform throwingHandTransform;

    [Header("Whacking Config"), SerializeField]
    private WhackingGearScript whackingGearScript;

    private enum PlayerState
    {
        Default,
        Attacking,
        Throwing
    }

    [SerializeField] private PlayerState currentState;

    // Throwing state
    private ThrowingGearScript currentlyHeldThrowGear;
    private Vector3 throwDir;

    private void Start()
    {
        currentState = PlayerState.Default;
    }

    private void Update()
    {
        var canMove = currentState == PlayerState.Default;
        var canRotate = currentState == PlayerState.Default;
        moveController.UpdateController(canMove, canRotate);

        if (currentState == PlayerState.Default)
        {
            if (input.primaryAction)
            {
                StartAttack();
            }
            else if (input.secondaryAction)
            {
                StartThrow();
            }
        }
        else if (currentState == PlayerState.Attacking)
        {
        }
        else if (currentState == PlayerState.Throwing)
        {
            // During the windup, before the gear is thrown, allow the player to aim
            if (currentlyHeldThrowGear)
            {
                throwDir = Camera.main.transform.forward;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(throwDir.ProjectOntoPlane(Vector3.up), Vector3.up), 0.4f);
            }
        }

        UpdateAnimations();
    }

    private void UpdateAnimations()
    {
        anim.SetInteger("Superstate", currentState == PlayerState.Default ? 0 : 1);
        anim.SetInteger("DefaultAnim", moveController.Controller.velocity.magnitude > 0.5f ? 1 : 0);
        anim.SetInteger("ActionAnim", currentState == PlayerState.Attacking ? 0 : 1);
    }

    private void StartAttack()
    {
        currentState = PlayerState.Attacking;
        animEventHandler.OnAttackEndEvent += DoEndAttack;
        animEventHandler.OnSwingStartEvent += DoSwingStart;
    }

    private void DoSwingStart()
    {
        whackingGearScript.SetGearActive(true);
    }

    private void DoEndAttack()
    {
        whackingGearScript.SetGearActive(false);
        
        currentState = PlayerState.Default;
        
        animEventHandler.OnAttackEndEvent -= DoEndAttack;
        animEventHandler.OnSwingStartEvent -= DoSwingStart;
    }

    private void StartThrow()
    {
        currentlyHeldThrowGear = Instantiate(throwGear, transform.position, Quaternion.identity);
        throwDir = Camera.main.transform.forward;
        currentlyHeldThrowGear.transform.SetParent(throwingHandTransform);
        currentlyHeldThrowGear.Setup();
        
        currentState = PlayerState.Throwing;

        animEventHandler.OnThrowEndEvent += DoEndThrow;
        animEventHandler.OnThrowReleaseEvent += DoThrowRelease;
    }

    private void DoThrowRelease()
    {
        currentlyHeldThrowGear.Throw(throwDir);
        currentlyHeldThrowGear = null;
    }

    private void DoEndThrow()
    {
        currentlyHeldThrowGear = null;
        
        currentState = PlayerState.Default;
        
        animEventHandler.OnThrowReleaseEvent -= DoThrowRelease;
        animEventHandler.OnThrowEndEvent -= DoEndThrow;
    }
}