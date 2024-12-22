using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;

    public float runSpeed = 40f;

    [Header("Dashing")]
    [SerializeField] private float dashingVelocity = 12f;
    [SerializeField] private float dashingTime = 0.15f;
    private Vector2 dashingDir;
    private bool isDashing;
    private bool canDash = true;

    float horizontalMove = 0f;
    bool jump = false;

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }

        if (Input.GetButtonDown("Dash") && canDash)
        {
            isDashing = true;
            canDash = false;
            dashingDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (dashingDir == Vector2.zero)
            {
                dashingDir = new Vector2(transform.localScale.x, 0);
            }

            StartCoroutine(StopDashing());
        }

        animator.SetBool("IsDashing", isDashing);
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            controller.GetComponent<Rigidbody2D>().linearVelocity = dashingDir.normalized * dashingVelocity;
            return;
        }

        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        jump = false;
    }

    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
        canDash = true;
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
        canDash = true; // Dash is reset when landing
    }
}
