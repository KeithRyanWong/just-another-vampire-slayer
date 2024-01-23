using Godot;

namespace VampireSurvivor.Game.Entity.Components.States;

public partial class MovementState : State
{
    public Vector2 Axis;

    public override void _Ready()
    {
        base._Ready();
        Axis = Vector2.Zero;
    }
    
    public Vector2 GetInputAxis()
    {
        Axis.X = Input.IsActionPressed("move_right") ? 1 : 0;
        Axis.X -= Input.IsActionPressed("move_left") ? 1 : 0;
        Axis.Y = Input.IsActionPressed("move_down") ? 1 : 0;
        Axis.Y -= Input.IsActionPressed("move_up") ? 1 : 0;
        return Axis.Normalized();
    }
    
    public void ApplyFriction(float amount)
    {
        if (Fsm.CharacterBody.Velocity.Length() > amount)
        {
            Fsm.CharacterBody.Velocity -= Fsm.CharacterBody.Velocity.Normalized() * amount;
        }
        else
        {
            Fsm.CharacterBody.Velocity = Vector2.Zero;
        }
    }
    
    public void ApplyMovement(Vector2 acceleration)
    {
        Fsm.CharacterBody.Velocity += acceleration;
        Fsm.CharacterBody.Velocity = Fsm.CharacterBody.Velocity.LimitLength(Fsm.Stats.MaxSpeed);
    }
}