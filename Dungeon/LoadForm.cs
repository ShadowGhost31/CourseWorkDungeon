using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Dungeon
{
      
    public partial class LoadForm : Form
    {
          
        private bool m_go_load;

          
        public bool GoLoad
        {
            get
            {
                return m_go_load;
            }
        }

          
        public LoadForm()
        {
            InitializeComponent();
        }

          
        private void LoadForm_Load(object sender, EventArgs e)
        {

            dataGridView_saves.ColumnCount = 7;   
            dataGridView_saves.RowCount = 0;   

              
            dataGridView_saves.Columns[0].Name = "Назва сейву";
            dataGridView_saves.Columns[1].Name = "Тяжкість";
            dataGridView_saves.Columns[2].Name = "Персонаж";
            dataGridView_saves.Columns[3].Name = "Рівень персонажу";
            dataGridView_saves.Columns[4].Name = "Ключ генерації підземелля";
            dataGridView_saves.Columns[5].Name = "Рівень підземелля";
            dataGridView_saves.Columns[6].Name = "Дата та час сейву";


            string folder_path = Path.GetDirectoryName(Application.ExecutablePath) + "\\data";
            if (!Directory.Exists(folder_path))
            {
                Directory.CreateDirectory(folder_path);
            }
            folder_path = Path.GetDirectoryName(Application.ExecutablePath) + "\\data\\saves";
            if (!Directory.Exists(folder_path))
            {
                Directory.CreateDirectory(folder_path);
            }
            folder_path = Path.GetDirectoryName(Application.ExecutablePath) + "\\data\\saves\\" + (Owner as MainForm).Nick;
            if (!Directory.Exists(folder_path))
            {
                Directory.CreateDirectory(folder_path);
            }
            string[] files = Directory.GetFiles("data\\saves\\" + (Owner as MainForm).Nick + "\\", "*.bin");

            dataGridView_saves.RowCount = files.Length;
            for (int i = 0; i < files.Length; i++)
            {
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();   
                    using (FileStream fs = new FileStream(files[i], FileMode.Open))   
                    {
                        string filename = (string)formatter.Deserialize(fs);
                        DungeonDifficulty total_difficulty_id = (DungeonDifficulty)formatter.Deserialize(fs);
                        string hero_name = (string)formatter.Deserialize(fs);
                        int hero_level = (int)formatter.Deserialize(fs);
                        string total_level_generation_number = (string)formatter.Deserialize(fs);
                        int hero_dungeon_level_id = (int)formatter.Deserialize(fs);
                        string datetime = (string)formatter.Deserialize(fs);
                        formatter.Deserialize(fs);   
                        DungeonInventoryStatus m_total_inventory_mode = (DungeonInventoryStatus)formatter.Deserialize(fs);
                        int m_id_clicked_cell = (int)formatter.Deserialize(fs);
                        string difficulty;
                        difficulty = "Легко";
                        dataGridView_saves.Rows[i].Cells[0].Value = filename;
                        dataGridView_saves.Rows[i].Cells[1].Value = difficulty;
                        dataGridView_saves.Rows[i].Cells[2].Value = hero_name;
                        dataGridView_saves.Rows[i].Cells[3].Value = hero_level;
                        dataGridView_saves.Rows[i].Cells[4].Value = total_level_generation_number;
                        dataGridView_saves.Rows[i].Cells[5].Value = (hero_dungeon_level_id + 1).ToString();
                        dataGridView_saves.Rows[i].Cells[6].Value = datetime;
                    }
                }
                catch
                {
                    MessageBox.Show("Помилка при зчитуванні!", "Рекорди", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (dataGridView_saves.RowCount == 0)
            {
                button_load.Enabled = false;
            }
            else
            {
                button_load.Enabled = true;
            }
        }

          
        private void button_back_Click(object sender, EventArgs e)
        {
            m_go_load = false;
            Close();
        }

          
        private void button_load_Click(object sender, EventArgs e)
        {
            m_go_load = true;
            (Owner as MainForm).SaveName = dataGridView_saves.SelectedRows[0].Cells[0].Value.ToString();
            Close();
        }
    }
}
