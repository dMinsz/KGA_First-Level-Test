using System;
using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

/*슬라이드 퍼즐 만들기
 * 5x5 판을 생성하고 랜덤한 숫자를 배치한다.
 * 시작위치는 상관없으며 ArrowKey입력시 해당 방향으로 이동한다.
 * 단, 밖으로 벗어날 수 없다.
 * 아래 예시는 0이 움직이는 것으로 가정한다.
 */

namespace SlidePuzzle
{
    internal class Program
    {
        //위치 저장용 구조체
        public struct Pos
        {
            public int x;
            public int y;
            public Pos(int x, int y)
            {
                this.x = x; this.y = y;
            }

            //입력 방향에 따라 움직여준다.
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

        //사용자가 입력한 것을 KEY 열거형으로 체크
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

        //그래픽 출력
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

        // 맵에서 0 의 위치를 이동할때 사용하는 함수
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

        //키 입력함수 를 KEY 열거형으로 변환시켜주는 함수
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

            while (true) // 게임루프
            {
                Console.Clear();

                Render(map);
                KEY input = InputKey();

                //종료키 입력시 게임루프 탈출
                if (input == KEY.End)
                {
                    break;
                }

                //에러와 종료키 예외처리
                if (input != KEY.End && input != KEY.err)
                {
                    swap(ref map, ref zeroPos, input);
                }


            }
        }
    }
}