using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static int mapindex;
    public static bool startGame;
    public GameObject EndoftheGameMenu;

    public void Awake()
    {
        EndoftheGameMenu.SetActive(false);
    }
    public void EndGameFunc()
    {
        EndoftheGameMenu.SetActive(true);   
    }
    public void RestartGame()
    {
        startGame = true;
        SceneManager.LoadScene("GameScene");
    }
    public void MainMenu()
    {
        startGame = false;
        mapindex = 0;
        SceneManager.LoadScene("GameScene");
    }
    public void MapIndex(int index)
    {
       mapindex = index;
    }
    public void StartGame()
    {
        startGame=true;
    }
}
