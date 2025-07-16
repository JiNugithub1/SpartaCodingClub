using Project_TextRPG;
using System.Numerics;

class Program
{
    static void Main(string[] args)
    {
        GameManager game = new GameManager();
        game.Start();
    }
}

class GameManager
{
    Player player;
    Inventory inventory;
    Shop shop;
    Rest rest;
    Battle battle;

    public void Start()
    {
        InitializePlayer();
        inventory = new Inventory();
        player.inventory = inventory; // Inventory 아이템에 능력치 값을 연결 
        shop = new Shop(inventory, player);
        rest = new Rest(player); // Rest 클래스에 Player 연결하여 HP와 연동한다. 이유는 휴식을 취함으로써 HP가 100이 되도록 하기 위함
        battle = new Battle(player, inventory);
        ShowIntro();
        MainLoop();
        
    }

    void InitializePlayer()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("당신의 이름을 설정해주세요.\n");
            string name = Console.ReadLine();

            Console.WriteLine($"당신의 이름은 {name} 입니다.\n");
            Console.WriteLine("1. 저장");
            Console.WriteLine("2. 취소\n");
            Console.Write("원하시는 행동을 입력해주세요.\n>> ");
            string input = Console.ReadLine();

            if (input == "1")
            {
                string job = ChooseJob();
                player = CreatePlayerByJob(name, job);
                Console.Clear();
                break;
            }
            else if (input == "2")
            {
                Console.Clear();
                continue;
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.\n");
            }
        }
    }

    string ChooseJob()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("당신의 직업을 선택해주세요.\n");
            Console.WriteLine("1. 전사");
            Console.WriteLine("2. 도적\n");
            Console.Write("원하시는 행동을 입력해주세요.\n>> ");
            string input = Console.ReadLine();

            if (input == "1") return "전사";
            else if (input == "2") return "도적";
            else
            {
                Console.Clear();
                Console.WriteLine("잘못된 입력입니다.\n");
            }
        }
    }

    Player CreatePlayerByJob(string name, string job)
    {
        if (job == "전사") return new Player(name, job, 1, 10, 5, 70, 1500);
        else if (job == "도적") return new Player(name, job, 1, 12, 3, 80, 1700);
        else return new Player(name, job, 1, 10, 5, 100, 1500);
    }

    void ShowIntro()
    {
        Console.Clear();
        Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
        Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n");
    }

    void MainLoop()
    {
        while (true)
        {
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("4. 휴식하기");
            Console.WriteLine("5. 전투하기");
            Console.Write("\n원하시는 행동을 입력해주세요.\n>> ");
            string input = Console.ReadLine();
            Console.Clear();

            switch (input)
            {
                case "1": player.ShowStatus(); break;
                case "2": inventory.Manage(); break;
                case "3": shop.Open(); break;
                case "4": rest.Resting(); break;
                case "5": battle.MainBattle(); break;
                default:
                    Console.WriteLine("잘못된 입력입니다.\n");
                    break;
            }
        }
    }
}

class Player
{
    public Inventory inventory;

    public string Name;
    public string Job;
    public int Level;
    public int Attack;
    public int Defense;
    public int HP;
    public int Gold;

    public Player(string name, string job, int level, int attack, int defense, int hp, int gold)
    {
        Name = name;
        Job = job;
        Level = level;
        Attack = attack;
        Defense = defense;
        HP = hp;
        Gold = gold;
    }

    public void ShowStatus()
    {
        while (true)
        {
            int bonusAtk = 0;
            int bonusDef = 0;
            if (inventory != null)
            {
                foreach (var item in inventory.items)
                {
                    if (item.Equipped)
                    {
                        bonusAtk += item.Attack;
                        bonusDef += item.Defense;
                    }
                }
            }

            Console.Clear();
            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
            Console.WriteLine($"Lv. {Level}");
            Console.WriteLine($"{Name} ({Job})");
            Console.WriteLine($"공격력 : {Attack + bonusAtk} {(bonusAtk > 0 ? $"(+{bonusAtk})" : "")}");
            Console.WriteLine($"방어력 : {Defense + bonusDef} {(bonusDef > 0 ? $"(+{bonusDef})" : "")}");
            Console.WriteLine($"체 력 : {HP}");
            Console.WriteLine($"Gold : {Gold} G\n");
            Console.WriteLine("0. 나가기\n");
            Console.Write("원하시는 행동을 입력해주세요.\n>> ");
            string input = Console.ReadLine();
            if (input == "0") break;
        }
        Console.Clear();
    }
}

