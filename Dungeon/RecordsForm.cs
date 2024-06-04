using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Dungeon
{
      
    public partial class RecordsForm : Form
    {
          
        public RecordsForm()
        {
            InitializeComponent();
        }

          
        private void RecordsForm_Load(object sender, EventArgs e)
        {
            radioButton_records_player.Checked = true;
            UpdateData();
        }

          
        private void radioButton_records_CheckedChanged(object sender, EventArgs e)
        {
            if ((radioButton_records_player.Checked && !radioButton_records_all.Checked) ||
               (!radioButton_records_player.Checked && radioButton_records_all.Checked))
            {
                UpdateData();
            }
        }

          
        private void button_back_Click(object sender, EventArgs e)
        {
            Close();
        }

          
        private void UpdateData()
        {
            dataGridView_records.ColumnCount = 11;   
            dataGridView_records.RowCount = 0;   

              
            dataGridView_records.Columns[0].Name = "Номер рекорду";
            dataGridView_records.Columns[1].Name = "Стан";
            dataGridView_records.Columns[2].Name = "Час проходження";
            dataGridView_records.Columns[3].Name = "Нік гравця";
            dataGridView_records.Columns[4].Name = "Складність";
            dataGridView_records.Columns[5].Name = "Персонаж";
            dataGridView_records.Columns[6].Name = "Досягнутий рівень персонажа";
            dataGridView_records.Columns[7].Name = "Ключ генерації світу";
            dataGridView_records.Columns[8].Name = "Пройдена відстань";
            dataGridView_records.Columns[9].Name = "Монстрів убито";
            dataGridView_records.Columns[10].Name = "Босів убито";

            try
            {
                string folder_path = Path.GetDirectoryName(Application.ExecutablePath) + "\\data";
                if (!Directory.Exists(folder_path))
                {
                    Directory.CreateDirectory(folder_path);
                }
                BinaryFormatter formatter = new BinaryFormatter();   
                using (FileStream fs = new FileStream("data\\records.bin", FileMode.OpenOrCreate))   
                {
                    if (fs.Length != 0)   
                    {
                        fs.Position = 0;   
                        int records_number = (int)formatter.Deserialize(fs);   
                        for (int i = 0; i < records_number; i++)   
                        {
                            int record_number = (int)formatter.Deserialize(fs);   
                            bool is_win = (bool)formatter.Deserialize(fs);   
                            int time = (int)formatter.Deserialize(fs);   
                            string nick = (string)formatter.Deserialize(fs);   
                            DungeonDifficulty difficulty_id = (DungeonDifficulty)formatter.Deserialize(fs);   
                            string character_name = (string)formatter.Deserialize(fs);   
                            int character_level = (int)formatter.Deserialize(fs);   
                            string world_key = (string)formatter.Deserialize(fs);   
                            double distance_walked = (double)formatter.Deserialize(fs);   
                            int monsters_killed = (int)formatter.Deserialize(fs);   
                            int bosses_killed = (int)formatter.Deserialize(fs);   

                            if (radioButton_records_all.Checked || (radioButton_records_player.Checked && nick == (Owner as MainForm).Nick))
                            {
                                dataGridView_records.Rows.Add();
                                int id = dataGridView_records.RowCount - 1;

                                  
                                dataGridView_records.Rows[id].Cells[0].Value = record_number;
                                string is_win_text;
                                if (is_win)
                                {
                                    is_win_text = "Перемога";
                                }
                                else
                                {
                                    is_win_text = "Поразка";
                                }
                                dataGridView_records.Rows[id].Cells[1].Value = is_win_text;
                                string time_text = "";
                                if (time / 60 / 60 < 10)
                                {
                                    time_text += "0";
                                }
                                time_text += (time / 60 / 60).ToString();
                                time_text += ":";
                                if ((time - time / 60 / 60 * 60) / 60 < 10) time_text += "0";
                                time_text += ((time - time / 60 / 60 * 60) / 60).ToString();
                                time_text += ":";
                                if (time - time / 60 / 60 * 60 - (time - time / 60 / 60 * 60) / 60 * 60 < 10) time_text += "0";
                                time_text += (time - time / 60 / 60 * 60 - (time - time / 60 / 60 * 60) / 60 * 60).ToString();
                                dataGridView_records.Rows[id].Cells[2].Value = time_text;
                                dataGridView_records.Rows[id].Cells[3].Value = nick;
                                string difficulty;
                                difficulty = "Легко";
                                dataGridView_records.Rows[id].Cells[1].Value = difficulty;
                                dataGridView_records.Rows[id].Cells[5].Value = character_name;
                                dataGridView_records.Rows[id].Cells[6].Value = character_level;
                                dataGridView_records.Rows[id].Cells[7].Value = world_key;
                                dataGridView_records.Rows[id].Cells[8].Value = distance_walked;
                                dataGridView_records.Rows[id].Cells[9].Value = monsters_killed;
                                dataGridView_records.Rows[id].Cells[10].Value = bosses_killed;
                            }
                        }
                    }
                    else   
                    {
                        dataGridView_records.RowCount = 0;   
                    }
                }
            }
            catch
            {
                MessageBox.Show("Помилка при читанні файлу!", "Рекорди", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void dataGridView_records_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
