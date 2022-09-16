using Grap.Components;

namespace Grap.Engine.Components
{
    public class Text : Component
    {
        public string Value { get; set; }
        public Vector2 Position { get; set; }
        public float Size { get; set; }
        public Text(Grap Root,string Name,string Value,Vector2 position,float size):base(Root,Name,ComponentType.TEXT) 
        {
            this.Value = Value;
            this.Position = position;
            this.Size = size;
        }
    }
}
