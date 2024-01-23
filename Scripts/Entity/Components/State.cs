using Godot;
using System;

public partial class State : Node
{
	public static readonly string TransitionStateSignal = "TransitionState";
	public FiniteStateMachine Fsm;
	public override void _Ready()
	{
		base._Ready();
		
		AddUserSignal(TransitionStateSignal);
	}

	public virtual void Enter()
	{
		
	}

	public virtual void Exit()
	{
		
	}
	
	public virtual void Update(double delta)
	{
		
	}

	public virtual void PhysicsUpdate(double delta)
	{
		
	}

	public virtual void HandleInput(InputEvent @event)
	{
		
	}

	public virtual void HandleUnhandledInput(InputEvent @event)
	{
		
	}

	protected void Transition(string stateName)
	{
		EmitSignal(TransitionStateSignal, stateName);
	}
}
