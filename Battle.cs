using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_TextRPG
{

    class Battle
    {
        List<Monster> currentMonsters = new List<Monster>(); // 전역으로 관리
        bool monstersInitialized = false; // 이미 몬스터를 생성했는지 확인
        Player player;
        Inventory inventory;
        public Battle(Player player, Inventory inventory)
        {
            this.player = player;
            this.inventory = inventory;
        }
        public void MainBattle()
        {
            while (true)
            {
                MonsterShowStatus();
                Console.WriteLine("\n\n[내정보]");
                Console.WriteLine($"Lv {player.Level} {player.Name} ({player.Job})\nHP {player.HP}\n");
                Console.WriteLine("1. 공격하기");
                Console.Write("\n\n원하시는 행동을 입력해주세요.\n>> ");
                string input = Console.ReadLine();
                if(input == "1") MyTurn(); 
                else
                { Console.WriteLine("잘못된 입력입니다.\n"); continue; }

            }
        }
        public void MyTurn()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\nBattle!!\n");
            Console.ResetColor();
            

            for (int i = 0; i < currentMonsters.Count; i++)
            {
                var mon = currentMonsters[i];
                Console.WriteLine($"{i + 1}. Lv.{mon.Lv} {mon.Name} HP: {mon.Hp}");
            }

            Console.WriteLine("\n\n[내정보]");
            Console.WriteLine($"Lv {player.Level} {player.Name} ({player.Job})\nHP {player.HP}\n");

            Console.Write("\n공격할 대상의 번호를 입력하세요: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int selected) &&
                selected >= 1 && selected <= currentMonsters.Count)
            {
                Monster target = currentMonsters[selected - 1];
                Console.WriteLine($"\n{target.Name}을(를) 공격합니다!");
                
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }

            Console.WriteLine("\n계속하려면 아무 키나 누르세요...");
            Console.ReadKey();
            Console.Clear();

        }
        public void MonsterShowStatus()
        {
            if (!monstersInitialized)
            {
                List<Monster> mons = new List<Monster>
                {
                    new Monster(1, "스파르타", 15),
                    new Monster(11, "해골메이지", 14),
                    new Monster(7, "오크", 45),
                    new Monster(4, "해골기사", 25),
                    new Monster(99, "드래곤", 100),
                    new Monster(14, "슬라임", 4)
                };

                Random rnd = new Random();
                List<Monster> shuffled = mons.OrderBy(m => rnd.Next()).ToList();
                int count = rnd.Next(1, 5); // 1~4

                currentMonsters = shuffled.Take(count).ToList();
                monstersInitialized = true; // 한 번만 섞고 저장
            }

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\nBattle!!\n");
            Console.ResetColor();
            Console.WriteLine($"몬스터 {currentMonsters.Count}마리 출현!\n");

            for (int i = 0; i < currentMonsters.Count; i++)
            {
                var mon = currentMonsters[i];
                Console.WriteLine($" Lv.{mon.Lv} {mon.Name} HP: {mon.Hp}");
            }
        }

        class Monster
        {
            public int Lv;
            public string Name;
            public int Hp;

            public Monster(int lv, string name, int hp)
            {
                Lv = lv;
                Name = name;
                Hp = hp;
            }
        }
    }
}
