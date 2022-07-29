
// Основная программа

namespace Tetris
{
    class Program
    {
        static void Main()
        {
            int mapWidth = 10; // Переменная содержащая ширину игравого пространства
            int allMapWidth = 15; // Переменная содержащая общюю ширину окна
            int mapHeight = 20; // Переменная содержащая высоту окна
            int interval = 500; // Переменная содержащая определенный интервал времени

            //Переменные ниже используются для увеличения окна игры и взаимодействия с ним
            int screenWidth = allMapWidth * 3;
            int screenHeight = mapHeight * 3;
            int gameScreenWidth = mapWidth * 3;

            ConsoleColor stuffColor = ConsoleColor.Green; // Переменная содержащая цвет символов
            int[,] map = new int[mapWidth, mapHeight]; // Массив отражает текущее состояние игрового поля (0 ячейкас свободна. 1-4 ячейки заняты элементом фгуры с соответствующим номером. 5 ячейка занята элементом старых фигур)  

            // ниже массив заполняется 0 что следовательно игравое поле пустое
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = 0;
                }
            }
            int countX = 3; // переменная содержащая начальное положение первого элемента фигуры по оси Х (строки)
            int countY = 0; // переменная содержащая начальное положение первого элемента фигуры по оси У (столбцы)
            int score = 0; // переменная содержащая начальное количество очков
            int type; // переменная в которую будет записан номер типа фигуры
            Random rand = new Random(); // Экземпляр класса для создания случайного числа

            // ниже задается размер окна
            Console.SetWindowSize(screenWidth, screenHeight);
            Console.SetBufferSize(screenWidth, screenHeight);

            Console.CursorVisible = false; // свойство определяющее видимость курсора 


