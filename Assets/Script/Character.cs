using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    public AudioSource musicSource;
	public AudioClip m1;
	public AudioClip m2;
    public GameObject wonMenu;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag.Equals("garu1")){
        	musicSource.PlayOneShot(m1);
             StartCoroutine(ok(1));//won
        }
        // else if(other.gameObject.tag.Equals("en1")){
        //     musicSource.PlayOneShot(m2);
        //     StartCoroutine(ok(2));
        //     d1.SetActive(false);
        //     d2.SetActive(true);
            
        // }
           
    }

 
    private IEnumerator ok(int id){

        yield return new WaitForSeconds(1);
        Time.timeScale = 0f;
        wonMenu.SetActive(true);

    }

}