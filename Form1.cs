using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace HotKeyEditor
{
    public partial class Form1 : Form
    {
        public string fname = string.Empty;
        public Form1()
        {
            
            InitializeComponent();
            fastColoredTextBox1.PreviewKeyDown += OnPreviewKeyDown;
            fastColoredTextBox1.KeyDown += OnKeyDown;
        }

        private void OnPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Control)
            {
                
                e.IsInputKey = true;
            }
        }

        private void richTextBox2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Move;
                fastColoredTextBox1.Text = e.Data.GetData(DataFormats.Text).ToString();
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                if (fname != string.Empty)
                {
                    File.WriteAllText(fname, fastColoredTextBox1.Text);
                }
                else
                {
                    saveFileDialog1.FileName = "new";
                    if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                        return;
                    fname = saveFileDialog1.FileName;
                    ActiveForm.Text = fname;
                    if (saveFileDialog1.DefaultExt != string.Empty && fname != string.Empty)
                    {
                        File.WriteAllText(fname, fastColoredTextBox1.Text);
                    }
                    else
                    {
                        ActiveForm.Text = "Empty";
                        fname = string.Empty;
                    }
                }
            }

            else if (e.Control && e.Shift && e.KeyCode == Keys.F)
            {
                fontDialog1.ShowDialog();
                fastColoredTextBox1.Font = fontDialog1.Font;
            }

            else if (e.Control && e.KeyCode == Keys.D)
            {
                openFileDialog1.FileName = string.Empty;
                if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                fname = openFileDialog1.FileName;
                ActiveForm.Text = "Empty";


                if(fname != string.Empty)
                    File.Delete(fname);
                fname = string.Empty;
            }

            else if (e.Control && e.KeyCode == Keys.S && e.Alt)
            {
                string text = string.Empty;
                text +=  "Font: " + fastColoredTextBox1.Font.Name + '\n';
                text += '\n' + "FontSize: " + fastColoredTextBox1.Font.Size + '\n';
                text += '\n' + "FontStyle: " + fastColoredTextBox1.Font.Style + '\n';
                text += '\n' + "ForeColor: " + fastColoredTextBox1.ForeColor.ToString() + '\n';
                text += '\n' + "BackColor: " + fastColoredTextBox1.BackColor.ToString();
                File.WriteAllText("style.cfg", text);
            }

            else if (e.Control && e.KeyCode == Keys.R)
            {
                if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                fastColoredTextBox1.ForeColor = colorDialog1.Color;
            }

            else if (e.Control && e.Shift && e.KeyCode == Keys.G)
            {
                if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                fastColoredTextBox1.BackColor = colorDialog1.Color;
            }

            else if (e.Control && e.KeyCode == Keys.A && e.Shift)
            {
                saveFileDialog1.FileName = string.Empty;
                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                fname = saveFileDialog1.FileName;
                ActiveForm.Text = fname;
                if (saveFileDialog1.DefaultExt != string.Empty && fname != string.Empty)
                {
                    File.WriteAllText(fname, fastColoredTextBox1.Text);
                }
            }

            else if (e.Control && e.KeyCode == Keys.O)
            {
                openFileDialog1.FileName = string.Empty;
                if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                fname = openFileDialog1.FileName;
                ActiveForm.Text = fname;
                if (fname != string.Empty)
                    fastColoredTextBox1.Text = File.ReadAllText(fname);
                else
                {
                    ActiveForm.Text = "Empty";
                    fname = string.Empty;
                }
            }

            else if (e.Control && e.Shift && e.KeyCode == Keys.N)
            {
                saveFileDialog1.FileName = string.Empty;
                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                fname = saveFileDialog1.FileName;
                ActiveForm.Text = fname;
                if (fname != string.Empty)
                {
                    File.WriteAllText(fname, string.Empty);
                    fastColoredTextBox1.Text = File.ReadAllText(fname);
                } 
                else
                {
                    ActiveForm.Text = "Empty";
                    fname = string.Empty;
                }
            }

            else if (e.Control && e.KeyCode == Keys.P)
            {
                openFileDialog1.FileName = string.Empty;
                if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                fname = openFileDialog1.FileName;
                ActiveForm.Text = fname;
                System.Drawing.Printing.PrintDocument document = new System.Drawing.Printing.PrintDocument();
                document.DocumentName = fname;
                printDialog1.Document = document;
                printDialog1.ShowDialog();
            }
            
            else if (e.Control && e.KeyCode == Keys.N)
            {
                if (System.Diagnostics.Process.Start("HotKeyEditor.exe") == null)
                    Application.Exit();
            }
        }
    }
}
