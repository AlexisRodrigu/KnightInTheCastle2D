using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    public GameObject player;
    [SerializeField] PlayerController playerControllerScript;
    [SerializeField] private int playerLives = 3;

    [SerializeField] private int score = 0;
    [SerializeField] private TextMeshProUGUI txtScore;

    private static GameManager _instance;
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        // Instantiate(player,player.transform.position, Quaternion.identity);
    }

    public void AddScore(int addScore)
    {
        score += addScore;
        txtScore.text = score.ToString();
    }
    public void HealthUp (float addHealth)
    {
        playerControllerScript.Health += addHealth;
    }
    //Funcion que te regresa al punto inicial
    public void ProcessPlayerDeath()
    {
        // playerLives = playerLives > 1 ? TakeLife() : ResetGameSession();
        if (playerLives > 1) TakeLife();
        else ResetGame();
    }
    //Construye la escena de nuevo
    void TakeLife()
    {
        playerLives--;
        var currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
    //Vielve a cargar la escena
    void ResetGame()
    {
        SceneManager.LoadScene("SceneOne");
        Destroy(gameObject);
    }
}
