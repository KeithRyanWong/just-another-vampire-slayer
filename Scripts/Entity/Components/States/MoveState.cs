using Godot;

public partial class MoveState : State
{
    [Export] public NodePath MovementComponent;
    private MovementComponent _movementComponent;
    [Export] public string MoveAnimationName;
    
    public override void _Ready()
    {
        base._Ready();
        _movementComponent = GetNode<MovementComponent>(MovementComponent);
    }

    public override void Enter()
    {
        base.Enter();
        _movementComponent.Sprite.Play(MoveAnimationName);
    }

    public override void Exit()
    {
        base.Exit();
        _movementComponent.Sprite.Stop();
    }

    public override void PhysicsUpdate(double delta)
    {
        base.PhysicsUpdate(delta);

        if (_movementComponent.TryMovement)
        {
            _movementComponent.ApplyMovement((float)delta);
            _movementComponent.Sprite.FlipH = _movementComponent.GetInputAxis().X switch
            {
                > 0 => false,
                < 0 => true,
                _ => _movementComponent.Sprite.FlipH
            };
        }
        else
        {
            Transition("IdleState");
        }
        
    }
}