class Inventory
{
    public List<Item> items = new List<Item>();
    public void Manage()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

            if (items.Count == 0)
            {
                Console.WriteLine("아이템이 없습니다.\n");
            }
            else
            {
                Console.WriteLine("[아이템 목록]");
                int index = 1;
                foreach (var item in items)
                {
                    Console.Write("- {0} ", index++);
                    if (item.Equipped) Console.Write("[E]");
                    Console.WriteLine($"{item.Name} | {item.GetStatText()} | {item.Description}");
                }
            }

            Console.WriteLine("\n1. 장착 관리");
            Console.WriteLine("0. 나가기\n");
            Console.Write("원하시는 행동을 입력해주세요.\n>> ");
            string input = Console.ReadLine();

            if (input == "0") break;
            else if (input == "1") ManageEquip();
        }
        Console.Clear();
    }

    void ManageEquip()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("인벤토리 - 장착 관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
            if (items.Count == 0)
            {
                Console.WriteLine("아이템이 없습니다.\n");
                Console.WriteLine("0. 나가기\n>> ");
                Console.ReadLine();
                return;
            }
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < items.Count; i++)
            {
                Console.Write("- {0} ", i + 1);
                if (items[i].Equipped) Console.Write("[E]");
                Console.WriteLine($"{items[i].Name} | {items[i].GetStatText()} | {items[i].Description}");
            }
            Console.WriteLine("0. 나가기\n>> ");
            Console.Write("원하시는 행동을 입력해주세요.\n>> ");
            string input = Console.ReadLine();

            if (input == "0") break;
            if (int.TryParse(input, out int idx) && idx >= 1 && idx <= items.Count)
            {
                items[idx - 1].Equipped = !items[idx - 1].Equipped;
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. 엔터를 누르세요.");
                Console.ReadLine();
            }
        }
    }
}

class Shop
{
    Inventory inventory;
    Player player;
    List<Item> shopItems = new List<Item>();

    public Shop(Inventory inv, Player p)
    {
        inventory = inv;
        player = p;

        shopItems.Add(new Item("수련자 갑옷", 0, 5, "수련에 도움을 주는 갑옷입니다.", 1000));
        shopItems.Add(new Item("무쇠갑옷", 0, 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 1200));
        shopItems.Add(new Item("스파르타의 갑옷", 0, 15, "전설의 갑옷입니다.", 3500));
        shopItems.Add(new Item("개발자 셔츠", 0, 9999, "집에 가지 못한 개발자의 땀내나는 셔츠입니다.", 99999));
        shopItems.Add(new Item("낡은 검", 2, 0, "쉽게 볼 수 있는 낡은 검입니다.", 600));
        shopItems.Add(new Item("청동 도끼", 5, 0, "어디선가 사용됐던 도끼입니다.", 1500));
        shopItems.Add(new Item("스파르타의 창", 7, 0, "전설의 창입니다.", 3000));
        shopItems.Add(new Item("개발자의 무기", 9999, 0, "개발자만이 사용할 수 있는 금지된 무기입니다.", 99999));
    }

