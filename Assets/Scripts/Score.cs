// Скрипт CoinCollect - сохранение очков
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI; // для работы с интерфейсом

public class Score : MonoBehaviour // отвечает за счетчик собранных монеток 
{
    public static int score; // количество очков

    private Text counter; // меняет непосредственно текст

    void Start()
    {
        counter = GetComponent<Text>(); // счетчик будет считывать текстовый компонент 
        score = 0; // начальное количество
    }

    // Update is called once per frame
    void Update()
    {
        counter.text = "Your score is " + Lines.score; // отображаем количество собранных элементов
    }
}
