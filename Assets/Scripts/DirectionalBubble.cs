using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalBubble : MonoBehaviour
{
    [Header("Variables"), Space(5)]
    [SerializeField] private float _propulsionForce;
    [SerializeField] private float _jumpAngle;
    private Vector2 direction;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision directional bubble");
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Player collision");
            JumpWithAngle(_propulsionForce, _jumpAngle);
        }
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

    public void JumpWithAngle(float propulsionForce, float jumpAngle)
    {
        // Utilisation d'Euler Angles et Quaternion pour la rotation
        // On crée une rotation qui tourne autour de l'axe Z avec l'angle spécifié.
        Quaternion rotation = Quaternion.Euler(0, 0, jumpAngle);
    
        // La direction de base du vecteur de saut est vers le haut (0, 1)
        direction = rotation * Vector2.up; // Applique la rotation à "Vector2.up" (haut)

        // Dessiner la ligne pour visualiser la direction
        Vector2 startPoint = PlayerController.Instance.transform.position;  // Position du joueur
        Debug.DrawLine(startPoint, startPoint + direction * 2f, Color.red, 0.5f); // Ligne rouge pendant 0.5 secondes
    
        // Annulation de la force de saut du player
        //PlayerController.Instance.transform.position = PlayerController.Instance._playerRb.velocity;
        
        // Application de la force au Rigidbody
        //PlayerController.Instance._playerRb.AddForce(direction * propulsionForce);
        StartCoroutine(PropulsionPlayer());

    }

    public IEnumerator PropulsionPlayer()
    {
        Vector2 currentVelocity = PlayerController.Instance._playerRb.velocity;
        //currentVelocity.y = 0;
        
        // Application de la force au Rigidbody
        PlayerController.Instance._playerRb.AddForce(direction * _propulsionForce);
        
        currentVelocity.y = 0;
        
        yield return null;
    }

}
