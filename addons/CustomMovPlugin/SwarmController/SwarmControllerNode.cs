using Godot;
using System;
using CustomAnimations;
using System.Collections.Generic;
    
namespace CustomAnimations{
    
        /// <summary>
        /// Тело, состоящее из нескольких плывущих по воздуху объектов
        /// </summary>
        public class SwarmController: Node2D{

            
        /// <summary>
        /// Класс члена роя
        /// </summary>
        public class SwarmCreep{

            /// <summary>
            /// Цель движения члена роя
            /// </summary>
            public Vector2 Target;

            /// <summary>
            /// свойство, хранящее объект, который подвергается изменениям
            /// </summary>
            public Node2D Node;

            public SwarmCreep(Node2D node){
                Node = node;
            }

            /// <summary>
            /// Метод, проверяющий, достаточно ли объект близко к своей цели
            /// </summary>
            /// <returns></returns>
            public bool NearTarget(float epsilon){
                return Node.Position.DistanceSquaredTo(Target) < epsilon;
            }

            public void MoveForward(float moveSpeed, float delta, float epsilon){
                if(!NearTarget(epsilon)){
                    if(Node.Position.DistanceTo(Target) > moveSpeed)
                    Node.Position += Node.Position.DirectionTo(Target)*moveSpeed;
                    else Node.Position = Target;
                };
            }
        }

        RandomNumberGenerator Randomiser = new RandomNumberGenerator();

        [Export]
        /// <summary>
        /// Описывает, какие типы насекомышей используются в рое
        /// </summary>
        public PackedScene SwarmSample;

        [Export]
        /// <summary>
        /// Описывает количество юнитов в рое
        /// </summary>
        public int SwarmSize;

        [Export]
        /// <summary>
        /// Описывает размер роя
        /// </summary>
        public float SwarmRadius = 0;

        [Export]
        public float MoveSpeed = 0.1f;

        [Export]
        /// <summary>
        /// Свойство, отвечающее за то, что член роя перестанет двигаться к его цели
        /// </summary>
        public float epsilon = 1;

        /// <summary>
        /// Список всех членов роя
        /// </summary>
        /// <typeparam name="SwarmMember"></typeparam>
        /// <returns></returns>
        public List<SwarmCreep> Kreeps = new List<SwarmCreep>();

        public Vector2 GetRandomPoint(){
            float r = SwarmRadius * (float)Math.Sqrt(Randomiser.Randf());
            float theta = Randomiser.Randf() * 2 * (float)Math.PI;
            return new Vector2((float)Math.Sin(theta)*r,(float)Math.Cos(theta)*r);
        }

        public void AddKreep(){
            SwarmCreep kreep = new SwarmCreep(SwarmSample.Instance<Node2D>());
            Kreeps.Add(kreep);
            kreep.Target = GetRandomPoint();
            kreep.Node.Position = GetRandomPoint();
            AddChild(kreep.Node);
        }

        public void RemoveKreep(SwarmCreep kreep){
            Kreeps.Remove(kreep);
            RemoveChild(kreep.Node);
        }

        public void LoadSwarm(){
            for (int i = 0; i < SwarmSize; i++)
            {
                AddKreep();
            }
        }

        public void UnloadSwarm(){
            foreach(SwarmCreep kreep in Kreeps){
                RemoveKreep(kreep);
            }
        }

        public void MoveCreeps(float delta){
            foreach(SwarmCreep kreep in Kreeps){
                kreep.MoveForward(MoveSpeed,delta,epsilon);
            }
        }

        public override void _Process(float delta)
        {
            base._Process(delta);
            foreach (SwarmCreep kreep in Kreeps)
            {
                if(kreep.NearTarget(epsilon)) kreep.Target = GetRandomPoint();
            }
            MoveCreeps(delta);
        }

        public override void _EnterTree()
        {
            base._EnterTree();
            LoadSwarm();
        }

        public override void _ExitTree()
        {
            base._ExitTree();
        }

    }

}
public class SwarmControllerNode : SwarmController
{
    

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
