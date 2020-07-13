using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMechanic : MonoBehaviour
{
    public List<GameObject> LabyrinthBaseCubes;
    public Vector2 firstPressPos;
    public Vector2 secondPressPos;
    public Vector2 currentSwipe;
    public Material Ball;
    private bool move = false;
    private Vector3 nextPoss;
    private string directionMove = "";
    private BoxCollider boxColl;
    public static int matCounter = 0;
    public List<string> gameLevels = new List<string> {"Menu", "Level_01","Level_02","Level_03"};

    void Start()
    {
        ManagerScript.Instance.currentLevel++;
        matCounter = 0;
        boxColl = GetComponent<BoxCollider>();
    }

    void Update()
    {
        if(move)
            Move();
        else
            Swipe();

        if(matCounter == LabyrinthBaseCubes.Count)
            StartCoroutine(LoadSceneWait());
        
    }

    IEnumerator LoadSceneWait()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(gameLevels[ManagerScript.Instance.currentLevel+1]);
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("LabyrinthCube")){
            move = false;
        }
    }

    private void OnCollisionStay(Collision other) {
        if(other.gameObject.CompareTag("LabyrinthCube")){
            move = false;
        }
    }

    public void Swipe()
    {   
        if(Input.GetMouseButtonDown(0))
        {
            firstPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
        }

        if(Input.GetMouseButtonUp(0))
        {
            secondPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
            currentSwipe.Normalize();
    
            if(currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f){
                boxColl.size = new Vector3 (0.5f, 0.9f, 1.0f);
                directionMove = "up";
                move = true;
                //Debug.Log("up swipe");
            }
                
            if(currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f){
                boxColl.size = new Vector3 (0.5f, 0.9f, 1.0f);
                directionMove = "down";
                move = true;
                //Debug.Log("down swipe");
            }           
                
            if(currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f){
                boxColl.size = new Vector3 (1.0f, 0.5f, 1.0f);
                directionMove = "left";
                move = true;
                //Debug.Log("left swipe");
            }               

            if(currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f){
                boxColl.size = new Vector3 (1.0f, 0.5f, 1.0f);
                directionMove = "right";
                move = true;
                //Debug.Log("right swipe");
            }               

        }
    }
    public void Move(){
        switch(directionMove){
            case "up":
                nextPoss = new Vector3(transform.position.x, transform.position.y + 50f, transform.position.z);
            break;
            case "down":
                nextPoss = new Vector3(transform.position.x, transform.position.y - 50f, transform.position.z);
            break;
            case "left":
                nextPoss = new Vector3(transform.position.x - 50f, transform.position.y, transform.position.z);
            break;
            case "right":
                nextPoss = new Vector3(transform.position.x + 50f, transform.position.y, transform.position.z);
            break;
        }
        transform.position = Vector3.MoveTowards(transform.position, nextPoss, Time.deltaTime * 225f);
    }

    /*
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    public void Swipe()
    {
        if(Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if(t.phase == TouchPhase.Began)
            {
                firstPressPos = new Vector2(t.position.x,t.position.y);
            }
            if(t.phase == TouchPhase.Ended)
            {
                secondPressPos = new Vector2(t.position.x,t.position.y);
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
                currentSwipe.Normalize();

                if(currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                    Debug.Log("up swipe");

                if(currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                    Debug.Log("down swipe");

                if(currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                    Debug.Log("left swipe");

                if(currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                    Debug.Log("right swipe");
            }
        }
    }
    */

}
