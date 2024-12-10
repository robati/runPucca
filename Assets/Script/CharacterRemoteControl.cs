using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRemoteControl : MonoBehaviour
{
        public float Speed=5f;
        public GameObject Goal;
            private Animator Animl;

    // Start is called before the first frame update
    void Start()
    {
                Animl=gameObject.GetComponentInChildren<Animator>();

        Animl.SetBool("moving",true);
    }

    // Update is called once per frame
    void Update()
    {
        float s1 = Time.deltaTime*Speed;
       // float s1 = Speed;
        this.transform.position  = Vector3.Lerp(this.transform.position,Goal.transform.position,s1* 2/10) ; 
        
        // this.transform.position  = Vector3.Lerp(this.transform.position - s1 * (this.transform.position-Goal.transform.position) ; 
        float x=this.transform.position.x-Goal.transform.transform.position.x; 
    }
}
