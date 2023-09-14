using Aspose.Words.Fonts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public int fontSize = 0;
        public System.Drawing.FontStyle fs = FontStyle.Regular;

        public string filename;
        public bool IsFileChanged;
        public FontSettings fontSetts;
        private AboutProgram aboutProgram;

        public Form1()
        {
            InitializeComponent();

            Init();
        }
        public void Init()
        {
            filename = "";
            IsFileChanged = false;
            UpdateTextWithTitle();

            
        }

        private void DarkTheme(object seder, EventArgs e)
        {
            textBox1.ForeColor = Color.White;
            textBox1.BackColor = Color.DimGray;
            menuStrip1.BackColor = Color.DarkGray;
        }

        private void LightTheme(object seder, EventArgs e)
        {
            textBox1.ForeColor = Color.Black;
            textBox1.BackColor = Color.White;
            menuStrip1.BackColor = Color.SeaShell;

        }


        private void DateNow(object sender, EventArgs e)
        {
            textBox1.Text += DateTime.Now;
        }
       

        public void CreateNewDocumenr(object sender, EventArgs e)
        {
            SaveUnsavedFile();
            textBox1.Text = "";
            filename = "";
            IsFileChanged = false;
            UpdateTextWithTitle();
        }


        public void OpenFile(object sender, EventArgs e)
        {
            SaveUnsavedFile();
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamReader sr = new StreamReader(openFileDialog1.FileName);
                    textBox1.Text = sr.ReadToEnd();
                    sr.Close();
                    filename = openFileDialog1.FileName;
                    IsFileChanged = false;
                }
                catch
                {
                    MessageBox.Show("Не удалось открыть файл");

                }
            }
            UpdateTextWithTitle();
        }


        public void SaveFile(string _filename)
        {
            if (_filename == "")
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    _filename = saveFileDialog1.FileName;
                }
            }
            try
            {
                StreamWriter sw = new StreamWriter(_filename + ".txt");
                sw.Write(textBox1.Text);
                sw.Close();
                filename = _filename;
                IsFileChanged = false;
            }
            catch
            {
                MessageBox.Show("Невозможно сохранить файл");
            }
            UpdateTextWithTitle();
        }
        public void Save(object sender, EventArgs e)
        {
            SaveFile(filename);
        }
        public void SaveAs(object sender, EventArgs e)
        {
            SaveFile("");
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            if (!IsFileChanged)
            {
                this.Text = "*" + this.Text.Replace('*', ' ');
                IsFileChanged = true;
            }
        }
        public void UpdateTextWithTitle()
        {
            if (filename != "")
                this.Text = filename + " - Notepad";
            else this.Text = "Безымянный - Notepad";
        }
        public void SaveUnsavedFile()
        {
            if (IsFileChanged)
            {
                DialogResult result = MessageBox.Show("Сохранть файл?", "Сохранение файла", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.Yes)
                {
                    SaveFile(filename);
                }
            }
        }
        public void CopyText()
        {
            Clipboard.SetText(textBox1.SelectedText);
        }
        public void CutText()
        {
            Clipboard.SetText(textBox1.SelectedText);
            textBox1.Text = textBox1.Text.Remove(textBox1.SelectionStart, textBox1.SelectionLength);
        }
        public void PasteText()
        {
            textBox1.Text = textBox1.Text.Substring(0, textBox1.SelectionStart) + Clipboard.GetText() + textBox1.Text.Substring(textBox1.SelectionStart, textBox1.Text.Length-textBox1.SelectionStart);

        }

        private void OnCopyClick(object sender, EventArgs e)
        {
            CopyText();
        }

        private void OnCutClick(object sender, EventArgs e)
        {
            CutText();  
        }

        private void OnPasteClick(object sender, EventArgs e)
        {
            PasteText();
        }

        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            SaveUnsavedFile();
        }

        private void OnFontClick(object sender, EventArgs e)
        {

            //FontDialog myFont = new FontDialog(); 
            //if(myFont.ShowDialog() == DialogResult.OK)
            //{
            //    textBox1.Font = myFont.Font;
            //}
            fontSetts = new FontSettings();
            fontSetts.Show();
        }

        private void OnFocus(object sender, EventArgs e)
        {
            if(fontSetts!= null)
            {
                fontSize = fontSetts.fontSize;
                fs = fontSetts.fs;
                textBox1.Font = new Font(textBox1.Font.FontFamily, fontSize, fs);
                fontSetts.Close();
            }
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aboutProgram = new AboutProgram();
            aboutProgram.Show();
        }
    }
}
