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
    [SerializeField] private bool _asDobleJump;
    private Vector3 velocity = Vector3.zero;
    
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
        float horizontalMovement = player.GetAxis("HorizontalMovement") * _moveSpeed * Time.deltaTime;
        
        Vector3 targetVelocity = new Vector2(horizontalMovement, _playerRb.velocity.y);
        _playerRb.velocity = Vector3.SmoothDamp(_playerRb.velocity, targetVelocity, ref velocity, 0.05f);
                
        if (player.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    public void Jump()
    {
        _playerRb.AddForce(transform.up * _jumpForce);
    }
}
