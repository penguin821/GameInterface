using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float levelStartDelay = 2f;
    public float turnDelay = .1f;
    public static GameManager instance = null;
    public BoardManager boardScript;
    public int playerFoodPoints = 100;
    public int playerBatteryPoints = 100;
    [HideInInspector] public bool playersTurn = true;

    private Text levelText;
    private GameObject levelImage;
    private GameObject startImage;
    private int level = 0;
    private List<Enemy> enemies;
    private bool enemiesMoving;
    private bool doingSetup;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        enemies = new List<Enemy>();
        boardScript = GetComponent<BoardManager>();
        InitGame();
    }

    private void OnLevelWasLoaded(int index)
    {
        level++;

        InitGame();
    }

    void InitGame()
    {
        doingSetup = true;

        startImage = GameObject.Find("StartImage");

        if (level > 0)
        {
            levelImage = GameObject.Find("LevelImage");
            levelText = GameObject.Find("LevelText").GetComponent<Text>();
            levelText.text = "Day " + level;
            levelImage.SetActive(true);
            Invoke("HideLevelImage", levelStartDelay);

            enemies.Clear();
            boardScript.SetupScene(level);
            startImage.SetActive(false);
        }
    }

    private void HideLevelImage()
    {
        levelImage.SetActive(false);
        doingSetup = false;
    }

    public void GameOver()
    {
        levelText.text = "After " + level + " days, you starved.";
        levelImage.SetActive(true);
        enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (level == 0)
        {
            startImage.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }


        if (playersTurn || enemiesMoving || doingSetup)
            return;

        StartCoroutine(MoveEnemies());
    }

    public void AddEnemyToList(Enemy script)
    {
        enemies.Add(script);
    }

    IEnumerator MoveEnemies()
    {
        enemiesMoving = true;
        yield return new WaitForSeconds(turnDelay);
        if (enemies.Count == 0)
        {
            yield return new WaitForSeconds(turnDelay);
        }
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].MoveEnemy();
            yield return new WaitForSeconds(enemies[i].moveTime);
        }

        playersTurn = true;
        enemiesMoving = false;
    }
}
