using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Grap
{
    public class DefaultData
    {
        public List<object> XLineData = new List<object>();
        public List<object> YLineData = new List<object>();

        public List<DefaultMember> DefaultMembers = new List<DefaultMember>();

        public void AddDefaultMember(DefaultMember DefaultMember)
        {
            DefaultMembers.Add(DefaultMember);
        }
    }

    public class DefaultMember
    {
        public string RenderText { get; set; }
        public object xData { get; set; }
        public object yData { get; set; }
        public DefaultData Root { get; set; }



        public DefaultMember(DefaultData root,string renderText, object xData, object yData)
        {
            this.Root = root;
            this.RenderText = renderText;
            this.xData = xData;
            this.yData = yData;

            if(Root.XLineData.Find(x => x == xData) == null)
            {
                Root.XLineData.Add(xData);
            }
            if (Root.XLineData.Find(y => y == yData) == null)
            {
                Root.YLineData.Add(yData);
            }

        }
    }
}