            while (true) // на данный момент игра продолжается бесконечно, условий для завершения игры нет
            {
                type = rand.Next(0, 4); // запись в переменную случайного числа
                Draw(map, stuffColor); // Метод отрисовывающий текущее состояние экрана

                // дальше в зависимости от значения переменной type выбирается одна из фигур
                if (type == 0)
                {
                    Line temp = new Line(countX, countY); // создается экземпляр класса Line
                    map = FillMap(map, temp.currentPosition); // в данной строчке сравниваются состояния игровога поля и всем участкам не занятым ранее упавшими фигурами присваивается значения из массива temp.currentPosition 
                    Draw(map, stuffColor); // отрисовывается текущее состояние поля
                    while (true)
                    {
                        if (Console.KeyAvailable == false) // если нет нажатия клавиши то при выполнении условий происходит двежение вниз
                        {
                            // условие проверяет не заняты ли нижние участки игрового поля и не закончилось ли поле
                            if (temp.firstPixelPosition[1] + 1 < map.GetLength(1)
                                && temp.secondPixelPosition[1] + 1 < map.GetLength(1)
                                && temp.thirdPixelPosition[1] + 1 < map.GetLength(1)
                                && temp.fourthPixelPosition[1] + 1 < map.GetLength(1)
                                && map[temp.firstPixelPosition[0], temp.firstPixelPosition[1] + 1] != 5
                                && map[temp.secondPixelPosition[0], temp.secondPixelPosition[1] + 1] != 5
                                && map[temp.thirdPixelPosition[0], temp.thirdPixelPosition[1] + 1] != 5
                                && map[temp.fourthPixelPosition[0], temp.fourthPixelPosition[1] + 1] != 5)
                            {
                                temp.MoveDown(); // метод выполняет движение вниз
                                map = FillMap(map, temp.currentPosition); // в данной строчке сравниваются состояния игровога поля и всем участкам не занятым ранее упавшими фигурами присваивается значения из массива temp.currentPosition
                            }
                            else // если вниз двигаться нельзя то элементам массива с текущими координатами фигуры присваивается значение 5.
                            {
                                map[temp.firstPixelPosition[0], temp.firstPixelPosition[1]] = 5;
                                map[temp.secondPixelPosition[0], temp.secondPixelPosition[1]] = 5;
                                map[temp.thirdPixelPosition[0], temp.thirdPixelPosition[1]] = 5;
                                map[temp.fourthPixelPosition[0], temp.fourthPixelPosition[1]] = 5;
                                map = FillMap(map, temp.currentPosition); // необходимость этой строчки под вопросом, решил что все же необходимо обновить текущее состояние массива.
                                break; // выход из цикла
                            }
                        }
                        else // дальше при условии нажатии одной из задействованных клавиш производится соответсвующее действие
                        {
                            ConsoleKey key = Console.ReadKey(true).Key;//Переменной key присваивается значение нажатой клавиши 

                            /*Дальше проверяется условие на возможность движения влевоб если правда то вызывается метод который перемещает элемент влево
                             если нет то проверяется условие на возможность движения вниз и если вниз можно то происходит движение вниз
                            после чего обновляется массив map и продолжается выполнение цикла. Если двигаться вниз нельзя то элементам массива с текущими координатами фигуры присваивается значение 5.
                             */
                            if (key == ConsoleKey.LeftArrow)
                            {
                                if (temp.firstPixelPosition[0] - 1 >= 0
                                    && temp.secondPixelPosition[0] - 1 >= 0
                                    && temp.thirdPixelPosition[0] - 1 >= 0
                                    && temp.fourthPixelPosition[0] - 1 >= 0
                                    && map[temp.firstPixelPosition[0] - 1, temp.firstPixelPosition[1]] != 5
                                    && map[temp.secondPixelPosition[0] - 1, temp.secondPixelPosition[1]] != 5
                                    && map[temp.thirdPixelPosition[0] - 1, temp.thirdPixelPosition[1]] != 5
                                    && map[temp.fourthPixelPosition[0] - 1, temp.fourthPixelPosition[1]] != 5)
                                {
                                    temp.MoveLeft();
                                }
                                if (temp.firstPixelPosition[1] + 1 < map.GetLength(1)
                                    && temp.secondPixelPosition[1] + 1 < map.GetLength(1)
                                    && temp.thirdPixelPosition[1] + 1 < map.GetLength(1)
                                    && temp.fourthPixelPosition[1] + 1 < map.GetLength(1)
                                    && map[temp.firstPixelPosition[0], temp.firstPixelPosition[1] + 1] != 5
                                    && map[temp.secondPixelPosition[0], temp.secondPixelPosition[1] + 1] != 5
                                    && map[temp.thirdPixelPosition[0], temp.thirdPixelPosition[1] + 1] != 5
                                    && map[temp.fourthPixelPosition[0], temp.fourthPixelPosition[1] + 1] != 5)
                                {
                                    temp.MoveDown();
                                }
                                else
                                {
                                    map[temp.firstPixelPosition[0], temp.firstPixelPosition[1]] = 5;
                                    map[temp.secondPixelPosition[0], temp.secondPixelPosition[1]] = 5;
                                    map[temp.thirdPixelPosition[0], temp.thirdPixelPosition[1]] = 5;
                                    map[temp.fourthPixelPosition[0], temp.fourthPixelPosition[1]] = 5;
                                    map = FillMap(map, temp.currentPosition);
                                    break;
                                }
                                map = FillMap(map, temp.currentPosition);

                            }
                            else if (key == ConsoleKey.RightArrow) // см. коментарий к предыдущему условию (только в данном случае движение вправо)
                            {
                                if (temp.firstPixelPosition[0] + 1 < map.GetLength(0)
                                && temp.secondPixelPosition[0] + 1 < map.GetLength(0)
                                && temp.thirdPixelPosition[0] + 1 < map.GetLength(0)
                                && temp.fourthPixelPosition[0] + 1 < map.GetLength(0)
                                && map[temp.firstPixelPosition[0] + 1, temp.firstPixelPosition[1]] != 5
                                && map[temp.secondPixelPosition[0] + 1, temp.secondPixelPosition[1]] != 5
                                && map[temp.thirdPixelPosition[0] + 1, temp.thirdPixelPosition[1]] != 5
                                && map[temp.fourthPixelPosition[0] + 1, temp.fourthPixelPosition[1]] != 5)
                                {
                                    temp.MoveRight();
                                }
                                if (temp.firstPixelPosition[1] + 1 < map.GetLength(1)
                                   && temp.secondPixelPosition[1] + 1 < map.GetLength(1)
                                   && temp.thirdPixelPosition[1] + 1 < map.GetLength(1)
                                   && temp.fourthPixelPosition[1] + 1 < map.GetLength(1)
                                   && map[temp.firstPixelPosition[0], temp.firstPixelPosition[1] + 1] != 5
                                   && map[temp.secondPixelPosition[0], temp.secondPixelPosition[1] + 1] != 5
                                   && map[temp.thirdPixelPosition[0], temp.thirdPixelPosition[1] + 1] != 5
                                   && map[temp.fourthPixelPosition[0], temp.fourthPixelPosition[1] + 1] != 5)
                                {
                                    temp.MoveDown();
                                }
                                else
                                {
                                    map[temp.firstPixelPosition[0], temp.firstPixelPosition[1]] = 5;
                                    map[temp.secondPixelPosition[0], temp.secondPixelPosition[1]] = 5;
                                    map[temp.thirdPixelPosition[0], temp.thirdPixelPosition[1]] = 5;
                                    map[temp.fourthPixelPosition[0], temp.fourthPixelPosition[1]] = 5;
                                    map = FillMap(map, temp.currentPosition);
                                    break;
                                }
                                map = FillMap(map, temp.currentPosition);

                            }
                            /* стрелка вверх запускает метод который поворачивает фигуру. После поворота происходт проверка не попала ли фигура куда ей
                              нельзя и если попала она еще раз поворачивается и тем самым возвращается в исходное положение.
                              Дальше аналогично предыдущим комментариям происходит движение вниз.*/
                            else if (key == ConsoleKey.UpArrow)
                            {
                                temp.MoveChange();
                                if (temp.firstPixelPosition[0] >= map.GetLength(0)
                                    || temp.secondPixelPosition[0] >= map.GetLength(0)
                                    || temp.thirdPixelPosition[0] >= map.GetLength(0)
                                    || temp.fourthPixelPosition[0] >= map.GetLength(0)
                                    || temp.firstPixelPosition[0] < 0
                                    || temp.secondPixelPosition[0] < 0
                                    || temp.thirdPixelPosition[0] < 0
                                    || temp.fourthPixelPosition[0] < 0
                                    || temp.firstPixelPosition[1] > map.GetLength(1) - 1
                                    || temp.secondPixelPosition[1] > map.GetLength(1) - 1
                                    || temp.thirdPixelPosition[1] > map.GetLength(1) - 1
                                    || temp.fourthPixelPosition[1] > map.GetLength(1) - 1
                                    || map[temp.firstPixelPosition[0], temp.firstPixelPosition[1]] == 5
                                    || map[temp.secondPixelPosition[0], temp.secondPixelPosition[1]] == 5
                                    || map[temp.thirdPixelPosition[0], temp.thirdPixelPosition[1]] == 5
                                    || map[temp.fourthPixelPosition[0], temp.fourthPixelPosition[1]] == 5
                                    || temp.firstPixelPosition[1] + 1 >= map.GetLength(1)
                                   || temp.secondPixelPosition[1] + 1 >= map.GetLength(1)
                                   || temp.thirdPixelPosition[1] + 1 >= map.GetLength(1)
                                   || temp.fourthPixelPosition[1] + 1 >= map.GetLength(1)
                                   || map[temp.firstPixelPosition[0], temp.firstPixelPosition[1] + 1] == 5
                                   || map[temp.secondPixelPosition[0], temp.secondPixelPosition[1] + 1] == 5
                                   || map[temp.thirdPixelPosition[0], temp.thirdPixelPosition[1] + 1] == 5
                                   || map[temp.fourthPixelPosition[0], temp.fourthPixelPosition[1] + 1] == 5)
                                {
                                    temp.MoveChange();
                                }
                                if (temp.firstPixelPosition[1] + 1 < map.GetLength(1)
                                   && temp.secondPixelPosition[1] + 1 < map.GetLength(1)
                                   && temp.thirdPixelPosition[1] + 1 < map.GetLength(1)
                                   && temp.fourthPixelPosition[1] + 1 < map.GetLength(1)
                                   && map[temp.firstPixelPosition[0], temp.firstPixelPosition[1] + 1] != 5
                                   && map[temp.secondPixelPosition[0], temp.secondPixelPosition[1] + 1] != 5
                                   && map[temp.thirdPixelPosition[0], temp.thirdPixelPosition[1] + 1] != 5
                                   && map[temp.fourthPixelPosition[0], temp.fourthPixelPosition[1] + 1] != 5)
                                {
                                    temp.MoveDown();
                                }
                                else
                                {
                                    map[temp.firstPixelPosition[0], temp.firstPixelPosition[1]] = 5;
                                    map[temp.secondPixelPosition[0], temp.secondPixelPosition[1]] = 5;
                                    map[temp.thirdPixelPosition[0], temp.thirdPixelPosition[1]] = 5;
                                    map[temp.fourthPixelPosition[0], temp.fourthPixelPosition[1]] = 5;
                                    map = FillMap(map, temp.currentPosition);
                                    break;
                                }
                                map = FillMap(map, temp.currentPosition);

                            }
                            else // это условие добавлено на случай нажатия любой другой клавиши и аналогично бездействию и дает движение вниз
                            {
                                if (temp.firstPixelPosition[1] + 1 < map.GetLength(1)
                                    && temp.secondPixelPosition[1] + 1 < map.GetLength(1)
                                    && temp.thirdPixelPosition[1] + 1 < map.GetLength(1)
                                    && temp.fourthPixelPosition[1] + 1 < map.GetLength(1)
                                    && map[temp.firstPixelPosition[0], temp.firstPixelPosition[1] + 1] != 5
                                    && map[temp.secondPixelPosition[0], temp.secondPixelPosition[1] + 1] != 5
                                    && map[temp.thirdPixelPosition[0], temp.thirdPixelPosition[1] + 1] != 5
                                    && map[temp.fourthPixelPosition[0], temp.fourthPixelPosition[1] + 1] != 5)
                                {
                                    temp.MoveDown(); 
                                    map = FillMap(map, temp.currentPosition); 
                                }
                                else 
                                {
                                    map[temp.firstPixelPosition[0], temp.firstPixelPosition[1]] = 5;
                                    map[temp.secondPixelPosition[0], temp.secondPixelPosition[1]] = 5;
                                    map[temp.thirdPixelPosition[0], temp.thirdPixelPosition[1]] = 5;
                                    map[temp.fourthPixelPosition[0], temp.fourthPixelPosition[1]] = 5;
                                    map = FillMap(map, temp.currentPosition); 
                                    break; 
                                }
                            }
                        }
                        while (Console.KeyAvailable == true) // без этого цикла при многократном нажатии клавиш управления, значения нажатой клавиши передовалось в следующее движение столько раз сколько была нажата клавиша
                        {
                            ConsoleKey t = Console.ReadKey(true).Key;
                        }
                        score = CountScore(map, score); //метод считает очки
                        map = ClearMap(map); //метод удаляет заполненные строки
                        Console.Clear(); // очистка всего экрана
                        Draw(map, stuffColor); // Отрисовка текущего состояния экрана
                        Thread.Sleep(interval); // приостановка программы на время записанное в переменной interval в милисекундах
                    }

                }
                //Дальше выполняется часть кода если переменная type равна 1 (фигура квадрат). Комментарии аналогичны предыдущем за исключением отсутсвия поворота фигуры.
                else if (type == 1)
                {
                    Box temp = new Box(countX, countY);
                    map = FillMap(map, temp.currentPosition);
                    Draw(map, stuffColor);
                    while (true)
                    {
                        if (Console.KeyAvailable == false)
                        {
                            if (temp.firstPixelPosition[1] + 1 < map.GetLength(1)
                                && temp.secondPixelPosition[1] + 1 < map.GetLength(1)
                                && temp.thirdPixelPosition[1] + 1 < map.GetLength(1)
                                && temp.fourthPixelPosition[1] + 1 < map.GetLength(1)
                                && map[temp.firstPixelPosition[0], temp.firstPixelPosition[1] + 1] != 5
                                && map[temp.secondPixelPosition[0], temp.secondPixelPosition[1] + 1] != 5
                                && map[temp.thirdPixelPosition[0], temp.thirdPixelPosition[1] + 1] != 5
                                && map[temp.fourthPixelPosition[0], temp.fourthPixelPosition[1] + 1] != 5)
                            {
                                temp.MoveDown();
                                map = FillMap(map, temp.currentPosition);
                            }
                            else
                            {
                                map[temp.firstPixelPosition[0], temp.firstPixelPosition[1]] = 5;
                                map[temp.secondPixelPosition[0], temp.secondPixelPosition[1]] = 5;
                                map[temp.thirdPixelPosition[0], temp.thirdPixelPosition[1]] = 5;
                                map[temp.fourthPixelPosition[0], temp.fourthPixelPosition[1]] = 5;
                                map = FillMap(map, temp.currentPosition);
                                break;
                            }
                        }
                        else
                        {
                            ConsoleKey key = Console.ReadKey(true).Key;
                            if (key == ConsoleKey.LeftArrow)
                            {
                                if (temp.firstPixelPosition[0] - 1 >= 0
                                    && temp.secondPixelPosition[0] - 1 >= 0
                                    && temp.thirdPixelPosition[0] - 1 >= 0
                                    && temp.fourthPixelPosition[0] - 1 >= 0
                                    && map[temp.firstPixelPosition[0] - 1, temp.firstPixelPosition[1]] != 5
                                    && map[temp.secondPixelPosition[0] - 1, temp.secondPixelPosition[1]] != 5
                                    && map[temp.thirdPixelPosition[0] - 1, temp.thirdPixelPosition[1]] != 5
                                    && map[temp.fourthPixelPosition[0] - 1, temp.fourthPixelPosition[1]] != 5)
                                {
                                    temp.MoveLeft();
                                }
                                if (temp.firstPixelPosition[1] + 1 < map.GetLength(1)
                                    && temp.secondPixelPosition[1] + 1 < map.GetLength(1)
                                    && temp.thirdPixelPosition[1] + 1 < map.GetLength(1)
                                    && temp.fourthPixelPosition[1] + 1 < map.GetLength(1)
                                    && map[temp.firstPixelPosition[0], temp.firstPixelPosition[1] + 1] != 5
                                    && map[temp.secondPixelPosition[0], temp.secondPixelPosition[1] + 1] != 5
                                    && map[temp.thirdPixelPosition[0], temp.thirdPixelPosition[1] + 1] != 5
                                    && map[temp.fourthPixelPosition[0], temp.fourthPixelPosition[1] + 1] != 5)
                                {
                                    temp.MoveDown();
                                }
                                else
                                {
                                    map[temp.firstPixelPosition[0], temp.firstPixelPosition[1]] = 5;
                                    map[temp.secondPixelPosition[0], temp.secondPixelPosition[1]] = 5;
                                    map[temp.thirdPixelPosition[0], temp.thirdPixelPosition[1]] = 5;
                                    map[temp.fourthPixelPosition[0], temp.fourthPixelPosition[1]] = 5;
                                    map = FillMap(map, temp.currentPosition);
                                    break;
                                }
                                map = FillMap(map, temp.currentPosition);

                            }
                            else if (key == ConsoleKey.RightArrow)
                            {
                                if (temp.firstPixelPosition[0] + 1 < map.GetLength(0)
                                && temp.secondPixelPosition[0] + 1 < map.GetLength(0)
                                && temp.thirdPixelPosition[0] + 1 < map.GetLength(0)
                                && temp.fourthPixelPosition[0] + 1 < map.GetLength(0)
                                && map[temp.firstPixelPosition[0] + 1, temp.firstPixelPosition[1]] != 5
                                && map[temp.secondPixelPosition[0] + 1, temp.secondPixelPosition[1]] != 5
                                && map[temp.thirdPixelPosition[0] + 1, temp.thirdPixelPosition[1]] != 5
                                && map[temp.fourthPixelPosition[0] + 1, temp.fourthPixelPosition[1]] != 5)
                                {
                                    temp.MoveRight();
                                }
                                if (temp.firstPixelPosition[1] + 1 < map.GetLength(1)
                                   && temp.secondPixelPosition[1] + 1 < map.GetLength(1)
                                   && temp.thirdPixelPosition[1] + 1 < map.GetLength(1)
                                   && temp.fourthPixelPosition[1] + 1 < map.GetLength(1)
                                   && map[temp.firstPixelPosition[0], temp.firstPixelPosition[1] + 1] != 5
                                   && map[temp.secondPixelPosition[0], temp.secondPixelPosition[1] + 1] != 5
                                   && map[temp.thirdPixelPosition[0], temp.thirdPixelPosition[1] + 1] != 5
                                   && map[temp.fourthPixelPosition[0], temp.fourthPixelPosition[1] + 1] != 5)
                                {
                                    temp.MoveDown();
                                }
                                else
                                {
                                    map[temp.firstPixelPosition[0], temp.firstPixelPosition[1]] = 5;
                                    map[temp.secondPixelPosition[0], temp.secondPixelPosition[1]] = 5;
                                    map[temp.thirdPixelPosition[0], temp.thirdPixelPosition[1]] = 5;
                                    map[temp.fourthPixelPosition[0], temp.fourthPixelPosition[1]] = 5;
                                    map = FillMap(map, temp.currentPosition);
                                    break;
                                }
                                map = FillMap(map, temp.currentPosition);

                            }
                            else
                            {
                                if (temp.firstPixelPosition[1] + 1 < map.GetLength(1)
                                    && temp.secondPixelPosition[1] + 1 < map.GetLength(1)
                                    && temp.thirdPixelPosition[1] + 1 < map.GetLength(1)
                                    && temp.fourthPixelPosition[1] + 1 < map.GetLength(1)
                                    && map[temp.firstPixelPosition[0], temp.firstPixelPosition[1] + 1] != 5
                                    && map[temp.secondPixelPosition[0], temp.secondPixelPosition[1] + 1] != 5
                                    && map[temp.thirdPixelPosition[0], temp.thirdPixelPosition[1] + 1] != 5
                                    && map[temp.fourthPixelPosition[0], temp.fourthPixelPosition[1] + 1] != 5)
                                {
                                    temp.MoveDown();
                                    map = FillMap(map, temp.currentPosition);
                                }
                                else
                                {
                                    map[temp.firstPixelPosition[0], temp.firstPixelPosition[1]] = 5;
                                    map[temp.secondPixelPosition[0], temp.secondPixelPosition[1]] = 5;
                                    map[temp.thirdPixelPosition[0], temp.thirdPixelPosition[1]] = 5;
                                    map[temp.fourthPixelPosition[0], temp.fourthPixelPosition[1]] = 5;
                                    map = FillMap(map, temp.currentPosition);
                                    break;
                                }
                            }
                        }
                        while (Console.KeyAvailable == true)
                        {
                            ConsoleKey t = Console.ReadKey(true).Key;
                        }
                        score = CountScore(map, score);
                        map = ClearMap(map);
                        Console.Clear();
                        Draw(map, stuffColor);
                        Thread.Sleep(interval);
                    }

                }
                //Дальше выполняется часть кода если переменная type равна 2 (фигура правый L блок). Комментарии аналогичны коментариям к типу 0 (линии) за исключением работы при нажатии клавишы вверх
                else if (type == 2)
                {
                    BlockLRight temp = new BlockLRight(countX, countY);
                    map = FillMap(map, temp.currentPosition);
                    Draw(map, stuffColor);
                    while (true)
                    {
                        if (Console.KeyAvailable == false)
                        {
                            if (temp.firstPixelPosition[1] + 1 < map.GetLength(1)
                                && temp.secondPixelPosition[1] + 1 < map.GetLength(1)
                                && temp.thirdPixelPosition[1] + 1 < map.GetLength(1)
                                && temp.fourthPixelPosition[1] + 1 < map.GetLength(1)
                                && map[temp.firstPixelPosition[0], temp.firstPixelPosition[1] + 1] != 5
                                && map[temp.secondPixelPosition[0], temp.secondPixelPosition[1] + 1] != 5
                                && map[temp.thirdPixelPosition[0], temp.thirdPixelPosition[1] + 1] != 5
                                && map[temp.fourthPixelPosition[0], temp.fourthPixelPosition[1] + 1] != 5)
                            {
                                temp.MoveDown();
                                map = FillMap(map, temp.currentPosition);
                            }
                            else
                            {
                                map[temp.firstPixelPosition[0], temp.firstPixelPosition[1]] = 5;
                                map[temp.secondPixelPosition[0], temp.secondPixelPosition[1]] = 5;
                                map[temp.thirdPixelPosition[0], temp.thirdPixelPosition[1]] = 5;
                                map[temp.fourthPixelPosition[0], temp.fourthPixelPosition[1]] = 5;
                                map = FillMap(map, temp.currentPosition);
                                break;
                            }
                        }
                        else
                        {
                            ConsoleKey key = Console.ReadKey(true).Key;
                            if (key == ConsoleKey.LeftArrow)
                            {
                                if (temp.firstPixelPosition[0] - 1 >= 0
                                    && temp.secondPixelPosition[0] - 1 >= 0
                                    && temp.thirdPixelPosition[0] - 1 >= 0
                                    && temp.fourthPixelPosition[0] - 1 >= 0
                                    && map[temp.firstPixelPosition[0] - 1, temp.firstPixelPosition[1]] != 5
                                    && map[temp.secondPixelPosition[0] - 1, temp.secondPixelPosition[1]] != 5
                                    && map[temp.thirdPixelPosition[0] - 1, temp.thirdPixelPosition[1]] != 5
                                    && map[temp.fourthPixelPosition[0] - 1, temp.fourthPixelPosition[1]] != 5)
                                {
                                    temp.MoveLeft();
                                }
                                if (temp.firstPixelPosition[1] + 1 < map.GetLength(1)
                                    && temp.secondPixelPosition[1] + 1 < map.GetLength(1)
                                    && temp.thirdPixelPosition[1] + 1 < map.GetLength(1)
                                    && temp.fourthPixelPosition[1] + 1 < map.GetLength(1)
                                    && map[temp.firstPixelPosition[0], temp.firstPixelPosition[1] + 1] != 5
                                    && map[temp.secondPixelPosition[0], temp.secondPixelPosition[1] + 1] != 5
                                    && map[temp.thirdPixelPosition[0], temp.thirdPixelPosition[1] + 1] != 5
                                    && map[temp.fourthPixelPosition[0], temp.fourthPixelPosition[1] + 1] != 5)
                                {
                                    temp.MoveDown();
                                }
                                else
                                {
                                    map[temp.firstPixelPosition[0], temp.firstPixelPosition[1]] = 5;
                                    map[temp.secondPixelPosition[0], temp.secondPixelPosition[1]] = 5;
                                    map[temp.thirdPixelPosition[0], temp.thirdPixelPosition[1]] = 5;
                                    map[temp.fourthPixelPosition[0], temp.fourthPixelPosition[1]] = 5;
                                    map = FillMap(map, temp.currentPosition);
                                    break;
                                }
                                map = FillMap(map, temp.currentPosition);

                            }
                            else if (key == ConsoleKey.RightArrow)
                            {
                                if (temp.firstPixelPosition[0] + 1 < map.GetLength(0)
                                && temp.secondPixelPosition[0] + 1 < map.GetLength(0)
                                && temp.thirdPixelPosition[0] + 1 < map.GetLength(0)
                                && temp.fourthPixelPosition[0] + 1 < map.GetLength(0)
                                && map[temp.firstPixelPosition[0] + 1, temp.firstPixelPosition[1]] != 5
                                && map[temp.secondPixelPosition[0] + 1, temp.secondPixelPosition[1]] != 5
                                && map[temp.thirdPixelPosition[0] + 1, temp.thirdPixelPosition[1]] != 5
                                && map[temp.fourthPixelPosition[0] + 1, temp.fourthPixelPosition[1]] != 5)
                                {
                                    temp.MoveRight();
                                }
                                if (temp.firstPixelPosition[1] + 1 < map.GetLength(1)
                                   && temp.secondPixelPosition[1] + 1 < map.GetLength(1)
                                   && temp.thirdPixelPosition[1] + 1 < map.GetLength(1)
                                   && temp.fourthPixelPosition[1] + 1 < map.GetLength(1)
                                   && map[temp.firstPixelPosition[0], temp.firstPixelPosition[1] + 1] != 5
                                   && map[temp.secondPixelPosition[0], temp.secondPixelPosition[1] + 1] != 5
                                   && map[temp.thirdPixelPosition[0], temp.thirdPixelPosition[1] + 1] != 5
                                   && map[temp.fourthPixelPosition[0], temp.fourthPixelPosition[1] + 1] != 5)
                                {
                                    temp.MoveDown();
                                }
                                else
                                {
                                    map[temp.firstPixelPosition[0], temp.firstPixelPosition[1]] = 5;
                                    map[temp.secondPixelPosition[0], temp.secondPixelPosition[1]] = 5;
                                    map[temp.thirdPixelPosition[0], temp.thirdPixelPosition[1]] = 5;
                                    map[temp.fourthPixelPosition[0], temp.fourthPixelPosition[1]] = 5;
                                    map = FillMap(map, temp.currentPosition);
                                    break;
                                }
                                map = FillMap(map, temp.currentPosition);

                            }
                            else if (key == ConsoleKey.UpArrow)
                            {
                                temp.MoveChange();
                                if (temp.firstPixelPosition[0] >= map.GetLength(0)
                                    || temp.secondPixelPosition[0] >= map.GetLength(0)
                                    || temp.thirdPixelPosition[0] >= map.GetLength(0)
                                    || temp.fourthPixelPosition[0] >= map.GetLength(0)
                                    || temp.firstPixelPosition[0] < 0
                                    || temp.secondPixelPosition[0] < 0
                                    || temp.thirdPixelPosition[0] < 0
                                    || temp.fourthPixelPosition[0] < 0
                                    || temp.firstPixelPosition[1] > map.GetLength(1) - 1
                                    || temp.secondPixelPosition[1] > map.GetLength(1) - 1
                                    || temp.thirdPixelPosition[1] > map.GetLength(1) - 1
                                    || temp.fourthPixelPosition[1] > map.GetLength(1) - 1
                                    || map[temp.firstPixelPosition[0], temp.firstPixelPosition[1]] == 5
                                    || map[temp.secondPixelPosition[0], temp.secondPixelPosition[1]] == 5
                                    || map[temp.thirdPixelPosition[0], temp.thirdPixelPosition[1]] == 5
                                    || map[temp.fourthPixelPosition[0], temp.fourthPixelPosition[1]] == 5
                                    || temp.firstPixelPosition[1] + 1 >= map.GetLength(1)
                                   || temp.secondPixelPosition[1] + 1 >= map.GetLength(1)
                                   || temp.thirdPixelPosition[1] + 1 >= map.GetLength(1)
                                   || temp.fourthPixelPosition[1] + 1 >= map.GetLength(1)
                                   || map[temp.firstPixelPosition[0], temp.firstPixelPosition[1] + 1] == 5
                                   || map[temp.secondPixelPosition[0], temp.secondPixelPosition[1] + 1] == 5
                                   || map[temp.thirdPixelPosition[0], temp.thirdPixelPosition[1] + 1] == 5
                                   || map[temp.fourthPixelPosition[0], temp.fourthPixelPosition[1] + 1] == 5)
                                {
                                    for (int i = 0; i < 3; i++)
                                    {
                                        temp.MoveChange();
                                    }
                                }
                                if (temp.firstPixelPosition[1] + 1 < map.GetLength(1)
                                   && temp.secondPixelPosition[1] + 1 < map.GetLength(1)
                                   && temp.thirdPixelPosition[1] + 1 < map.GetLength(1)
                                   && temp.fourthPixelPosition[1] + 1 < map.GetLength(1)
                                   && map[temp.firstPixelPosition[0], temp.firstPixelPosition[1] + 1] != 5
                                   && map[temp.secondPixelPosition[0], temp.secondPixelPosition[1] + 1] != 5
                                   && map[temp.thirdPixelPosition[0], temp.thirdPixelPosition[1] + 1] != 5
                                   && map[temp.fourthPixelPosition[0], temp.fourthPixelPosition[1] + 1] != 5)
                                {
                                    temp.MoveDown();
                                }
                                else
                                {
                                    map[temp.firstPixelPosition[0], temp.firstPixelPosition[1]] = 5;
                                    map[temp.secondPixelPosition[0], temp.secondPixelPosition[1]] = 5;
                                    map[temp.thirdPixelPosition[0], temp.thirdPixelPosition[1]] = 5;
                                    map[temp.fourthPixelPosition[0], temp.fourthPixelPosition[1]] = 5;
                                    map = FillMap(map, temp.currentPosition);
                                    break;
                                }
                                map = FillMap(map, temp.currentPosition);

                            }
                            else
                            {
                                if (temp.firstPixelPosition[1] + 1 < map.GetLength(1)
                                    && temp.secondPixelPosition[1] + 1 < map.GetLength(1)
                                    && temp.thirdPixelPosition[1] + 1 < map.GetLength(1)
                                    && temp.fourthPixelPosition[1] + 1 < map.GetLength(1)
                                    && map[temp.firstPixelPosition[0], temp.firstPixelPosition[1] + 1] != 5
                                    && map[temp.secondPixelPosition[0], temp.secondPixelPosition[1] + 1] != 5
                                    && map[temp.thirdPixelPosition[0], temp.thirdPixelPosition[1] + 1] != 5
                                    && map[temp.fourthPixelPosition[0], temp.fourthPixelPosition[1] + 1] != 5)
                                {
                                    temp.MoveDown();
                                    map = FillMap(map, temp.currentPosition);
                                }
                                else
                                {
                                    map[temp.firstPixelPosition[0], temp.firstPixelPosition[1]] = 5;
                                    map[temp.secondPixelPosition[0], temp.secondPixelPosition[1]] = 5;
                                    map[temp.thirdPixelPosition[0], temp.thirdPixelPosition[1]] = 5;
                                    map[temp.fourthPixelPosition[0], temp.fourthPixelPosition[1]] = 5;
                                    map = FillMap(map, temp.currentPosition);
                                    break;
                                }
                            }
                        }
                        while (Console.KeyAvailable == true)
                        {
                            ConsoleKey t = Console.ReadKey(true).Key;
                        }
                        score = CountScore(map, score);
                        map = ClearMap(map);
                        Console.Clear();
                        Draw(map, stuffColor);
                        Thread.Sleep(interval);
                    }

                }
                else  //Дальше выполняется часть кода если переменная type равна 3 (фигура левый L блок). Комментарии аналогичны предыдущем.
                {
                    BlockLLeft temp = new BlockLLeft(countX, countY);
                    map = FillMap(map, temp.currentPosition);
                    Draw(map, stuffColor);
                    while (true)
                    {
                        if (Console.KeyAvailable == false)
                        {
                            if (temp.firstPixelPosition[1] + 1 < map.GetLength(1)
                                && temp.secondPixelPosition[1] + 1 < map.GetLength(1)
                                && temp.thirdPixelPosition[1] + 1 < map.GetLength(1)
                                && temp.fourthPixelPosition[1] + 1 < map.GetLength(1)
                                && map[temp.firstPixelPosition[0], temp.firstPixelPosition[1] + 1] != 5
                                && map[temp.secondPixelPosition[0], temp.secondPixelPosition[1] + 1] != 5
                                && map[temp.thirdPixelPosition[0], temp.thirdPixelPosition[1] + 1] != 5
                                && map[temp.fourthPixelPosition[0], temp.fourthPixelPosition[1] + 1] != 5)
                            {
                                temp.MoveDown();
                                map = FillMap(map, temp.currentPosition);
                            }
                            else
                            {
                                map[temp.firstPixelPosition[0], temp.firstPixelPosition[1]] = 5;
                                map[temp.secondPixelPosition[0], temp.secondPixelPosition[1]] = 5;
                                map[temp.thirdPixelPosition[0], temp.thirdPixelPosition[1]] = 5;
                                map[temp.fourthPixelPosition[0], temp.fourthPixelPosition[1]] = 5;
                                map = FillMap(map, temp.currentPosition);
                                break;
                            }
                        }
                        else
                        {
                            ConsoleKey key = Console.ReadKey(true).Key;
                            if (key == ConsoleKey.LeftArrow)
                            {
                                if (temp.firstPixelPosition[0] - 1 >= 0
                                    && temp.secondPixelPosition[0] - 1 >= 0
                                    && temp.thirdPixelPosition[0] - 1 >= 0
                                    && temp.fourthPixelPosition[0] - 1 >= 0
                                    && map[temp.firstPixelPosition[0] - 1, temp.firstPixelPosition[1]] != 5
                                    && map[temp.secondPixelPosition[0] - 1, temp.secondPixelPosition[1]] != 5
                                    && map[temp.thirdPixelPosition[0] - 1, temp.thirdPixelPosition[1]] != 5
                                    && map[temp.fourthPixelPosition[0] - 1, temp.fourthPixelPosition[1]] != 5)
                                {
                                    temp.MoveLeft();
                                }
                                if (temp.firstPixelPosition[1] + 1 < map.GetLength(1)
                                    && temp.secondPixelPosition[1] + 1 < map.GetLength(1)
                                    && temp.thirdPixelPosition[1] + 1 < map.GetLength(1)
                                    && temp.fourthPixelPosition[1] + 1 < map.GetLength(1)
                                    && map[temp.firstPixelPosition[0], temp.firstPixelPosition[1] + 1] != 5
                                    && map[temp.secondPixelPosition[0], temp.secondPixelPosition[1] + 1] != 5
                                    && map[temp.thirdPixelPosition[0], temp.thirdPixelPosition[1] + 1] != 5
                                    && map[temp.fourthPixelPosition[0], temp.fourthPixelPosition[1] + 1] != 5)
                                {
                                    temp.MoveDown();
                                }
                                else
                                {
                                    map[temp.firstPixelPosition[0], temp.firstPixelPosition[1]] = 5;
                                    map[temp.secondPixelPosition[0], temp.secondPixelPosition[1]] = 5;
                                    map[temp.thirdPixelPosition[0], temp.thirdPixelPosition[1]] = 5;
                                    map[temp.fourthPixelPosition[0], temp.fourthPixelPosition[1]] = 5;
                                    map = FillMap(map, temp.currentPosition);
                                    break;
                                }
                                map = FillMap(map, temp.currentPosition);

                            }
                            else if (key == ConsoleKey.RightArrow)
                            {
                                if (temp.firstPixelPosition[0] + 1 < map.GetLength(0)
                                && temp.secondPixelPosition[0] + 1 < map.GetLength(0)
                                && temp.thirdPixelPosition[0] + 1 < map.GetLength(0)
                                && temp.fourthPixelPosition[0] + 1 < map.GetLength(0)
                                && map[temp.firstPixelPosition[0] + 1, temp.firstPixelPosition[1]] != 5
                                && map[temp.secondPixelPosition[0] + 1, temp.secondPixelPosition[1]] != 5
                                && map[temp.thirdPixelPosition[0] + 1, temp.thirdPixelPosition[1]] != 5
                                && map[temp.fourthPixelPosition[0] + 1, temp.fourthPixelPosition[1]] != 5)
                                {
                                    temp.MoveRight();
                                }
                                if (temp.firstPixelPosition[1] + 1 < map.GetLength(1)
                                   && temp.secondPixelPosition[1] + 1 < map.GetLength(1)
                                   && temp.thirdPixelPosition[1] + 1 < map.GetLength(1)
                                   && temp.fourthPixelPosition[1] + 1 < map.GetLength(1)
                                   && map[temp.firstPixelPosition[0], temp.firstPixelPosition[1] + 1] != 5
                                   && map[temp.secondPixelPosition[0], temp.secondPixelPosition[1] + 1] != 5
                                   && map[temp.thirdPixelPosition[0], temp.thirdPixelPosition[1] + 1] != 5
                                   && map[temp.fourthPixelPosition[0], temp.fourthPixelPosition[1] + 1] != 5)
                                {
                                    temp.MoveDown();
                                }
                                else
                                {
                                    map[temp.firstPixelPosition[0], temp.firstPixelPosition[1]] = 5;
                                    map[temp.secondPixelPosition[0], temp.secondPixelPosition[1]] = 5;
                                    map[temp.thirdPixelPosition[0], temp.thirdPixelPosition[1]] = 5;
                                    map[temp.fourthPixelPosition[0], temp.fourthPixelPosition[1]] = 5;
                                    map = FillMap(map, temp.currentPosition);
                                    break;
                                }
                                map = FillMap(map, temp.currentPosition);

                            }
                            else if (key == ConsoleKey.UpArrow)
                            {
                                temp.MoveChange();
                                if (temp.firstPixelPosition[0] >= map.GetLength(0)
                                    || temp.secondPixelPosition[0] >= map.GetLength(0)
                                    || temp.thirdPixelPosition[0] >= map.GetLength(0)
                                    || temp.fourthPixelPosition[0] >= map.GetLength(0)
                                    || temp.firstPixelPosition[0] < 0
                                    || temp.secondPixelPosition[0] < 0
                                    || temp.thirdPixelPosition[0] < 0
                                    || temp.fourthPixelPosition[0] < 0
                                    || temp.firstPixelPosition[1] > map.GetLength(1) - 1
                                    || temp.secondPixelPosition[1] > map.GetLength(1) - 1
                                    || temp.thirdPixelPosition[1] > map.GetLength(1) - 1
                                    || temp.fourthPixelPosition[1] > map.GetLength(1) - 1
                                    || map[temp.firstPixelPosition[0], temp.firstPixelPosition[1]] == 5
                                    || map[temp.secondPixelPosition[0], temp.secondPixelPosition[1]] == 5
                                    || map[temp.thirdPixelPosition[0], temp.thirdPixelPosition[1]] == 5
                                    || map[temp.fourthPixelPosition[0], temp.fourthPixelPosition[1]] == 5
                                    || temp.firstPixelPosition[1] + 1 >= map.GetLength(1)
                                   || temp.secondPixelPosition[1] + 1 >= map.GetLength(1)
                                   || temp.thirdPixelPosition[1] + 1 >= map.GetLength(1)
                                   || temp.fourthPixelPosition[1] + 1 >= map.GetLength(1)
                                   || map[temp.firstPixelPosition[0], temp.firstPixelPosition[1] + 1] == 5
                                   || map[temp.secondPixelPosition[0], temp.secondPixelPosition[1] + 1] == 5
                                   || map[temp.thirdPixelPosition[0], temp.thirdPixelPosition[1] + 1] == 5
                                   || map[temp.fourthPixelPosition[0], temp.fourthPixelPosition[1] + 1] == 5)
                                {
                                    for (int i = 0; i < 3; i++)
                                    {
                                        temp.MoveChange();
                                    }
                                }
                                if (temp.firstPixelPosition[1] + 1 < map.GetLength(1)
                                   && temp.secondPixelPosition[1] + 1 < map.GetLength(1)
                                   && temp.thirdPixelPosition[1] + 1 < map.GetLength(1)
                                   && temp.fourthPixelPosition[1] + 1 < map.GetLength(1)
                                   && map[temp.firstPixelPosition[0], temp.firstPixelPosition[1] + 1] != 5
                                   && map[temp.secondPixelPosition[0], temp.secondPixelPosition[1] + 1] != 5
                                   && map[temp.thirdPixelPosition[0], temp.thirdPixelPosition[1] + 1] != 5
                                   && map[temp.fourthPixelPosition[0], temp.fourthPixelPosition[1] + 1] != 5)
                                {
                                    temp.MoveDown();
                                }
                                else
                                {
                                    map[temp.firstPixelPosition[0], temp.firstPixelPosition[1]] = 5;
                                    map[temp.secondPixelPosition[0], temp.secondPixelPosition[1]] = 5;
                                    map[temp.thirdPixelPosition[0], temp.thirdPixelPosition[1]] = 5;
                                    map[temp.fourthPixelPosition[0], temp.fourthPixelPosition[1]] = 5;
                                    map = FillMap(map, temp.currentPosition);
                                    break;
                                }
                                map = FillMap(map, temp.currentPosition);

                            }
                            else
                            {
                                if (temp.firstPixelPosition[1] + 1 < map.GetLength(1)
                                    && temp.secondPixelPosition[1] + 1 < map.GetLength(1)
                                    && temp.thirdPixelPosition[1] + 1 < map.GetLength(1)
                                    && temp.fourthPixelPosition[1] + 1 < map.GetLength(1)
                                    && map[temp.firstPixelPosition[0], temp.firstPixelPosition[1] + 1] != 5
                                    && map[temp.secondPixelPosition[0], temp.secondPixelPosition[1] + 1] != 5
                                    && map[temp.thirdPixelPosition[0], temp.thirdPixelPosition[1] + 1] != 5
                                    && map[temp.fourthPixelPosition[0], temp.fourthPixelPosition[1] + 1] != 5)
                                {
                                    temp.MoveDown();
                                    map = FillMap(map, temp.currentPosition);
                                }
                                else
                                {
                                    map[temp.firstPixelPosition[0], temp.firstPixelPosition[1]] = 5;
                                    map[temp.secondPixelPosition[0], temp.secondPixelPosition[1]] = 5;
                                    map[temp.thirdPixelPosition[0], temp.thirdPixelPosition[1]] = 5;
                                    map[temp.fourthPixelPosition[0], temp.fourthPixelPosition[1]] = 5;
                                    map = FillMap(map, temp.currentPosition);
                                    break;
                                }
                            }
                        }
                        while (Console.KeyAvailable == true)
                        {
                            ConsoleKey t = Console.ReadKey(true).Key;
                        }
                        score = CountScore(map, score);
                        map = ClearMap(map);
                        Console.Clear();
                        Draw(map, stuffColor);
                        Thread.Sleep(interval);
                    }

                }
            }

