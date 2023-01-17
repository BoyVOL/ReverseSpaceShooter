using Godot;
using System;

public class CustomShip : RigidBody2D
{

    public PropulsionControl PropControll = null;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    public override void _PhysicsProcess(float delta){
        AppliedForce = Vector2.Zero;
        if(PropControll != null) PropControll._AddonUpdate();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
