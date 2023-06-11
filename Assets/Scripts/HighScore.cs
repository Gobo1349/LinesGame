using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // для работы с интерфейсом

public class HighScore : MonoBehaviour
{
    [SerializeField]
    private Text[] counter = new Text[2]; // меняет непосредственно текст в таблице рекордов

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
