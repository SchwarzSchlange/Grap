using System;
using System.Drawing;
using System.Windows.Forms;
using Component = Grap.Components.Component;

namespace Grap
{
    public partial class grapWindow : Form
    {
        public Grap Grap;

        public grapWindow()
        {
            InitializeComponent();
        }

        private void grapWindow_Load(object sender, EventArgs e)
        {

            
            Grap = new Grap(grapPanel, Grap.ChartType.DEFAULT);
            Grap.Theme(Color.White, 0.01f,System.Drawing.Drawing2D.SmoothingMode.HighQuality);
            Grap.SetupAxis("X", "Y");
            Grap.Offset = 100f;
            Grap.Start("X-Y Chart");

            DefaultData data = new DefaultData();

            for(float i = -4; i <= 4;i++)
            {
                data.AddDefaultMember(new DefaultMember(data, "a",(float)Math.Atan(i), i ));
            }
            


            Grap.InsertData(data);
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
