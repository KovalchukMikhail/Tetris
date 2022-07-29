// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;

namespace Tetris
{
    public class Box
    {
        public int tipe = 0;
        public int[,] currentPosition = new int[10, 20]; // массив в который будут записываться занятые ячейки, пустая ячейка будет с "0", а остальные по номеру пикселя.
        public int[] firstPixelPosition = new int[2]; // массив для записи индексов положения первого пикселя в общем массиве
        public int[] secondPixelPosition = new int[2]; // массив для записи индексов положения второг пикселя в общем массиве
        public int[] thirdPixelPosition = new int[2]; // массив для записи индексов положения третьег пикселя в общем массиве
        public int[] fourthPixelPosition = new int[2]; // массив для записи индексов положения четвертого пикселя в общем массиве

        public Box(int initialX, int initialY) //конструктор для линии которая будет появляться в начале каждого раунда
        {
            for (int i = 0; i < 10; i++) // цикл для заполнения массива currentPosition 
            {
                for (var j = 0; j < 20; j++)
                {
                    currentPosition[i, j] = 0;
                }
            }

            // записываем координаты всех четырех точек в массив currentPosition
            currentPosition[initialX, initialY] = 1;
            currentPosition[initialX + 1, initialY] = 2;
            currentPosition[initialX, initialY + 1] = 3;
            currentPosition[initialX + 1, initialY + 1] = 4;

            firstPixelPosition = Check(1, currentPosition);
            secondPixelPosition = Check(2, currentPosition);
            thirdPixelPosition = Check(3, currentPosition);
            fourthPixelPosition = Check(4, currentPosition);
        }
        public int[] Check(int number, int[,] array) // метод для определения координат пикселя в массиве currentPosition
        {
            int[] result = new int[2];
            for (int i = 0; i < 10; i++) // цикл для поиска в масиве нужного числа 
            {
                for (var j = 0; j < 20; j++)
                {
                    if (number == array[i, j])
                    {
                        result[0] = i;
                        result[1] = j;
                    }
                }
            }
            return result;
        }

        public void MoveDown()
        {

            currentPosition[firstPixelPosition[0], firstPixelPosition[1]] = 0;
            currentPosition[secondPixelPosition[0], secondPixelPosition[1]] = 0;
            currentPosition[thirdPixelPosition[0], thirdPixelPosition[1]] = 0;
            currentPosition[fourthPixelPosition[0], fourthPixelPosition[1]] = 0;

            currentPosition[firstPixelPosition[0], firstPixelPosition[1] + 1] = 1;
            currentPosition[secondPixelPosition[0], secondPixelPosition[1] + 1] = 2;
            currentPosition[thirdPixelPosition[0], thirdPixelPosition[1] + 1] = 3;
            currentPosition[fourthPixelPosition[0], fourthPixelPosition[1] + 1] = 4;

            firstPixelPosition = Check(1, currentPosition);
            secondPixelPosition = Check(2, currentPosition);
            thirdPixelPosition = Check(3, currentPosition);
            fourthPixelPosition = Check(4, currentPosition);
        }
        public void MoveLeft()
        {

            currentPosition[firstPixelPosition[0], firstPixelPosition[1]] = 0;
            currentPosition[secondPixelPosition[0], secondPixelPosition[1]] = 0;
            currentPosition[thirdPixelPosition[0], thirdPixelPosition[1]] = 0;
            currentPosition[fourthPixelPosition[0], fourthPixelPosition[1]] = 0;

            currentPosition[firstPixelPosition[0] - 1, firstPixelPosition[1]] = 1;
            currentPosition[secondPixelPosition[0] - 1, secondPixelPosition[1]] = 2;
            currentPosition[thirdPixelPosition[0] - 1, thirdPixelPosition[1]] = 3;
            currentPosition[fourthPixelPosition[0] - 1, fourthPixelPosition[1]] = 4;

            firstPixelPosition = Check(1, currentPosition);
            secondPixelPosition = Check(2, currentPosition);
            thirdPixelPosition = Check(3, currentPosition);
            fourthPixelPosition = Check(4, currentPosition);
        }
        public void MoveRight()
        {

            currentPosition[firstPixelPosition[0], firstPixelPosition[1]] = 0;
            currentPosition[secondPixelPosition[0], secondPixelPosition[1]] = 0;
            currentPosition[thirdPixelPosition[0], thirdPixelPosition[1]] = 0;
            currentPosition[fourthPixelPosition[0], fourthPixelPosition[1]] = 0;

            currentPosition[firstPixelPosition[0] + 1, firstPixelPosition[1]] = 1;
            currentPosition[secondPixelPosition[0] + 1, secondPixelPosition[1]] = 2;
            currentPosition[thirdPixelPosition[0] + 1, thirdPixelPosition[1]] = 3;
            currentPosition[fourthPixelPosition[0] + 1, fourthPixelPosition[1]] = 4;

            firstPixelPosition = Check(1, currentPosition);
            secondPixelPosition = Check(2, currentPosition);
            thirdPixelPosition = Check(3, currentPosition);
            fourthPixelPosition = Check(4, currentPosition);
        }
    }
}
