using System;
using System.Windows.Forms;

namespace Dungeon
{
      
    public partial class SaveForm : Form
    {
          
        private bool m_go_save;

          
        public bool GoSave
        {
            get
            {
                return m_go_save;
            }
        }

          
        public SaveForm()
        {
            InitializeComponent();
        }

          
        private void SaveForm_Load(object sender, EventArgs e)
        {
            m_go_save = false;
            textBox_save_name.Text = (Owner as MainForm).SaveName;
        }

          
        private void textBox_save_name_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 'A' && e.KeyChar <= 'Z') ||
               (e.KeyChar >= 'a' && e.KeyChar <= 'z') ||
               (e.KeyChar >= 'А' && e.KeyChar <= 'Я') ||
               (e.KeyChar >= 'а' && e.KeyChar <= 'я') ||
               (e.KeyChar >= '0' && e.KeyChar <= '9') ||
                e.KeyChar == ' ' ||
                e.KeyChar == '_' ||
                e.KeyChar == '-' ||
                e.KeyChar == 8)
            {
                return;
            }
            else
            {
                e.KeyChar = '\0';
            }
        }

          
        private void textBox_save_name_TextChanged(object sender, EventArgs e)
        {
            if (textBox_save_name.Text != "")
            {
                button_save.Enabled = true;
            }
            else
            {
                button_save.Enabled = false;
            }
        }

          
        private void button_back_Click(object sender, EventArgs e)
        {
            m_go_save = false;
            Close();
        }

          
        private void button_save_Click(object sender, EventArgs e)
        {
            m_go_save = true;
            (Owner as MainForm).SaveName = textBox_save_name.Text;
            Close();
        }
    }
}
