using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Game : MonoBehaviour
{
    public AudioSource audio;
    Button[,] buttons; // массив кнопок
    Image[] images;

    Lines lines;


    void Start()
    {
        lines = new Lines(ShowBox, PlayCut);
        InitButtons(); // инициализаци€ кнопок 
        InitImages(); // инициализаци€ картинок
        ShowBox(1, 2, 3);
        lines.Start();
    }

    public void ShowBox(int x, int y, int ball) // отображение картинок
    {
        buttons[x, y].GetComponent<Image>().sprite = images[ball].sprite; // обращаемс€ к опр кнопке и помещаем в нее спрайт пикчи 
    }

    public void PlayCut() // дл€ воспроизведени€ звука 
    {
        audio.Play();
    }

    public void Click() // вызываетс€ при нажатии кнопки  
    {
        string name = EventSystem.current.currentSelectedGameObject.name;
        int nr = GetNumber(name);
        int x = nr % Lines.SIZE; // координаты кнопки 
        int y = nr / Lines.SIZE;
      //  Debug.Log($"clicked {name} {x} {y}");
        lines.Click(x, y);
    }

    private void InitButtons() // инициализаци€ кнопок 
    {
        buttons = new Button[Lines.SIZE, Lines.SIZE];
        for (int i = 0; i < Lines.SIZE * Lines.SIZE; i++)
        {
            buttons[i % Lines.SIZE, i / Lines.SIZE] = GameObject.Find($"Button ({i})").GetComponent<Button>(); // соответствие кнопок игры и массива 
        }
    }

    private void InitImages() // инициализаци€ картинок
    {
        images = new Image[Lines.BALLS];
        for (int i = 0; i < Lines.BALLS; i++)
        {
            images[i] = GameObject.Find($"Image ({i})").GetComponent<Image>();
        }
    }

    private int GetNumber(string name) // получить номер по имени с помощью регул€рных выражений 
    {
        // –егул€рные выражени€ Ч это механизм дл€ поиска и замены текста
        Regex regex = new Regex("\\((\\d+)\\)"); // определ€ем регул€рное выражение - шаблон 
        Match match = regex.Match(name); // Ќаходит во входной строке подстроку, совпадающую с шаблоном регул€рного выражени€, и возвращает первое вхождение в качестве единого объекта Match.
        if (!match.Success) // если не найден шаблон регул€рного выражени€ во входной строке 
        {
            throw new Exception("Unrecognized button"); // если не нашли - ошибка 
        }
        Group group = match.Groups[1]; // ѕолучает коллекцию групп, соответствующих регул€рному выражению. 0ой - все что совпало, 1ый - то что было в скобках 
        string Number = group.Value;
        return Convert.ToInt32(Number);
    }
}
