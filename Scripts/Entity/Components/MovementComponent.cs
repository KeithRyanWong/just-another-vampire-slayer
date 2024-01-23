using Godot;
using VampireSurvivor.Game.Entity;

public partial class MovementComponent : Node
{
    [ExportGroup("CharacterNodes")]
    [Export] public NodePath CharacterBody;
    [Export] public NodePath AnimatedSprite;
    
    // [Export] public NodePath AnimatedSprite;
    private Entity _character;
    private Entity _target;
    private BaseStats _stats;
    public AnimatedSprite2D Sprite;

    public bool TryMovement => GetInputAxis() != Vector2.Zero;

    public Vector2 Axis;
    
    public override void _Ready()
    {
        base._Ready();

        _character = GetNode<Entity>(CharacterBody);
        _stats = (BaseStats)GetNodeAndResource(CharacterBody + ":Stats")[1];
        if (_character.IsNPC) _target = GetNode<Entity>(_character.Target);
        Sprite = GetNode<AnimatedSprite2D>(AnimatedSprite);
        
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
    
    public void ApplyFriction(float delta)
    {
        float amount = _stats.Friction * delta;
        if (_character.Velocity.Length() > amount)
        {
            _character.Velocity -= _character.Velocity.Normalized() * amount;
        }
        else
        {
            _character.Velocity = Vector2.Zero;
        }

        _character.MoveAndSlide();
    }
    
    public void ApplyMovement(float delta)
    {
        Vector2 effectiveAcceleration = GetInputAxis() * _stats.Acceleration * delta;
        _character.Velocity += effectiveAcceleration;
        _character.Velocity = _character.Velocity.LimitLength(_stats.MaxSpeed);
        _character.MoveAndSlide();
    }
}