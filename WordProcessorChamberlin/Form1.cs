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
    public partial class Form1 : Form
    {
        int num_documents = 1;
        Dictionary<ToolStripMenuItem, ChildEditor> children = new Dictionary<ToolStripMenuItem, ChildEditor>();
        public Form1()
        {
            InitializeComponent();

        }

        public void create_document( string name )
        {
            // Auto-generate the document name
            String documentName = "Document " + num_documents;

            // Update the Toolstrip Menu item
            ToolStripMenuItem nav_li = new ToolStripMenuItem(documentName);

            // Instantiate the new child form
            ChildEditor form = new ChildEditor(this, documentName, name, nav_li);

            // Specify the child form's parent
            form.MdiParent = this;

            // Show the form inside of its parent
            form.Show();

            // Add this form to our collection so we can keep track of it
            children[ nav_li ] = form;

            // increment the unique ID for child documents
            num_documents++;

            // Add the new document to our dropdown in the navigation
            Window.DropDownItems.Add( nav_li );

            // Bind the click event to this new item
            nav_li.Click += new System.EventHandler( bringToFront );

        }

        private void bringToFront(object sender, EventArgs e)
        {
            // Sender was nav_li, so cast it to that
            ToolStripMenuItem li = (ToolStripMenuItem)sender;
            children[li].Activate();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            create_document( ((ToolStripMenuItem)sender).Name );
        }

        public void childFormClosed( ChildEditor form )
        {
            Window.DropDownItems.Remove(form.nav_li);
        }
    }
}
