using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System.Security.Cryptography.X509Certificates;

public class Character
{
    public string Name { get; set; } // имя
    public int MaxHP { get; set; } // макс хп
    public int HP { get; set; } // текущее хп
    public int Damage { get; set; } // сила урона
    public int Heal { get; set; } // сила лечения
    public Character(string name, int maxhp, int hp, int damage, int heal)
    {
        Name = name;
        MaxHP = maxhp;
        HP = hp;
        Damage = damage;
        Heal = heal;
    }
    public void Attack(Character attacker, Character deffender)
    {
        Random random = new Random();
        int damage = random.Next(1, attacker.Damage);
        g_Damage(deffender, damage);
        Console.WriteLine($"{attacker.Name} атакует {deffender.Name} на {damage} урона");
    }
    public void g_Damage(Character character, int damage)
    {
        character.HP -= damage;
    }
    public void g_Heal(Character character)
    {
        Random random = new Random();
        int heal = random.Next(1, character.Heal);
        character.HP = Math.Min(character.HP + heal, character.MaxHP);
        Console.WriteLine($"{character.Name} лечится на {heal} хп\nТеперь он имеет {character.HP} здоровья");
    }
    public string Info()
    {
        return $"имя: {Name}\nмакс хп: {MaxHP}\nтекущее хп: {HP}\nсила урона: {Damage}\nсила лечения: {Heal}";
    }
}
public class Game
{
    public Character FirstPlayer { get; set; }
    public Character SecondPlayer { get; set; }
    public Character CurrentPlayer;
    public void CreatePlayers()
    {
        Random random = new Random();

        Console.Write("Введите имя первого персонажа: ");
        string first_name = Console.ReadLine();
        int first_maxhp = random.Next(10, 20);
        int first_hp = first_maxhp;
        int first_damage = random.Next(3, 5);
        int first_heal = random.Next(1, 3);
        FirstPlayer = new Character(first_name, first_maxhp, first_hp, first_damage, first_heal);

        Console.Write("Введите имя второго персонажа: ");
        string second_name = Console.ReadLine();
        int second_maxhp = random.Next(10, 20);
        int second_hp = first_maxhp;
        int second_damage = random.Next(3, 5);
        int second_heal = random.Next(1, 3);
        SecondPlayer = new Character(second_name, second_maxhp, second_hp, second_damage, second_heal);
    }
    public void Attack(Character attacker, Character deffender)
    {
        Random random = new Random();
        int damage = random.Next(1, attacker.Damage);
        g_Damage(deffender, damage);
        Console.WriteLine($"{attacker.Name} атакует {deffender.Name} на {damage} урона\nТекущее здоровье {deffender.Name} = {deffender.HP}");
    }
    public void g_Damage(Character character, int damage)
    {
        character.HP -= damage;
    }
    public void g_Heal(Character character)
    {
        Random random = new Random();
        int heal = random.Next(1, character.Heal);
        character.HP = Math.Min(character.HP + heal, character.MaxHP);
        Console.WriteLine($"{character.Name} лечится на {heal} хп\nТеперь он имеет {character.HP} здоровья");
    }
    private Character GetPlayer(Character player)
    {
        if (player == FirstPlayer)
            return SecondPlayer;
        else
            return FirstPlayer;
    }
    public void StartGame()
    {
        Console.WriteLine("Начало боя!");
        Console.Write("-------------------------------------------------\n");
        Console.WriteLine($"Первый игрок:\n{FirstPlayer.Info()}");
        Console.Write("-------------------------------------------------\n");
        Console.WriteLine($"Второй игрок:\n{SecondPlayer.Info()}");
        while (FirstPlayer.HP > 0 && SecondPlayer.HP > 0)
        {
            if (CurrentPlayer == FirstPlayer)
                Console.WriteLine($"Ходит игрок {FirstPlayer.Name}[{FirstPlayer.HP}] (противник-{SecondPlayer.Name}[{SecondPlayer.HP}])\n1. Атака\n2. Лечение");
            else if (CurrentPlayer == SecondPlayer)
                Console.WriteLine($"Ходит игрок {SecondPlayer.Name}[{SecondPlayer.HP}] (противник-{FirstPlayer.Name}[{FirstPlayer.HP}])\n1. Атака\n2. Лечение");
            int command = int.Parse(Console.ReadLine());

            if (CurrentPlayer == FirstPlayer)
            {
                
                switch (command)
                {
                    case 1:
                        Attack(CurrentPlayer, GetPlayer(CurrentPlayer));
                        break;
                    case 2:
                        g_Heal(CurrentPlayer);
                        break;
                }
                
            }
            else if (CurrentPlayer == SecondPlayer)
            {
                
                switch (command)
                {
                    case 1:
                        Attack(CurrentPlayer, FirstPlayer);
                        break;
                    case 2:
                        g_Heal(CurrentPlayer);
                        break;
                }
            }
            CurrentPlayer = GetPlayer(CurrentPlayer);
            

            if (FirstPlayer.HP == 0 || FirstPlayer.HP < 0)
            {
                Console.Write($"Первый игрок под ником {FirstPlayer.Name} проиграл\n{SecondPlayer.Name} победил!!!");
                break;
            }


            else if (SecondPlayer.HP == 0 || SecondPlayer.HP < 0)
            {
                Console.Write($"Второй игрок под ником {SecondPlayer.Name} проиграл\n{FirstPlayer.Name} победил!!!");
                break;
            }
        }
    }
}
class Program
{
    static void Main(string[] args)
    {
        Game game = new Game();
        game.CreatePlayers();
        game.StartGame();
       
    }

}
