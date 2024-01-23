using Godot;

namespace VampireSurvivor.Game.Entity.Components.States;

public partial class IdleState : MovementState
{
    [Export] public string IdleAnimationName;
    
    public override void _Ready()
    {
        base._Ready();
    }

    public override void Enter()
    {
        base.Enter();
        Fsm.Sprite.Play(IdleAnimationName);
    }

    public override void Exit()
    {
        base.Exit();
        Fsm.Sprite.Stop();
    }

    public override void PhysicsUpdate(double delta)
    {
        base.PhysicsUpdate(delta);

        Axis = GetInputAxis();
        if (Axis == Vector2.Zero)
        {
            ApplyFriction(Fsm.Stats.Friction * (float) delta);
        }
        else
        {
            Transition("MoveState");
        }

        Fsm.CharacterBody.MoveAndSlide();
    }
}