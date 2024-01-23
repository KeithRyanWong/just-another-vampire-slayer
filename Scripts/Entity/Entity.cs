using Godot;
using System;
using VampireSurvivor.Game.Entity;

public partial class Entity : CharacterBody2D
{
	[Export] public BaseStats Stats;
	[Export] private Node HealthComponent;
	
	private AnimatedSprite2D Sprite;
	
	public Vector2 Axis;

	public override void _Ready()
	{
		base._Ready();
		Axis = Vector2.Zero;

		Sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
	}
}
