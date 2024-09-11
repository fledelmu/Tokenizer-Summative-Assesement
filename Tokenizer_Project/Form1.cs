using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TokenizerProject;

namespace Tokenizer_Project
{
    public partial class Form1 : Form
    {
        private Implementation t;
        private List<string> tokens;
        public Form1()
        {
            InitializeComponent();
            t = new Implementation();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void button1_Click(object sender, EventArgs e)
     {
         string inputText = textBox1.Text;
         tokens = t.Tokenize(inputText);

         dataGridView1.Rows.Clear();

            List<Tuple<string, string>> classifications = t.Classify(tokens);


            for (int i = 0; i < tokens.Count; i++)
         {
             string token = tokens[i];
             string classs = classifications[i].Item2;
             string granulatedString = t.GranularPhase(token);

             dataGridView1.Rows.Add(token, classs, granulatedString);
         }
    
     textBox1.Clear();
     }


    }
}
