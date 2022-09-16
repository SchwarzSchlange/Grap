using Grap.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grap.Engine.Components
{
    public class Line : Component
    {
        public Vector2 Start { get; set; }
        public Vector2 End { get; set; }
        public Vector2 Origin { get; set; }

        public Line(Grap Root,string Name,Vector2 start,Vector2 end):base(Root,Name,ComponentType.LINE)
        {
            this.Start = start;
            this.End = end;

            Vector2 Dif = Start - End;
            Dif.x = Math.Abs(Dif.x);
            Dif.y = Math.Abs(Dif.y);

            this.Origin = new Vector2(this.Start.x + Dif.x/2, this.End.y - Dif.y/2);
        }
    }
}