            // метод принимает массив и цвет значков и отрисовывает игровое поле.
            void Draw(int[,] array, ConsoleColor pixelColor)
            {
                char pixelChar = '█';
                char pixelCharOne = '@';
                Console.ForegroundColor = pixelColor;
                for (int i = 0; i < map.GetLength(0); i++)
                {
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                        if (array[i, j] == 5)
                        {
                            for (int k = 0; k < 3; k++)
                            {
                                for (int m = 0; m < 3; m++)
                                {
                                    Console.SetCursorPosition(i * 3 + k, j * 3 + m);
                                    Console.Write(pixelChar);
                                }
                            }

                        }
                        else if (array[i, j] != 0)
                        {
                            for (int k = 0; k < 3; k++)
                            {
                                for (int m = 0; m < 3; m++)
                                {
                                    Console.SetCursorPosition(i * 3 + k, j * 3 + m);
                                    Console.Write(pixelCharOne);
                                }
                            }
                        }

                    }
                }
                for (int i = 0; i < screenHeight; i++)
                {
                    Console.SetCursorPosition(gameScreenWidth, i);
                    Console.Write("|");
                }
                Console.SetCursorPosition(32, 20);
                Console.Write($"Score:{score}");
                Console.SetCursorPosition(32, 25);
                Console.Write($"Type:{type}");

            }

            // Метод берет два массива, если значение перемменой в массиве не равно 5 то он присваевает этой переменной значение равное значению переменной с темеже индесами из второго массива
            int[,] FillMap(int[,] firstArray, int[,] secondArray)
            {
                for (int i = 0; i < firstArray.GetLength(0); i++)
                {
                    for (int j = 0; j < firstArray.GetLength(1); j++)
                    {
                        if (firstArray[i, j] != 5) firstArray[i, j] = secondArray[i, j];
                    }
                }
                return firstArray;
            }
            // метод принимает массив и текущее количество очков, при условии что какая либо строка полностью занята количество очков увеличивается на 10 за каждую строку. Метод возвращает новое значение количества очков.
            int CountScore(int[,] array, int score)
            {
                for (int i = 0; i < array.GetLength(1); i++)
                {
                    int sum = 0;
                    for (int j = 0; j < array.GetLength(0); j++)
                    {
                        sum = sum + map[j, i];
                    }
                    if (sum == 50) score = score + 10;
                }
                return score;
            }
            /* Метод принимает массив. Если сумма в какой либо строке равна 50 (тоесть вся строка занята)
             то последовательно для этой и всех верхних строк элементам строки присваивается значение элементов строки сверху
             */
            int[,] ClearMap(int[,] array)
            {
                for (int i = 0; i < array.GetLength(1); i++)
                {
                    int sum = 0;
                    for (int j = 0; j < array.GetLength(0); j++)
                    {
                        sum = sum + array[j, i];
                    }
                    if (sum == 50)
                    {
                        for (int k = i; k > 0; k--)
                        {
                            for (int m = 0; m < array.GetLength(0); m++)
                            {
                                array[m, k] = array[m, k - 1];
                            }
                        }
                    }
                }
                return array;
            }
        }
    }
}
