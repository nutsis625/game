using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class player : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform groundCheck;

    [Header("Attack Settings")]
    public float attackCooldown = 0.5f;
    public Transform attackPoint;
    public float attackRange = 0.8f;
    public int attackDamage = 25;
    public LayerMask enemyLayers;

    [Header("Animation")]
    public Animator animator;

    private Rigidbody2D rb;
    private bool isFacingRight = true;
    [SerializeField] CharacterBase characterBase;
    private float nextAttackTime = 0f;
    private bool isAttacking = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (animator == null) animator = GetComponent<Animator>();
    }

    private void Update()
    {

        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        if (moveInput > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && isFacingRight)
        {
            Flip();
        }

        if (Input.GetButtonDown("Jump") && characterBase.IsGrounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);

        }

        if (Time.time >= nextAttackTime && Input.GetButtonDown("Fire1") && !isAttacking)
        {
            Attack();
            nextAttackTime = Time.time + attackCooldown;
        }
        UpdateAnimations();
    }



    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void Attack()
    {
        isAttacking = true;
        
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    
    public void EndAttack()
    {
        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    public void TakeDamage(int damage)
    {
        
        animator.SetTrigger("Hurt");
        
    }

    private void UpdateAnimations()
    {
       
        animator.SetBool("IsGround", characterBase.IsGrounded);
        animator.SetBool("Run", characterBase.IsGrounded && rb.velocity.x != 0);
        animator.SetBool("Jump", !characterBase.IsGrounded && rb.velocity.y > 0);
        //animator.SetBool("Attack", isAttacking);
    }
}