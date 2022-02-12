using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float slowMotionSpeed;
    public int level;
    public float slowMotionTime;
    public Canvas canvas;
    public Text levelText;

    void Start()
    {
        if (PlayerPrefs.HasKey("Level"))
        {
            level = PlayerPrefs.GetInt("Level");
            levelText.text = level.ToString();
        }
    }
    public void LevelTransition(bool isNextLevel)
    {
        GameObject.Find("Player").GetComponentInChildren<ShootManager>().enabled = false;
        GameObject.Find("Ennemy").GetComponentInChildren<BotManager>().enabled = false;
        canvas.enabled = false;
        Time.timeScale = slowMotionSpeed;
        if (isNextLevel)
        {
            level++;
            PlayerPrefs.SetInt("Level", level);
        }
        StartCoroutine(NewLevel());
    }
    IEnumerator NewLevel()
    {
        yield return new WaitForSeconds(slowMotionTime);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void ResetLevel()
    {
        PlayerPrefs.SetInt("Level", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }
}
