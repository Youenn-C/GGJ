using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    
    [Header("References"), Space(5)]
    [SerializeField] private GameObject _playerGo;
    public Rigidbody2D _playerRb;
    
    [Header("Variables"), Space(5)]
    [SerializeField] private int _jumpForce;
    [SerializeField] private int _moveSpeed;
    [Space(5)]
    [SerializeField] private bool _isAlive;
    [SerializeField] private bool _isGrounded;
    [SerializeField] private string[] _isGroundedTags;
    [SerializeField] private bool _asDobleJump;
    private Vector3 velocity = Vector3.zero;
    
    public bool canMove = true;
    
    [Header("Rewired"), Space(5)]
    public Player player;
    [SerializeField] private int playerId = 0;

    void Awake()
    {
        player = ReInput.players.GetPlayer(playerId);

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if (canMove)
        {
            float horizontalMovement = player.GetAxis("HorizontalMovement") * _moveSpeed * Time.deltaTime;
        
            Vector3 targetVelocity = new Vector2(horizontalMovement, _playerRb.velocity.y);
            _playerRb.velocity = Vector3.SmoothDamp(_playerRb.velocity, targetVelocity, ref velocity, 0.05f);
                
            if (player.GetButtonDown("Jump") && _isGrounded)
            {
                Jump(_jumpForce);
            }
        }
    }

    public void Jump(float jumpForce)
    {
        _playerRb.AddForce(transform.up * _jumpForce);
        _isGrounded = false;
    }

    public void JumpWithAngle(float jumpForce, float jumpAngle)
    {
        float angleInRadians = jumpAngle * Mathf.PI / 180.0f;
        Vector2 direction = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));
        Debug.Log(direction * jumpForce);
        _playerRb.AddForce(direction * jumpForce);
        _isGrounded = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (var tag in _isGroundedTags)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                _isGrounded = true;
            }
        }
    }
}
