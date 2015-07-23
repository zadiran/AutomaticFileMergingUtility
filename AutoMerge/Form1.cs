using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoMerge
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        TextFile source;
        TextFile mod1;
        TextFile mod2;
        TextFile output = new TextFile();

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        //открытие исходного файла
        private void FileSrcOpen(object sender, EventArgs e)
        {
            FileWork.openAndReadFile(ref source);
            richTextBox1.Clear();
            if (source.text != null)
            {
                Queue<string> tmp = new Queue<string>(source.text);
            
            foreach (string str in source.text)
            {
                richTextBox1.AppendText(str + "\n");
            }
           }
        }
        
        //открытие файла модификации1
        private void FileMod1Open(object sender, EventArgs e)
        {
            FileWork.openAndReadFile(ref mod1);
            richTextBox2.Clear();
            if (mod1.text != null)
            {
                Queue<string> tmp = new Queue<string>(mod1.text);
                foreach (string str in mod1.text)
                {
                    richTextBox2.AppendText(str + "\n");
                }
            }
            
        }
        
        //открытие файла модификации2
        private void FileMod2Open(object sender, EventArgs e)
        {
            FileWork.openAndReadFile(ref mod2);
            richTextBox3.Clear();
            if (mod2.text != null)
            {
                Queue<string> tmp = new Queue<string>(mod2.text);
                foreach (string str in mod2.text)
                {
                    richTextBox3.AppendText(str + "\n");
                }
            }
        }
        
        //запуск анализатора
        private void StartProcessing(object sender, EventArgs e)
        {
            if ((source.text != null) && (mod1.text != null) && (mod1.text != null))
            {
                output = Merge.buildOutputFile(source, mod1, mod2, 1);
                richTextBox4.Clear();
                foreach (string str in output.text)
                {
                    richTextBox4.AppendText(str + "\n");
                }
            }
        }
        private void StartHQProcessing(object sender, EventArgs e)
        {
            if ((source.text != null) && (mod1.text != null) && (mod1.text != null))
            {
                output = Merge.buildOutputFile(source, mod1, mod2, 2);
                richTextBox4.Clear();
                foreach (string str in output.text)
                {
                    richTextBox4.AppendText(str + "\n");
                }
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void SaveFile(object sender, EventArgs e)
        {
            FileWork.saveFile(output);
        }

        
    }
}
