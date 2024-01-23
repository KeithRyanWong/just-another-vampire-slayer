using Godot;

namespace VampireSurvivor.Game.Entity.Components.States;

public partial class MoveState : MovementState
{
    [Export] public string RunAnimationName;
    
    public override void _Ready()
    {
        base._Ready();
    }

    public override void Enter()
    {
        base.Enter();
        Fsm.Sprite.Play(RunAnimationName);
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
        if (Axis != Vector2.Zero)
        {
            ApplyMovement(Axis * Fsm.Stats.Acceleration * (float)delta);
            if (Axis.X > 0)
                Fsm.Sprite.FlipH = false;
            else if(Axis.X < 0)
                Fsm.Sprite.FlipH = true;
        }
        else
        {
            Transition("IdleState");
        }

        Fsm.CharacterBody.MoveAndSlide();
    }
}