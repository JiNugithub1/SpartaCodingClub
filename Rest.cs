using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_TextRPG
{
    class Rest
    {
        Player player;
        Item item;
        public Rest(Player player)
        {
            this.player = player;
        }
        public void Resting()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("휴식하기");
                Console.Write("500 G 를 내면 체력을 회복할 수 있습니다.");
                Console.WriteLine($"[보유 골드] {player.Gold} G\n");
                Console.WriteLine($"{player.Name}의 체력 : {player.HP}");

                Console.WriteLine("\n1. 휴식하기");
                Console.WriteLine("0. 나가기\n");
                Console.Write("원하시는 행동을 입력해주세요.\n>> ");
                string input = Console.ReadLine();

                if (input == "0") break;
                else if (input == "1") RestHP();
            }
            Console.Clear();
        }
        public void RestHP()
        {
            if (player.Gold >= 500)
            {
                if (player.HP == 100)
                {
                    Console.WriteLine("이미 체력이 충분합니다!");
                }
                else
                {
                    player.Gold -= 500;
                    player.HP = 100;
                    Console.WriteLine("휴식을 완료했습니다. 체력이 회복되었습니다!");
                }
            }
            else
            {
                Console.WriteLine("Gold 가 부족합니다.");
            }
            Console.WriteLine("엔터를 누르면 계속합니다...");
            Console.ReadLine();
        }

    }
}
