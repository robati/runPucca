using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
// using Camera.ScreenPointToRay;
using UnityEngine.UI;
using UnityEngine.EventSystems;
 using UnityEngine.SceneManagement;
// using UnityEngine.SceneManagement;

public class gamec : MonoBehaviour
{


    public GameObject Pucca;
    public GameObject P_Spine;
    public Text DebugText;
    bool right=false;
    bool left=false;
    bool high=false;
    bool low=false;
    int SpeedConst=180;
    public GameObject puccaMenu;
    public GameObject PauseMenu;
    public GameObject Camera_Gameobject;
    public GameObject g;
    public GameObject GuidPanel;
    bool[] j_one={false,false,false,false,false,false};
    bool[] j_two={false,false,false,false,false,false};
    Vector3 CameraTmp_Position; 
    private Camera cam;
    float middle;
    bool CanShrk=true;
    bool CanGo=true;
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

        // puccaMenu.gameObject.SetActive(true);
        // PauseGame(true);
        // PauseMenu.gameObject.SetActive(false);
        // middle=cam.ScreenToWorldPoint(new Vector3(Screen.width/2,0,0)).x;
        // Pucca.transform.position =new Vector3(middle,Pucca.transform.position.y,Pucca.transform.position.z);
        // int firstRun = PlayerPrefs.GetInt("savedFirstRun") ;
 
 
        // if (firstRun == 0) // remember "==" for comparing, not "=" which assigns value
        // {
        // ShowHelper(false);
        // }
        }
    public void crash()
    {
        //  p.transform.position  = p.transform.position +new Vector3(10,0,0) ;    
                    // Debug.Log(P_Spine.transform.position);  
            GameObject a = Instantiate<GameObject>(g);
            a.transform.position=P_Spine.transform.position+new Vector3(70,40,0);
            Animator a3= a.GetComponent<Animator>();
            a3.SetTrigger("thru");
        // p1.text="click"; 
        
        // P_Spine.transform.Rotate(new Vector3(0,0,20),Space.World);

        //rb.AddForce(new Vector2(sidewaysforce * Time.deltaTime,0));//, ForceMode2D.VelocityChange);
        //rb.AddForce(-sidewaysforce * Time.deltaTime, 0, ForceMode.VelocityChange);

                               
        // Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
        // Vector3 m_movementVector = new Vector3();
        // Update the movement vector
        // m_movementVector.x = touchDeltaPosition.x;
        // m_movementVector.y = 0.0f;
        // m_movementVector.z = touchDeltaPosition.y;   
        // rb.AddForce(m_movementVector * 5 * Time.deltaTime);

    }
    private void FixedUpdate() {
            Camera_Gameobject.transform.position+=new Vector3(Pucca.transform.position.x - CameraTmp_Position.x,0,0);
            CameraTmp_Position=Pucca.transform.position;
    }
    void Update()
    { 
        if(Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
        
        if(Input.GetKeyDown(KeyCode.G)){
            sendSHR();
            // Debug.Log("size="+P_Spine.transform.position.x+" "+Screen.width/2 );
        }
        //  if(EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))

        //    DebugText.text=("Touched the UI");

        // }
        middle=cam.WorldToScreenPoint(Pucca.transform.position).x;

        float x=middle;//Screen.width/2;//P_Spine.transform.position.x;//Screen.width/2 ;
        timeShr+=Time.deltaTime;
        if(timeShr>1){
            // Debug.Log("yes");
            timeShr=0;
            CanShrk=true;
        // DebugText.text="ey"+IsPointerOverUIObject();
        }
        
    // bool flag=moveCharProcess();
    // if(!flag)
    //          Animl.SetBool("moving",false);

        var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);    
        if(move!=Vector3.zero){
            PostMoveProc(move.x>0?MOVE.RIGHT:move.x!=0?MOVE.LEFT:MOVE.LOW);
            Pucca.transform.position += move * SpeedConst * Time.deltaTime;    
        }   

        
        else if (Input.touchCount == 1)
        {
            //  DebugText.text+="GetTouch(0)";

            bool flag=moveCharProcess();

        //    if(!flag){
        //    if(CanGo){
        //  if(!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)&&){
            if(!IsPointerOverUIObject()){

                var touch = Input.GetTouch(0);
                // DebugText.text ="at1";
                if (touch.position.x <x)
                {
                    moveTo(Time.deltaTime*SpeedConst,2);
                }
                else if (touch.position.x > x)
                { 
                    moveTo(Time.deltaTime*SpeedConst,1);
                }            

            }
            else
            CanGo=true;
        }
        
        else if(Input.touchCount>1){
                // DebugText.text ="at3";//+x+ ","+touch.position.x+"f";

            moveCharProcess();

            var touch = Input.GetTouch(0);
            var touch1 = Input.GetTouch(1);
            // DebugText.text+="GetTouch(1);";
            if(j_one[5]){
                if(touch.position.x >x || touch1.position.x >x )
                    moveTo(Time.deltaTime*SpeedConst,1);
            }

            else if(touch.position.x <x || touch1.position.x <x )
                moveTo(Time.deltaTime*SpeedConst,2); //LEFT
            else
                moveTo(Time.deltaTime*SpeedConst,1);
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
    if (!CanShrk)
    {
        // j_one[MOVE.SHR.GetHashCode()]=false;
        return;
    }
                // DebugText.text+="b";
        GameObject a = Instantiate<GameObject>(g);
    //    j_one[MOVE.SHR.GetHashCode()]=false;
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
        //Rotate(new Vector3(0,0,-1),Space.World);
        
        Animl.SetTrigger("shrk");
        }
    public bool moveCharProcess(){

            bool flag=false;
            int i=0;
            foreach(MOVE jahat in  Enum.GetValues(typeof(MOVE))){
            // for(int i=1;i<5;i++){
                i++;
                if(j_one[i]){
                    flag=true;
                    moveTo(Time.deltaTime*SpeedConst,i);
                }
                else if(j_two[i])
                    flag=true;
            }
            // if(j_one[MOVE.SHR.GetHashCode()]){
            //     flag=true;
            //     crash();
            // }
            
            return flag;
    }
    public void OnEnt(int d){
        // DebugText.text="a/"+d;
        Debug.Log(d);
        CanGo=false;
        foreach(MOVE jahat in  Enum.GetValues(typeof(MOVE))){
            j_two[jahat.GetHashCode()]=j_one[jahat.GetHashCode()];
            if(d==jahat.GetHashCode()){
                j_one[jahat.GetHashCode()]=true;}
            else if(d==-1*jahat.GetHashCode()){

                j_one[jahat.GetHashCode()]=false; }
        }

    }

    void moveTo(float s , int dir){
       
        // DebugText.text="direction/"+dir;
       
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
        // Debug.Log(P_Spine.transform.rotation.z);
        // float f=-P_Spine.transform.rotation.z;
        // P_Spine.transform.Rotate(new Vector3(0,0,f),Space.World);
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
     public void RsetGame(){
        //  Application.LoadLevel(Application.loadedLevel);
         SceneManager.LoadScene(0);//TODO: loadscene call start unity
        puccaMenu.gameObject.SetActive(false);

        PauseGame(false);
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
     public void QuitGame(){
Application.Quit();
     }
     public void MuteGame(bool muteGame){
DebugText.text+=muteGame;
if(!muteGame)
             AudioListener.volume = 0f;
else
             AudioListener.volume = 1f;

     }
     public void ShowHelper(bool showAgain){
        
         PlayerPrefs.SetInt("savedFirstRun",1) ;
         GuidPanel.SetActive(true);
         if(showAgain)
                  PlayerPrefs.SetInt("savedFirstRun",0) ;

        
     }
}
