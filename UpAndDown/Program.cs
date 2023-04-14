using System;

namespace UpAndDown
{
    internal class Program
    {

        public enum STATE { small, big, collect, err}

        public static void init(out int target, out int chance)
        {
            Random random = new Random();
            target = random.Next(0, 1000);

            chance = 10;
        }
        public static STATE CheckNum(int target,int num)
        {
            if (target == num)
            {
                return STATE.collect;
            }
            else if (target > num)
            {
                return STATE.big;
            }
            else if (target < num)
            {
                return STATE.small;
            }
        
            return STATE.err;
        }
        static void Main(string[] args)
        {
            int target;
            int chance;
            int guess;

            init(out target, out chance);

            while (true)// 게임루프
            {
                Console.WriteLine("컴퓨터의 숫자를 맞춰보세요:");


                if (int.TryParse(Console.ReadLine(), out guess))
                {
                    Console.WriteLine("입력한 수는 {0}입니다.", guess);
                }
                else
                {
                    Console.WriteLine("잘못된 수를 입력하였습니다..");
                    Console.WriteLine("다시 수를 입력해주세요(0~999)");
                    continue;
                }

                if (guess > 999 || 0 > guess)
                {
                    Console.WriteLine("수의 범위를 벗어났습니다.\n 다시 수를 입력해주세요(0~999)");
                    continue;
                }


                STATE result = CheckNum(target, guess);

                switch (result)
                {
                    case STATE.small:
                        --chance;
                        Console.WriteLine("수가 더 작습니다.");
                        break;
                    case STATE.big:
                        --chance;
                        Console.WriteLine("수가 더 큽니다.");
                        break;
                    case STATE.collect:
                        Console.WriteLine("정답입니다.");
                        break;
                    case STATE.err:
                        Console.WriteLine("잘못입력하셨습니다.");
                        break;
                    default:
                        break;
                }

                if (chance == 0)
                {
                    Console.WriteLine("기회를 모두 소진하여 게임이 끝났습니다.");
                    Console.WriteLine("정답은 {0}였습니다",target);

                    Console.WriteLine("다시 시작하려면 R키를 누르세요 다른키를 입력하면 종료합니다.");
                    
                    ConsoleKeyInfo inputkey= Console.ReadKey();
                    
                    if (ConsoleKey.R == inputkey.Key)
                    {
                        init(out target, out chance);
                        continue;
                    }

                    break;
                }

                Console.WriteLine("{0} 번의 기회가 남았습니다\n",chance);

            }
        }
    }
}