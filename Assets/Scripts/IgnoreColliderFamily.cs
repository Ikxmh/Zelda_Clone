using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreColliderFamily : MonoBehaviour
{

    [SerializeField] private BoxCollider2D parent;
    [SerializeField] private BoxCollider2D children;

    // Start is called before the first frame update
    private void Start()
    {
        Physics2D.IgnoreCollision(parent, children); // ignore overlapping collision 
    }
}
