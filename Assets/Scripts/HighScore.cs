using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // ��� ������ � �����������

public class HighScore : MonoBehaviour
{
    [SerializeField]
    private Text[] counter = new Text[2]; // ������ ��������������� ����� � ������� ��������

    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            counter[i].text = ("x" + PlayerPrefs.GetInt("score" + i));
        }
    }

    //void Update()
    //{
        
    //}
}
