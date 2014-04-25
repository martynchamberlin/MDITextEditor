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

        public ChildEditor( Form1 form, string title, ToolStripMenuItem nav_li )
        {
            InitializeComponent();
            this.Text = title;
            this.nav_li = nav_li;
            this.parentForm = form;
            this.FormClosed += new FormClosedEventHandler(FormIsClosing);
        }
         
        void FormIsClosing(object sender, FormClosedEventArgs e)
        {
           this.parentForm.childFormClosed( this );
        }
    }


}
