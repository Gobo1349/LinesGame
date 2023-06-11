using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // ��� ������ � �����������

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

    public void LoadGame(int num) // ����� ������ ������ 
    {
        SceneManager.LoadScene(num); // �������� ����������� ���� - ��������� ��������� �����
    }

    public void Restart() // ���������� ������ 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
    }

    public void GameExit() // ����� ������ �� ���� 
    {
        Application.Quit();
    }

    public void Reset() // ����� ������ ��������� 
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
    }

}
