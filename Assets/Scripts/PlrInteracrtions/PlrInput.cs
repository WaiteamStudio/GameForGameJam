using UnityEngine;

[RequireComponent(typeof(PlrMovement))]
public class PlrInput : MonoBehaviour
{
    private PlrMovement plrMovement; //объект класса
    private PlayerForm currentForm = PlayerForm.Fire; //начальна€ форма
    private TeleportationPoint nearTeleportationPoint = null; // “екуща€ точка телепортации р€дом с персонажем

    [SerializeField] private Sprite fireFormSprite; //спрайт дл€ огненной формы
    [SerializeField] private Sprite waterFormSprite; //спрайт дл€ вод€ной формы
    private SpriteRenderer spriteRenderer; //компонент спрайт рендерер пресонажа 

    private void Awake()
    {
        plrMovement = GetComponent<PlrMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        UpdateSprite();
    }

    private void Update()
    {
        float horizontalDirection = Input.GetAxisRaw(GlobalStringVars.HORIZONTAL_AXIS);
        bool isJumpButtonPressed = Input.GetButtonDown(GlobalStringVars.JUMP);
        bool isSwitchFormPressed = Input.GetButtonDown(GlobalStringVars.SWITCH_FORM);
        bool isTeleportPressed = Input.GetButtonDown(GlobalStringVars.TELEPORT_BUTTON);

        plrMovement.Move(horizontalDirection, isJumpButtonPressed);

        if (isSwitchFormPressed)
        {
            SwitchForm();
        }

        if (isTeleportPressed)
        {
            TryTeleport();
        }
    }

    private void SwitchForm()
    {
        currentForm = currentForm == PlayerForm.Fire ? PlayerForm.Water : PlayerForm.Fire;
        UpdateSprite();
        Debug.Log("ѕерсонаж сменил форму на: " + currentForm);
    }

    private void UpdateSprite()
    {
        if (currentForm == PlayerForm.Fire)
        {
            spriteRenderer.sprite = fireFormSprite;
        }
        else
        {
            spriteRenderer.sprite = waterFormSprite;
        }
    }

    public void SetNearTeleportationPoint(TeleportationPoint teleportPoint)
    {
        nearTeleportationPoint = teleportPoint;
    }

    private void TryTeleport()
    {
        if (currentForm == PlayerForm.Water && nearTeleportationPoint != null)
        {
            Transform target = nearTeleportationPoint.GetTeleportTarget();
            if (target != null)
            {
                transform.position = target.position; // “елепортаци€ к точке
                Debug.Log("ѕерсонаж телепортировалс€!");
            }
        }
        else
        {
            Debug.Log("“елепортаци€ невозможна: либо персонаж не в форме воды, либо р€дом нет точки телепортации.");
        }
    }
}
