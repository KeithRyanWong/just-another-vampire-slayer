using Godot;
using System;

public partial class HealthBarComponent : TextureProgressBar
{
    [Export] public NodePath HealthComponentNode;
    [Export] public bool ShowHealthBar = true;
    
    private HealthComponent _healthComponent;
    
    public override void _Ready()
    {
        base._Ready();
        _healthComponent = GetNode<HealthComponent>(HealthComponentNode);

        _healthComponent.Connect(HealthComponent.MaxHealthGainedSignal, new Callable(this, nameof(SetMaxHealth)));
        _healthComponent.Connect(HealthComponent.MaxHealthLostSignal, new Callable(this, nameof(SetMaxHealth)));
        _healthComponent.Connect(HealthComponent.HealthGainedSignal, new Callable(this, nameof(SetHealth)));
        _healthComponent.Connect(HealthComponent.HealthLostSignal, new Callable(this, nameof(SetHealth)));

        SetupHealthBar();
    }

    private void SetupHealthBar()
    {
        MinValue = 0;
        FillMode = (int)TextureProgressBar.FillModeEnum.LeftToRight;
        SetMaxHealth();
        SetHealth();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        if (ShowHealthBar)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    public void SetHealth()
    {
        GD.Print("setting Health: ", _healthComponent.CurrentHealth);
        SetValueNoSignal(_healthComponent.CurrentHealth);
    }

    public void SetMaxHealth()
    {
        MaxValue = _healthComponent.MaxHealth;
    }
}
