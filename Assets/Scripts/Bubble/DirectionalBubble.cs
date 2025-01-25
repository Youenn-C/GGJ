using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalBubble : MonoBehaviour
{
    [Header("References"), Space(5)]
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _targetRotation;
    
    [SerializeField] private Transform _vectorTarget;
    [SerializeField] private Transform _bubbleTransform;
    
    [SerializeField] private Collider2D _bubbleCollider;
    [SerializeField] private int _targetAngle;
    [SerializeField] private int _force;
    [SerializeField] private Vector2 _direction;
    
    [SerializeField, Range(0,10)] private float timeRemaining = 3f; // Temps initial en secondes
    [SerializeField] private float currentTime;
    
    
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        _playerPrefab = GameObject.FindGameObjectWithTag("Player");
        
        _targetRotation.transform.localEulerAngles = new Vector3(0, 0, _targetAngle);
        
        Vector2 bubble = new Vector2(_bubbleTransform.position.x, _bubbleTransform.position.y);
        Vector2 target = new Vector2(_vectorTarget.position.x, _vectorTarget.position.y);
        
        // Calcul du vecteur de direction allant de 'bubble' à 'target'
        _direction = (target - bubble).normalized;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController.Instance._playerRb.constraints = RigidbodyConstraints2D.FreezeAll;
        StartCoroutine(LaunchPlayer());
    }


    public IEnumerator LaunchPlayer()
    {
        Debug.Log("move");
        _playerPrefab.transform.position = transform.position;
        Debug.Log("cooldown");
        yield return new WaitForSeconds(2f);
        Debug.Log("ropultion");
        PlayerController.Instance._playerRb.constraints = RigidbodyConstraints2D.None;
        PlayerController.Instance._playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        PlayerController.Instance._playerRb.AddForce(_direction * _force, ForceMode2D.Impulse);

        StartCoroutine(ForcedMovement());
    }

    public IEnumerator ForcedMovement()
    {
        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime; // Décrémente le temps
            
            if (_targetAngle > 0)
            {
                Vector3 targetVelocity = new Vector2(currentTime, PlayerController.Instance._playerRb.velocity.y);
                PlayerController.Instance._playerRb.velocity = Vector3.SmoothDamp(PlayerController.Instance._playerRb.velocity, targetVelocity, ref velocity, 0.05f);
            }
            else if (_targetAngle < 0)
            {
                Vector3 targetVelocity = new Vector2(-currentTime, PlayerController.Instance._playerRb.velocity.y);
                PlayerController.Instance._playerRb.velocity = Vector3.SmoothDamp(PlayerController.Instance._playerRb.velocity, targetVelocity, ref velocity, 0.05f);
            }
        }
        
        currentTime = timeRemaining;
        yield return null;

    }
    }

    void JumpWithAngle(float propulsionForce, float jumpAngle)
    {
        PlayerController.Instance.JumpWithAngle(propulsionForce, jumpAngle);
    }

    /*public void JumpWithAngle(float propulsionForce, float jumpAngle)
    {
        Debug.Log("jump direction");
    
        // Conversion de l'angle en radians
        float angleInRadians = jumpAngle * Mathf.PI / 180.0f;
    
        // Calcul de la direction du saut
        Vector2 direction = new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians));
    
        // Dessiner le vecteur direction pour le débogage
        Vector2 startPoint = PlayerController.Instance.transform.position;  // Position actuelle du joueur
        Debug.DrawLine(startPoint, startPoint + direction * 2f, Color.red, 0.5f); // Ligne rouge pendant 0.5 secondes
    
        // Application de la force au Rigidbody
        PlayerController.Instance._playerRb.AddForce(direction * propulsionForce );
    }*/

    //public void JumpWithAngle(float propulsionForce, float jumpAngle)
    //{
    //    // Utilisation d'Euler Angles et Quaternion pour la rotation
    //    // On crée une rotation qui tourne autour de l'axe Z avec l'angle spécifié.
    //    Quaternion rotation = Quaternion.Euler(0, 0, jumpAngle);

    //    // La direction de base du vecteur de saut est vers le haut (0, 1)
    //    direction = rotation * Vector2.up; // Applique la rotation à "Vector2.up" (haut)

    //    // Dessiner la ligne pour visualiser la direction
    //    Vector2 startPoint = PlayerController.Instance.transform.position;  // Position du joueur
    //    Debug.DrawLine(startPoint, startPoint + direction * 2f, Color.red, 0.5f); // Ligne rouge pendant 0.5 secondes

    //    // Annulation de la force de saut du player
    //    //PlayerController.Instance.transform.position = PlayerController.Instance._playerRb.velocity;

    //    // Application de la force au Rigidbody
    //    //PlayerController.Instance._playerRb.AddForce(direction * propulsionForce);
    //    StartCoroutine(PropulsionPlayer());

    //}

    //public IEnumerator PropulsionPlayer()
    //{
    //    Vector2 currentVelocity = PlayerController.Instance._playerRb.velocity;
    //    //currentVelocity.y = 0;

    //    // Application de la force au Rigidbody
    //    PlayerController.Instance._playerRb.AddForce(direction * _propulsionForce);

    //    currentVelocity.y = 0;

    //    yield return null;
    //}

}
