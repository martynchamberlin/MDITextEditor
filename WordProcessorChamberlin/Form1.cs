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
        // Total number of documents created during program execution
        int num_documents = 1;

        // Number of documents visible to the user
        int active_documents = 0;

        // Key-value array storing the form and a corresponding navigation item
        Dictionary<ToolStripMenuItem, ChildEditor> children = new Dictionary<ToolStripMenuItem, ChildEditor>();

        public Form1()
        {
            InitializeComponent();
        }

        public void create_document(string name)
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
            children[nav_li] = form;

            // increment the unique ID for child documents
            num_documents++;

            // Add the new document to our dropdown in the navigation
            Window.DropDownItems.Add(nav_li);

            // Bind the click event to this new item
            nav_li.Click += new System.EventHandler(bringToFront);
        }

        private void bringToFront(object sender, EventArgs e)
        {
            // Sender was nav_li, so cast it to that
            ToolStripMenuItem li = (ToolStripMenuItem)sender;
            children[li].Activate();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            create_document(((ToolStripMenuItem)sender).Name);
            active_documents++;
            enableOrDisableDropdownItems();
        }

        public void childFormClosed(ChildEditor form)
        {
            // Remove the item from the nav
            Window.DropDownItems.Remove(form.nav_li);

            // Update number of active documents
            active_documents--;

            // If that was the last MDI window, then disable pertinent dropdowns

            enableOrDisableDropdownItems();
        }

        private void boldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Checkmark bold in the nav dropdown immediatley. We are also doing this
            // when you click on the dropdown in the future, but that blinks for a second
            // because Windows is mysteriously sluggish. This way, you do not see that blink
            // if, when the person re-clicks the dropdown, they have not changed to a different
            // MDI child window since the time that this function executed.
            boldToolStripMenuItem.Checked = boldToolStripMenuItem.Checked ? false : true;

            ChildEditor form = (ChildEditor)this.ActiveMdiChild;
            // We always have to first make sure that an MdiChild actually exists.
            // Otherwise we can a compile error when trying to access the properties.
            // In theory this check shouldn't have to happen because we don't even allow the dropdown
            // to be Enabled unless an MDI exists. But better safe than sorry!
            if (form != null)
            {
                if (form.bold)
                {
                    form.makeUnbold();
                }
                else
                {
                    form.makeBold();
                }
            }
        }

        private void italicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            italicToolStripMenuItem.Checked = italicToolStripMenuItem.Checked ? false : true;

            ChildEditor form = (ChildEditor)this.ActiveMdiChild;
            if (form != null)
            {
                if (form.italic)
                {
                    form.makeUnitalic();
                }
                else
                {
                    form.makeItalic();
                }
            }
        }

        /**
         * The user can flip between different MDI windows. The below function allows us to
         * programatically check if the currently highlighted window is bold or italicized,
         * at the event (or moment) of dropdown click.
         *
         * The crazy part about this is that Windows is insanely sluggish, so there is a brief
         * moment from the time that you click and view the dropdown, to the time that the 
         * checkmark and uncheckmark action (as per this function) execute. 
         */
        private void formatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChildEditor form = (ChildEditor)this.ActiveMdiChild;
            if (form != null)
            {
                if (form.bold)
                {
                    boldToolStripMenuItem.Checked = true;
                }
                else
                {
                    boldToolStripMenuItem.Checked = false;
                }
                if (form.italic)
                {
                    italicToolStripMenuItem.Checked = true;
                }
                else
                {
                    italicToolStripMenuItem.Checked = false;
                }
            }
        }

        private void uncheckmark_a_list( ToolStripMenuItem ul )
        {
            foreach (ToolStripMenuItem nav_li in ul.DropDown.Items)
            {
                nav_li.Checked = false;
            }
        }

        private void ptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // First, uncheckmark all previous pt's
            uncheckmark_a_list(fontSize);

            ChildEditor form = (ChildEditor)this.ActiveMdiChild;
            ToolStripMenuItem li = (ToolStripMenuItem)sender;
            li.Checked = true;
            string name = li.Name;

            // The name of all the font size toolstrip menu items starts with pt and then followed
            // by the integer of the font size. Hence, we need to strip the first two characters off 
            // and convert the result to a float.
            
            float font_size = Convert.ToInt64(name.Substring(2));
            if (form != null)
            {
                form.size = font_size;
                form.refresh_font();
            }

        }

        // Let the user close the application
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Let the user close the currently active MDI window
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChildEditor form = (ChildEditor)this.ActiveMdiChild;
            if (form != null)
            {
                form.Close();
            }
        }

        private void fontFamilyToolStripMenuItemChild_Clicked(object sender, EventArgs e)
        {
            // First, uncheckmark all previous font families 
            uncheckmark_a_list(fontFamilyToolStripMenuItem);

            // Now checkmark the appropriate one
            ((ToolStripMenuItem)sender).Checked = true;

            ChildEditor form = (ChildEditor)this.ActiveMdiChild;
            string font_family = ((ToolStripMenuItem)sender).Text;
            if (form != null)
            {
                form.family = font_family;
                form.refresh_font();
            }
        }

        public void enableOrDisableDropdownItems()
        {
            if (active_documents > 0)
            {
                formatToolStripMenuItem.Enabled = true;
                Window.Enabled = true;
                closeToolStripMenuItem.Enabled = true;
            }
            else
            {
                formatToolStripMenuItem.Enabled = false;
                Window.Enabled = false;
                closeToolStripMenuItem.Enabled = false;
            }
        }
    }
}
