using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float jumpSpeed = 2f;
    public float ringMovementSpeed,playerRotationSpeedDown,playerRotationSpeedUp,bgMoveSpeed,easySpeed,normalSpeed,hardSpeed,advanceSpeed,spawnEasyTime,spawnNormalTime,spawnHardTime,spawnAdvanceTime,jumpSpeedEasy,jumpSpeedNormal,jumpSpeedHard,jumpSpeedAdvance=2f;
    public float spawnTime=2f;
    public Sprite easySprite,normalSprite,hardSprite,AdvanceSprite;
    public List<GameObject> spawnPositions;
    public GameObject explodePrefab,bgSpawnPos,tutPanel,menuPanel,nextLevel2Panel,nextLevel3Panel,nextLevel4Panel,adPanel,pausePanel,winPanel,failedPanel,bgPerhabLevel1,bgPerhabLevel2,bgPerhabLevel3,bgPerhabLevel4,bgLevel1,bgLevel2,bgLevel3,bgLevel4;
    public GameObject[] ringPrefab;
    bool check=false;
    public bool isGameOver=false;
    public Camera c;
    public AudioSource ExplosionSound,TapSound,PointSound,MenuSound,ClickSound;
    public Text ScoreTxt,tutText,winText,failText;
    public int Score;
    public string[] tutorials;
    private Vector3 _originalPos;
    public bool easyLevel,normalLevel,hardLevel,advanceLevel;
    int tutCount=0;
    public GameObject tutNextButton,startButton,player;
    

    void Awake(){
        if(instance==null){
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start(){
        MenuSound.Play();
        easyLevel=true;
        normalLevel=false;
        hardLevel=false;
        advanceLevel=false;
        nextLevel2Panel.SetActive(false);
        nextLevel3Panel.SetActive(false);
        nextLevel4Panel.SetActive(false);
        adPanel.SetActive(false);
        tutorialNext();
        _originalPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update(){
        if(!isGameOver){
            ScoreTxt.text=""+Score;
            if(check==false){
                if(easyLevel){
                    StartCoroutine(delaySpawnEasy());
                    if(Score == 1){
                        Score = 0;
                        Time.timeScale = 0;
                        nextLevel2Panel.SetActive(true);
                    }
                }
                else if(normalLevel){
                    StartCoroutine(delaySpawnNormal());
                    if(Score == 2){
                        Time.timeScale = 0;
                        adPanel.SetActive(true);
                        float timeRemaining = 3;
                        if(timeRemaining > 0){
                            timeRemaining -= 1;
                            StartCoroutine(stop());     
                        }   
                        Score = 4;
                    }
                    if(Score == 5){
                        Score = 0;
                        Time.timeScale = 0;
                        nextLevel3Panel.SetActive(true);
                    }
                }
                else if(hardLevel){
                    StartCoroutine(delaySpawnNormal());
                    if(Score == 7){
                        Score = 0;
                        Time.timeScale = 0;
                        nextLevel4Panel.SetActive(true);
                    }
                }
                else if(advanceLevel){
                    StartCoroutine(delaySpawnNormal());
                }
            
            }
        }
        else{
            failText.text="Score: "+Score;
            if(player!=null){
                player.GetComponent<Rigidbody2D>().bodyType=RigidbodyType2D.Kinematic;
            } 
        }
    }

    public void tutorialNext(){  
        tutText.text=""+tutorials[tutCount];
        if(tutCount<tutorials.Length-1){
             tutCount=tutCount+1;
        }
        else{  
            tutNextButton.SetActive(false);
            startButton.SetActive(true);
        }
    }

    public void PlayGame(){
        menuPanel.SetActive(false);
        if(PlayerPrefs.GetInt("Tut")==0){
            tutPanel.SetActive(true);
        }
        else{
            GameManager.instance.isGameOver=false;
            player.GetComponent<Rigidbody2D>().bodyType= RigidbodyType2D.Dynamic;
            tutPanel.SetActive(false);
        }
        nextLevel2Panel.SetActive(false);
        nextLevel3Panel.SetActive(false);
        nextLevel4Panel.SetActive(false);
        GameManager.instance.isGameOver=false;
        player.GetComponent<Rigidbody2D>().bodyType= RigidbodyType2D.Dynamic;
    }

    public void StartGame(){
        GameManager.instance.isGameOver=false;
        player.GetComponent<Rigidbody2D>().bodyType= RigidbodyType2D.Dynamic;
        tutPanel.SetActive(false);
        menuPanel.SetActive(false);
        PlayerPrefs.SetInt("Tut",PlayerPrefs.GetInt("Tut")+1);
    }
    IEnumerator stop(){
        check=true;
        yield return new WaitForSeconds(1);
       check=false;
    }

    IEnumerator  delaySpawnEasy(){
        check=true;
        yield return new WaitForSeconds(spawnTime);
        check=false;
    }

    IEnumerator  delaySpawnNormal(){
        check=true;
        SpawnRingsNoramal();
        yield return new WaitForSeconds(spawnTime);
        check=false;
    }

     void SpawnRingsNoramal(){
        Instantiate(ringPrefab[Random.Range(0,2)],spawnPositions[Random.Range(0,spawnPositions.Count)].transform.position,Quaternion.identity);
    }

   public void SpawnBG(){
        if(easyLevel){
            Instantiate(bgPerhabLevel1,bgSpawnPos.transform.position,Quaternion.identity);
        }
        else  if(normalLevel){
            Instantiate(bgPerhabLevel2,bgSpawnPos.transform.position,Quaternion.identity);
        }
         else  if(hardLevel){
            Instantiate(bgPerhabLevel3,bgSpawnPos.transform.position,Quaternion.identity);
        }
         else  if(advanceLevel){
            Instantiate(bgPerhabLevel4,bgSpawnPos.transform.position,Quaternion.identity);
        }
        
    }

    public void Pause(){
        Time.timeScale=0f;
        pausePanel.SetActive(true);
    }
    public void skip(){
        Score = 0;
        Time.timeScale=1f;
        adPanel.SetActive(false);
    }

    public void Resume(){
        Time.timeScale=1f;
        pausePanel.SetActive(false);
    }

    public void Restart(){
        Application.LoadLevel(0);
    }

    public void Home(){
        Application.LoadLevel(0);
    }


    public void easy(){
        bgLevel1.SetActive(true);
        bgLevel2.SetActive(false);
        bgLevel3.SetActive(false);
        bgLevel4.SetActive(false);
        nextLevel2Panel.SetActive(false);
        nextLevel3Panel.SetActive(false);
        nextLevel4Panel.SetActive(false);
        jumpSpeed=jumpSpeedEasy;
        easyLevel=true;
        normalLevel=false;
        hardLevel=false;
        advanceLevel=false;
        spawnTime=spawnEasyTime;
        bgMoveSpeed=easySpeed;
        ringMovementSpeed=easySpeed;
        player.GetComponent<SpriteRenderer>().sprite=easySprite;
    }

    public void normal(){
        bgLevel1.SetActive(false);
        bgLevel2.SetActive(true);
        bgLevel3.SetActive(false);
        bgLevel4.SetActive(false);
        nextLevel2Panel.SetActive(false);
        nextLevel3Panel.SetActive(false);
        nextLevel4Panel.SetActive(false);
        Time.timeScale = 1;
        jumpSpeed=jumpSpeedNormal;
        easyLevel=false;
        normalLevel=true;
        hardLevel=false;
        advanceLevel=false;
        spawnTime=spawnNormalTime;
        bgMoveSpeed=normalSpeed;
        ringMovementSpeed=normalSpeed;
        player.GetComponent<SpriteRenderer>().sprite=normalSprite;
    }

    public void hard(){
        bgLevel1.SetActive(false);
        bgLevel2.SetActive(false);
        bgLevel3.SetActive(true);
        bgLevel4.SetActive(false);
        nextLevel2Panel.SetActive(false);
        nextLevel3Panel.SetActive(false);
        nextLevel4Panel.SetActive(false);
        Time.timeScale = 1;
        jumpSpeed=jumpSpeedHard;
        easyLevel=false;
        normalLevel=false;
        hardLevel=true;
        advanceLevel=false;
        spawnTime=spawnHardTime;
        bgMoveSpeed=hardSpeed;
        ringMovementSpeed=hardSpeed;
        player.GetComponent<SpriteRenderer>().sprite=hardSprite;
    }
    public void advance(){
        bgLevel1.SetActive(false);
        bgLevel2.SetActive(false);
        bgLevel3.SetActive(false);
        bgLevel4.SetActive(true);
        nextLevel2Panel.SetActive(false);
        nextLevel3Panel.SetActive(false);
        nextLevel4Panel.SetActive(true);
        Time.timeScale = 1;
        jumpSpeed=jumpSpeedAdvance;
        easyLevel=false;
        normalLevel=false;
        hardLevel=false;
        advanceLevel=true;
        spawnTime=spawnAdvanceTime;
        bgMoveSpeed=advanceSpeed;
        ringMovementSpeed=advanceSpeed;
        player.GetComponent<SpriteRenderer>().sprite=AdvanceSprite;
    }


}
