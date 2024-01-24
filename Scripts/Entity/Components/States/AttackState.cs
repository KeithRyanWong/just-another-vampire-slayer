using Godot;
using System;

public partial class AttackState : State
{
    [Export] public NodePath MovementComponent;
    private MovementComponent _movementComponent;
    [Export] public NodePath AttackComponent;
    private VampireSurvivor.Scripts.Entity.Components.Attacks.AttackComponent _attackComponent;
    [Export] public string AttackAnimationName;

    public override void _Ready()
    {
        base._Ready();
        _movementComponent = GetNode<MovementComponent>(MovementComponent);
        _attackComponent = GetNode<VampireSurvivor.Scripts.Entity.Components.Attacks.AttackComponent>(AttackComponent);
    }

    public override void Enter()
    {
        base.Enter();
        // _attackComponent.StartAttack();
        _movementComponent.Sprite.Play(AttackAnimationName);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void PhysicsUpdate(double delta)
    {
        base.PhysicsUpdate(delta);
        _movementComponent.ApplyFriction((float) delta);
        // if (AttackComplete)
        {
            Transition(_movementComponent.TryMovement ? "MoveState" : "IdleState");
        }
    }
    
}
