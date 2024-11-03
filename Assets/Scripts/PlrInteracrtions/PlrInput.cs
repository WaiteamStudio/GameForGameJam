using UnityEngine;

[RequireComponent(typeof(PlrMovement))]
public class PlrInput : MonoBehaviour
{
    private PlrMovement plrMovement; //������ ������

    [SerializeField]
    Weapon Weapon;
    PlayerController playerController;
    private void Awake()
    {
        plrMovement = GetComponent<PlrMovement>();
        playerController = GetComponent<PlayerController>();
    }
    private void Update()
    {
        float horizontalDirection = Input.GetAxisRaw(GlobalStringVars.HORIZONTAL_AXIS);
        bool isJumpButtonPressed = Input.GetButtonDown(GlobalStringVars.JUMP);
        bool isSwitchFormPressed = Input.GetButtonDown(GlobalStringVars.SWITCH_FORM);
        bool isTeleportPressed = Input.GetButtonDown(GlobalStringVars.TELEPORT_BUTTON);
        bool isFirePressed = Input.GetButtonDown(GlobalStringVars.FIRE);
        if(isFirePressed)
            Weapon.Shoot();
        plrMovement.Move(horizontalDirection, isJumpButtonPressed);
        if (isSwitchFormPressed)
        {
            playerController.SwitchForm();
        }
        if (isTeleportPressed)
        {
            playerController.TryTeleport();
        }
    }
}