    public void Open()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
            Console.WriteLine($"[보유 골드] {player.Gold} G\n");

            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < shopItems.Count; i++)
            {
                var item = shopItems[i];
                Console.WriteLine($"- {i + 1} {item.Name} | {item.GetStatText()} | {item.Description} | {(item.Purchased ? "구매완료" : item.Price + " G")}");
            }

            Console.WriteLine("\n1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("0. 나가기\n");
            Console.Write("원하시는 행동을 입력해주세요.\n>> ");
            string input = Console.ReadLine();

            if (input == "0") break;
            else if (input == "1") BuyItem();
            else if (input == "2") SellItem();
        }
        Console.Clear();
    }

    void BuyItem()
    {
        Console.Clear();
        Console.WriteLine("상점 - 아이템 구매");
        Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
        Console.WriteLine($"[보유 골드] {player.Gold} G\n");

        Console.WriteLine("[아이템 목록]");
        for (int i = 0; i < shopItems.Count; i++)
        {
            var item = shopItems[i];
            Console.WriteLine($"- {i + 1} {item.Name} | {item.GetStatText()} | {item.Description} | {(item.Purchased ? "구매완료" : item.Price + " G")}");
        }
        Console.WriteLine("0. 나가기\n>> ");
        Console.Write("구매할 아이템 번호를 입력하세요: ");
        string input = Console.ReadLine();

        if (input == "0") return;
        if (int.TryParse(input, out int idx) && idx >= 1 && idx <= shopItems.Count)
        {
            var item = shopItems[idx - 1];
            if (item.Purchased)
            {
                Console.WriteLine("이미 구매한 아이템입니다.");
            }
            else if (player.Gold >= item.Price)
            {
                player.Gold -= item.Price;
                item.Purchased = true;
                inventory.items.Add(new Item(item.Name, item.Attack, item.Defense, item.Description, item.Price, true));
                Console.WriteLine("구매를 완료했습니다.");
            }
            else
            {
                Console.WriteLine("Gold 가 부족합니다.");
            }
        }
        else
        {
            Console.WriteLine("잘못된 입력입니다.");
        }
        Console.WriteLine("엔터를 누르면 돌아갑니다.");
        Console.ReadLine();
    }
    void SellItem()
    {
        Console.Clear();
        Console.WriteLine("상점 - 아이템 판매");
        Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
        Console.WriteLine($"[보유 골드] {player.Gold} G\n");

        if (inventory.items.Count == 0)
        {
            Console.WriteLine("판매할 아이템이 없습니다.\n");
            Console.WriteLine("0. 나가기\n>> ");
            Console.ReadLine();
            return;
        }

        Console.WriteLine("[아이템 목록]");
        for (int i = 0; i < inventory.items.Count; i++)
        {
            Console.Write("- {0}. ", i + 1);
            if (inventory.items[i].Equipped) Console.Write("[E]");
            Console.WriteLine($"{inventory.items[i].Name} | {inventory.items[i].GetStatText()} | {inventory.items[i].Description}");
        }

        Console.WriteLine("\n0. 나가기");
        Console.Write("\n판매할 아이템 번호를 선택해주세요\n>> ");
        string input = Console.ReadLine();

        if (input == "0") return;

        if (int.TryParse(input, out int choice))
        {
            if (choice >= 1 && choice <= inventory.items.Count)
            {
                Project_TextRPG.Item selectedItem = inventory.items[choice - 1];

                if (selectedItem.Equipped)
                {
                    Console.WriteLine("\n장착 중인 아이템은 판매할 수 없습니다.");
                    Console.WriteLine("계속하려면 Enter를 누르세요...");
                    Console.ReadLine();
                    return;
                }

                int sellPrice = (int)(selectedItem.Price * 0.85); // 원가의 85%로 되팔 수 있다.
                player.Gold += sellPrice;
                inventory.items.RemoveAt(choice - 1);

                Console.WriteLine($"\n{selectedItem.Name}을(를) {sellPrice} G에 판매했습니다.");
                Console.WriteLine("계속하려면 Enter를 누르세요...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("잘못된 번호입니다.");
                Console.WriteLine("계속하려면 Enter를 누르세요...");
                Console.ReadLine();
            }
        }
        else
        {
            Console.WriteLine("숫자를 입력해주세요.");
            Console.WriteLine("계속하려면 Enter를 누르세요...");
            Console.ReadLine();
        }
    }
}




