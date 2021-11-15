using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyState
{
    idle,
    walk, 
    attack,
    stagger
}
public class Enemy : MonoBehaviour
{
    public EnemyState currentState;
    public FloatValue maxHealth;
    [SerializeField] float health;
    [SerializeField] string enemyName;
    [SerializeField] int baseAttack;
    [SerializeField] public float moveSpeed;

    private void Awake()
    { 
       health = maxHealth.initialValue;
        
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
    public void Knock(Rigidbody2D myRigidBody, float damage)
    {
        StartCoroutine(KnockCo(myRigidBody));
        TakeDamage(damage);
    }
    // Start is called before the first frame update
    private IEnumerator KnockCo(Rigidbody2D myRigidBody)
    {
        if (myRigidBody != null)
        {
            yield return new WaitForSeconds(.3f); // basically wait. 
            myRigidBody.velocity = Vector2.zero;
            currentState = EnemyState.idle;
            myRigidBody.velocity = Vector2.zero;
        }

    }
}
