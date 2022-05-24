using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace NotePad
{
    public partial class Form1 : Form
    {
        string file = "";
        string style = "Arial";
        int size = 12;
    
        public Form1()
        {
            InitializeComponent();
            FontSize.SelectedIndex = 4;
            //TextType.SelectedIndex = 0;
            
            ScrollBar();
           
        }

        void ScrollBar()
        {
            // Create and initialize a VScrollBar.
            VScrollBar vScrollBar1 = new VScrollBar();
            HScrollBar hScrollBar1 = new HScrollBar();
            // Dock the scroll bar to the right side of the form.
            vScrollBar1.Dock = DockStyle.Fill;
            hScrollBar1.Dock = DockStyle.Fill;

            // Add the scroll bar to the form.
            Controls.Add(vScrollBar1);
            Controls.Add(hScrollBar1);

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Look for file to open
            DialogResult dlg = openFileDialog1.ShowDialog();
            // user presses okay
            if (dlg == DialogResult.OK)
            {
                // File path
                file = openFileDialog1.FileName;
                WriteToFile();
                
            }
            if (file != "")
            {
                this.Text = file;
            }
           
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(file))
            {

                File.WriteAllText(file, txtContent.Text);
            }
            
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtContent.Text = "";
        }

        void wordCount()
        {
            MatchCollection collection = Regex.Matches(txtContent.Text, @"[\S]+");
            StripWordCount.Text = "Word Count : " + collection.Count;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Made by Nickolas Reid :D");
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            string name = saveFileDialog1.FileName;
            try
            {
                File.WriteAllText(name, txtContent.Text);
            }
            catch (Exception)
            {

            }
        }

        void WriteToFile()
        {
            txtContent.Text = "";
            // Reads from file path
            System.IO.StreamReader fileRead = new System.IO.StreamReader(file);
            // Empty string to hold lines
            string lines;

            // Try the file out
            try
            {
                // While there are lines
                while ((lines = fileRead.ReadLine()) != null)
                {
                    //Write lines
                    txtContent.Text += lines + "\n";
                }
                wordCount();
            }
            catch (Exception)
            {
                MessageBox.Show("You messed up some how!");
            }
        }

        private void txtContent_TextChanged(object sender, EventArgs e)
        {
            wordCount();
        }

        private void FontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtContent.SelectionLength > 0)
            {
                size = Convert.ToInt32(FontSize.SelectedItem.ToString());
                Font font1 = new Font(style, size);
                txtContent.SelectionFont = font1;
            }
            else
            {
                size = Convert.ToInt32(FontSize.SelectedItem.ToString());
                Font font1 = new Font(style, size);
                txtContent.Font = font1;
            }

        }

        private void FontStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtContent.SelectionLength > 0)
            {
                //style = TextType.SelectedItem.ToString();
                Font font1 = new Font(style, size);
                txtContent.SelectionFont = font1;
            }
            else
            {
                //style = TextType.SelectedItem.ToString();
                Font font1 = new Font(style, size);
                txtContent.Font = font1;
            }
        }

        private void boldToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (txtContent.SelectionLength > 0)
            {
                txtContent.SelectionFont = new Font(txtContent.Font, FontStyle.Bold);
            }
            else
            {
                txtContent.Font = new Font(txtContent.Font, FontStyle.Bold);
            }
        }

        private void italicToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (txtContent.SelectionLength > 0)
            {
                txtContent.SelectionFont = new Font(txtContent.Font, FontStyle.Italic);
            }
            else
            {
                txtContent.Font = new Font(txtContent.Font, FontStyle.Italic);
            }
        }

        private void regularToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (txtContent.SelectionLength > 0)
            {
                txtContent.SelectionFont = new Font(txtContent.Font, FontStyle.Regular);
            }
            else
            {
                txtContent.Font = new Font(txtContent.Font, FontStyle.Regular);
            }
        }

        private void underlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtContent.SelectionLength > 0)
            {
                txtContent.SelectionFont = new Font(txtContent.Font, FontStyle.Underline);
            }
            else
            {
                txtContent.Font = new Font(txtContent.Font, FontStyle.Underline);
            }
        }

        private void strikeoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtContent.SelectionLength > 0)
            {
                txtContent.SelectionFont = new Font(txtContent.Font, FontStyle.Strikeout);
            }
            else
            {
                txtContent.Font = new Font(txtContent.Font, FontStyle.Strikeout);
            }
        }

        private void highlightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtContent.SelectionLength > 0)
            {
                txtContent.SelectionBackColor = Color.Yellow;
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtContent.Text != "")
            {
                txtContent.Copy();
            }
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtContent.SelectedText != "")
            {
                txtContent.Cut();
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text) == true)
            {
               
                    if (MessageBox.Show("Do you want to paste your text here?", "Warning", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        txtContent.SelectionStart += txtContent.SelectionLength;
                    }
                    txtContent.Paste();
                        
                   
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtContent.CanUndo == true)
            {
                txtContent.Undo();
                txtContent.ClearUndo();
            }
        }

        void wordSearch(string word)
        {
            System.IO.StreamReader fileRead = new System.IO.StreamReader(file);
            int counter = 0;
            while (fileRead != null)
            {
                for (int i = 0; i < fileRead.ReadToEnd().Length; i++)
                {
                    if (word[counter] == fileRead.ReadToEnd()[i])
                    {
                        if (counter == word[i])
                        {
                            i -= counter;
                            txtContent.SelectionStart = i;
                            //txtContent.SelectionLength = i + word.length;
                            break;
                        }
                        else
                        {
                            counter++;
                        }
                    }
                }
            }
        }
    }
}
