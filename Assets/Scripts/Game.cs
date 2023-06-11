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
    Button[,] buttons; // ������ ������
    Image[] images;

    Lines lines;


    void Start()
    {
        lines = new Lines(ShowBox, PlayCut);
        InitButtons(); // ������������� ������ 
        InitImages(); // ������������� ��������
        ShowBox(1, 2, 3);
        lines.Start();
    }

    public void ShowBox(int x, int y, int ball) // ����������� ��������
    {
        buttons[x, y].GetComponent<Image>().sprite = images[ball].sprite; // ���������� � ��� ������ � �������� � ��� ������ ����� 
    }

    public void PlayCut() // ��� ��������������� ����� 
    {
        audio.Play();
    }

    public void Click() // ���������� ��� ������� ������  
    {
        string name = EventSystem.current.currentSelectedGameObject.name;
        int nr = GetNumber(name);
        int x = nr % Lines.SIZE; // ���������� ������ 
        int y = nr / Lines.SIZE;
      //  Debug.Log($"clicked {name} {x} {y}");
        lines.Click(x, y);
    }

    private void InitButtons() // ������������� ������ 
    {
        buttons = new Button[Lines.SIZE, Lines.SIZE];
        for (int i = 0; i < Lines.SIZE * Lines.SIZE; i++)
        {
            buttons[i % Lines.SIZE, i / Lines.SIZE] = GameObject.Find($"Button ({i})").GetComponent<Button>(); // ������������ ������ ���� � ������� 
        }
    }

    private void InitImages() // ������������� ��������
    {
        images = new Image[Lines.BALLS];
        for (int i = 0; i < Lines.BALLS; i++)
        {
            images[i] = GameObject.Find($"Image ({i})").GetComponent<Image>();
        }
    }

    private int GetNumber(string name) // �������� ����� �� ����� � ������� ���������� ��������� 
    {
        // ���������� ��������� � ��� �������� ��� ������ � ������ ������
        Regex regex = new Regex("\\((\\d+)\\)"); // ���������� ���������� ��������� - ������ 
        Match match = regex.Match(name); // ������� �� ������� ������ ���������, ����������� � �������� ����������� ���������, � ���������� ������ ��������� � �������� ������� ������� Match.
        if (!match.Success) // ���� �� ������ ������ ����������� ��������� �� ������� ������ 
        {
            throw new Exception("Unrecognized button"); // ���� �� ����� - ������ 
        }
        Group group = match.Groups[1]; // �������� ��������� �����, ��������������� ����������� ���������. 0�� - ��� ��� �������, 1�� - �� ��� ���� � ������� 
        string Number = group.Value;
        return Convert.ToInt32(Number);
    }
}
