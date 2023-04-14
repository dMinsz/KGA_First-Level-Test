using System;
using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace SlidePuzzle
{
    internal class Program
    {
        public struct Pos
        {
            public int x;
            public int y;
            public Pos(int x, int y)
            {
                this.x = x; this.y = y;
            }

            public void move(KEY dir)
            {
                switch (dir)
                {
                    case KEY.Up:
                        y--;
                        break;
                    case KEY.Down:
                        y++;
                        break;
                    case KEY.Left:
                        x--;
                        break;
                    case KEY.Right:
                        x++;
                        break;
                    default:
                        break;
                }
            }
        }

        public enum KEY { Up, Down, Left, Right, End , err }

        //숫자 1~100까지넣고 섞는버전
        static void MakeMap(out int[,] map, out Pos zeroPos)
        {
            map = new int[5, 5];
            Random random = new Random();


            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = random.Next(1, 100);
                }
            }

            int x = random.Next(5);
            int y = random.Next(5);

            zeroPos = new Pos(x, y);

            map[y, x] = 0;
        }

        //1~25까지 넣고 섞는버전
        static void MakeMap2(out int[,] map, out Pos zeroPos)
        {
            map = new int[5, 5];
            Random random = new Random();
            int num = 1;

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = num++;
                }
            }

            int lengthRow = map.GetLength(1);

            //이차원배열 섞기
            for (int i = map.Length - 1; i > 0; i--)
            {
                int i0 = i / lengthRow;
                int i1 = i % lengthRow;

                int j = random.Next(i + 1);
                int j0 = j / lengthRow;
                int j1 = j % lengthRow;

                int temp = map[i0, i1];
                map[i0, i1] = map[j0, j1];
                map[j0, j1] = temp;
            }

            int x = random.Next(5);
            int y = random.Next(5);

            zeroPos = new Pos(x, y);

            map[y, x] = 0;
        }

        static void Render(int[,] map)
        {
            //맵그리기
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write("{0}\t", map[i, j]);
                }
                Console.WriteLine();
            }

            Console.WriteLine("Q 끝내기, <- 왼쪽, -> 오른쪽, ↑ 위쪽, ↓ 아래쪽");
        }

        static void swap(ref int[,] map, ref Pos zeroPos, KEY dir)
        {

            Pos tempPos = zeroPos;
            zeroPos.move(dir);


            if (zeroPos.x >= 5 || zeroPos.x < 0 || zeroPos.y>=5||zeroPos.y<0)
            { // 범위를 벗어나면 처리 안함
                zeroPos = tempPos;
                return;
            }

            int temp = map[zeroPos.y, zeroPos.x];

            map[zeroPos.y, zeroPos.x] = 0; // 움직일곳 0으로
            map[tempPos.y, tempPos.x] = temp;// 0이였던곳을 바뀔값으로
        }

        static KEY InputKey()
        {
            ConsoleKeyInfo inputkey = Console.ReadKey();
            KEY result;
            switch (inputkey.Key)
            {
                case ConsoleKey.UpArrow:
                    result = KEY.Up;
                    break;
                case ConsoleKey.DownArrow:
                    result = KEY.Down;
                    break;
                case ConsoleKey.LeftArrow:
                    result = KEY.Left;
                    break;
                case ConsoleKey.RightArrow:
                    result = KEY.Right;
                    break;
                case ConsoleKey.Q:
                    result = KEY.End;
                    break;
                default:
                    result = KEY.err;
                    break;
            }

            return result;
        }

        static void Main(string[] args)
        {
            int[,] map;
            Pos zeroPos;
            MakeMap2(out map, out zeroPos);

            while (true)
            {
                Console.Clear();

                Render(map);
                KEY input = InputKey();

                if (input == KEY.End)
                {
                    break;
                }

                if (input != KEY.End && input != KEY.err)
                {
                    swap(ref map, ref zeroPos, input);
                }


            }
        }
    }
}