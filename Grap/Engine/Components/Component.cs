using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grap.Components
{
    public class Component
    {
        public enum ComponentType
        {
            TEXT,
            LINE,
            RECTANGLE,
            CIRCLE
        };

        public string Name {get;set;}
        public bool isVisible { get; set; } = true;
        public ComponentType componentType;

        public Component(Grap Root,string name, ComponentType componentType)
        {
            this.Name = name;
            
            this.componentType = componentType;

            Root.Components.Add(this);
            
        }

        public static T Find<T>(Grap Root,string name)
        {
            return (T)Convert.ChangeType(Root.Components.Find(x => x.Name == name),typeof(T));

        }
    }
}
