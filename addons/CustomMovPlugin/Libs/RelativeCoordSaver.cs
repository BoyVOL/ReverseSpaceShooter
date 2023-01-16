using Godot;
using System;

namespace CustomAnimations{

    public class RelativeCoords: Node2D{

        [Export]
        public NodePath RefNodePath = new NodePath("");
        
        protected Node2D RefNode = null;

        public Vector2 GetRelPosition(Node2D refNode){
            if(refNode != null){
                return GlobalPosition-RefNode.GlobalPosition;
            } else return GlobalPosition;
        }

        public Vector2 GetRelPosition(Node2D refNode, Node2D TargetNode){
            if(TargetNode != null){
                if(refNode != null){
                    return TargetNode.GlobalPosition-RefNode.GlobalPosition;
                } else{
                    return TargetNode.GlobalPosition;
                } 
            } else return GetRelPosition(refNode);
        }

        public void SetRelPosition(Node2D refNode, Vector2 newPosition){
            if(refNode != null){
                GlobalPosition = newPosition+RefNode.GlobalPosition;
            } else GlobalPosition = newPosition;
        }

        public float GetRelRotation(Node2D refNode){
            if(refNode != null){
                return GlobalRotation-RefNode.GlobalRotation;
            } else return GlobalRotation;
        }

        public void SetRelRotation(Node2D refNode,float newRotation){
            if(refNode != null){
                GlobalRotation = newRotation+RefNode.GlobalRotation;
            } else GlobalRotation = newRotation;
        }

        public override void _EnterTree()
        {
            base._EnterTree();
            RefNode = GetNodeOrNull<Node2D>(RefNodePath);
        }
    }

    /// <summary>
    /// Нод2D, сохраняющий изменения относительного положения и угла относительно другой ноды
    /// </summary>
    public class CoordSaver: RelativeCoords{

        public Vector2 OldPosition = Vector2.Zero;

        float OldRotation = 0;

        public override void _Process(float delta)
        {
            base._Process(delta);
            OldPosition = GetRelPosition(RefNode);
            OldRotation = GetRelRotation(RefNode);
        }
    }

}