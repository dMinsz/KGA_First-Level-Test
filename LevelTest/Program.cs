namespace LevelTest
{
    internal class Program
    {
        //문장을 입력 받은 후 단어를 입력받아 문장중 단어가 시작하는 위치를 출력하는함수
        public static int Solution1(string str,string target) 
        {
            return str.IndexOf(target);
        }

        //문자열을 입력받으면 단어의 갯수를 출력하기
        public static int Solution2(string str)
        {
            string[] result;
            result = str.Split(" ");
            return result.Length;
        }

        //매개변수로 주어진 수가 소수인지 판별하는 코드
        public static bool IsPrime(int n)
        {
            for (int i = 2; i*i <= n; i++)
            {
                if (n % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        //양의 정수 를 매개변수로 받아 각 자리 수의 합을 구하는 코드
        public static int SumOfDigits(int num) 
        {
            int origin = num; // 원본값
            int count = 0;//자리수구하기
            int result = 0; // 결과값
            while (num != 0)
            {
                num /= 10;
                ++count;
            }

            for (int i = 0; i < count; i++) 
            {
                result += origin % 10;
                origin /= 10;
            }

            return result;
        }

        static int[] FindCommonItems(int[] arr1, int[] arr2, int[] arr3)
        {
            List<int> list = new List<int>();

            list.AddRange(arr1);
            list.AddRange(arr2);
            list.AddRange(arr3);

            list =list.GroupBy(x => x).Where(g => g.Count() > 1).Select(y => y.Key).ToList();

            return list.ToArray();
        }
        static void Main(string[] args)
        {
            //string str = Console.ReadLine();
            //string target = Console.ReadLine();

            //테스트코드

            //Console.WriteLine(Solution1("pineapple apple","apple"));
            //Console.WriteLine(Solution2("한단어 두단어 세단어"));

            //소수구하기
            Console.WriteLine(IsPrime(11));


            //Console.WriteLine(SumOfDigits(123));

            //FindCommonItems 테스트
            //int[] arr1 = { 1, 5, 5, 10 };
            //int[] arr2 = { 3, 4, 5, 5, 10 };
            //int[] arr3 = { 5, 5, 10, 20 };
            //var result = FindCommonItems(arr1, arr2, arr3);


            //for (int i = 0; i < result.Length; i++)
            //{
            //    if (i == result.Length - 1)
            //    {
            //        Console.Write("{0}", result[i]);
            //        break;
            //    }
            //    Console.Write("{0},", result[i]);
            //}

            //랜덤테스트
            //Random random = new Random();
            //int target = random.Next(0, 1000);

            //int chance = 10;




        }
    }
}