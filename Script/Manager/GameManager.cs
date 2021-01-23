using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private PlayerController player;
    public Door doorExit;
    public bool gameOver;
    public List<Enemy> enemies = new List<Enemy>();
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        // player = FindObjectOfType<PlayerController>();
        // doorExit = FindObjectOfType<Door>();
    }
    public void Update()
    {   
        if(player!=null)
            gameOver = player.isDead;
        UIManager.instance.GameOverUI(gameOver);
    }
    public void ReStarScence()
    {
        Debug.Log("1");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PlayerPrefs.DeleteKey("playerHealth");
    }
    public void NewGame()
    {   
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(1);
    }
    public void ContinueGame()
    {   
        if(PlayerPrefs.HasKey("sceneIndex"))
            SceneManager.LoadScene(PlayerPrefs.GetInt("sceneIndex"));
        else
            NewGame();
    }
    public void GoToMian()
    { 
        SceneManager.LoadScene(0);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void isPlayer(PlayerController controller)
    {
        player = controller;
    }
    public void isDoor(Door door)
    {
        doorExit = door;
    }
    public void IsEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
    }
    public void Enemydead(Enemy enemy)
    {
        enemies.Remove(enemy);
        if (enemies.Count == 0)
        {
            doorExit.OpenDoor();
            SaveData();
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public float LoadHealth()
    {
        if (!PlayerPrefs.HasKey("playerHealth"))
        {
            PlayerPrefs.SetFloat("playerHealth", 3f);
        }
        float currentHealth = PlayerPrefs.GetFloat("playerHealth");
        return currentHealth;
    }
    public void SaveData()
    {
        PlayerPrefs.SetFloat("playerHealth", player.health);
        PlayerPrefs.SetInt("sceneIndex",SceneManager.GetActiveScene().buildIndex+1);
        PlayerPrefs.Save();     
    }
}
