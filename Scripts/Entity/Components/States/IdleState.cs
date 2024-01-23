using Godot;

public partial class IdleState : State
{
    [Export] public NodePath MovementComponent;
    private MovementComponent _movementComponent;
    [Export] public string IdleAnimationName;   
    
    public override void _Ready()
    {
        base._Ready();
        _movementComponent = GetNode<MovementComponent>(MovementComponent);
    }

    public override void Enter()
    {
        base.Enter();
        _movementComponent.Sprite.Play(IdleAnimationName);
    }

    public override void Exit()
    {
        base.Exit();
        _movementComponent.Sprite.Stop();
    }

    public override void PhysicsUpdate(double delta)
    {
        base.PhysicsUpdate(delta);
        if (!_movementComponent.TryMovement)
        {
            _movementComponent.ApplyFriction((float) delta);
        }
        else
        {
            Transition("MoveState");
        }
    }
}