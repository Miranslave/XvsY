
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int health = 1;
    public int dmg = 1;
    public float speed = 2;
    [SerializeField] private Transform baseTarget;
    private Rigidbody2D rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // pas de gravité
        rb.freezeRotation = true; // évite la rotation physique
        baseTarget = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    
    private void FixedUpdate() // utiliser FixedUpdate pour la physique
    {
        Vector2 direction; //= Vector2.left;
        
        // direction en fonction de la position de la base
        direction = (new Vector2(baseTarget.position.x,this.transform.position.y) - rb.position).normalized; 
        // applique une vélocité constante
        rb.linearVelocity = direction * speed;
    }

    
    
}
