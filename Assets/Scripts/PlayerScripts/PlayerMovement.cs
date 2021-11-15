using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerState
{
    walk,
    attack,
    interact,
    stagger,
    idle
}

public class PlayerMovement : MonoBehaviour
{
    // Player's movement variables
    
    [SerializeField] private float speed;
    private Rigidbody2D myRigidBody;
    private Vector3 change;
    // Player's movement animations
    private Animator animator;
    public PlayerState currentState;
    [SerializeField] private bool timeToUpdateAnimationAndMove = false;
    // health
    public FloatValue currentHealth;
    public SignalSender playerHealthSignal;
    // scene transferring
    public VectorValue startingPosition;
    // inventory references
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private SpriteRenderer receivedItemSprite;

    
    
    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerState.walk;
        animator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);

        transform.position = startingPosition.initalValue;   
    }
    // Update is called once per frame
    void Update()
    {
        // is the player in an interacting state?
        if (currentState == PlayerState.interact)
        {
            return;
        }
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("Attack") && currentState != PlayerState.attack
            &&  currentState != PlayerState.stagger)
        {
            StartCoroutine(AttackCo());
        }
        else if (currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            timeToUpdateAnimationAndMove = true;
        }
    }
    void FixedUpdate()
    {
         if (timeToUpdateAnimationAndMove)
        {
            UpdateAnimationAndMove(); 
            timeToUpdateAnimationAndMove = false;
        } 
    }
    private IEnumerator AttackCo()
    {
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.3f);
        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }
    }
    private void RaiseItem()
    {
        if (playerInventory.currentItem != null)
        {
            if (currentState != PlayerState.interact)
            {
                animator.SetBool("receive item", true);
                currentState = PlayerState.interact;
                receivedItemSprite.sprite = playerInventory.currentItem.itemSprite;
            }
            else
            {
                animator.SetBool("receive item", false);
                currentState = PlayerState.idle;
                receivedItemSprite.sprite = null;
                playerInventory.currentItem = null;
            }
        }
    }
    private void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }
    private void MoveCharacter()
    {
        myRigidBody.MovePosition(transform.position + change.normalized * speed * Time.fixedDeltaTime);
    }
    public void Knock(float damage)
    {
        currentHealth.RuntimeValue -= damage;
        playerHealthSignal.Raise();
        
        if (currentHealth.RuntimeValue > 0)
        {
            StartCoroutine(KnockCo());
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
    private IEnumerator KnockCo()
    {
        if (myRigidBody != null)
        {
            yield return new WaitForSeconds(.3f); // basically wait. 
            myRigidBody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            myRigidBody.velocity = Vector2.zero;
        }
    }
}
