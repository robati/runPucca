using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    public AudioSource musicSource;
	public AudioClip m1;
	public AudioClip m2;
    public GameObject d1;
    public GameObject d2;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag.Equals("garu1")){
            // Debug.Log("yeah");
        	musicSource.PlayOneShot(m1);
             StartCoroutine(ok(1));


           

            //WaitForSeconds(1);
           // d1.SetActive(false);


        }
        else if(other.gameObject.tag.Equals("en1")){
            // Debug.Log("damit");
            musicSource.PlayOneShot(m2);
            StartCoroutine(ok(2));
            d1.SetActive(false);
            d2.SetActive(true);
            
        }
           
    }

 
    private IEnumerator ok(int id){

        yield return new WaitForSeconds(1);
         Time.timeScale = 0f;
        if(id==1){
            d1.SetActive(true);
            d2.SetActive(false);
        }   
                //  d1.SetActive(false);
        else if(id==2)
            d2.SetActive(false);

    }

}