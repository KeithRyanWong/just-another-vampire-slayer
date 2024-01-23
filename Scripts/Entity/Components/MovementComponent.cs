using Godot;

public partial class MovementComponent : Node
{
    [ExportGroup("CharacterNodes")]
    [Export] public NodePath CharacterBody;
    [Export] public NodePath AnimatedSprite;
    
    // [Export] public NodePath AnimatedSprite;
    private Entity _character;
    private BaseStats _stats;
    public AnimatedSprite2D Sprite;

    public bool TryMovement => GetDirectionalAxis() != Vector2.Zero;

    public Vector2 Axis;
    
    public override void _Ready()
    {
        base._Ready();

        _character = GetNode<Entity>(CharacterBody);
        _stats = (BaseStats)GetNodeAndResource(CharacterBody + ":Stats")[1];
        Sprite = GetNode<AnimatedSprite2D>(AnimatedSprite);
        
        Axis = Vector2.Zero;
    }
    
    public Vector2 GetDirectionalAxis()
    {
        if (_character.IsNPC)
        {
            //Can add a range check in here in the future for range based following
            
            Vector2 position = _character.Position;
            Vector2 targetPosition = _character.Target.Position;
        
            Axis.X = position.X < targetPosition.X ? 1 : 0;
            Axis.X -= position.X > targetPosition.X ? 1 : 0;
            Axis.Y = position.Y < targetPosition.Y ? 1 : 0;
            Axis.Y -= position.Y > targetPosition.Y ? 1 : 0;
        }
        else
        {
            Axis.X = Input.IsActionPressed("move_right") ? 1 : 0;
            Axis.X -= Input.IsActionPressed("move_left") ? 1 : 0;
            Axis.Y = Input.IsActionPressed("move_down") ? 1 : 0;
            Axis.Y -= Input.IsActionPressed("move_up") ? 1 : 0;
        }
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
        Vector2 effectiveAcceleration = GetDirectionalAxis() * _stats.Acceleration * delta;
        _character.Velocity += effectiveAcceleration;
        _character.Velocity = _character.Velocity.LimitLength(_stats.MaxSpeed);
        _character.MoveAndSlide();
    }
}