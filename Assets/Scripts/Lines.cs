using System;
using UnityEngine;
using System.Collections;

// нужны делегаты - определения функции, которая должны быть реализованы для функционирования программы - что бцдет выходить из модели 
public delegate void ShowBox(int x, int y, int ball);
public delegate void PlayCut(); // для воспроизведения звука

public class Lines // что входит в модель 
{
    public const int SIZE = 9;
    public const int BALLS = 7;
    const int ADD_BALLS = 3; // сколько шариков добавляется
    System.Random random = new System.Random(); // генератор случайных чисел 
    ShowBox showBox; // описываем делегаты. (будем вызывать методы, которые находятся в другом классе) - переменная делегата 
    PlayCut playCut;

    public static int score; // кол-во очков

    int[,] map; // карта - все поле 
    int fromX, fromY; // откуда взяли шарик
    bool isBallSelected; // выбран ли шарик 

    // конструктор 
    public Lines(ShowBox showBox, PlayCut playCut)
    {
        this.showBox = showBox; // при необходимости - вызываем метод из того, что при создании экземпляра нам передали - присваиваем адрес метода 
        this.playCut = playCut; // ПОТОМ ПОПРОБОВАТЬ ПО ДРУГОМУ 
        map = new int[SIZE, SIZE]; // инициализация карты - массива 
    }

    public void Start()
    {
        score = 0; // в начале игры - 0 очков
        ClearMap(); // очистка поля 
        AddRandomBalls(); // добавление на поле случайных шаров 
        isBallSelected = false; // в начале - шарик не выбран 
    }

    public void Click(int x, int y) // метод нажатия - координаты куда пользователь нажал 
    {
        Debug.Log(PlayerPrefs.GetInt("score0"));
        if (IsGameOver())
        {
            //if (score > PlayerPrefs.GetInt("score"))
            //{
            //    PlayerPrefs.SetInt("score", score);
            //}
            if (score > PlayerPrefs.GetInt("score0"))
            {
                PlayerPrefs.SetInt("score2", PlayerPrefs.GetInt("score1"));
                PlayerPrefs.SetInt("score1", PlayerPrefs.GetInt("score0"));
                PlayerPrefs.SetInt("score0", score); 
            } 
            else if (score > PlayerPrefs.GetInt("score1"))
            {
                PlayerPrefs.SetInt("score2", PlayerPrefs.GetInt("score1"));
                PlayerPrefs.SetInt("score1", score);
            }
            else if (score > PlayerPrefs.GetInt("score2"))
            {
                PlayerPrefs.SetInt("score2", score);
            }
            Start();

        }
        else
        {
            //два события - щелчок по шарику(выбор какой перемещать) и щелчок по пустому месту(выбор куда перемещать)
            if (map[x, y] > 0) // если щелкнули по шарику 
            {
                TakeBall(x, y); // берем шарик 
            }
            else // если щелкнули по пустой клетке 
            {
                MoveBall(x, y); // перемещаем шарик в пустую клетку 
            }
        }
    }

    private void TakeBall(int x, int y)
    {
        fromX = x; // откуда взяли шарик 
        fromY = y;
        isBallSelected = true; // шарик взят 
    }

    private void MoveBall(int x, int y)
    {
        if (!isBallSelected) return; // если шарик не выбран - выходим 
        if (!CanMove(x, y)) return; // может ли шарик переместиться 
        SetMap(x, y, map[fromX, fromY]); // переместили 
        SetMap(fromX, fromY, 0); // в координаты, где он был - пустую клетку 
        isBallSelected = false; // шарик уже не выбран, мы его переместили
        if (!CutLines()) // если не было вырезано линий
        {
            AddRandomBalls(); // добавляем новые шарики 
            CutLines(); // если появившиеся шарики создали новые линии, то сразу вырежем их 
        }
    }

    private void ClearMap() // очистка поля
    {
        for (int i = 0; i < SIZE; i++)
            for (int j = 0; j < SIZE; j++)
                SetMap(i, j, 0); // в каких координатах и что мы должны показать 
    }

    private bool OnMap(int x, int y) // проверка, что координаты находятся на доске
    {
        return x >= 0 && x < SIZE &&
               y >= 0 && y < SIZE;
    }

    private int GetMap(int x, int y) // проверка, что координаты не выходят за пределы матрицы
    {
        if (!OnMap(x, y)) return 0;
        return map[x, y];
    }

