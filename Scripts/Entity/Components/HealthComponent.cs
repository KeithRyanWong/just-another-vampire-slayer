using Godot;
using System;

public partial class HealthComponent : Node
{
    [Export] public NodePath Character;
    private Entity _character;
    
    private BaseStats Stats => _character.Stats;

    public int MaxHealth { get; private set; }
    public int CurrentHealth { get; private set; }
    public bool CanTakeDamage { get; set; } = true;

    public readonly static string HealthGainedSignal = "HealthGainedSignal";
    public readonly static string HealthLostSignal = "HealthLostSignal";
    public readonly static string MaxHealthGainedSignal = "MaxHealthGainedSignal";
    public readonly static string MaxHealthLostSignal = "MaxHealthLostSignal";
    public readonly static string HealthReducedToZeroSignal = "HealthReducedToZeroSignal";
    public override void _Ready()
    {
        base._Ready();

        _character = GetNode<Entity>(Character);
        
        MaxHealth = Stats.StartingHealth;
        CurrentHealth = Stats.StartingHealth;
        
        AddUserSignal(HealthGainedSignal);
        AddUserSignal(HealthLostSignal);
        AddUserSignal(MaxHealthGainedSignal);
        AddUserSignal(MaxHealthLostSignal);
    }

    public void IncreaseMaxHealth(int amount)
    {
        MaxHealth += amount;
        EmitSignal(MaxHealthGainedSignal, amount);
    }
    public void DecreaseMaxHealth(int amount)
    {
        MaxHealth -= amount;
        EmitSignal(MaxHealthLostSignal, amount);
    }
    
    public void Heal(int amount)
    {
        var effectiveAmount = amount + CurrentHealth > MaxHealth ? MaxHealth - CurrentHealth : amount;
        CurrentHealth += effectiveAmount;
        EmitSignal(HealthGainedSignal, effectiveAmount);
    }

    public void DealDamage(int amount)
    {
        var effectiveAmount = CurrentHealth - amount < 0 ? CurrentHealth : amount;
        CurrentHealth -= effectiveAmount;
        EmitSignal(HealthLostSignal, effectiveAmount);
        if (CurrentHealth == 0) EmitSignal(HealthReducedToZeroSignal);
    }
}
