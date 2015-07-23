using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AutoMerge
{
    static class FileWork
    {
        
        internal static void openAndReadFile(ref TextFile buffer)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "All files (*.*)|*.*";
            if(openFile.ShowDialog() == DialogResult.OK)
            {
                string[] tmp = File.ReadAllLines(openFile.FileName);
                buffer.text = new Queue<string>(tmp);
            }
        }

        internal static void saveFile(TextFile output)
        {
            
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "All files (*.*)|*.*";
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                if (output.text!=null)
                {
                    File.WriteAllLines(saveFile.FileName, output.getText());
                }
                
                //string[] tmp = File.ReadAllLines(openFile.FileName);
                //buffer.text = new Queue<string>(tmp);
            }
	{
		 
	}
        }
    }
}
