using UnityEngine;

[RequireComponent(typeof(PlrMovement))]
public class PlrInput : MonoBehaviour
{
    private PlrMovement plrMovement; //������ ������
    private PlayerForm currentForm = PlayerForm.Fire; //��������� �����
    private TeleportationPoint nearTeleportationPoint = null; // ������� ����� ������������ ����� � ����������

    [SerializeField] private Sprite fireFormSprite; //������ ��� �������� �����
    [SerializeField] private Sprite waterFormSprite; //������ ��� ������� �����
    private SpriteRenderer spriteRenderer; //��������� ������ �������� ��������� 

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
        Debug.Log("�������� ������ ����� ��: " + currentForm);
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
                transform.position = target.position; // ������������ � �����
                Debug.Log("�������� ����������������!");
            }
        }
        else
        {
            Debug.Log("������������ ����������: ���� �������� �� � ����� ����, ���� ����� ��� ����� ������������.");
        }
    }
}
