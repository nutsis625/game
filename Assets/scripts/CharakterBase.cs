using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    private bool _isGrounded;
    [SerializeField] private Animator _animator;
    public bool IsGrounded { get { return _isGrounded; } private set { } }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(1);
        if (collision.gameObject.layer == LayerMask.NameToLayer("floor"))
        {
            _isGrounded = true;
            Debug.Log(_isGrounded);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log(2);
        if (collision.gameObject.layer == LayerMask.NameToLayer("floor"))
        {
            _isGrounded = false;
            Debug.Log(_isGrounded);
        }
    }
}