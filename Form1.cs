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
            richTextBox2.PreviewKeyDown += OnPreviewKeyDown;
            richTextBox2.KeyDown += OnKeyDown;
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
                richTextBox2.Text = e.Data.GetData(DataFormats.Text).ToString();
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                if (fname != string.Empty)
                {
                    File.WriteAllText(fname, richTextBox2.Text);
                }
                else
                {
                    saveFileDialog1.FileName = "new";
                    saveFileDialog1.ShowDialog();
                    fname = saveFileDialog1.FileName;
                    ActiveForm.Text = fname;
                    if (saveFileDialog1.DefaultExt != string.Empty && fname != string.Empty)
                    {
                        File.WriteAllText(fname, richTextBox2.Text);
                    }
                    else
                    {
                        ActiveForm.Text = "Empty";
                        fname = string.Empty;
                    }
                }
            }

            else if (e.Control && e.KeyCode == Keys.F)
            {
                fontDialog1.ShowDialog();
                richTextBox2.Font = fontDialog1.Font;
            }

            else if (e.Control && e.KeyCode == Keys.D)
            {
                openFileDialog1.FileName = string.Empty;
                openFileDialog1.ShowDialog();
                fname = openFileDialog1.FileName;
                ActiveForm.Text = "Empty";


                if(fname != string.Empty)
                    File.Delete(fname);
                fname = string.Empty;
            }

            else if (e.Control && e.KeyCode == Keys.S && e.Alt)
            {
                string text = string.Empty;
                text +=  "Font: " + richTextBox2.Font.Name + '\n';
                text += '\n' + "FontSize: " + richTextBox2.Font.Size + '\n';
                text += '\n' + "FontStyle: " + richTextBox2.Font.Style + '\n';
                text += '\n' + "ForeColor: " + richTextBox2.ForeColor.ToString() + '\n';
                text += '\n' + "BackColor: " + richTextBox2.BackColor.ToString();
                File.WriteAllText("style.cfg", text);
            }

            else if (e.Control && e.KeyCode == Keys.R)
            {
                colorDialog1.ShowDialog();
                richTextBox2.ForeColor = colorDialog1.Color;
            }

            else if (e.Control && e.KeyCode == Keys.G)
            {
                colorDialog1.ShowDialog();
                richTextBox2.BackColor = colorDialog1.Color;
            }

            else if (e.Control && e.Shift && e.KeyCode == Keys.S)
            {
                saveFileDialog1.FileName = string.Empty;
                saveFileDialog1.ShowDialog();
                fname = saveFileDialog1.FileName;
                ActiveForm.Text = fname;
                if (saveFileDialog1.DefaultExt != string.Empty && fname != string.Empty)
                {
                    File.WriteAllText(fname, richTextBox2.Text);
                }
            }

            else if (e.Control && e.KeyCode == Keys.O)
            {
                openFileDialog1.FileName = string.Empty;
                openFileDialog1.ShowDialog();
                fname = openFileDialog1.FileName;
                ActiveForm.Text = fname;
                if (fname != string.Empty)
                    richTextBox2.Text = File.ReadAllText(fname);
                else
                {
                    ActiveForm.Text = "Empty";
                    fname = string.Empty;
                }
            }

            else if (e.Control && e.Shift && e.KeyCode == Keys.N)
            {
                saveFileDialog1.FileName = string.Empty;
                saveFileDialog1.ShowDialog();
                fname = saveFileDialog1.FileName;
                ActiveForm.Text = fname;
                if (fname != string.Empty)
                {
                    File.WriteAllText(fname, string.Empty);
                    richTextBox2.Text = File.ReadAllText(fname);
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
                openFileDialog1.ShowDialog();
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
