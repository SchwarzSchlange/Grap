using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grap
{
    public class Vector2
    {
        public float x { get; set; }
        public float y { get; set; }

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        #region OperatorOverrides
        public static Vector2 operator +(Vector2 current,Vector2 other)
        {
            return new Vector2(current.x + other.x, current.y + other.y);
        }

        public static Vector2 operator -(Vector2 current, Vector2 other)
        {
            return new Vector2(current.x - other.x, current.y - other.y);
        }

        public static Vector2 operator /(Vector2 current, Vector2 other)
        {
            return new Vector2(current.x / other.x, current.y / other.y);
        }

        public static Vector2 operator *(Vector2 current, Vector2 other)
        {
            return new Vector2(current.x * other.x, current.y * other.y);
        }

        public void Debug()
        {
            MessageBox.Show($"X : {this.x} {Environment.NewLine} Y : {this.y}");
        }
        #endregion


        public bool Equals(Vector2 other)
        {
            if(this.x == other.x && this.y == other.y)
            { return true; }
            else { return false; }  
        }
    }
}
