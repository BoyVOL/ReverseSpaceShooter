using Godot;
using System;
using CustomAnimations;

namespace CustomAnimations
{
    public class VectorDampener{
        /// <summary>
        /// Предыдущий вход
        /// </summary>
        private Vector2 xp;

        /// <summary>
        /// Переменные состояния
        /// </summary>
        private Vector2 y = Vector2.Zero,yd = Vector2.Zero;

        private float _w;

        /// <summary>
        /// Константы
        /// </summary>
        private float k1,k2,k3;

        /// <summary>
        /// Метод, задающий константы
        /// </summary>
        /// <param name="f">Натуральная частота системы, или скорость, с которой система изменяется</param>
        /// <param name="z">коэффициент сопротивления</param>
        /// <param name="r">начальный ответ системы</param>
        public void SetConstants(float f, float z, float r){
            _w = 2*(float)Math.PI*f;
            k1 = z / ((float)Math.PI * f);
            k2 = 1 / (_w*_w);
            k3 = r*z/_w;
            GD.Print(k1,k2,k3);
        }

        public void SetCoords(Vector2 X){
            xp = X;
            y = X;
            yd = Vector2.Zero;
        }

        /// <summary>
        /// Метод, обновляющий состояние класса. 
        /// </summary>
        /// <param name="T">промежуток времени</param>
        /// <param name="x">целевая координата</param>
        /// <returns></returns>
        public Vector2 Update(float T, Vector2 x){
            Vector2 xd = (x-xp)/T;
            xp = x;
            float StabK1,StabK2;
            StabK1 = k1;
            StabK2 = Math.Max(k2,Math.Max(T*T/2+T*k1/2,T*k1));//Вычисляем стабильный коэффициент, чтобы не привести к разбалансировки модели
            y=y+T*yd;
            Vector2 Accel = (x+k3*xd-y-k1*yd)/StabK2;
            yd = yd+T*Accel;
            return y;
        }
    }
}

public class SecondOrder : RelativeCoords
{

    /// <summary>
    /// Поле, хранящее ноду, которую в данный момент двигают анимации данной ноды
    /// </summary>

    [Export]
    public float f=1,z=1,r=1;

    [Export]
    public NodePath TargetNodePath = new NodePath("");

    protected Node2D TargetNode = null;

    protected VectorDampener Dampener = new VectorDampener();

    public override void _EnterTree()
    {
        base._EnterTree();
        Dampener.SetCoords(GlobalPosition);
        Dampener.SetConstants(f,z,r);
        TargetNode = GetNodeOrNull<Node2D>(TargetNodePath);
    }
    
    public override void _Process(float delta)
    {
        base._Process(delta);
        Vector2 Damp = Dampener.Update(delta,GetRelPosition(RefNode,TargetNode));
        SetRelPosition(RefNode,Damp);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
