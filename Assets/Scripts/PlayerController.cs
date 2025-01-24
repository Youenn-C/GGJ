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
                
        if (player.GetButtonDown("Jump"))
        {
            Jump(_jumpForce);
        }
    }

    public void Jump(float jump_force)
    {
        _playerRb.AddForce(Vector2.up * jump_force);
    }

    public void JumpWithAngle(float jumpForce, float jumpAngle)
    {
        Debug.Log("jump direction");
        float angleInRadians = jumpAngle * Mathf.PI / 180.0f;
        Vector2 direction = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));
        _playerRb.AddForce(direction * jumpForce);
    }
}
