using Godot;
using System;

[Tool]
public class CustomMovPlugin : EditorPlugin
{    
    public string LibDir = "res://addons/CustomMovPlugin/";

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
        AddCustomType("SwarmController","Node2D",GD.Load<Script>("res://addons/CustomMovPlugin/SwarmController/SwarmControllerNode.cs"),
        GD.Load<Texture>("res://addons/CustomMovPlugin/SwarmController/icon.png"));
        AddCustomType("MoveAngleFollower","Node2D",GD.Load<Script>("res://addons/CustomMovPlugin/MoveAngleFollower/MoveAngleFollowerNode.cs"),
        GD.Load<Texture>("res://addons/CustomMovPlugin/MoveAngleFollower/icon.png"));
        AddCustomType("SecondOrder","Node2D",GD.Load<Script>("res://addons/CustomMovPlugin/SecondOrder/SecondOrderNode.cs"),
        GD.Load<Texture>("res://addons/CustomMovPlugin/SecondOrder/icon.png"));
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        RemoveCustomType("SwarmController");
        RemoveCustomType("MoveAngleFollower");
        RemoveCustomType("SecondOrder");
    }
}
