using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private Animator Anim;

    void Start()
    {
        Anim=this.GetComponentInChildren<Animator>();
 
    }

    private void OnCollisionEnter2D(Collision2D other) {
        // Debug.Log((other.gameObject.tag));
                if(other.gameObject.tag.Equals("Player")){
                    Anim.SetTrigger("flee");
                }

    }
}
