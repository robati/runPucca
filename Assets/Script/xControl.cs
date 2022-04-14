using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xControl : MonoBehaviour
{
    public GameObject player;
    public float Speed=0.01f;
    // Start is called before the first frame update
    float intSpeed=0;
      private Animator Anim;
//  public Animation animation;
 private float animSpeed = 0.6f;
 gamec.MOVE lastJahat= gamec.MOVE.LEFT; 

    void Start()
    {
          
  
        Anim=this.GetComponentInChildren<Animator>();
        intSpeed=Speed;
        //  animation = GetComponent<Animation>();
//  animation["name of animation"].speed = animSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //if(touch.position.x <c1.x ||touch1.position.x < c1.x )
            
    }
     private void FixedUpdate() {
        float s1 = Time.deltaTime*Speed;
        this.transform.position  = this.transform.position - s1 * (this.transform.position-player.transform.position) ; 
        float x=this.transform.position.x-player.transform.transform.position.x;
        if(x>10||x<-10){
        if(this.transform.position.x>player.transform.transform.position.x)
            turnAround(gamec.MOVE.LEFT);
        else
        
            turnAround(gamec.MOVE.RIGHT);
        }

    }
    public void turnAround(gamec.MOVE d){

        if(lastJahat==d)
            return;
        Vector3 newScale = this.transform.localScale;
        newScale.x *= -1;
        this.transform.localScale = newScale;
        lastJahat=d;
    }
   private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag.Equals("shc")){
            Speed-=intSpeed/2;
            // Debug.Log("oh"+Speed+" "+intSpeed);
            Destroy(other.gameObject);
            //m
            if(Speed>0){
                Anim.speed=0.5f*Anim.speed;
                
            }
            else
            if(Anim!=null)
                Anim.SetTrigger("xend");

        	// musicSource.PlayOneShot(m1);
            // d1.SetActive(true);
            // d2.SetActive(false);

            // StartCoroutine(ok(1));

            //WaitForSeconds(1);
           // d1.SetActive(false);


        }
        }
}
