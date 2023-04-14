using System;
/*
 * Up & Down 게임 만들기
 * 컴퓨터는 0~999 중에 랜덤한 숫자를 뽑는다.
 * 유저는 10번의 기회가 있다.
 * 플레이어가 수를 입력 하면 컴퓨터는 그 수가 큰지, 작은지, 정답인지 알려준다.
 * 10번의 기회 소진시 게임을 종료할껀지 재시작 할껀지 선택 할수 있다.
 */
namespace UpAndDown
{
    internal class Program
    {

        // 입력한 수의 상태
        public enum STATE { small, big, collect, err}

        //기본세팅
        public static void init(out int target, out int chance)
        {
            Random random = new Random();
            target = random.Next(0, 1000);

            chance = 10;
        }
        // target 과 입력된 num 을 비교하여 상태 세팅
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

                //입력 예외처리
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

                //입력 예외처리 수의 범위내에 입력만 받는다.
                if (guess > 999 || 0 > guess)
                {
                    Console.WriteLine("수의 범위를 벗어났습니다.\n 다시 수를 입력해주세요(0~999)");
                    continue;
                }


                STATE result = CheckNum(target, guess);

                //입력된 수를 체크하고 비교해서 결과를 만든다.
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

                //게임 기회 모두 소비시
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