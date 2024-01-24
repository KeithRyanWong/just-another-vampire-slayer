using Godot;

namespace VampireSurvivor.Scripts.Entity.Components.Attacks;

// Attack Controller is responsible for starting and tracking all live attacks belonging to Character.
//
public partial class AttackController : Node
{
    [Export] public NodePath Character;
    private global::Entity _character;
    private System.Collections.Generic.Dictionary<string, AttackComponent> _attacks;
    private BaseStats _stats => _character.Stats;
    
    public override void _Ready()
    {
        base._Ready();

        _character = GetNode<global::Entity>(Character);
        foreach (var child in GetChildren())
        {
            if (child is not AttackComponent attack) continue;
            _attacks[attack.Name.ToString().ToLower()] = attack;
        }
    }

    public void HandleAttackRequest(string name)
    {
        
    }
}