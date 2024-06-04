using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Dungeon
{
   
    public partial class LauncherForm : Form
    {
       
        private bool m_is_registration = false;

   
        private MainForm m_main_form;

  
        public LauncherForm()
        {
            InitializeComponent();
        }

   
        private void LauncherForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

      
        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
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

      
        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (textBox_nick.Text != "" && textBox_password.Text != "")
            {
                button_right.Enabled = true;
            }
            else
            {
                button_right.Enabled = false;
            }
        }

     
        private void button_left_Click(object sender, EventArgs e)
        {
            if (m_is_registration)
            {
                m_is_registration = false;
                Text = "Авторизація";
                label_main.Text = "Вхід до існуючого облікового запису";
                button_left.Text = "Реєстрація";
                button_right.Text = "Увійти";
                textBox_password.PasswordChar = '*';
            }
            else
            {
                m_is_registration = true;
                Text = "Реєстрація";
                label_main.Text = "Створення нового облікового запису";
                button_left.Text = "Авторизація";
                button_right.Text = "Створити";
                textBox_password.PasswordChar = '\0';
            }
        }

  
        private void button_right_Click(object sender, EventArgs e)
        {
            if (m_is_registration)
            {
                string nick = textBox_nick.Text; 

                int users_number = 0; 
                try
                {
                    string folder_path = Path.GetDirectoryName(Application.ExecutablePath) + "\\data";
                    if (!Directory.Exists(folder_path))
                    {
                        Directory.CreateDirectory(folder_path);
                    }
                    BinaryFormatter formatter = new BinaryFormatter(); 
                    using (FileStream fs = new FileStream("data\\users.bin", FileMode.OpenOrCreate)) 
                    {
                        if (fs.Length != 0) 
                        {
                            fs.Position = 0; 
                            users_number = (int)formatter.Deserialize(fs);
                            for (int i = 0; i < users_number; i++) 
                            {
                                string finded_nick = (string)formatter.Deserialize(fs); 
                                string finded_password_md5 = (string)formatter.Deserialize(fs);
                                if (finded_nick == nick) 
                                {
                                    MessageBox.Show("Користувач з таким логіном уже зареєстрований!", "Регістрація", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Помилка при зчитуванні!", "Регістрація", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                users_number++;

                string password = textBox_password.Text; 
                MD5 md5Hash = MD5.Create();
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                string password_md5 = sBuilder.ToString(); //MD5

                try
                {
                    BinaryFormatter formatter = new BinaryFormatter(); 
                    using (FileStream fs = new FileStream("data\\users.bin", FileMode.OpenOrCreate)) 
                    {
                        if (fs.Length != 0) 
                        {
                            fs.Position = 0; 
                            formatter.Serialize(fs, users_number); 
                            fs.Position = fs.Length; 
                        }
                        else 
                        {
                            formatter.Serialize(fs, 1); 
                        }
                        formatter.Serialize(fs, nick); 
                        formatter.Serialize(fs, password_md5); // пароль в файл
                    }
                }
                catch
                {
                    MessageBox.Show("Помилка при записі!", "Регістрація", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                textBox_password.Clear();

                Hide();

                MessageBox.Show("Ви успішно зарегіструвались!", "Регістрація", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                if (m_main_form == null || m_main_form.IsDisposed)
                {
                    m_main_form = new MainForm();
                    m_main_form.Owner = this;
                }
                else
                {
                    m_main_form.LoadForm();
                }

                m_main_form.IsRegistration = true;
                m_main_form.Nick = nick;

                m_main_form.Show();
            }
            else
            {
                string nick = textBox_nick.Text;

                string password = textBox_password.Text; // пароль
                MD5 md5Hash = MD5.Create();
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                string password_md5 = sBuilder.ToString(); //  MD5 

                try
                {
                    string folder_path = Path.GetDirectoryName(Application.ExecutablePath) + "\\data";
                    if (!Directory.Exists(folder_path))
                    {
                        Directory.CreateDirectory(folder_path);
                    }
                    BinaryFormatter formatter = new BinaryFormatter(); 
                    using (FileStream fs = new FileStream("data\\users.bin", FileMode.OpenOrCreate)) 
                    {
                        if (fs.Length != 0) 
                        {
                            fs.Position = 0; 
                            int users_number = (int)formatter.Deserialize(fs); 
                            bool is_finded = false; 
                            for (int i = 0; i < users_number; i++) // всі кор
                            {
                                string finded_nick = (string)formatter.Deserialize(fs); //ник
                                string finded_password_md5 = (string)formatter.Deserialize(fs);
                                if (finded_nick == nick) 
                                {
                                    if (finded_password_md5 == password_md5) 
                                    {
                                        is_finded = true;
                                        break;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Невірный пароль!", "Авторизація", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }
                                }
                            }
                            if (!is_finded)
                            {
                                MessageBox.Show("Введене ім'я не знайдене!", "Авторизація", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        else
                        {
                            formatter.Serialize(fs, 0); 
                            MessageBox.Show("Введене ім'я не знайдене!", "Авторизація", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Помилка при зчитуванні!", "Авторизація", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                textBox_password.Clear(); 

                Hide(); 

                MessageBox.Show("Ви успішно авторизувались!", "Авторизація", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);


                if (m_main_form == null || m_main_form.IsDisposed)
                {
                    m_main_form = new MainForm(); 
                    m_main_form.Owner = this;
                }
                else
                {
                    m_main_form.LoadForm();
                }

                m_main_form.IsRegistration = false;
                m_main_form.Nick = nick;

                m_main_form.Show();
            }
        }
    }
}
