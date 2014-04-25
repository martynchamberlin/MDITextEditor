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
        Dictionary<int, ChildEditor> children = new Dictionary<int, ChildEditor>();
        public Form1()
        {
            InitializeComponent();

        }

        public void create_document()
        {
            // Auto-generate the document name
            String name = "Document " + num_documents;

            // Update the Toolstrip Menu item
            ToolStripMenuItem item = new ToolStripMenuItem( name );

            // Instantiate the new child form
            ChildEditor form = new ChildEditor( this, name, item );

            // Specify the child form's parent
            form.MdiParent = this;

            // Show the form inside of its parent
            form.Show();

            // Add this form to our collection so we can keep track of it
            children[num_documents] = form;

            // increment the unique ID for child documents
            num_documents++;

            // Add the new document to our dropdown in the navigation
            Window.DropDownItems.Add( item );
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            create_document();
        }

        public void childFormClosed( ChildEditor form )
        {
            Window.DropDownItems.Remove(form.nav_li);
            
        }
    }
}
