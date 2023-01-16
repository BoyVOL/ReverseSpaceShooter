using Godot;
using System;
using CustomAnimations;
using System.Collections.Generic;
    
namespace CustomAnimations{
    
        /// <summary>
        /// Тело, состоящее из нескольких плывущих по воздуху объектов
        /// </summary>
        public class MoveAngleFollowerParent: CoordSaver{
                public float GetSpeedAngle(){
                        return OldPosition.DirectionTo(GetRelPosition(RefNode)).Angle();
                }

                public override void _Process(float delta){
                        Rotation = GetSpeedAngle();
                        base._Process(delta);
                }
        }
            
}

public class MoveAngleFollower: MoveAngleFollowerParent{
        
}
