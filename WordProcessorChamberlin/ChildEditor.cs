using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WordProcessorChamberlin
{
    /**
     * Cannot inherit from Form1 becuase then this form would be both MDIParent and MDIChild,
     * and that causes a runtime error when we attempt to instantiate an object of this class
     */
    public partial class ChildEditor : Form
    {
        // Store object of parent window dropdown for this Child Editor window
        public ToolStripMenuItem nav_li;
        // Store object of parent window
        public Form1 parentForm;

        private Timer timer = new Timer();

        public ChildEditor( Form1 form, string title, string color, ToolStripMenuItem nav_li )
        {
            InitializeComponent();
            this.Text = title;
            this.nav_li = nav_li;
            this.parentForm = form;

            this.FormClosed += new FormClosedEventHandler(FormIsClosing);

            // Specify the proper background color for this child form and the rich editor.
            // First step is to initialize our colors
            Dictionary<string, int[]> colors = new Dictionary<string, int[]>();
            colors["yellow"] = new int[]{255, 253, 143};
            colors["orange"] = new int[]{255, 183, 127};
            colors["lgreen"] = new int[]{216, 253,138};
            colors["dgreen"] = new int[]{163,253,115};
            colors["lblue"] = new int[]{154,241,254};
            colors["dblue"] = new int[]{118, 182,252};
            colors["purple"] = new int[]{185, 157, 252};
            colors["pink"] = new int[]{222, 259, 253};
            colors["lgray"] = new int[]{247, 247, 247};
            colors["dgray"] = new int[]{203, 203, 203};

            // Update background to appropriate color
            
            Color col = System.Drawing.Color.FromArgb(((int)(((byte)(colors[color][0])))), ((int)(((byte)(colors[color][1])))), ((int)(((byte)(colors[color][2])))));
            BackColor = col;
            richTextBox1.BackColor = col;
        }
         
        void FormIsClosing(object sender, FormClosedEventArgs e)
        {
           this.parentForm.childFormClosed( this );
        }



    }


}
