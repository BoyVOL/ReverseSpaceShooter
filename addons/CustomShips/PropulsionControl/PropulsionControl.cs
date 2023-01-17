using Godot;
using System;

public class PropulsionControl : Addon<CustomShip>
{
    [Export]
    public Vector2 ForwardForce;

    public override void _EnterTree()
    {
        base._EnterTree();
        Parent.PropControll = this;
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
    }

    public override void _AddonUpdate()
    {
        base._AddonUpdate();
        if(Input.IsActionPressed("thrust_forward")) Parent.AddForce(Vector2.Zero,ForwardForce);
    }

    public override void _ExitTree(){
        base._ExitTree();
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
