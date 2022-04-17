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
    GameControl.MOVE lastJahat= GameControl.MOVE.LEFT; 

    void Start()
    {
        Anim=this.GetComponentInChildren<Animator>();
        intSpeed=Speed;
    }

     private void FixedUpdate() {
        float s1 = Time.deltaTime*Speed;
        this.transform.position  = this.transform.position - s1 * (this.transform.position-player.transform.position) ; 
        float x=this.transform.position.x-player.transform.transform.position.x;
        if(x>10||x<-10){
        if(this.transform.position.x>player.transform.transform.position.x)
            turnAround(GameControl.MOVE.LEFT);
        else
            turnAround(GameControl.MOVE.RIGHT);
        }

    }
    public void turnAround(GameControl.MOVE d){

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
            Destroy(other.gameObject);

            if(Speed>0){
                Anim.speed=0.5f*Anim.speed;
            }
            else
                if(Anim!=null)
                    Anim.SetTrigger("xend");
        }
    }
}
