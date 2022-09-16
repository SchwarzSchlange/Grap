using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Grap
{
    public class PieData
    {
        public List<PieMember> PieMembers = new List<PieMember>();

        public void AddPieMember(PieMember PieMember)
        {
            PieMembers.Add(PieMember);
        }
    }

    public class PieMember
    {
        public string RenderText { get; set; }
        public float Value { get; set; }
        public PieData Root { get; set; }

        public PieMember(PieData root, string renderText,float Value)
        {
            this.Root = root;
            this.RenderText = renderText;
            this.Value = Value;

            if(root.PieMembers.Find(x => x.Value == Value) != null)
            {
                return;
            }

        }
    }
}
