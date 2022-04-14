using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    // Start is called before the first frame update
    private static bool _created = false;
     public GameObject puccaMenu;
    public GameObject PauseMenu;
        //  //Accessible only trough editor or from this class
        //  [SerializeField]
        //  private int maxLives = 5;
 
        //  public int livesLeft;
 
         private void Awake()
         {
             if (!_created)
             {
                 DontDestroyOnLoad(this.gameObject);
                 _created = true;
                 init();
             }
         }
              public void PauseGame(bool pauseGame){
         if(pauseGame){
            Time.timeScale = 0f;
            AudioListener.pause = true;
            PauseMenu.SetActive(true);
         }
         else{
            Time.timeScale = 1;
            AudioListener.pause = false;
            PauseMenu.SetActive(false);

         }

     }
     void init(){
         puccaMenu.gameObject.SetActive(true);
        PauseGame(true);
        PauseMenu.gameObject.SetActive(false);
     }
    void Start()
    {
                
    }

    // Update is called once per frame
    void Update()
    {//TODO: 1-make xc from random places.2-make it time limited to get to garu3-clean code and scene files 4-make source tree work for g s!
    //BUG Fix: when pushing right and sh it goes left and sh . make the tutorial!
        
    }
}
