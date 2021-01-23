using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject healthBar;
    public GameObject pauseMenu;
    public Slider bossHealthBar;
    public GameObject gameOverPanel;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else { Destroy(gameObject); }
        //healthBar = GameObject.Find("Health Bar");
        //pauseMenu = GameObject.Find("Canvas").transform.GetChild(2).gameObject;
        //bossHealthBar = GameObject.Find("Canvas").transform.GetChild(4).gameObject.transform.GetChild(0).gameObject.GetComponent<Slider>();
        //gameOverPanel = GameObject.Find("Canvas").transform.GetChild(3).gameObject;
    }

    public void UpdataHealth(float currentHealth)
    {
        switch (currentHealth)
        {
            case 3:
                healthBar.transform.GetChild(0).gameObject.SetActive(true);
                healthBar.transform.GetChild(1).gameObject.SetActive(true);
                healthBar.transform.GetChild(2).gameObject.SetActive(true);
                break;
            case 2:
                healthBar.transform.GetChild(0).gameObject.SetActive(true);
                healthBar.transform.GetChild(1).gameObject.SetActive(true);
                healthBar.transform.GetChild(2).gameObject.SetActive(false);
                break;
            case 1:
                healthBar.transform.GetChild(0).gameObject.SetActive(true);
                healthBar.transform.GetChild(1).gameObject.SetActive(false);
                healthBar.transform.GetChild(2).gameObject.SetActive(false);
                break;
            case 0:
                healthBar.transform.GetChild(0).gameObject.SetActive(false);
                healthBar.transform.GetChild(1).gameObject.SetActive(false);
                healthBar.transform.GetChild(2).gameObject.SetActive(false);
                break;
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void RePlay()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void SetBossHealth(float health)
    {
        GameObject.Find("Canvas").transform.GetChild(4).gameObject.SetActive(true);
        bossHealthBar.maxValue = health;
    }
    public void UpdateBossHealth(float health)
    {
        bossHealthBar.value = health;
    }
    public void GameOverUI(bool playerDead)
    {
        gameOverPanel.SetActive(playerDead);
    }
}
