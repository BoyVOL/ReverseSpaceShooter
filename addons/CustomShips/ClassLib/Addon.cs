using Godot;
using System;

public class Addon<T> : Node2D where T: class{
    
    protected T Parent;

    public override void _EnterTree()
    {
        base._EnterTree();
        Parent = GetParent<T>();
    }

    /// <summary>
    /// Method for finalising Addon for his parent
    /// </summary>
    public virtual void _AddonUpdate(){
        GD.Print("AddonUpdate");
    }

};