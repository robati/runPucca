using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
// using Camera.ScreenPointToRay;
using UnityEngine.UI;
using UnityEngine.EventSystems;
 using UnityEngine.SceneManagement;
// using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{


    public GameObject Pucca;
    public GameObject P_Spine;
    public Text DebugText;
    int SpeedConst=180;
    public GameObject puccaMenu;
    public GameObject PauseMenu;
    public GameObject Camera_Gameobject;
    public GameObject g;
    public GameObject GuidPanel;
    public GameObject Xprefab;
    public GameObject rprefab;
    bool[] j_one={false,false,false,false,false,false};//On Enter
    bool[] j_two={false,false,false,false,false,false};//On Exit
    Vector3 CameraTmp_Position; 
    private Camera cam;
    float middle;
    bool CanShrk=true;
    // bool CanGo=true;
    float timeShr=0;
    MOVE lastJahat=MOVE.RIGHT; 
     
    public enum MOVE          
    {
        RIGHT=1,
        LEFT,
        HIGH,
        LOW,
        SHR
    }
    private Animator Animl;
    // Start is called before the first frame update
    void Start()
    {         
        cam = Camera.main;

        CameraTmp_Position=Pucca.transform.position;

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Animl=Pucca.GetComponentInChildren<Animator>();

//Creating random enemies
        for(int i=0 ; i<4 ; i++){
            Vector3 x= new Vector3(UnityEngine.Random.Range(400,2000),UnityEngine.Random.Range(180,500),-120);
            GameObject x1=Instantiate<GameObject>(Xprefab,x,new Quaternion());
            x1.SetActive(true);
        }

//Creating random rocks
        for(int i=0 ; i<3 ; i++){
            Vector3 y= new Vector3(UnityEngine.Random.Range(i*600+10,(i+1)*600),UnityEngine.Random.Range(220,460),-100);
            GameObject y1=Instantiate<GameObject>(rprefab,y,new Quaternion());
            int s=UnityEngine.Random.Range(20,50);
            Vector3 m=y1.transform.localScale;
            y1.transform.localScale=new Vector3(m.x*s,m.y*s,1);
            y1.SetActive(true);

        }

        }
    
    private void FixedUpdate() {
            Camera_Gameobject.transform.position+=new Vector3(Pucca.transform.position.x - CameraTmp_Position.x,0,0);
            CameraTmp_Position=Pucca.transform.position;
    }
    void Update()
    { 
        //Windows movement control
        if(Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
        
        if(Input.GetKeyDown(KeyCode.G)){
            sendSHR();
        }

        middle=cam.WorldToScreenPoint(Pucca.transform.position).x;

        float x=middle;//Screen.width/2;//P_Spine.transform.position.x;//Screen.width/2 ;
        timeShr+=Time.deltaTime;
        if(timeShr>1){
            timeShr=0;
            CanShrk=true;
        }
        

        var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);    
        if(move!=Vector3.zero){
            PostMoveProc(move.x>0?MOVE.RIGHT:move.x!=0?MOVE.LEFT:MOVE.LOW);
            Pucca.transform.position += move * SpeedConst * Time.deltaTime;    
        }   

        //Android movement control
        
        else if (Input.touchCount == 1)
        {
            //  DebugText.text+="0";
            moveCharProcess();
            if(!IsPointerOverUIObject()){

                var touch = Input.GetTouch(0);
                if (touch.position.x <x)
                {
                    moveTo(Time.deltaTime*SpeedConst,MOVE.LEFT.GetHashCode()); 
                }
                else if (touch.position.x > x)
                { 
                    moveTo(Time.deltaTime*SpeedConst,MOVE.RIGHT.GetHashCode());
                }            

            }
            // else
            // CanGo=true;
        }
        
        else if(Input.touchCount>1){
        // foreach(MOVE jahat in  Enum.GetValues(typeof(MOVE))){
        //    if(j_one[jahat.GetHashCode()]){
        //        DebugText.text +=jahat.GetHashCode();
        //    }}

            moveCharProcess();

            var touch = Input.GetTouch(0);
            var touch1 = Input.GetTouch(1);

            if(j_one[MOVE.SHR.GetHashCode()]||j_two[MOVE.SHR.GetHashCode()]){ //on enter and on exit pointer equals input touch.
                if(!(j_one[MOVE.LOW.GetHashCode()]||j_two[MOVE.LOW.GetHashCode()]||
                j_one[MOVE.HIGH.GetHashCode()]||j_two[MOVE.HIGH.GetHashCode()] )){
                    if(touch.position.x >x || touch1.position.x >x ){
                        // DebugText.text+="righta";
                        moveTo(Time.deltaTime*SpeedConst,1);
                    }   
                }
            }
            else if(touch.position.x <x || touch1.position.x <x ){
                 // DebugText.text+="left";
                moveTo(Time.deltaTime*SpeedConst,2); //LEFT
            }
            else{                                  
                // DebugText.text+="rightb";
                moveTo(Time.deltaTime*SpeedConst,1);
            }
        }
        else{
             Animl.SetBool("moving",false);
        }
    }
     private bool IsPointerOverUIObject()
     {
         var eventDataCurrentPosition = new PointerEventData(EventSystem.current)
         {
             position = new Vector2(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y)
         };
         var results = new List<RaycastResult>();
         EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
         return results.Count > 0;
     }
    public void sendSHR(){
        if (!CanShrk){
            return;
        }
        GameObject a = Instantiate<GameObject>(g);
        CanShrk=false;
        if(lastJahat==MOVE.LEFT){
            Vector3 newScale = a.transform.localScale;
            newScale.x *= -1;
            a.transform.localScale = newScale;
            a.transform.position=P_Spine.transform.position+new Vector3(-70,30,0);
        }
        else{
            a.transform.position=P_Spine.transform.position+new Vector3(70,30,0); 
        }
        Animator a3= a.GetComponent<Animator>();
        a3.SetTrigger("thru");
        P_Spine.transform.rotation=new Quaternion(0,0,0,0);        
        Animl.SetTrigger("shrk");
    }
    public bool moveCharProcess(){
            bool flag=false;
            int i=0;
            foreach(MOVE jahat in  Enum.GetValues(typeof(MOVE))){
                i++;
                if(j_one[i]){
                    flag=true;
                    moveTo(Time.deltaTime*SpeedConst,i);
                }
                else if(j_two[i])
                    flag=true;
            }
            
            return flag;
    }
    public void OnEnt(int d){
        Debug.Log(d);
        // CanGo=false;
        foreach(MOVE jahat in  Enum.GetValues(typeof(MOVE))){
            j_two[jahat.GetHashCode()]=j_one[jahat.GetHashCode()];
            if(d==jahat.GetHashCode()){
                j_one[jahat.GetHashCode()]=true;
            }
            else if(d==-1*jahat.GetHashCode()){
                j_one[jahat.GetHashCode()]=false;
            }
        }

    }

    void moveTo(float s , int dir){

        Vector3 jahatVec3=Vector3.zero;
        MOVE m=MOVE.RIGHT;

        if(dir == MOVE.SHR.GetHashCode()){
            sendSHR();
            return;
        }
        if(dir==MOVE.RIGHT.GetHashCode()){
            jahatVec3=Vector3.right ;
            m=MOVE.RIGHT; 
        }
        if( dir==MOVE.LEFT.GetHashCode()){
            jahatVec3=Vector3.left; 
            m=MOVE.LEFT; 
        }
        if(dir==MOVE.HIGH.GetHashCode()){
            jahatVec3=Vector3.up ; 
            m=MOVE.HIGH; 
        }
        if(dir==MOVE.LOW.GetHashCode()){
            jahatVec3=Vector3.down;
            m=MOVE.LOW; 
        }

        PostMoveProc(m);
        Pucca.transform.position  = Pucca.transform.position +s*jahatVec3;

    }
    public void PostMoveProc(MOVE dir){

        turnAround(dir);
        Animl.SetBool("moving",true);

        P_Spine.transform.rotation= new Quaternion(0,0,0,0);
        Camera_Gameobject.transform.position+=new Vector3(Pucca.transform.position.x - CameraTmp_Position.x,0,0);
        CameraTmp_Position=Pucca.transform.position;
    }
    public void turnAround(MOVE d){
        if(d==MOVE.LEFT||d==MOVE.RIGHT){
            if(lastJahat==d)
                return;
            Vector3 newScale = P_Spine.transform.localScale;
            newScale.x *= -1;
            P_Spine.transform.localScale = newScale;
            lastJahat=d;
        }
       
    }
    //  public void RsetGame(){
    //     SceneManager.LoadScene(0);
    //     puccaMenu.gameObject.SetActive(false);
    //     PauseGame(false);
    //  }
    //  public void PauseGame(bool pauseGame){
    //      if(pauseGame){
    //         Time.timeScale = 0f;
    //         AudioListener.pause = true;
    //         PauseMenu.SetActive(true);
    //     }
    //      else{
    //         Time.timeScale = 1;
    //         AudioListener.pause = false;
    //         PauseMenu.SetActive(false);
    //     }

    //  }
    //  public void QuitGame(){
    //     Application.Quit();
    //  }
    //  public void MuteGame(bool muteGame){
    // // DebugText.text+=muteGame;
    //     if(!muteGame)
    //          AudioListener.volume = 0f;
    //     else
    //          AudioListener.volume = 1f;

    //  }

}
