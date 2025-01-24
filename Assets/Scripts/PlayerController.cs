using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour
{
    [Header("References"), Space(5)]
    [SerializeField] private GameObject _playerGo;
    [SerializeField] private Rigidbody2D _playerRb;
    
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
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float horizontalMovement = player.GetAxis("HorizontalMovement") * _moveSpeed * Time.deltaTime;
        
        Vector3 targetVelocity = new Vector2(horizontalMovement, _playerRb.velocity.y);
        _playerRb.velocity = Vector3.SmoothDamp(_playerRb.velocity, targetVelocity, ref velocity, 0.05f);
        
        //if (player.GetButton("RightMovement"))
        //{
        //    _playerRb.MovePosition(transform.position + transform.right * _moveSpeed * Time.fixedDeltaTime);
        //}
        //
        //if (player.GetButton("LeftMovement"))
        //{
        //    _playerRb.MovePosition(transform.position + (-transform.right) * _moveSpeed * Time.fixedDeltaTime);
        //}
        
        if (player.GetButtonDown("Jump"))
        {
            _playerRb.AddForce(transform.up * _jumpForce);
        }
    }
}
