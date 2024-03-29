using Godot;
using System;

[Tool]
public class CustomShips : EditorPlugin
{    
    public string LibDir = "res://addons/CustomShips/";

    public void AddCustomType(string Type, string parent){
        AddCustomType(Type,parent,GD.Load<Script>(LibDir+Type+"/"+Type+".cs"),
        GD.Load<Texture>(LibDir+"/"+Type+"/icon.png"));
    }

    public void AddAutoloadSingleton(string Name){
        AddAutoloadSingleton(Name,LibDir+"/AutoloadScenes/"+Name+".tscn");
    }
    
    public override void _EnterTree(){
        base._EnterTree();
        GD.Print("Plugin ready");
        AddCustomType("CustomShip","RigidBody2D");
        AddCustomType("PropulsionControl","Node2D");
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        RemoveCustomType("CustomShip");
        RemoveCustomType("PropulsionControl");
    }
}