    private void SetMap(int x, int y, int ball)
    {
        map[x, y] = ball;
        showBox(x, y, ball);
    }

    private void AddRandomBalls() // добавление нескольких шариков 
    {
        for (int i = 0; i < ADD_BALLS; i++)
        {
            AddRandomBall();
        }
    }

    private void AddRandomBall() // добавление одного шарика
    {
        int x, y;
        int loop = SIZE * SIZE; // количество клеток 
        do
        {
            x = random.Next(SIZE); // от 0 до SIZE
            y = random.Next(SIZE);
            if (--loop <= 0) return; // чтобы не было зацикливания. Если поле заполнено целиком, нужна такая проверка 
        } while (map[x, y] > 0); // ищем пустую клетку 
        int ball = 1 + random.Next(BALLS - 1); // цвет 
        SetMap(x, y, ball); // помещаем шарик 
    }

    private bool[,] used; // помечаем, были мы в клетке или не были 

    private bool CanMove(int toX, int toY) // можно ли переместить шарик из одной координаты в другую - в заблокированные зоны переместить НЕЛЬЗЯ   
    {
        used = new bool[SIZE, SIZE];
        Walk(fromX, fromY, true); // "гуляем" по массиву
        return used[toX, toY]; // были ли мы в этой клетке 
    }

    // РЕКУРСИЯ
    private void Walk(int x, int y, bool start = false) // доп параметр - проверка, является ли запуск метода первым 
    {
        if (!start)
        {
            if (!OnMap(x, y)) return; // проверяем, что находимся на карте
            // потом проверяем, пустая ли клетка
            if (map[x, y] > 0) return; // клетка занята
            if (used[x, y]) return; // если находились в ячейке - выходим
        }
        used[x, y] = true; // пишем, что тут мы были - а то зациклится
        Walk(x + 1, y); //вправо 
        Walk(x - 1, y);
        Walk(x, y + 1);
        Walk(x, y - 1); 
    }

    private bool[,] mark; // какие шарики надо удалить

    private bool CutLines() // метод удаления линий из одинаковых шариков
    {
        int balls = 0; // сколько шариков для вырезания нашли 
        mark = new bool[SIZE, SIZE];
        for (int x = 0; x < SIZE; x++)
            for (int y = 0; y < SIZE; y++) // перебираем все клетки, ищем в них линии
            {
                balls += CutLine(x, y, 1, 0); // пытаемся вырезать линию, начиная с этой координаты, в направлении направо 
                balls += CutLine(x, y, 0, 1); // вниз 
                balls += CutLine(x, y, 1, 1); // вправо вниз (по диагонали вправо)
                balls += CutLine(x, y, -1, 1); // по диагонали влево 
            }
        if (balls > 0)
        {
        //    Debug.Log(balls);
            playCut(); // звук эффект 
            for (int x = 0; x < SIZE; x++)
                for (int y = 0; y < SIZE; y++)
                    if (mark[x, y]) // если элемент помечен для удаления
                    {
                        SetMap(x, y, 0); // удаляем
                        score++;
                        Debug.Log(score);
                    }
            return true; 
        }
        return false; 
    }

    private int CutLine(int x0, int y0, int sx, int sy) // ищем линию. откуда мы ищем - в каком направлении 
    {
        int ball = map[x0, y0]; // с какого шарика начали
        if (ball == 0) return 0; // если клетка пустая - ничего не будет 
        int count = 0; // сколько нашли шариков 
        for (int x = x0, y = y0; GetMap(x, y) == ball; x += sx, y += sy) // если шарик в выбранном направлении такой же, как текущий 
        {
            count++; // 
        }
        if (count < 5)
        {
            return 0; // меньше 5 нас не интересует, минимум линия - 5 шариков
        }
        else 
         //   score += count;
        for (int x = x0, y = y0; GetMap(x, y) == ball; x += sx, y += sy)
        {
            mark[x, y] = true; // записали шарик, который нужно удалить 
        }
            return count; // возвращаем кол-во найденных элементов
    }
     
    private bool IsGameOver() // а не конец ли игры? 
    {
        for (int x = 0; x < SIZE; x++)
            for (int y = 0; y < SIZE; y++) // перебираем все клетки
                if (map[x, y] == 0) // если хоть одна клетка пустая
                    return false;
        return true; // иначе
    }
}
