using Godot;

namespace VampireSurvivor.Scripts.Entity.Components.Attacks;

// Base attack component to be extended into actual attacks.
// I can thnk of a couple different ways to do this, and I'm sure there's more
// Right now the attack controller just has a dictionary populated by it's children in the tree.
// I think what we do is hide the attack components (they'll be dummies)
// Then when we instantiate the attack we'll duplicate the node. Which means that each attack component should readyup
// on creation, and then a fire or start function that actually starts it.
// Attack components should have a healthcomponent and hitbox component as well if they can be blocked.
// What about connected attacks that can be interrupted though? Like the main characters sword attacks...
// I guess the attack controller will need to keep track of all living attacks
// Then each attack component can have an interruptable boolean. If it's interruptable it will have an interrupt method
// Optionally, could have a resume as well if it's something that can be paused rather than stopped.
public partial class AttackComponent : Node
{
    [Export] public bool IsInterruptable = false;
    // All of these will be children, so don't need nodepaths. Leaving as notes though to remind me from the editor.
    [Export] public NodePath HealthComponent;
    [Export] public NodePath HitBoxComponent;
    [Export] public NodePath AnimatedSprite;
    [Export] public NodePath HurtBoxComponent;
    [Export] public NodePath Timer;

    public readonly static string HitDetected = "HitDetectedSignal";
    
    public override void _Ready()
    {
        base._Ready();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        // if hurtbox detects collision, should emit signal with ID of hitbox collided with for things like area specific armor
    }

    public virtual void Start()
    {
        
    }

    // Stops the attack completely, essentially reducing its health to zero
    public virtual void InterruptAndkill()
    {
        
    }

    // Pauses the attack. For situations like hitting a freeze barrier
    public virtual void Interrupt()
    {
        
    }

    // Resumes the attack
    public virtual void Continue()
    {
        
    }
}