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
   public GameObject WinMenu;
    
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
         StopTime(pauseGame);
         PauseMenu.SetActive(pauseGame);
   }
   public void StopTime(bool stop){
         AudioListener.pause = stop;
         Time.timeScale = stop? 0f:1f;
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
     public void PauseAndWinMenu(){
         StopTime(true);
         WinMenu.SetActive(true);
     }


    
    //TODO:2-make it time limited to get to garu
    // make the tutorial!
        
    
}
