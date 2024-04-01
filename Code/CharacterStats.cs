using Godot;
using System;

[GlobalClass]
public partial class CharacterStats : Resource
{
    [Export]
    public float Agility {get; set;} = 1f;
    [Export]
    public float Firearm {get; set;}
    [Export]
    public float Strength {get; set;}
    [Export]
    public float Explosives {get; set;}
    [Export]
    public float Driving {get; set;}
    [Export]
    public int MaxHealth {get; set;}    
    [Export]
    public int StartingHealth {get; set;}
    
    public CharacterStats()  
    {
        Agility = 1f;
        Firearm = 1f;
        Strength  = 1f;
        Explosives = 1f;
        Driving = 1f;
        MaxHealth = 100;
        StartingHealth = 100;
    }
    
    }