using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    [SerializeField] private float thrust;
    [SerializeField] private float damage;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("BreakableObject") && this.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Pot>().Smash();
        }
        else if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player") && other.isTrigger)
        {
            if (other.gameObject.CompareTag("Enemy") && gameObject.CompareTag("Enemy")) // Enemies shouldn't kill each other
                return;
            
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
            if (hit != null)
            {
                Vector2 forceDirection = hit.transform.position - transform.position; // determine 
                Vector2 force = forceDirection.normalized * thrust; // # of direction to push into
                hit.velocity = force; // apply that force to the player or enemy

                if (other.gameObject.CompareTag("Enemy") && other.isTrigger)
                {
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    other.GetComponent<Enemy>().Knock(hit, damage);
                }
                if (other.gameObject.CompareTag("Player"))
                {
                    if(other.GetComponent<PlayerMovement>().currentState != PlayerState.stagger)
                    {
                        hit.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;
                        other.GetComponent<PlayerMovement>().Knock(damage);
                    }   
                }
            }
        }
    }

}
