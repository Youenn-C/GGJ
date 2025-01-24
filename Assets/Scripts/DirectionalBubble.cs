using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalBubble : MonoBehaviour
{
    [Header("References"), Space(5)]
    [SerializeField] private PlayerController _player;

    [Header("Variables"), Space(5)]
    [SerializeField] private float _jumpForce = 500;
    [SerializeField] private float _jumpAngle = 45;

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
        Debug.Log("Collision directional bubble");
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Player collision");
            _player.JumpWithAngle(_jumpForce, _jumpAngle);
        }
    }
}
