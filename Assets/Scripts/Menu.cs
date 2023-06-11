using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // для работы с интерфейсом

public class Menu : MonoBehaviour
{
    //Start is called before the first frame update
    //void Start()
    //{

    //}

    //Update is called once per frame
    //void Update()
    //{

    //}

    public void LoadGame(int num) // метод выбора уровня 
    {
        SceneManager.LoadScene(num); // позволит запускаться игре - запускаем следующую сцену
    }

    public void Restart() // перезапуск уровня 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
    }

    public void GameExit() // метод выхода из игры 
    {
        Application.Quit();
    }

    public void Reset() // метод сброса прогресса 
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
    }

}
