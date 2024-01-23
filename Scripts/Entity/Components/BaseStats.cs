using Godot;

[GlobalClass]
public partial class BaseStats : Resource
{
    [Export] public float MaxSpeed = 300f;
    [Export] public float Acceleration = 1500f;
    [Export] public float Friction = 1200f;

}