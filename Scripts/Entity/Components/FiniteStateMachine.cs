using Godot;
using System;
using System.Collections.Generic;
using VampireSurvivor.Game.Entity;

public partial class FiniteStateMachine : Node
{
	private Dictionary<string, State> _states;
	private State _currentState;
	
	[ExportGroup("Setup")]
	[Export]
	public NodePath InitialState;
	[Export]
	public NodePath CharacterBody2D;
	public CharacterBody2D CharacterBody;
	// [Export] 
	// public NodePath StatSheet;
	public BaseStats Stats;
	[Export] 
	public NodePath AnimatedSprite;
	public AnimatedSprite2D Sprite;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Sprite = GetNode<AnimatedSprite2D>(AnimatedSprite);
		CharacterBody = GetNode<CharacterBody2D>(CharacterBody2D);
		Stats = (BaseStats)GetNodeAndResource(CharacterBody2D + ":Stats")[1];
		
		
		_states = new Dictionary<string, State>();
		foreach (Node child in GetChildren())
		{
			if (child is State state)
			{
				_states[state.Name.ToString().ToLower()] = state;
				state.Fsm = this;
				// Don't think I need this ready call as the framework should call anything on the scene tree
				// state._Ready();
				state.Exit();
				state.Connect(State.TransitionStateSignal, new Callable(this, nameof(TransitionState)));
			}
		}

		_currentState = GetNode<State>(InitialState);
		_currentState.Enter();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_currentState.Update((float) delta);
	}

	public override void _PhysicsProcess(double delta)
	{
		_currentState.PhysicsUpdate((float) delta);
	}

	public override void _Input(InputEvent @event)
	{
		_currentState.HandleInput(@event);
	}
	public override void _UnhandledInput(InputEvent @event) 
	{
		_currentState.HandleUnhandledInput(@event);
	}

	public void TransitionState(string newState)
	{
		string state = newState.ToLower();
		if (!_states.ContainsKey(state) || _currentState == _states[state])
			return;
		
		_currentState.Exit();
		_currentState = _states[state];
		_currentState.Enter();
	}
}
