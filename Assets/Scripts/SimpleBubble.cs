using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBubble : MonoBehaviour
{
    [Header("References"), Space(5)]
    [SerializeField] private PlayerController _player;

    [Header("Variables"), Space(5)]
    [SerializeField] private float _jumpForce = 500;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision simple bubble");
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Player collision");
            _player.Jump(_jumpForce);
        }
    }
}
