using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    public AudioSource musicSource;
	public AudioClip m1;
	public AudioClip m2;
    public GameObject wonMenu;
    public MenuControl MenuControl;
 
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag.Equals("garu1")){
        	musicSource.PlayOneShot(m1);
            StartCoroutine(ok(1));//won
        }     
    }

 
    private IEnumerator ok(int id){

        yield return new WaitForSeconds(1);
        MenuControl.PauseGame(true);
        // Time.timeScale = 0f;
        // wonMenu.SetActive(true);

    }

}