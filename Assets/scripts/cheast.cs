using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cheast : MonoBehaviour
{
    [SerializeField] SpriteRenderer closedCheast;
    [SerializeField] SpriteRenderer openedCheast;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            closedCheast.enabled = false;
            openedCheast.enabled = true;
        }
    }
}