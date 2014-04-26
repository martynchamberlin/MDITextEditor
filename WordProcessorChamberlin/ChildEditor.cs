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

        // Variables to keep track of the font formatting for the textarea
        public bool bold = false;
        public bool italic = false;
        public string family = "Microsoft Sans Serif";
        public float size = 11F;

        private Timer timer = new Timer();

        /* 
         * @param Form1 form - a reference to the calling parent MDI
         * @param string title - the title of the form
         * @param string color - the color, as specified by the user, of the form background
         * @param ToolStripMenuItem nav_li - object that is storing this document in the primary navigation as a dropdown of "View"
         */
        public ChildEditor( Form1 form, string title, string color, ToolStripMenuItem nav_li )
        {
            InitializeComponent();

            this.Text = title;
            this.nav_li = nav_li;
            this.parentForm = form;

            this.FormClosed += new FormClosedEventHandler(FormIsClosing);

            // Specify the proper background color for this child form and the rich editor.
            // First step is to initialize our colors. These RGB arrays are colors I made up.
            // They look a little garish on my screen... turning them into gradients would help.
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
            // Not an easy way to make this control transparent, so its background color must be manually
            // specified as well
            richTextBox1.BackColor = col;
        }
         
        void FormIsClosing(object sender, FormClosedEventArgs e)
        {
           this.parentForm.childFormClosed( this );
        }

        public void makeBold()
        {
            this.bold = true;
            refresh_font();
        }

        public void makeUnbold()
        {
            this.bold = false;
            refresh_font();
        }

        public void makeItalic()
        {
            this.italic = true;
            refresh_font();
        }

        public void makeUnitalic()
        {
            this.italic = false;
            refresh_font();
        }

        public void changeFontSize( float size )
        {
            this.size = size;
            refresh_font();
        }

        public void changeFontFamily( string family )
        {
            this.family = family;
            refresh_font();
        }

        /** 
         * This is the only place in which we are actually updating the font to the view.
         * Everywhere else is simply backend signaling.
         */ 
        public void refresh_font()
        {
            if (bold && italic)
            {
                this.richTextBox1.Font = new System.Drawing.Font(family, size, (System.Drawing.FontStyle)(System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            }
            else if (italic)
            {
                this.richTextBox1.Font = new System.Drawing.Font(family, size, (System.Drawing.FontStyle)(System.Drawing.FontStyle.Italic), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            }
            else if (bold)
            {
                this.richTextBox1.Font = new System.Drawing.Font(family, size, (System.Drawing.FontStyle)(System.Drawing.FontStyle.Bold), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            }
            else
            {
                this.richTextBox1.Font = new System.Drawing.Font(family, size, (System.Drawing.FontStyle)(System.Drawing.FontStyle.Regular), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            }
        }

        public void changeAlignment( HorizontalAlignment alignment )
        {
            this.richTextBox1.SelectionAlignment = alignment;
        }
    }
}
