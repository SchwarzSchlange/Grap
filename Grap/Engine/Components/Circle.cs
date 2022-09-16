using Grap.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grap.Engine.Components
{
    public class Circle : Component
    {
        public Vector2 Position { get; set; }
        public float Size { get; set; }
        public float StartDegree { get; set; }
        public float Degree { get; set; }
        public bool isPie { get; set; }

        public Circle(Grap Root, string Name, Vector2 position,float size,float startdegree, float degree,bool isPie) : base(Root, Name, ComponentType.CIRCLE)
        {
            this.Position = position;
            this.Size = size;
            this.StartDegree = startdegree;
            this.Degree = degree;
            this.isPie = isPie;
        }

    }
}
