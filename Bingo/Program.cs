using System;
using System.Runtime.InteropServices;


namespace Bingo
{
    internal class Program
    {

        static void MakeMap(out int[,] map)
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
        }

        static (int,int) CheckMap(int[,] map,int target) 
        {
  
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i,j] == target)
                    {
                        return (i, j);
                    }
                }
            }

            return (-1,-1); // 에러
        }
        static void Render(int[,] map , int score)
        {
            Console.WriteLine("======빙고========");
            Console.WriteLine("{0}개 빙고",score);
            //맵그리기
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == -1)
                    {
                        Console.Write("#\t");
                    }
                    else 
                    {
                        Console.Write("{0}\t", map[i, j]);
                    }
                }
                Console.WriteLine();
            }

            Console.Write("입력해라:");

        }


        static void CheckBingo(int[,] map,ref int score) 
        {
            int result = 0;


            int countRow = 0;
            int countColumn = 0;

            int rowValue = 0;
            int ColumnValue = 0;

            for (int i = 0; i < map.GetLength(0); i++)
            {

                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i,j] == -1)
                    {
                        rowValue++;
                    }
                    
                }

                if (rowValue == 5)
                {
                    countRow++;
                }

                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[j, i] == -1)
                    {
                        ColumnValue++;
                    }
                }

                if (ColumnValue == 5)
                {
                    countColumn++;
                }

                rowValue = 0;
                ColumnValue = 0;
            }

            //대각선 체크
            if (map[0,0] == -1 &&map[0, 0] == map[1,1] && map[1,1] == map[2,2] 
                && map[2,2] == map[3,3] && map[3,3] == map[4,4])
            {
                result++;
            }

            result += countRow + countColumn;
            
            score = result;

        }

        static void Main(string[] args)
        {
            int[,] map;
            int score = 0;
            int x, y;
            MakeMap(out map);

            while (true)
            {
                Console.Clear();
                Render(map, score);

                String input = Console.ReadLine();

                if (input == null || input == "")
                {
                    continue;
                } // 혹시나 null 값이나 빈값일때 예외처리용


                int target = int.Parse(input);

                (x,y) = CheckMap(map, target);

                if ((x,y) == (-1,-1))
                {
                    Console.Write("잘못입력했다..");
                    continue;
                }

                map[x,y] = -1;

                CheckBingo(map, ref score);

                if (score >= 3) // 3줄이상 빙고일시 종료
                {
                    Console.Clear();
                    Render(map, score);
                    Console.Write("3줄 빙고성공! 게임을 종료합니다.");
                    break;
                }


            }

        }
    }
}