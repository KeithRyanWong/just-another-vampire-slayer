using Godot;
using System;


// Doesn't seem like attackstate should handle animation.
// No, it should, but only for the character, which could be different from the actual attack going out
// I guess I should make a characterAnimationComponent so that that responsibility can be removed from the StateMachine
// But it does still make sense to have a different handler for character animation and attack animations
// Even with the characters body/physical attacks, there will be a separate attack component that will just be the collision box
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
