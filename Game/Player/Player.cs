using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] public float MaxSpeed = 300f;
	[Export] public float Acceleration = 1500f;
	[Export] public float Friction = 1200f;

	private AnimatedSprite2D Sprite;
	
	public Vector2 Axis;

	public override void _Ready()
	{
		base._Ready();
		Axis = Vector2.Zero;

		Sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		Move(delta);
	}

	public Vector2 GetInputAxis()
	{
		Axis.X = Input.IsActionPressed("move_right") ? 1 : 0;
		Axis.X -= Input.IsActionPressed("move_left") ? 1 : 0;
		Axis.Y = Input.IsActionPressed("move_down") ? 1 : 0;
		Axis.Y -= Input.IsActionPressed("move_up") ? 1 : 0;
		return Axis.Normalized();
	}

	public void Move(double delta)
	{
		Axis = GetInputAxis();
		if (Axis == Vector2.Zero)
		{
			ApplyFriction(Friction * (float)delta);
			Sprite.Play("idle");
		}
		else
		{
			ApplyMovement(Axis * Acceleration * (float)delta);
			Sprite.Play("run");
			if (Axis.X > 0)
			{
				Sprite.FlipH = false;
			}
			else if(Axis.X < 0)
			{
				Sprite.FlipH = true;
			}
		}

		MoveAndSlide();
	}

	public void ApplyFriction(float amount)
	{
		if (Velocity.Length() > amount)
		{
			Velocity -= Velocity.Normalized() * amount;
		}
		else
		{
			Velocity = Vector2.Zero;
		}
	}

	public void ApplyMovement(Vector2 acceleration)
	{
		Velocity += acceleration;
		Velocity = Velocity.LimitLength(MaxSpeed);
	}
}
