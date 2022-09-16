using Grap.Engine.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grap.Components
{
    public class ComponentRenderer
    {
        public int RenderDelay = 1000;
        public async void Update(Grap grap)
        {
            while(true)
            {
                
                for (int i = 0;i < grap.Components.Count;i++)
                {
                    if (grap.Components[i].componentType == Component.ComponentType.TEXT)
                    {
                        grap.DrawText((Text)grap.Components[i]);
                    }
                    else if(grap.Components[i].componentType == Component.ComponentType.LINE)
                    {
                        grap.DrawLine((Line)grap.Components[i]);
                    }
                    else if (grap.Components[i].componentType == Component.ComponentType.RECTANGLE)
                    {
                        grap.DrawRectangle((Rect)grap.Components[i]);
                    }
                    else if (grap.Components[i].componentType == Component.ComponentType.CIRCLE)
                    {
                        grap.DrawCircle((Circle)grap.Components[i]);
                    }


                   
                }

                await Task.Delay(RenderDelay);
                grap.Clear();
                
            }
        }
    }
}
