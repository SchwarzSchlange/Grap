using Grap.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grap.Engine.Components
{
    public class Rect : Component
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }

        public Rect(Grap Root, string Name, Vector2 pos, Vector2 size) : base(Root, Name, ComponentType.RECTANGLE)
        {
            this.Position = pos;
            Size = size;
        }
    }
}
