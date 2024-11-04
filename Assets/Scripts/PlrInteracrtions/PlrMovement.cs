using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlrMovement : MonoBehaviour
{
    [SerializeField] private float jumpForce; //сила прыжка
    [SerializeField] public bool isGrounded = false; //булевая переменная проверки стоит ли на земле персонаж
    [SerializeField] private float speed; //скорость персонажа
    [SerializeField] private Transform groundColliderTransform; //трансформ для того чтоб узнать позицию нижнего коллайдера персонажа
    [SerializeField] private float jumpOffset; //множитель на который мы увеличиваем наш обычный ground collider чтоб прыжок был более отзывчивым
    [SerializeField] private LayerMask groundMask; //layer на котором находится земля
    private Rigidbody2D rb; //риджит боди объекта
    public bool IsMoving;
    private Animator anim;
    private float dir = 0;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Vector3 overlapCirclePosition = groundColliderTransform.position;
        isGrounded = Physics2D.OverlapCircle(overlapCirclePosition, jumpOffset, groundMask);
        if (isGrounded) { anim.SetBool("isJumping", false); anim.SetBool("isGrounded", true); }
        else anim.SetBool("isGrounded", false);
        anim.SetFloat("speed", Mathf.Abs(dir));
        anim.SetFloat("verticalSpeed", rb.velocity.y);
    }

    public void Move(float direction, bool isJumpButtonPressed)
    {
        dir = direction;
        if (isJumpButtonPressed)
        {
            Jump();

        }

        if (direction != 0)
        {
            HorizontalMovement(direction);
            IsMoving = true;
        }
        else
            IsMoving = false;
    }

    private void Jump()
    {
        SoundManager.PlaySound(SoundManager.Sound.PlayerJump);
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            anim.SetBool("isJumping", true);
        }
    }

    private void HorizontalMovement(float direction)
    {
        if(ServiceLocator.current.Get<PlayerController>().GetHealthComponent().GetForm() == PlayerForm.Fire)
        {
            SoundManager.PlaySound(SoundManager.Sound.PlayerMoveFireForm);
        }
        else
        {
            SoundManager.PlaySound(SoundManager.Sound.PlayerMoveWaterForm);
        }
        rb.velocity = new Vector2(direction*speed, rb.velocity.y);

        transform.localScale = new Vector3(Mathf.Sign(rb.velocity.x)* 0.54922f, 0.54922f, 0.54922f);//0.54922
    }
}
