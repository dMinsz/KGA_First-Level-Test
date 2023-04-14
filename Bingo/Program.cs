using System;
using System.Runtime.InteropServices;

/*빙고게임 만들기
 * 5x5 판을 생성하고 랜덤한 숫자를 배치한다.
 * 원하는 숫자 입력시 해당 숫자는 특수기호로 바꾼다.
 * 생성한 숫자외의 다른 수를 입력할 수 없다.(예외처리)
 * 종료 조건은 빙고 3줄 이상시 종료한다.
 */

namespace Bingo
{
    internal class Program
    {

        //빙고판 만들기
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

        //맵에 target 이 있는지 어디에있는지 체크한후 결과로 리턴
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

        //빙고판 그리기
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

        //빙고가 몇개인지 체크하는 함수
        static void CheckBingo(int[,] map,ref int score) 
        {
            int result = 0;

            int countRow = 0; // 빙고 줄 체크
            int countColumn = 0;

            int rowValue = 0; // 빙고 열 체크
            int ColumnValue = 0;

            for (int i = 0; i < map.GetLength(0); i++)
            {

                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i,j] == -1) // 맞춘곳은 -1 로 바꿨으니 확인
                    {
                        rowValue++;
                    }
                    
                }

                if (rowValue == 5) // 5개 즉 한줄 빙고시 카운트 상승
                {
                    countRow++;
                }

                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[j, i] == -1)// 맞춘곳은 -1 로 바꿨으니 확인
                    {
                        ColumnValue++;
                    }
                }

                if (ColumnValue == 5) // 5개 즉 한열 빙고시 카운트 상승
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

            int target;

            while (true) // 게임루프
            {
                Console.Clear();
                Render(map, score);

                //입력 예외처리
                if (!int.TryParse(Console.ReadLine(), out target))
                {
                    Console.WriteLine("잘못된 입력을 하였습니다..");
                    continue;
                }
              
                (x,y) = CheckMap(map, target);

                if ((x,y) == (-1,-1)) // 이미 체크한곳 또다시 체크시
                {
                    Console.Write("이미 체크된곳을 입력했다..");
                    continue;
                }

                map[x,y] = -1; // 체크된곳은 -1 로 표시해준다.

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