using Grap.Components;
using Grap.Engine.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace Grap
{
    public class Grap
    {
        public enum ChartType
        {
            DEFAULT,
            PIE
        };

        public Control Root { get; private set; } //Picture Box Control 
        public List<Component> Components { get; set; } = new List<Component>(); // Components
        public ChartType ChartDrawType { get; private set; } // Chart Type = Default

        private ComponentRenderer Renderer = new ComponentRenderer(); // Component updater render

        public float Offset { get; set; } = 30; // Offset to root borders

        public object Data { get; set; }

        private Pen Pen = new Pen(new SolidBrush(Color.Aqua), 0.1f);

        private string[] AxisNames = new string[2];

     

        public Grap(Control Root,ChartType type)
        {
            this.Root = Root;
            
            this.ChartDrawType = type;

         
         
        }

        public void Clear()
        {
            this.Root.Paint += new PaintEventHandler(delegate (object e, PaintEventArgs args)
            {
                args.Graphics.Clear(Root.BackColor);
            });

            Root.Invalidate();
           
        }

        public void Theme(Color color,float PenSize,SmoothingMode smoothingMode)
        {
            Pen.Color = color;
            Pen.Width = PenSize;
            Pen.Alignment = PenAlignment.Center;
            this.Root.Paint += new PaintEventHandler(delegate (object e, PaintEventArgs args)
            {
                args.Graphics.SmoothingMode = smoothingMode;
            });
        }
        public void SetupAxis(string xDir,string yDir)
        {
            AxisNames[0] = xDir;
            AxisNames[1] = yDir;
        }
        public void InsertData(object generalData)
        {
            this.Data = generalData;
            if(ChartDrawType == ChartType.DEFAULT)
            {
                DefaultData data = (DefaultData)Convert.ChangeType(generalData, typeof(DefaultData));
                if (data == null || data.XLineData.Count <= 0 || data.YLineData.Count <= 0)
                    return;

                data.XLineData.Sort();
                data.YLineData.Sort();

                var xLine = Component.Find<Line>(this, "XLine");
                var yLine = Component.Find<Line>(this, "YLine");

                float xLenght = Math.Abs((xLine.End - xLine.Start).x);
                float yLenght = Math.Abs((yLine.End - yLine.Start).y);

                // Grap(1) == xLenght/2 

                float factor = 1f;
               
                factor = xLenght / ((float)data.XLineData.Last() - (float)data.XLineData.First()) /2;
                


                float factorY = 1f;
               
                factorY = yLenght / ((float)data.YLineData.Last() )/2;
                


              

                foreach (var xData in data.XLineData)
                {
                    new Text(this, "XLineDataText", xData.ToString(), xLine.Origin + new Vector2((float)xData * factor, 10), 8f);
                }


                foreach (var yData in data.YLineData)
                {
                    new Text(this, "YLineDataText", yData.ToString(), yLine.Origin - new Vector2(8 * yData.ToString().Length, (float)yData * factorY), 8f);
                }

                Vector2 lastVector = null;


                for (int i = 0; i < data.DefaultMembers.Count; i++)
                {
                    Vector2 Position = xLine.Origin - new Vector2(-(float)data.DefaultMembers[i].xData * factor, (float)data.DefaultMembers[i].yData * factorY);
                    
                    var cRect = new Circle(this, "origin", Position,5f,0,360,false);
                    //new Line(this, "lineH", cRect.Position, new Vector2(yLine.Start.x, cRect.Position.y));
                    new Line(this, "lineV", cRect.Position, new Vector2(cRect.Position.x, xLine.Start.y));
                    //new Line(this, "lineA", cRect.Position, new Vector2(xLine.Start.x, xLine.End.y));
                    if (data.DefaultMembers[i] != data.DefaultMembers.First())
                    {
                        new Line(this, "lineR", Position, lastVector);
                    }

                    lastVector = Position;
                    
                    //new Text(this, "nameText", $"({data.DefaultMembers[i].xData},{data.DefaultMembers[i].yData})", cRect.Position + new Vector2(-20, -20), 10);
                }

            }
            else if(ChartDrawType == ChartType.PIE)
            {
                PieData Data = (PieData)Convert.ChangeType(generalData, typeof(PieData));


                Vector2 BoxSize = new Vector2(Root.Width, Root.Height);

                Vector2 Center = BoxSize / new Vector2(2f,2f);


                int ElementCount = Data.PieMembers.Count;
                float ElementTotal = 0;
                Data.PieMembers.ForEach(m => ElementTotal += m.Value);

                float Oran = 360f / ElementTotal;
                float i = 1f;
                float lastDegree = 0f;
                float degreeTotal = 0f;
                foreach (PieMember pData in Data.PieMembers)
                {
                    float derece = pData.Value * Oran;
                    new Text(this, "pieTest", $"{derece.ToString()} - {pData.RenderText}", new Vector2(30f,30f+i),12f);
                    i += 24f;
                 
                    new Circle(this, "DegreeCircle", Center, BoxSize.y - Offset, degreeTotal, derece, true);
                    
              
                    
                    degreeTotal += derece;
                    lastDegree = pData.Value;
                }


            }
        }

        public void Start(string Title)
        {
            if(this.ChartDrawType == ChartType.DEFAULT)
            {
                // DEFAULT CHART

                Vector2 BoxSize = new Vector2(Root.Width, Root.Height);

                var BorderRect = new Rect(this, "Borders", new Vector2(BoxSize.x / 2, BoxSize.y / 2), BoxSize - new Vector2(Offset, Offset));

                var xLine = new Line(this,"XLine", BorderRect.Position-new Vector2(BorderRect.Size.x/2,0f), BorderRect.Position + new Vector2(BorderRect.Size.x / 2, 0f));
                var yLine = new Line(this, "YLine", BorderRect.Position - new Vector2(0f, BorderRect.Size.y/2f), BorderRect.Position + new Vector2(0f, BorderRect.Size.y / 2f));

                new Text(this, "Header", Title,BorderRect.Position - new Vector2(BorderRect.Size.x/2f, BorderRect.Size.y/2 + 15f), 15f);
                new Text(this, "XName", AxisNames[0], xLine.End + new Vector2(5,0), 8);
                new Text(this, "YName", AxisNames[1], yLine.End - new Vector2(0,15), 8);

                Renderer.Update(this);
            }
            else if(this.ChartDrawType == ChartType.PIE)
            {
                // PIE CHART

                Vector2 BoxSize = new Vector2(Root.Width, Root.Height);
                new Circle(this, "MainKreis", new Vector2(BoxSize.x/2, BoxSize.y/2), BoxSize.y-Offset,0,360,false);
                new Circle(this, "InnerKreis", new Vector2(BoxSize.x / 2, BoxSize.y / 2), (BoxSize.y - Offset)/20f, 0, 360, false);
                

                Renderer.Update(this);
            }
        }

        public void DrawLine(Line line)
        {
            this.Root.Paint += new PaintEventHandler(delegate (object e, PaintEventArgs args)
            {
                args.Graphics.DrawLine(Pen, line.Start.x,line.Start.y, line.End.x,line.End.y);
            });
        }

        public void DrawRectangle(Rect rectangle)
        {
            this.Root.Paint += new PaintEventHandler(delegate (object e, PaintEventArgs args)
            {
                args.Graphics.DrawRectangle(Pen, new Rectangle((int)(rectangle.Position.x-(rectangle.Size.x/2f)),(int)(rectangle.Position.y-(rectangle.Size.y/2f)),(int)(rectangle.Size.x),(int)(rectangle.Size.y)));
            });
        }

        public void DrawText(Text text)
        {
            this.Root.Paint += new PaintEventHandler(delegate (object e, PaintEventArgs args)
            {
                
                args.Graphics.DrawString(text.Value, new Font("Monaco", text.Size, FontStyle.Regular),new SolidBrush(Color.White),text.Position.x-text.Size/2f,text.Position.y-text.Size/2f);
            });
        }

        public void DrawCircle(Circle circle)
        {
            this.Root.Paint += new PaintEventHandler(delegate (object e, PaintEventArgs args)
            {
                if(circle.isPie)
                {
                    args.Graphics.DrawPie(Pen, new Rectangle((int)circle.Position.x - (int)circle.Size / 2, (int)circle.Position.y - (int)circle.Size / 2, (int)circle.Size, (int)circle.Size), circle.StartDegree, circle.Degree);
                }
                else
                {
                    args.Graphics.DrawArc(Pen, new Rectangle((int)circle.Position.x - (int)circle.Size / 2, (int)circle.Position.y - (int)circle.Size / 2, (int)circle.Size, (int)circle.Size), circle.StartDegree, 360);
                }

               
            });
        }
        
    }
}
