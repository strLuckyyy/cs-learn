using System;
using System.Collections.Generic;

public interface IDamageable
{
    void TakeDamage(float damage);
    void RecoverHealth(float amount);
}

public class Character : IDamageable
{
    public Character(string name, float force, float speed, float health, float defense)
    {
        Name    = name;
        _force   = force;
        _speed   = speed;
        _health  = health;
        _defense = defense;
    }

    private float _maxHealth;
    private float _health;
    private float _force;
    private float _defense;
    private float _speed;

    public string Name { get; set; }
    public float  MaxHealth 
    { 
        get; set => _maxHealth += Math.Max(_maxHealth + value, 0);
    }
    public float  Health
    {
        get; set => _health = Math.Clamp(_health + value, 0, _maxHealth);
    }
    public float  Force
    {
        get; set => _force = Math.Min(_force + value, 0);
    }
    public float  Defense
    {
        get; set => _defense = Math.Min(_defense + value, 0);
    }
    public float  Speed
    {
        get; set => _speed = Math.Min(_speed + value, 0);
    }
    
    public void PrintStatus()
    {
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Health: {Health}/{MaxHealth}");
        Console.WriteLine($"Force: {Force}");
        Console.WriteLine($"Defense: {Defense}");
        Console.WriteLine($"Speed: {Speed}");
    }

    public void TakeDamage(float damage)
    {
        float effectiveDamage = Math.Max(damage - Defense / 100, 0);
        Health -= effectiveDamage;
        Console.WriteLine($"{Name} took {effectiveDamage} damage!");
    }

    public void RecoverHealth(float amount)
    {
        Health += amount;
        Console.WriteLine($"{Name} recovered {amount} health!");
    }
}


public class MiniGame
{
    private List<Character> _characters = new List<Character>();
    public List<Character> Characters => _characters;

    public static bool ConfigGame()
    {
        Console.WriteLine("Configure your character:");
        Console.Write("Enter name: ");
        string name = Console.ReadLine();

        Console.Write("Enter force (0-100): ");
        float force = float.Parse(Console.ReadLine());

        Console.Write("Enter speed (0-100): ");
        float speed = float.Parse(Console.ReadLine());

        Console.Write("Enter health (0-100): ");
        float health = float.Parse(Console.ReadLine());

        Console.Write("Enter defense (0-100): ");
        float defense = float.Parse(Console.ReadLine());

        Character character = new Character(name, force, speed, health, defense);
        character.PrintStatus();

        _characters.Add(character);

        Console.Write("Do you want to create more one? (y/n): ");
        string confirmation = Console.ReadLine();
        if(confirmation.ToLower() == "y")
        {
            return ConfigGame();
        }

        Console.WriteLine("Game configuration completed!");
        return true;
    }


    public static void Main(string[] args)
    {
        bool isConfigured = ConfigGame();

        while (isConfigured)
        {
            Console.WriteLine("Game is running...");
            break;
        }
    }
}