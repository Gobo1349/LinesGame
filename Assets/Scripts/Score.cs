// ������ CoinCollect - ���������� �����
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI; // ��� ������ � �����������

public class Score : MonoBehaviour // �������� �� ������� ��������� ������� 
{
    public static int score; // ���������� �����

    private Text counter; // ������ ��������������� �����

    void Start()
    {
        counter = GetComponent<Text>(); // ������� ����� ��������� ��������� ��������� 
        score = 0; // ��������� ����������
    }

    // Update is called once per frame
    void Update()
    {
        counter.text = "Your score is " + Lines.score; // ���������� ���������� ��������� ���������
    }
}
