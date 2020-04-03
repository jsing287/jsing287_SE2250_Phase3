using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.AI;

public class PlayerManager : MonoBehaviour
{
    #region Singleton

    public static PlayerManager instance;


    // At the start of the game assigns this singleton to this script
    void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject player;
    public Text score;
    private int presentScore;
    private PlayerStats myStats;
    public Text showHealth;
    public Text showArmor;
    private NavMeshAgent agent;
    public bool hasDisplayed;
    public bool levelCompleted;
    private int enemiesDefeated;
    public Text win;
    private int enemies;
    private GameObject increaseLevel;
   
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            presentScore = 0;
          PlayerPrefs.SetInt("value", 0);
        }
        else
        {
            presentScore = PlayerPrefs.GetInt("value");
        }
      
        score = this.gameObject.GetComponentInChildren<Text>();
        
        myStats = player.gameObject.GetComponent<PlayerStats>();
        agent = player.gameObject.GetComponent<NavMeshAgent>();
        hasDisplayed = false;
        score.text = "Score: 0";

        levelCompleted = false;

        enemies = 0;

        increaseLevel = GameObject.Find("LevelUp");
        increaseLevel.SetActive(false);

        
     
       
      
        
    }



    void Update()
    {
      
        if(!hasDisplayed)
        {
            displayStats();
        }
        else if(hasDisplayed)
        {
          
            displayUpgradeMessage();
            
        }


        if(levelCompleted)
        {
            LevelUp();
        }

     
        
    }
   

    public void KillPlayer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Score(int damage)
    {
        presentScore += damage;
        score.text = "Score: " + presentScore;
        

    }

    public void Defeats()
    {
        enemies++;
        if(enemies==2)
        {
            increaseLevel.SetActive(true);
        }
    }


    public void Upgrade()
    {
        hasDisplayed = !hasDisplayed;
        StartCoroutine(Wait());
        agent.speed *= 2;
        agent.acceleration += 500;
        
    }
    void displayStats()
    {
        showHealth.text = "Health: " + myStats.CurrentHealth;
        showArmor.text = "Armor: " + myStats.GetArmor();
        score.text = "Score " + presentScore;
    }

    void displayUpgradeMessage()
    {
        showHealth.fontSize = 20;
       
        showHealth.text = "DOUBLED SPEED AND ACCELERATION";
        showArmor.text = "";
        
    }

  
    

    public void LevelUp()
    {
        StartCoroutine(Wait());
        PlayerPrefs.SetInt("value", presentScore);
        SceneManager.LoadScene("Level2");
    }

    IEnumerator Wait()
    {
        
        yield return new WaitForSeconds(3.0f);
        
        hasDisplayed = !hasDisplayed;
        showHealth.fontSize = 20;
   
      
    }

    public void EndGame()
    {
        win.gameObject.SetActive(true);
        StartCoroutine(End());
        
    }


    IEnumerator End()
    {
        
        yield return new WaitForSeconds(8.0f);
        hasDisplayed = !hasDisplayed;
        SceneManager.LoadScene(0);
    }



}
