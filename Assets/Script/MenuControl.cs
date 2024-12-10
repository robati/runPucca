using System.Collections;
 using UnityEngine.SceneManagement;

using System.Collections.Generic;
using UnityEngine;

public class MenuControl : MonoBehaviour
{
    // Start is called before the first frame update
   private static bool _created = false;
   public GameObject puccaMenu;
   public GameObject PauseMenu;
    
   private void Awake()
     {
        if (!_created){
            DontDestroyOnLoad(this.gameObject);
            _created = true;
            init();
        }
     }

     public void RsetGame()
     {
        SceneManager.LoadScene(0);
        puccaMenu.gameObject.SetActive(false);
        PauseGame(false);
      }
    public void PauseGame(bool pauseGame)
    {
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
   public void QuitGame(){
        Application.Quit();
     }
     public void MuteGame(bool muteGame){
    // DebugText.text+=muteGame;
        if(!muteGame)
             AudioListener.volume = 0f;
        else
             AudioListener.volume = 1f;
     }
      public void ShowHelper(bool showAgain){
        
         PlayerPrefs.SetInt("savedFirstRun",1) ;
        //  GuidPanel.SetActive(true);
         if(showAgain)
            PlayerPrefs.SetInt("savedFirstRun",0) ;
     }
     void init(){
        puccaMenu.gameObject.SetActive(true);
        PauseGame(true);
        PauseMenu.gameObject.SetActive(false);
     }


    
    //TODO:2-make it time limited to get to garu
    // make the tutorial!
        
    
}
