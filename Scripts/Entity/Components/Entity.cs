using Godot;


public partial class Entity : CharacterBody2D
{
	[ExportGroup("NPC Setup")]
	[Export] public bool IsNPC;
	[Export] public NodePath TargetNode;
	
	[ExportGroup("Stats")]
	[Export] public BaseStats Stats;
	[Export] private Node HealthComponent;
	
	public AnimatedSprite2D Sprite;
	public CharacterBody2D Target;
	
	public Vector2 Axis;

	public override void _Ready()
	{
		base._Ready();
		Axis = Vector2.Zero;

		Sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		if (IsNPC) Target = GetNode<CharacterBody2D>(TargetNode);
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