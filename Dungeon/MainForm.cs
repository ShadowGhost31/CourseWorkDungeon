using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Dungeon
{
      
    public partial class MainForm : Form
    {
          

          
        public bool IsRegistration;

          
        public string Nick;

          
        private string m_total_level_generation_number;

          
        public string SaveName;

          
        private bool m_is_autosave;

          
        private DungeonFormStatus m_form_status;

         
        private bool m_is_form_loaded = false;

         
        private List<Keys> m_enabled_keys = new List<Keys>();

         
        private static readonly int dungeon_levels_number = 10;

         
        private static readonly string m_usage_font_name = "Segoe WP Black";

         
        private static readonly Size vignette_size = new Size(950, 950);

         
        private static readonly Size showing_size = new Size(vignette_size.Width * 15 / 10, vignette_size.Height * 15 / 10);

         
        public static Size ShowingSize
        {
            get
            {
                return showing_size;
            }
        }

         

         
        private bool m_can_minimize = true;

         
        private byte m_total_resolution_id;

         
        private static readonly int sounds_number = 5;

         
        private static readonly AudioEffect[] sounds = new AudioEffect[sounds_number];

         
         
         
        public static AudioEffect GetSound(int id)
        {
            if (id < 0 || id > sounds_number) id = 0;
            return sounds[id];
        }

         
        private static readonly int musics_number = 3;

         
        private static readonly AudioEffect[] musics = new AudioEffect[musics_number];

         
        private static int m_total_music_id;

         
        private static readonly Bitmap[,,] images_helmets = new Bitmap[10, 3, 5];

         
        public static Bitmap[,,] ImagesHelmets
        {
            get
            {
                return images_helmets;
            }
        }

         
        private static readonly Bitmap[,,] images_armours = new Bitmap[10, 3, 4];

         
        public static Bitmap[,,] ImagesArmours
        {
            get
            {
                return images_armours;
            }
        }

         
        private static readonly GifAnimation[,] images_swords_gif = new GifAnimation[30, 4];

         
        public static GifAnimation[,] ImagesSwordsGif
        {
            get
            {
                return images_swords_gif;
            }
        }

         
        private static readonly Bitmap[] images_swords = new Bitmap[30];

         
        public static Bitmap[] ImagesSwords
        {
            get
            {
                return images_swords;
            }
        }

         
        private static readonly byte audio_edit_value = 5;

         
        private static byte m_audio_volume_sound;

          
        public static byte VolumeSound
        {
            set
            {
                m_audio_volume_sound = value;
                for (int i = 0; i < sounds_number; i++)
                {
                    if (sounds[i] != null)
                    {
                        sounds[i].SetVolume(m_audio_volume_sound);
                    }
                }
            }
            get
            {
                return m_audio_volume_sound;
            }
        }

          
        private static byte m_audio_volume_music;

          
        public static byte VolumeMusic
        {
            set
            {
                m_audio_volume_music = value;
                for (int i = 0; i < musics_number; i++)
                {
                    if (musics[i] != null)
                    {
                        musics[i].SetVolume(m_audio_volume_music);
                    }
                }
            }
            get
            {
                return m_audio_volume_music;
            }
        }

         

         
        private DungeonInventoryStatus m_total_inventory_mode = DungeonInventoryStatus.None;

         
        private DungeonInventoryStatus m_last_inventory_mode;

         
        private int m_id_clicked_cell = -1;

         
        private Cursor m_total_cursor;

         
        private bool m_interface_is_show = true;

         
        private bool is_show_item_info = true;

         
        private bool is_game_menu = false;

         
        public bool IsGamePaused
        {
            get
            {
                return is_game_menu;
            }
        }

         
        private bool is_code_insert = false;

         
        private bool is_game_saved = false;

         
        private int m_id_level_showing_map = -1;

         
        private int m_showing_map_cells_number_in_line;

         
        private static readonly int minimap_cells_number_in_line = 18;

         
        private bool m_interface_is_show_vignette;

         
        private bool m_is_show_blood_effect_on_hit;

         
        public bool IsShowBloodEffectOnHit
        {
            get
            {
                return m_is_show_blood_effect_on_hit;
            }
        }

         
        private bool m_interface_is_show_hero_level;

         
        private bool m_interface_is_show_stats;

         
        private bool m_interface_is_show_dungeon_level;

         
        private bool m_interface_is_show_potions;

         
        private bool m_interface_is_show_buttons;

         
        private bool m_interface_is_show_minimap;

         

         
        private static DungeonLevel[] m_dungeon_levels;

         
        private DungeonHero m_hero;

         
        public DungeonHero Hero
        {
            get
            {
                return m_hero;
            }
        }

         
        private byte choosed_difficulty_id;

         
        private byte total_difficulty_id;

         
        private byte choosed_character_id;

         

         
         
         
         
        private int length_1;
        private int length_2;
        private int length_4;
        private int length_8;
        private int length_20;

         
        private Rectangle interface_rectangle_level;

         
        private Rectangle[] interface_rectangle_level_text = new Rectangle[3];

         
        private Rectangle interface_rectangle_stats_health;

         
        private Rectangle interface_rectangle_stats_total_health;

         
        private Rectangle interface_rectangle_stats_number_health;

         
        private Rectangle interface_rectangle_stats_energy;

         
        private Rectangle interface_rectangle_stats_total_energy;

         
        private Rectangle interface_rectangle_stats_number_energy;

         
        private Rectangle interface_rectangle_dungeon_level;

         
        private Rectangle interface_rectangle_inventory;

         
        private Rectangle interface_rectangle_statistic;

         
        private Rectangle interface_rectangle_map;
        private Rectangle[,] interface_rectangle_map_cells;

        private Rectangle[,] interface_rectangle_map_levels = new Rectangle[2, 5];
        private bool[,] interface_is_entered_map_levels = new bool[2, 5];

         
        private Rectangle[] interface_rectangle_inventory_cells = new Rectangle[30];
        private bool[] interface_is_entered_inventory_cells = new bool[30];

         
        private Rectangle[] interface_rectangle_inventory_special_cells_texts = new Rectangle[special_items_number_first_row + special_items_number_second_row * 2];

         
        private static readonly int special_items_number_first_row = 4;
        private static readonly int special_items_number_second_row = 3;
        private static readonly int special_items_number = special_items_number_first_row + special_items_number_second_row;
        private Rectangle[] interface_rectangle_inventory_special_cells = new Rectangle[special_items_number];
        private bool[] interface_is_entered_inventory_special_cells = new bool[special_items_number];

         
        private Rectangle interface_rectangle_inventory_item_name;

         
        private Rectangle interface_rectangle_inventory_item_description;

         
        private Rectangle interface_rectangle_inventory_item_info_button;
        private bool interface_is_entered_inventory_item_info_button;

         
        private Rectangle[] interface_rectangle_inventory_item_buttons = new Rectangle[3];
        private bool[] interface_is_entered_inventory_item_button = new bool[3];

         
        private Rectangle interface_rectangle_inventory_button;
        private bool interface_is_entered_inventory_button;

         
        private Rectangle interface_rectangle_statistic_button;
        private bool interface_is_entered_statistic_button;

         
        private Rectangle interface_rectangle_map_button;
        private bool interface_is_entered_map_button;

         
        private Rectangle interface_rectangle_left_side_window;

         
        private Rectangle[] interface_rectangle_statistic_texts = new Rectangle[3];

         
        private Rectangle[] interface_rectangle_statistic_stats_texts = new Rectangle[9];

         
        private Rectangle[] interface_rectangle_statistic_stats_values = new Rectangle[9];

         
        private Rectangle[,] interface_rectangle_statistic_stats_blocks = new Rectangle[9, 10];

         
        private Rectangle[] interface_rectangle_statistic_stats_buttons = new Rectangle[9];

         
        private Rectangle interface_rectangle_minimap;
        private Rectangle[,] interface_rectangle_minimap_cells = new Rectangle[minimap_cells_number_in_line, minimap_cells_number_in_line];

         
        private Rectangle interface_rectangle_active_potions_headers;
        private Rectangle[] interface_rectangle_active_potions_text;
        private string[] showing_potions_text;
        private DungeonStats[] effects_stats;
        private int showing_potions_number = 0;
         

         
         
         
         
        private Rectangle interface_main_menu_rectangle_background;

         
        private Rectangle interface_main_menu_rectangle_logo;

         
        private Rectangle interface_main_menu_rectangle_text;

         
        private Rectangle interface_main_menu_rectangle_game_button;
        private bool interface_main_menu_is_entered_game_button;

         
        private Rectangle interface_main_menu_rectangle_rules_button;
        private bool interface_main_menu_is_entered_rules_button;

         
        private Rectangle interface_main_menu_rectangle_records_button;
        private bool interface_main_menu_is_entered_records_button;

         
        private Rectangle interface_main_menu_rectangle_settings_button;
        private bool interface_main_menu_is_entered_settings_button;

         
        private Rectangle interface_main_menu_rectangle_exit_button;
        private bool interface_main_menu_is_entered_exit_button;
         

         
         
         
         
        Rectangle interface_confirm_exit_rectangle_text;

         
        private Rectangle interface_confirm_exit_rectangle_confirm_button;
        private bool interface_confirm_exit_is_entered_confirm_button;

         
        private Rectangle interface_confirm_exit_rectangle_cancel_button;
        private bool interface_confirm_exit_is_entered_cancel_button;
         

         
         
         
        private static readonly byte rules_pages_number = 5;
        private static readonly string[] rules_texts = new string[rules_pages_number];
        private byte rules_total_page;

         
        private Rectangle rules_rectangle_text;

         
        private Rectangle rules_rectangle_arrow_left;
        private bool rules_is_entered_arrow_left;

         
        private Rectangle rules_rectangle_arrow_right;
        private bool rules_is_entered_arrow_right;
         

         
         
         
         
        private Rectangle[] settings_rectangle_buttons = new Rectangle[6];
        private bool[] settings_is_entered_buttons = new bool[6];
         

         
         
         
        private static readonly byte settings_screen_number = 8;
        private int[] resolutions_width = new int[settings_screen_number];
        private int[] resolutions_height = new int[settings_screen_number];

         
        private Rectangle settings_screen_rectangle_main_text;

         
        private Rectangle[] settings_screen_rectangle_radiobuttons = new Rectangle[settings_screen_number];
        private bool[] settings_screen_is_entered_radiobuttons = new bool[settings_screen_number];

         
        private Rectangle[] settings_screen_rectangle_texts = new Rectangle[settings_screen_number];
         

         
         
         
        private static readonly byte settings_other_number = 3;

         
        private Rectangle[] settings_other_rectangle_checkboxes = new Rectangle[settings_other_number];
        private bool[] settings_other_is_entered_checkboxes = new bool[settings_other_number];

         
        private Rectangle[] settings_other_rectangle_texts = new Rectangle[settings_other_number];
         

         
         
         
        private static readonly byte settings_audio_number = 2;

         
        private Rectangle[] settings_audio_rectangle_texts = new Rectangle[settings_audio_number];

         
        private Rectangle[] settings_audio_rectangle_buttons_minus = new Rectangle[settings_audio_number];
        private bool[] settings_audio_is_entered_buttons_minus = new bool[settings_audio_number];

         
        private Rectangle[] settings_audio_rectangle_values = new Rectangle[settings_audio_number];

         
        private Rectangle[] settings_audio_rectangle_buttons_plus = new Rectangle[settings_audio_number];
        private bool[] settings_audio_is_entered_buttons_plus = new bool[settings_audio_number];
         

         
         
         
        private static readonly byte settings_interface_number = 6;
        private bool[] settings_interface_checkboxes_choosed_ids = new bool[settings_interface_number];
        private string[] settings_interface_checkboxes_texts = new string[settings_interface_number];

         
        private Rectangle[] settings_interface_rectangle_checkboxes = new Rectangle[settings_interface_number];
        private bool[] settings_interface_is_entered_checkboxes = new bool[settings_interface_number];

         
        private Rectangle[] settings_interface_rectangle_texts = new Rectangle[settings_interface_number];
         

         
         
         
        private static readonly byte keys_number = 24;  
        private static readonly byte keys_id_first_potion = 13;
        private Keys[] settings_controls_keys_choosed_keys = new Keys[keys_number];
        private string[] settings_controls_keys_texts = new string[keys_number];

        private int id_changing_key = -1;

         
        private Rectangle settings_controls_rectangle_main_text;

         
        private Rectangle[] settings_controls_rectangle_buttons = new Rectangle[keys_number];
        private bool[] settings_controls_is_entered_buttons = new bool[keys_number];

         
        private Rectangle[] settings_controls_rectangle_texts = new Rectangle[keys_number];
         

         
         
         
        private static readonly byte difficulties_number = 4;

         
        private Rectangle new_game_difficulty_rectangle_difficulty_header;
        private string[] difficulties_headers = new string[difficulties_number];

         
        private Rectangle new_game_difficulty_rectangle_difficulty_description;
        private string[] difficulties_texts = new string[difficulties_number];

         
        private Rectangle new_game_difficulty_rectangle_arrow_left;
        private bool new_game_difficulty_is_entered_arrow_left;

         
        private Rectangle new_game_difficulty_rectangle_arrow_right;
        private bool new_game_difficulty_is_entered_arrow_right;

         
        private Rectangle new_game_difficulty_rectangle_button_next;
        private bool new_game_difficulty_is_entered_button_next;

         
        private Rectangle new_game_difficulty_rectangle_button_back;
        private bool new_game_difficulty_is_entered_button_back;
         

         
         
         
        private static readonly byte characters_number = 4;

         
        private Rectangle new_game_character_rectangle_character_header;
        private string[] characters_names = new string[characters_number];

         
        private Rectangle new_game_character_rectangle_character_image;
        private Bitmap[] characters_textures = new Bitmap[characters_number];
        private DungeonHero[] characters = new DungeonHero[characters_number];

         
        private Rectangle new_game_character_rectangle_character_items_text;

         
        private const int character_start_items = 3;
        private Rectangle[] new_game_character_rectangle_character_items = new Rectangle[character_start_items];
        private DungeonItem[,] characters_items = new DungeonItem[characters_number, character_start_items];

         
        private Rectangle new_game_character_rectangle_character_description;
        private string[] characters_texts = new string[characters_number];

         
        private Rectangle new_game_character_rectangle_character_stats;
        private double[,] characters_stats = new double[characters_number, 9];

         
        private Rectangle new_game_character_rectangle_arrow_left;
        private bool new_game_character_is_entered_arrow_left;

         
        private Rectangle new_game_character_rectangle_arrow_right;
        private bool new_game_character_is_entered_arrow_right;
         

         
         
         
        private static readonly byte level_generation_nums_number = 8;

         
        private Rectangle[] new_game_level_generation_rectangle_arrows_up = new Rectangle[level_generation_nums_number];
        private bool[] new_game_level_generation_is_entered_arrows_up = new bool[level_generation_nums_number];

         
        private Rectangle[] new_game_level_generation_rectangle_values = new Rectangle[level_generation_nums_number];
        private byte[] choosed_level_generation_nums_values = new byte[level_generation_nums_number];

         
        private Rectangle[] new_game_level_generation_rectangle_arrows_down = new Rectangle[level_generation_nums_number];
        private bool[] new_game_level_generation_is_entered_arrows_down = new bool[level_generation_nums_number];

         
        private Rectangle new_game_level_generation_rectangle_button;
        private bool new_game_level_generation_is_entered_button;

         
        private Rectangle new_game_level_generation_rectangle_button_reset;
        private bool new_game_level_generation_is_entered_button_reset;

         
        private int world_key_number;
        private Random world_key;
         

         
         
         
         
        private Rectangle in_game_menu_rectangle_background;
         

         
         
         
        private static int loading_chapters;
        private static int loading_chapters_total;
        private string loading_text_procent;
        private string loading_text;

         
        private Rectangle loading_rectangle_text;

         
        private Rectangle loading_rectangle_bar;
        private Rectangle loading_rectangle_bar_total;
         

         
         
         
        private DungeonDoor m_door_code_opened;

         
        private Rectangle code_rectangle_background;

         
        private Rectangle code_rectangle_text;

         
        private static readonly byte code_nums_number = 4;
         
        private Rectangle[] code_rectangle_arrows_up = new Rectangle[code_nums_number];
        private bool[] code_is_entered_arrows_up = new bool[code_nums_number];

         
        private Rectangle[] code_rectangle_values = new Rectangle[code_nums_number];
        private byte[] choosed_code_nums_values = new byte[code_nums_number];

         
        private Rectangle[] code_rectangle_arrows_down = new Rectangle[code_nums_number];
        private bool[] code_is_entered_arrows_down = new bool[code_nums_number];

         
        private Rectangle code_rectangle_button_back;
        private bool code_is_entered_button_back;

         
        private Rectangle code_rectangle_button_open;
        private bool code_is_entered_button_open;
         

         
         
         
        private static readonly byte result_number = 9;

         
        private Rectangle result_rectangle_main_text;

         
        private Rectangle[] result_rectangle_texts = new Rectangle[result_number];
        private string[] m_result_text = new string[result_number];

         
        private Rectangle result_rectangle_button_load;
        private bool result_is_entered_button_load;

         
        private Rectangle result_rectangle_button_go_menu;
        private bool result_is_entered_button_go_menu;
         

         
        public MainForm()
        {
            InitializeComponent();
        }

         
        private void MainForm_Load(object sender, EventArgs e)
        {
            rules_texts[0] = "Гра складається з десяти рівнів, на кожному з яких розташовані кімнати та коридори для переміщення між ними.";
            rules_texts[0] += "Розмір та кількість кімнат на рівні залежить від вибраного рівня складності. У кімнатах можуть зустрічатися скрині з";
            rules_texts[0] += "предметів і монстрів. Кімнати можуть бути зачинені дверима, ключі від яких знаходяться у монстрів. На рівні також";
            rules_texts[0] += "є бонусна кімната, яка зачинена дверима з кодовим замком. Підказки до кодового замку знаходяться в записках, ";
            rules_texts[0] += "які можна знайти в скринях. На рівні також є кімната боса, а за нею - перехід на наступний рівень.";
            rules_texts[0] += "Мета гри - вбити останнього боса і отримати його таємничий артефакт.";

            rules_texts[1] = "Персонаж має 9 характеристик:\n";
            rules_texts[1] += "1) Максимальне здоров'я персонажа;";
            rules_texts[1] += "2) Максимальна енергія - енергія витрачається удар і ходьбу; ";
            rules_texts[1] += "3) Інтелект - що вищий інтелект, то більше вписувалося очок досвіду отримує персонаж при вбивстві монстра; ";
            rules_texts[1] += "4) Регенерація - швидкість відновлення здоров'я;";
            rules_texts[1] += "5) Відновлення - швидкість відновлення енергії;";
            rules_texts[1] += "6) Швидкість персонажа; ";
            rules_texts[1] += "7) Сила удару; ";
            rules_texts[1] += "8) Спритність - що вищий спритність, тим менше енергії витрачається; ";
            rules_texts[1] += "9) Удача - від удачі залежить кількість предметів, що випадають із скринь і монстрів, а також збільшується ймовірність отримати більше очок скіла при підвищенні рівня.";

            rules_texts[2] = "Для прокачування персонажа потрібні окуляри скілла.";
            rules_texts[2] += "Їх можна отримати при підвищенні рівня, кількість одержуваних очок залежить від успіху персонажа.";
            rules_texts[2] += "Для підвищення рівня персонажа потрібні окуляри досвіду.";
            rules_texts[2] += "Коли кількість очок досвіду стає більше або дорівнює 10, рівень персонажа підвищується.";
            rules_texts[2] += "Окуляри досвіду даються за вбивство монстрів, відкриття скринь і відкриття дверей.";

            rules_texts[3] = "У грі є кілька типів предметів:\n";
            rules_texts[3] += "Екіпірування персонажа (шолом, броня, меч, зілля, артефакт) - змінюють характеристики персонажа.";
            rules_texts[3] += "Ключі - зберігаються у монстрів, кожен ключ відкриває одні двері, того ж кольору, що і ключ.";
            rules_texts[3] += "Записки - містять підказки для кодового замку від дверей, що ведуть до бонусної кімнати.";

            rules_texts[4] = "Екіпірування персонажа:\n";
            rules_texts[4] += "Шолом - збільшує максимальну енергію персонажа, може зменшувати швидкість пересування, а також підвищувати/знижувати відновлення.";
            rules_texts[4] += "Броня - збільшує максимальне здоров'я персонажа, може зменшувати швидкість пересування, а також підвищувати/знижувати регенерацію.";
            rules_texts[4] += "Меч - збільшує силу персонажа, може зменшувати спритність.";
            rules_texts[4] += "Зілля - можуть давати позитивні або негативні ефекти для 1-3 характеристик одночасно. Діють протягом деякого часу.";
            rules_texts[4] += "Артефакти - можуть давати позитивні або негативні ефекти для 1-3 характеристик одночасно. Діють нескінченно. Можна екіпірувати лише один артефакт.";

             
             
             
            resolutions_width[1] = 800; resolutions_height[1] = 600;
            resolutions_width[2] = 1024; resolutions_height[2] = 768;
            resolutions_width[3] = 1280; resolutions_height[3] = 1024;
            resolutions_width[4] = 1366; resolutions_height[4] = 768;
            resolutions_width[5] = 1440; resolutions_height[5] = 900;
            resolutions_width[6] = 1600; resolutions_height[6] = 900;
            resolutions_width[7] = 1920; resolutions_height[7] = 1080;
             

             
             
             
            difficulties_headers[0] = "Легко";
            difficulties_texts[0] = "Для новачків.\nРівні невеликого розміру, монстри слабкі, шанс випадання предметів високий.";


            BackColor = Color.Black;  

            CalculateInterface();

            m_form_status = DungeonFormStatus.MainLoading;

            SetLoadingChapters(5);

            LoadAll();

            Refresh();
        }

         
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
            Owner.Show();
            Owner.Activate();
        }

         
        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
             
            if (Properties.Settings.Default.settings_screen_resolution_id != 0 && WindowState == FormWindowState.Maximized)
            {
                 
            }
             
            else if (Properties.Settings.Default.settings_screen_resolution_id == 0 && WindowState == FormWindowState.Normal)
            {
                for (byte i = 1; i < settings_screen_number; i++)
                {
                    if (Width == resolutions_width[i])
                    {
                        Properties.Settings.Default.settings_screen_resolution_id = i;  
                        break;
                    }
                }
                if (Properties.Settings.Default.settings_screen_resolution_id == 0)  
                {
                    Properties.Settings.Default.settings_screen_resolution_id = 1;  
                }
            }
            CalculateInterface();
        }

         
        private void MainForm_Activated(object sender, EventArgs e)
        {
            if (m_total_resolution_id == 0)  
            {
                TopMost = true;
            }
            else
            {
                TopMost = false;
            }
            if (m_is_form_loaded && musics[m_total_music_id].IsPaused)
            {
                musics[m_total_music_id].Play();
            }
        }

         
        private void MainForm_Deactivate(object sender, EventArgs e)
        {
            if (m_can_minimize)
            {
                TopMost = false;
                if (m_total_resolution_id == 0)  
                {
                    WindowState = FormWindowState.Minimized;
                }
                if (m_form_status == DungeonFormStatus.Game) PauseGame();
                m_enabled_keys.Clear();
                ResetAllButtonsIsEntryValue();
                Refresh();
                if (m_is_form_loaded && !musics[m_total_music_id].IsPaused)
                {
                    musics[m_total_music_id].Pause();
                }
            }
        }

         
        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (m_enabled_keys.IndexOf(e.KeyCode) == -1)
            {
                if (m_form_status != DungeonFormStatus.Loading && m_form_status != DungeonFormStatus.MainLoading && m_form_status != DungeonFormStatus.Saving)
                {
                    m_enabled_keys.Add(e.KeyCode);  
                }
                else return;

                if (m_form_status == DungeonFormStatus.LoadingReady)  
                {
                    is_game_menu = false;
                    musics[m_total_music_id].Stop();
                    ChooseRandomMusicId();
                    musics[m_total_music_id].Play();
                    ResumeGame();
                    return;
                }
                else if (m_form_status == DungeonFormStatus.MainLoadingReady)
                {
                    m_total_music_id = 0;
                    musics[m_total_music_id].Play();
                    m_form_status = DungeonFormStatus.Menu;
                    Refresh();
                    return;
                }
                else if (m_form_status == DungeonFormStatus.Game)
                {
                     
                    if (e.KeyCode == Properties.Settings.Default.settings_controls_inventory || e.KeyCode == Keys.NumPad1)
                    {
                        if (is_code_insert)
                        {
                            is_code_insert = false;
                        }
                        m_id_clicked_cell = -1;  
                        if (m_total_inventory_mode == DungeonInventoryStatus.Inventory)  
                        {
                            m_total_inventory_mode = DungeonInventoryStatus.None;  
                        }
                        else  
                        {
                            m_total_inventory_mode = DungeonInventoryStatus.Inventory;  
                        }
                    }

                     
                    if (e.KeyCode == Properties.Settings.Default.settings_controls_statistic || e.KeyCode == Keys.NumPad2)
                    {
                        if (is_code_insert)
                        {
                            is_code_insert = false;
                        }
                        if (m_total_inventory_mode == DungeonInventoryStatus.Statistic)  
                        {
                            m_total_inventory_mode = DungeonInventoryStatus.None;  
                        }
                        else  
                        {
                            m_total_inventory_mode = DungeonInventoryStatus.Statistic;  
                        }
                    }

                     
                    if (e.KeyCode == Properties.Settings.Default.settings_controls_map || e.KeyCode == Keys.NumPad3)
                    {
                        if (is_code_insert)
                        {
                            is_code_insert = false;
                        }
                        m_id_level_showing_map = m_hero.DungeonLevel.Id;
                        if (m_total_inventory_mode == DungeonInventoryStatus.Map)  
                        {
                            m_total_inventory_mode = DungeonInventoryStatus.None;  
                        }
                        else  
                        {
                            m_total_inventory_mode = DungeonInventoryStatus.Map;  
                        }
                    }

                     
                    if (e.KeyCode == Properties.Settings.Default.settings_controls_action_pickup_item)
                    {
                        m_hero.PickUpItem();
                    }

                     
                    if (e.KeyCode == Properties.Settings.Default.settings_controls_choose_up)
                    {
                        if (m_id_clicked_cell == -1)
                        {
                            m_id_clicked_cell = 20;
                        }
                        else
                        {
                            if (m_id_clicked_cell >= 10 && m_id_clicked_cell < 30)
                            {
                                m_id_clicked_cell -= 10;
                            }
                            else if (m_id_clicked_cell >= 34 && m_id_clicked_cell < 37)
                            {
                                m_id_clicked_cell -= 4;
                            }
                            else
                            {
                                m_id_clicked_cell = -1;
                            }
                        }
                    }

                     
                    if (e.KeyCode == Properties.Settings.Default.settings_controls_choose_down)
                    {
                        if (m_id_clicked_cell == -1)
                        {
                            m_id_clicked_cell = 0;
                        }
                        else
                        {
                            if (m_id_clicked_cell >= 0 && m_id_clicked_cell < 20)
                            {
                                m_id_clicked_cell += 10;
                            }
                            else if (m_id_clicked_cell >= 30 && m_id_clicked_cell < 33)
                            {
                                m_id_clicked_cell += 4;
                            }
                            else if (m_id_clicked_cell == 33)
                            {
                                m_id_clicked_cell += 3;
                            }
                            else
                            {
                                m_id_clicked_cell = -1;
                            }
                        }
                    }

                     
                    if (e.KeyCode == Properties.Settings.Default.settings_controls_choose_left)
                    {
                        if (m_id_clicked_cell == -1)
                        {
                            m_id_clicked_cell = 9;
                        }
                        else
                        {
                            if (m_id_clicked_cell != 30 && m_id_clicked_cell != 34)
                            {
                                if (m_id_clicked_cell == 0 || m_id_clicked_cell == 10)
                                {
                                    m_id_clicked_cell = 33;
                                }
                                else if (m_id_clicked_cell == 20)
                                {
                                    m_id_clicked_cell = 36;
                                }
                                else
                                {
                                    m_id_clicked_cell -= 1;
                                }
                            }
                            else
                            {
                                m_id_clicked_cell = -1;
                            }
                        }
                    }

                     
                    if (e.KeyCode == Properties.Settings.Default.settings_controls_choose_right)
                    {
                        if (m_id_clicked_cell == -1)
                        {
                            m_id_clicked_cell = 0;
                        }
                        else
                        {
                            if (m_id_clicked_cell != 9 && m_id_clicked_cell != 19 && m_id_clicked_cell != 29)
                            {
                                if (m_id_clicked_cell == 33)
                                {
                                    m_id_clicked_cell = 0;
                                }
                                else if (m_id_clicked_cell == 36)
                                {
                                    m_id_clicked_cell = 20;
                                }
                                else
                                {
                                    m_id_clicked_cell += 1;
                                }
                            }
                            else
                            {
                                m_id_clicked_cell = -1;
                            }
                        }
                    }

                     
                    if (e.KeyCode == Properties.Settings.Default.settings_controls_item_use)
                    {
                        if (m_id_clicked_cell >= 0 && m_id_clicked_cell < 30)
                        {
                            m_hero.Use(m_hero.Container.Items[m_id_clicked_cell]);
                        }
                        else if (m_id_clicked_cell >= 30 && m_id_clicked_cell < 37)
                        {
                            m_hero.Use(m_hero.ContainerSpecialItems.Items[m_id_clicked_cell]);
                        }
                    }

                     
                    if (e.KeyCode == Properties.Settings.Default.settings_controls_item_drop)
                    {
                        if (m_id_clicked_cell >= 0 && m_id_clicked_cell < 30)
                        {
                            m_hero.Drop(m_hero.Container.Items[m_id_clicked_cell]);
                        }
                        else if (m_id_clicked_cell >= 30 && m_id_clicked_cell < 37)
                        {
                            m_hero.Drop(m_hero.ContainerSpecialItems.Items[m_id_clicked_cell]);
                        }
                    }

                     
                    if (e.KeyCode == Properties.Settings.Default.settings_controls_item_destroy)
                    {
                        if (m_id_clicked_cell >= 0 && m_id_clicked_cell < 30)
                        {
                            m_hero.Destroy(m_hero.Container.Items[m_id_clicked_cell]);
                        }
                        else if (m_id_clicked_cell >= 30 && m_id_clicked_cell < 37)
                        {
                            m_hero.Destroy(m_hero.ContainerSpecialItems.Items[m_id_clicked_cell]);
                        }
                    }

                     
                    if (e.KeyCode == Properties.Settings.Default.settings_controls_use_potion_1)
                    {
                        m_hero.Use(m_hero.ContainerSpecialItems.Items[4]);
                    }

                     
                    if (e.KeyCode == Properties.Settings.Default.settings_controls_use_potion_2)
                    {
                        m_hero.Use(m_hero.ContainerSpecialItems.Items[5]);
                    }

                     
                    if (e.KeyCode == Properties.Settings.Default.settings_controls_use_potion_3)
                    {
                        m_hero.Use(m_hero.ContainerSpecialItems.Items[6]);
                    }

                     
                    if (e.KeyCode == Properties.Settings.Default.settings_controls_item_info)
                    {
                        is_show_item_info = !is_show_item_info;
                    }

                     
                    if (e.KeyCode == Properties.Settings.Default.settings_controls_open_ingame_menu)
                    {
                        PauseGame();
                        Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y);  
                        return;
                    }

                     
                    if (e.KeyCode == Properties.Settings.Default.settings_controls_hide_interface)
                    {
                        m_interface_is_show = !m_interface_is_show;
                    }

                     
                    if (e.KeyCode == Properties.Settings.Default.settings_controls_game_save)
                    {
                        is_game_menu = true;
                        StopTimers();
                        m_hero.DungeonLevel.PauseMonstersActions();
                        JustSave();
                    }

                     
                    if (e.KeyCode == Properties.Settings.Default.settings_controls_game_load)
                    {
                        is_game_menu = true;
                        StopTimers();
                        m_hero.DungeonLevel.PauseMonstersActions();
                        if (is_game_saved)
                        {
                            JustLoad();
                        }
                        else
                        {
                            m_form_status = DungeonFormStatus.InGameMenuConfirmLoad;
                        }
                    }
                }
                else if (m_form_status == DungeonFormStatus.InGameMenu)
                {
                     
                    if (e.KeyCode == Properties.Settings.Default.settings_controls_open_ingame_menu)
                    {
                        ResumeGame();
                         
                        return;
                    }
                }
                else if (m_form_status == DungeonFormStatus.SettingsControls)  
                {
                    if (id_changing_key != -1)
                    {
                        if (e.KeyCode != Keys.None)
                        {
                             
                            for (int i = 0; i < keys_number; i++)
                            {
                                if (settings_controls_keys_choosed_keys[i] == e.KeyCode)
                                {
                                    settings_controls_keys_choosed_keys[i] = Keys.None;
                                }
                            }

                             
                            settings_controls_keys_choosed_keys[id_changing_key] = e.KeyCode;

                            id_changing_key = -1;

                            SetAndSaveSettingsControls();

                            Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y);  
                        }
                    }
                }

                if (e.KeyCode == Keys.Escape)
                {
                    if (m_form_status == DungeonFormStatus.Menu)
                    {
                        m_form_status = DungeonFormStatus.ConfirmExit;
                    }
                    else if (m_form_status == DungeonFormStatus.InGameMenu)
                    {
                        ResumeGame();
                    }
                    else if (m_form_status == DungeonFormStatus.InGameMenuConfirmExit)
                    {
                        m_form_status = DungeonFormStatus.InGameMenu;
                    }
                    else if (m_form_status == DungeonFormStatus.InGameMenuConfirmLoad)
                    {
                        m_form_status = DungeonFormStatus.InGameMenu;
                    }
                    else if (m_form_status == DungeonFormStatus.NewGameStart)
                    {
                        m_form_status = DungeonFormStatus.Menu;
                    }
                    else if (m_form_status == DungeonFormStatus.NewGameDifficulty)
                    {
                        m_form_status = DungeonFormStatus.NewGameStart;
                    }
                    else if (m_form_status == DungeonFormStatus.NewGameCharacter)
                    {
                        m_form_status = DungeonFormStatus.NewGameDifficulty;
                    }
                    else if (m_form_status == DungeonFormStatus.NewGameLevelGenerate)
                    {
                        m_form_status = DungeonFormStatus.NewGameCharacter;
                    }
                    else if (m_form_status == DungeonFormStatus.Game)
                    {
                        if (is_code_insert)
                        {
                            HideCodeInsert();
                        }
                        else if (m_total_inventory_mode != DungeonInventoryStatus.None)
                        {
                            m_total_inventory_mode = DungeonInventoryStatus.None;
                        }
                        else
                        {
                            PauseGame();
                        }
                    }
                    else if (m_form_status == DungeonFormStatus.Rules)
                    {
                        if (is_game_menu)
                        {
                            m_form_status = DungeonFormStatus.InGameMenu;
                        }
                        else
                        {
                            m_form_status = DungeonFormStatus.Menu;
                        }
                    }
                    else if (m_form_status == DungeonFormStatus.Settings)
                    {
                        if (is_game_menu)
                        {
                            m_form_status = DungeonFormStatus.InGameMenu;
                        }
                        else
                        {
                            m_form_status = DungeonFormStatus.Menu;
                        }
                    }
                    else if (m_form_status == DungeonFormStatus.SettingsScreen)
                    {
                        m_form_status = DungeonFormStatus.Settings;
                    }
                    else if (m_form_status == DungeonFormStatus.SettingsOther)
                    {
                        m_form_status = DungeonFormStatus.Settings;
                    }
                    else if (m_form_status == DungeonFormStatus.SettingsAudio)
                    {
                        m_form_status = DungeonFormStatus.Settings;
                    }
                    else if (m_form_status == DungeonFormStatus.SettingsInterface)
                    {
                        m_form_status = DungeonFormStatus.Settings;
                    }
                    else if (m_form_status == DungeonFormStatus.SettingsControls)
                    {
                        m_form_status = DungeonFormStatus.Settings;
                    }
                    else if (m_form_status == DungeonFormStatus.ConfirmExit)
                    {
                        m_form_status = DungeonFormStatus.Menu;
                    }

                    Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y);  
                }
            }
        }

         
        private void GameForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (m_enabled_keys.IndexOf(e.KeyCode) != -1)
            {
                m_enabled_keys.RemoveAt(m_enabled_keys.IndexOf(e.KeyCode));  
            }
        }

         
        private void GameForm_MouseMove(object sender, MouseEventArgs e)
        {
            bool is_cursor = false;

            if (m_form_status == DungeonFormStatus.Menu)
            {
                 
                if (interface_main_menu_rectangle_game_button.Contains(e.Location))
                {
                    interface_main_menu_is_entered_game_button = true;
                    is_cursor = true;
                }
                else interface_main_menu_is_entered_game_button = false;

                 
                if (interface_main_menu_rectangle_rules_button.Contains(e.Location))
                {
                    interface_main_menu_is_entered_rules_button = true;
                    is_cursor = true;
                }
                else interface_main_menu_is_entered_rules_button = false;

                 
                if (interface_main_menu_rectangle_records_button.Contains(e.Location))
                {
                    interface_main_menu_is_entered_records_button = true;
                    is_cursor = true;
                }
                else interface_main_menu_is_entered_records_button = false;

                 
                if (interface_main_menu_rectangle_settings_button.Contains(e.Location))
                {
                    interface_main_menu_is_entered_settings_button = true;
                    is_cursor = true;
                }
                else interface_main_menu_is_entered_settings_button = false;

                 
                if (interface_main_menu_rectangle_exit_button.Contains(e.Location))
                {
                    interface_main_menu_is_entered_exit_button = true;
                    is_cursor = true;
                }
                else interface_main_menu_is_entered_exit_button = false;
            }
            else if (m_form_status == DungeonFormStatus.Rules)
            {
                 
                if (rules_total_page > 0)
                {
                    if (rules_rectangle_arrow_left.Contains(e.Location))
                    {
                        rules_is_entered_arrow_left = true;
                        is_cursor = true;
                    }
                    else
                    {
                        rules_is_entered_arrow_left = false;
                    }
                }

                 
                if (rules_total_page < rules_pages_number - 1)
                {
                    if (rules_rectangle_arrow_right.Contains(e.Location))
                    {
                        rules_is_entered_arrow_right = true;
                        is_cursor = true;
                    }
                    else
                    {
                        rules_is_entered_arrow_right = false;
                    }
                }


                 
                if (settings_rectangle_buttons[5].Contains(e.Location))
                {
                    settings_is_entered_buttons[5] = true;
                    is_cursor = true;
                }
                else settings_is_entered_buttons[5] = false;
            }
            else if (m_form_status == DungeonFormStatus.Settings || m_form_status == DungeonFormStatus.InGameMenu)
            {
                 
                for (int i = 0; i < 6; i++)
                {
                    if (settings_rectangle_buttons[i].Contains(e.Location))
                    {
                        settings_is_entered_buttons[i] = true;
                        is_cursor = true;
                    }
                    else settings_is_entered_buttons[i] = false;
                }
            }
            else if (m_form_status == DungeonFormStatus.SettingsScreen)
            {
                 
                for (int i = 0; i < settings_screen_number; i++)
                {
                    if (settings_screen_rectangle_radiobuttons[i].Contains(e.Location) ||
                        settings_screen_rectangle_texts[i].Contains(e.Location))
                    {
                        settings_screen_is_entered_radiobuttons[i] = true;
                        is_cursor = true;
                    }
                    else
                    {
                        settings_screen_is_entered_radiobuttons[i] = false;
                    }
                }

                 
                if (settings_rectangle_buttons[5].Contains(e.Location))
                {
                    settings_is_entered_buttons[5] = true;
                    is_cursor = true;
                }
                else settings_is_entered_buttons[5] = false;
            }
            else if (m_form_status == DungeonFormStatus.SettingsOther)
            {
                 
                for (int i = 0; i < settings_other_number; i++)
                {
                    if (settings_other_rectangle_checkboxes[i].Contains(e.Location) ||
                        settings_other_rectangle_texts[i].Contains(e.Location))
                    {
                        settings_other_is_entered_checkboxes[i] = true;
                        is_cursor = true;
                    }
                    else
                    {
                        settings_other_is_entered_checkboxes[i] = false;
                    }
                }

                 
                if (settings_rectangle_buttons[5].Contains(e.Location))
                {
                    settings_is_entered_buttons[5] = true;
                    is_cursor = true;
                }
                else settings_is_entered_buttons[5] = false;
            }
            else if (m_form_status == DungeonFormStatus.SettingsAudio)
            {
                for (int i = 0; i < settings_audio_number; i++)
                {
                    if (settings_audio_rectangle_buttons_minus[i].Contains(e.Location))
                    {
                        settings_audio_is_entered_buttons_minus[i] = true;
                        is_cursor = true;
                    }
                    else
                    {
                        settings_audio_is_entered_buttons_minus[i] = false;
                    }
                    if (settings_audio_rectangle_buttons_plus[i].Contains(e.Location))
                    {
                        settings_audio_is_entered_buttons_plus[i] = true;
                        is_cursor = true;
                    }
                    else
                    {
                        settings_audio_is_entered_buttons_plus[i] = false;
                    }
                }

                 
                if (settings_rectangle_buttons[5].Contains(e.Location))
                {
                    settings_is_entered_buttons[5] = true;
                    is_cursor = true;
                }
                else settings_is_entered_buttons[5] = false;
            }
            else if (m_form_status == DungeonFormStatus.SettingsInterface)
            {
                 
                for (int i = 0; i < settings_interface_number; i++)
                {
                    if (settings_interface_rectangle_checkboxes[i].Contains(e.Location) ||
                        settings_interface_rectangle_texts[i].Contains(e.Location))
                    {
                        settings_interface_is_entered_checkboxes[i] = true;
                        is_cursor = true;
                    }
                    else
                    {
                        settings_interface_is_entered_checkboxes[i] = false;
                    }
                }

                 
                if (settings_rectangle_buttons[5].Contains(e.Location))
                {
                    settings_is_entered_buttons[5] = true;
                    is_cursor = true;
                }
                else settings_is_entered_buttons[5] = false;
            }
            else if (m_form_status == DungeonFormStatus.SettingsControls)
            {
                 
                for (int i = 0; i < keys_number; i++)
                {
                    if (settings_controls_rectangle_buttons[i].Contains(e.Location) ||
                        settings_controls_rectangle_texts[i].Contains(e.Location))
                    {
                        settings_controls_is_entered_buttons[i] = true;
                        is_cursor = true;
                    }
                    else
                    {
                        settings_controls_is_entered_buttons[i] = false;
                    }
                }

                 
                if (settings_rectangle_buttons[5].Contains(e.Location))
                {
                    settings_is_entered_buttons[5] = true;
                    is_cursor = true;
                }
                else settings_is_entered_buttons[5] = false;
            }
            else if (m_form_status == DungeonFormStatus.NewGameStart)
            {
                 
                if (settings_rectangle_buttons[0].Contains(e.Location))
                {
                    settings_is_entered_buttons[0] = true;
                    is_cursor = true;
                }
                else settings_is_entered_buttons[0] = false;

                 
                if (settings_rectangle_buttons[2].Contains(e.Location))
                {
                    settings_is_entered_buttons[2] = true;
                    is_cursor = true;
                }
                else settings_is_entered_buttons[2] = false;

                 
                if (settings_rectangle_buttons[4].Contains(e.Location))
                {
                    settings_is_entered_buttons[4] = true;
                    is_cursor = true;
                }
                else settings_is_entered_buttons[4] = false;
            }
            else if (m_form_status == DungeonFormStatus.NewGameDifficulty)
            {
                 
                if (choosed_difficulty_id > 0)
                {
                    if (new_game_difficulty_rectangle_arrow_left.Contains(e.Location))
                    {
                        new_game_difficulty_is_entered_arrow_left = true;
                        is_cursor = true;
                    }
                    else
                    {
                        new_game_difficulty_is_entered_arrow_left = false;
                    }
                }

                 
                if (choosed_difficulty_id < difficulties_number - 1)
                {
                    if (new_game_difficulty_rectangle_arrow_right.Contains(e.Location))
                    {
                        new_game_difficulty_is_entered_arrow_right = true;
                        is_cursor = true;
                    }
                    else
                    {
                        new_game_difficulty_is_entered_arrow_right = false;
                    }
                }

                 
                if (new_game_difficulty_rectangle_button_next.Contains(e.Location))
                {
                    new_game_difficulty_is_entered_button_next = true;
                    is_cursor = true;
                }
                else new_game_difficulty_is_entered_button_next = false;

                 
                if (new_game_difficulty_rectangle_button_back.Contains(e.Location))
                {
                    new_game_difficulty_is_entered_button_back = true;
                    is_cursor = true;
                }
                else new_game_difficulty_is_entered_button_back = false;
            }
            else if (m_form_status == DungeonFormStatus.NewGameCharacter)
            {
                 
                if (choosed_character_id > 0)
                {
                    if (new_game_character_rectangle_arrow_left.Contains(e.Location))
                    {
                        new_game_character_is_entered_arrow_left = true;
                        is_cursor = true;
                    }
                    else
                    {
                        new_game_character_is_entered_arrow_left = false;
                    }
                }

                 
                if (choosed_character_id < characters_number - 1)
                {
                    if (new_game_character_rectangle_arrow_right.Contains(e.Location))
                    {
                        new_game_character_is_entered_arrow_right = true;
                        is_cursor = true;
                    }
                    else
                    {
                        new_game_character_is_entered_arrow_right = false;
                    }
                }

                 
                if (new_game_difficulty_rectangle_button_next.Contains(e.Location))
                {
                    new_game_difficulty_is_entered_button_next = true;
                    is_cursor = true;
                }
                else new_game_difficulty_is_entered_button_next = false;

                 
                if (new_game_difficulty_rectangle_button_back.Contains(e.Location))
                {
                    new_game_difficulty_is_entered_button_back = true;
                    is_cursor = true;
                }
                else new_game_difficulty_is_entered_button_back = false;
            }
            else if (m_form_status == DungeonFormStatus.NewGameLevelGenerate)
            {
                 
                for (int i = 0; i < level_generation_nums_number; i++)
                {
                    if (new_game_level_generation_rectangle_arrows_up[i].Contains(e.Location))
                    {
                        new_game_level_generation_is_entered_arrows_up[i] = true;
                        is_cursor = true;
                    }
                    else new_game_level_generation_is_entered_arrows_up[i] = false;
                    if (new_game_level_generation_rectangle_arrows_down[i].Contains(e.Location))
                    {
                        new_game_level_generation_is_entered_arrows_down[i] = true;
                        is_cursor = true;
                    }
                    else new_game_level_generation_is_entered_arrows_down[i] = false;
                }

                 
                if (new_game_level_generation_rectangle_button.Contains(e.Location))
                {
                    new_game_level_generation_is_entered_button = true;
                    is_cursor = true;
                }
                else new_game_level_generation_is_entered_button = false;

                 
                if (new_game_level_generation_rectangle_button_reset.Contains(e.Location))
                {
                    new_game_level_generation_is_entered_button_reset = true;
                    is_cursor = true;
                }
                else new_game_level_generation_is_entered_button_reset = false;

                 
                if (new_game_difficulty_rectangle_button_next.Contains(e.Location))
                {
                    new_game_difficulty_is_entered_button_next = true;
                    is_cursor = true;
                }
                else new_game_difficulty_is_entered_button_next = false;

                 
                if (new_game_difficulty_rectangle_button_back.Contains(e.Location))
                {
                    new_game_difficulty_is_entered_button_back = true;
                    is_cursor = true;
                }
                else new_game_difficulty_is_entered_button_back = false;
            }
            else if (m_form_status == DungeonFormStatus.Game)
            {
                if (m_total_inventory_mode != DungeonInventoryStatus.None)
                {
                    if (m_total_inventory_mode == DungeonInventoryStatus.Inventory)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            for (int i2 = 0; i2 < 10; i2++)
                            {
                                int cell_id = i * 10 + i2;

                                if (interface_rectangle_inventory_cells[cell_id].Contains(e.Location))
                                {
                                    interface_is_entered_inventory_cells[cell_id] = true;
                                    is_cursor = true;
                                }
                                else
                                {
                                    interface_is_entered_inventory_cells[cell_id] = false;
                                }
                            }
                        }
                        if (m_id_clicked_cell != -1)
                        {
                            DungeonItem item;
                            if (m_id_clicked_cell >= 30)
                            {
                                item = m_hero.ContainerSpecialItems.Items[m_id_clicked_cell - 30];
                            }
                            else
                            {
                                item = m_hero.Container.Items[m_id_clicked_cell];
                            }
                            if (item != null)
                            {
                                 
                                if (interface_rectangle_inventory_item_info_button.Contains(e.Location))
                                {
                                    interface_is_entered_inventory_item_info_button = true;
                                    is_cursor = true;
                                }
                                else
                                {
                                    interface_is_entered_inventory_item_info_button = false;
                                }

                                 
                                if (item.CanUse)
                                {
                                    if (interface_rectangle_inventory_item_buttons[0].Contains(e.Location))
                                    {
                                        interface_is_entered_inventory_item_button[0] = true;
                                        is_cursor = true;
                                    }
                                    else
                                    {
                                        interface_is_entered_inventory_item_button[0] = false;
                                    }
                                }

                                 
                                if (item.CanDrop)
                                {
                                    if (interface_rectangle_inventory_item_buttons[1].Contains(e.Location))
                                    {
                                        interface_is_entered_inventory_item_button[1] = true;
                                        is_cursor = true;
                                    }
                                    else
                                    {
                                        interface_is_entered_inventory_item_button[1] = false;
                                    }
                                }

                                 
                                if (item.CanDestroy)
                                {
                                    if (interface_rectangle_inventory_item_buttons[2].Contains(e.Location))
                                    {
                                        interface_is_entered_inventory_item_button[2] = true;
                                        is_cursor = true;
                                    }
                                    else
                                    {
                                        interface_is_entered_inventory_item_button[2] = false;
                                    }
                                }
                            }
                            for (int i = 0; i < special_items_number; i++)
                            {
                                if (m_hero.ContainerSpecialItems.Items[i] != null)
                                {
                                    if (interface_rectangle_inventory_special_cells[i].Contains(e.Location))
                                    {
                                        interface_is_entered_inventory_special_cells[i] = true;
                                        is_cursor = true;
                                    }
                                    else
                                    {
                                        interface_is_entered_inventory_special_cells[i] = false;
                                    }
                                }
                                else  
                                {
                                    if (interface_rectangle_inventory_special_cells[i].Contains(e.Location))  
                                    {
                                        if (i == 0 && item is DungeonItemHelmet)
                                        {
                                            interface_is_entered_inventory_special_cells[i] = true;
                                            is_cursor = true;
                                        }
                                        else if (i == 1 && item is DungeonItemArmour)
                                        {
                                            interface_is_entered_inventory_special_cells[i] = true;
                                            is_cursor = true;
                                        }
                                        else if (i == 2 && item is DungeonItemArtifact)
                                        {
                                            interface_is_entered_inventory_special_cells[i] = true;
                                            is_cursor = true;
                                        }
                                        else if (i == 3 && item is DungeonItemSword)
                                        {
                                            interface_is_entered_inventory_special_cells[i] = true;
                                            is_cursor = true;
                                        }
                                        else if (i >= 4 && i <= 6 && item is DungeonItemPotion)
                                        {
                                            interface_is_entered_inventory_special_cells[i] = true;
                                            is_cursor = true;
                                        }
                                        else
                                        {
                                            interface_is_entered_inventory_special_cells[i] = false;
                                        }
                                    }
                                    else
                                    {
                                        interface_is_entered_inventory_special_cells[i] = false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < special_items_number; i++)  
                            {
                                if (m_hero.ContainerSpecialItems.Items[i] != null)  
                                {
                                    if (interface_rectangle_inventory_special_cells[i].Contains(e.Location))  
                                    {
                                        interface_is_entered_inventory_special_cells[i] = true;
                                        is_cursor = true;
                                    }
                                    else
                                    {
                                        interface_is_entered_inventory_special_cells[i] = false;
                                    }
                                }
                                else
                                {
                                    interface_is_entered_inventory_special_cells[i] = false;
                                }
                            }
                        }
                    }
                    else if (m_total_inventory_mode == DungeonInventoryStatus.Statistic)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            for (int i2 = 0; i2 < 3; i2++)
                            {
                                int id = i * 3 + i2;
                                DungeonStats stat = (DungeonStats)id;

                                bool can_up_stat = false;
                                if (m_hero.SkillPoints > 0)
                                {
                                    if (m_hero.GetStatValue(stat) < DungeonStatsInfo.Max(stat) - 0.25)
                                    {
                                        can_up_stat = true;
                                    }
                                }
                                if (can_up_stat)
                                {
                                    if (interface_rectangle_statistic_stats_buttons[id].Contains(e.Location))
                                    {
                                        is_cursor = true;
                                    }
                                }
                            }
                        }
                    }
                    else if (m_total_inventory_mode == DungeonInventoryStatus.Map)
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            for (int i2 = 0; i2 < 5; i2++)
                            {
                                if (interface_rectangle_map_levels[i, i2].Contains(e.Location))
                                {
                                    interface_is_entered_map_levels[i, i2] = true;
                                    is_cursor = true;
                                }
                                else
                                {
                                    interface_is_entered_map_levels[i, i2] = false;
                                }
                            }
                        }
                    }
                }

                if (m_interface_is_show_buttons)
                {
                     
                    if (interface_rectangle_inventory_button.Contains(e.Location))
                    {
                        interface_is_entered_inventory_button = true;
                        is_cursor = true;
                    }
                    else
                    {
                        interface_is_entered_inventory_button = false;
                    }

                     
                    if (interface_rectangle_statistic_button.Contains(e.Location))
                    {
                        interface_is_entered_statistic_button = true;
                        is_cursor = true;
                    }
                    else
                    {
                        interface_is_entered_statistic_button = false;
                    }

                     
                    if (interface_rectangle_map_button.Contains(e.Location))
                    {
                        interface_is_entered_map_button = true;
                        is_cursor = true;
                    }
                    else
                    {
                        interface_is_entered_map_button = false;
                    }
                }

                if (is_code_insert)
                {
                     
                    for (int i = 0; i < code_nums_number; i++)
                    {
                        if (code_rectangle_arrows_up[i].Contains(e.Location))
                        {
                            code_is_entered_arrows_up[i] = true;
                            is_cursor = true;
                        }
                        else code_is_entered_arrows_up[i] = false;
                        if (code_rectangle_arrows_down[i].Contains(e.Location))
                        {
                            code_is_entered_arrows_down[i] = true;
                            is_cursor = true;
                        }
                        else code_is_entered_arrows_down[i] = false;
                    }

                     
                    if (code_rectangle_button_back.Contains(e.Location))
                    {
                        code_is_entered_button_back = true;
                        is_cursor = true;
                    }
                    else code_is_entered_button_back = false;

                     
                    if (code_rectangle_button_open.Contains(e.Location))
                    {
                        code_is_entered_button_open = true;
                        is_cursor = true;
                    }
                    else code_is_entered_button_open = false;

                }
            }
            else if (m_form_status == DungeonFormStatus.ConfirmExit ||
                m_form_status == DungeonFormStatus.InGameMenuConfirmExit ||
                m_form_status == DungeonFormStatus.InGameMenuConfirmLoad)
            {
                 
                if (interface_confirm_exit_rectangle_confirm_button.Contains(e.Location))
                {
                    interface_confirm_exit_is_entered_confirm_button = true;
                    is_cursor = true;
                }
                else interface_confirm_exit_is_entered_confirm_button = false;

                 
                if (interface_confirm_exit_rectangle_cancel_button.Contains(e.Location))
                {
                    interface_confirm_exit_is_entered_cancel_button = true;
                    is_cursor = true;
                }
                else interface_confirm_exit_is_entered_cancel_button = false;
            }
            else if (m_form_status == DungeonFormStatus.GameEndWin || m_form_status == DungeonFormStatus.GameEndLoose)
            {
                 
                if (result_rectangle_button_load.Contains(e.Location))
                {
                    result_is_entered_button_load = true;
                    is_cursor = true;
                }
                else result_is_entered_button_load = false;

                 
                if (result_rectangle_button_go_menu.Contains(e.Location))
                {
                    result_is_entered_button_go_menu = true;
                    is_cursor = true;
                }
                else result_is_entered_button_go_menu = false;
            }

            if (is_cursor)
            {
                m_total_cursor = Cursors.Hand;
            }
            else
            {
                m_total_cursor = Cursors.Default;
            }
            if (Cursor.Current != m_total_cursor)
            {
                Cursor.Current = m_total_cursor;
            }
            if (m_form_status != DungeonFormStatus.Game)
            {
                Refresh();
            }
        }

         
        private void GameForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (m_form_status == DungeonFormStatus.Menu)
            {
                 
                if (interface_main_menu_rectangle_game_button.Contains(e.Location))
                {
                    m_form_status = DungeonFormStatus.NewGameStart;
                }

                 
                else if (interface_main_menu_rectangle_rules_button.Contains(e.Location))
                {
                    m_form_status = DungeonFormStatus.Rules;
                }

                 
                else if (interface_main_menu_rectangle_records_button.Contains(e.Location))
                {
                    m_can_minimize = false;
                    RecordsForm records_form = new RecordsForm();
                    records_form.Owner = this;
                    records_form.ShowDialog();
                    m_can_minimize = true;
                }

                 
                else if (interface_main_menu_rectangle_settings_button.Contains(e.Location))
                {
                    m_form_status = DungeonFormStatus.Settings;
                }

                 
                else if (interface_main_menu_rectangle_exit_button.Contains(e.Location))
                {
                    m_form_status = DungeonFormStatus.ConfirmExit;
                }
            }
            else if (m_form_status == DungeonFormStatus.InGameMenu)
            {
                 
                if (settings_rectangle_buttons[0].Contains(e.Location))
                {
                    ResumeGame();
                }

                 
                else if (settings_rectangle_buttons[1].Contains(e.Location))
                {
                    JustSave();
                }

                 
                else if (settings_rectangle_buttons[2].Contains(e.Location))
                {
                    if (is_game_saved)
                    {
                        JustLoad();
                    }
                    else
                    {
                        m_form_status = DungeonFormStatus.InGameMenuConfirmLoad;
                    }
                }

                 
                else if (settings_rectangle_buttons[3].Contains(e.Location))
                {
                    m_form_status = DungeonFormStatus.Rules;
                }

                 
                else if (settings_rectangle_buttons[4].Contains(e.Location))
                {
                    m_form_status = DungeonFormStatus.Settings;
                }

                 
                else if (settings_rectangle_buttons[5].Contains(e.Location))
                {
                    if (is_game_saved)
                    {
                        EndGame(DungeonFormStatus.Menu);
                    }
                    else
                    {
                        m_form_status = DungeonFormStatus.InGameMenuConfirmExit;
                    }
                }
            }
            else if (m_form_status == DungeonFormStatus.InGameMenuConfirmExit)
            {
                 
                if (interface_confirm_exit_rectangle_confirm_button.Contains(e.Location))
                {
                    musics[m_total_music_id].Stop();
                    m_total_music_id = 0;
                    musics[m_total_music_id].Play();
                    EndGame(DungeonFormStatus.Menu);
                }

                 
                else if (interface_confirm_exit_rectangle_cancel_button.Contains(e.Location))
                {
                    m_form_status = DungeonFormStatus.InGameMenu;
                }
            }
            else if (m_form_status == DungeonFormStatus.InGameMenuConfirmLoad)
            {
                 
                if (interface_confirm_exit_rectangle_confirm_button.Contains(e.Location))
                {
                    JustLoad();
                }

                 
                else if (interface_confirm_exit_rectangle_cancel_button.Contains(e.Location))
                {
                    m_form_status = DungeonFormStatus.InGameMenu;
                }
            }
            else if (m_form_status == DungeonFormStatus.Rules)
            {
                 
                if (rules_total_page > 0)
                {
                    if (rules_rectangle_arrow_left.Contains(e.Location))
                    {
                        rules_total_page--;
                    }
                }

                 
                if (rules_total_page < rules_pages_number - 1)
                {
                    if (rules_rectangle_arrow_right.Contains(e.Location))
                    {
                        rules_total_page++;
                    }
                }

                 
                if (settings_rectangle_buttons[5].Contains(e.Location))
                {
                    if (is_game_menu)
                    {
                        m_form_status = DungeonFormStatus.InGameMenu;
                    }
                    else
                    {
                        m_form_status = DungeonFormStatus.Menu;
                    }
                }
            }
            else if (m_form_status == DungeonFormStatus.Settings)
            {
                 
                if (settings_rectangle_buttons[0].Contains(e.Location))
                {
                    m_form_status = DungeonFormStatus.SettingsScreen;
                }

                 
                else if (settings_rectangle_buttons[1].Contains(e.Location))
                {
                    m_form_status = DungeonFormStatus.SettingsOther;
                }

                 
                else if (settings_rectangle_buttons[2].Contains(e.Location))
                {
                    m_form_status = DungeonFormStatus.SettingsAudio;
                }

                 
                else if (settings_rectangle_buttons[3].Contains(e.Location))
                {
                    m_form_status = DungeonFormStatus.SettingsInterface;
                }

                 
                else if (settings_rectangle_buttons[4].Contains(e.Location))
                {
                    m_form_status = DungeonFormStatus.SettingsControls;
                }

                 
                else if (settings_rectangle_buttons[5].Contains(e.Location))
                {
                    if (is_game_menu)
                    {
                        m_form_status = DungeonFormStatus.InGameMenu;
                    }
                    else
                    {
                        m_form_status = DungeonFormStatus.Menu;
                    }
                }
            }
            else if (m_form_status == DungeonFormStatus.SettingsScreen)
            {
                 
                for (byte i = 0; i < settings_screen_number; i++)
                {
                    if (settings_screen_rectangle_radiobuttons[i].Contains(e.Location) ||
                        settings_screen_rectangle_texts[i].Contains(e.Location))
                    {
                        m_total_resolution_id = i;
                        SetAndSaveSettingsScreen();  
                        CheckResolution();  
                        break;
                    }
                }

                 
                if (settings_rectangle_buttons[5].Contains(e.Location))
                {
                    m_form_status = DungeonFormStatus.Settings;
                }
            }
            else if (m_form_status == DungeonFormStatus.SettingsOther)
            {
                 
                if (settings_other_rectangle_checkboxes[0].Contains(e.Location) ||
                    settings_other_rectangle_texts[0].Contains(e.Location))
                {
                    m_interface_is_show_vignette = !m_interface_is_show_vignette;
                    SetAndSaveSettingsOther();
                }
                else if (settings_other_rectangle_checkboxes[1].Contains(e.Location) ||
                    settings_other_rectangle_texts[1].Contains(e.Location))
                {
                    m_is_show_blood_effect_on_hit = !m_is_show_blood_effect_on_hit;
                    SetAndSaveSettingsOther();
                }
                else if (settings_other_rectangle_checkboxes[2].Contains(e.Location) ||
                    settings_other_rectangle_texts[2].Contains(e.Location))
                {
                    m_is_autosave = !m_is_autosave;
                    SetAndSaveSettingsOther();
                }

                 
                else if (settings_rectangle_buttons[5].Contains(e.Location))
                {
                    m_form_status = DungeonFormStatus.Settings;
                }
            }
            else if (m_form_status == DungeonFormStatus.SettingsAudio)
            {
                 
                if (settings_audio_rectangle_buttons_minus[0].Contains(e.Location))
                {
                    if (VolumeSound > 0)
                    {
                        VolumeSound -= audio_edit_value;
                        SetAndSaveSettingsAudio();
                    }
                }

                 
                else if (settings_audio_rectangle_buttons_minus[1].Contains(e.Location))
                {
                    if (VolumeMusic > 0)
                    {
                        VolumeMusic -= audio_edit_value;
                        SetAndSaveSettingsAudio();
                    }
                }

                 
                else if (settings_audio_rectangle_buttons_plus[0].Contains(e.Location))
                {
                    if (VolumeSound < 100)
                    {
                        VolumeSound += audio_edit_value;
                        SetAndSaveSettingsAudio();
                    }
                }

                 
                else if (settings_audio_rectangle_buttons_plus[1].Contains(e.Location))
                {
                    if (VolumeMusic < 100)
                    {
                        VolumeMusic += audio_edit_value;
                        SetAndSaveSettingsAudio();
                    }
                }

                 
                else if (settings_rectangle_buttons[5].Contains(e.Location))
                {
                    m_form_status = DungeonFormStatus.Settings;
                }
            }
            else if (m_form_status == DungeonFormStatus.SettingsInterface)
            {
                 
                for (byte i = 0; i < settings_interface_number; i++)
                {
                    if (settings_interface_rectangle_checkboxes[i].Contains(e.Location) ||
                        settings_interface_rectangle_texts[i].Contains(e.Location))
                    {
                        settings_interface_checkboxes_choosed_ids[i] = !settings_interface_checkboxes_choosed_ids[i];
                        SetAndSaveSettingsInterface();
                        GetSettingsInterface();  
                        break;
                    }
                }

                 
                if (settings_rectangle_buttons[5].Contains(e.Location))
                {
                    m_form_status = DungeonFormStatus.Settings;
                }
            }
            else if (m_form_status == DungeonFormStatus.SettingsControls)
            {
                 
                for (byte i = 0; i < keys_number; i++)
                {
                    if (settings_controls_rectangle_buttons[i].Contains(e.Location) ||
                        settings_controls_rectangle_texts[i].Contains(e.Location))
                    {
                        if (i == id_changing_key)  
                        {
                            id_changing_key = -1;  
                        }
                        else
                        {
                            id_changing_key = i;
                        }
                    }
                }

                 
                if (settings_rectangle_buttons[5].Contains(e.Location))
                {
                    m_form_status = DungeonFormStatus.Settings;
                }
            }
            else if (m_form_status == DungeonFormStatus.NewGameStart)
            {
                 
                if (settings_rectangle_buttons[0].Contains(e.Location))
                {
                    CreateCharacters();
                    choosed_difficulty_id = 0;  
                    choosed_character_id = 0;  
                    m_total_inventory_mode = DungeonInventoryStatus.None;
                    m_id_clicked_cell = -1;
                    is_show_item_info = true;
                    is_game_menu = false;
                    is_code_insert = false;
                    for (int i = 0; i < level_generation_nums_number; i++)  
                    {
                        choosed_level_generation_nums_values[i] = 0;
                    }
                    m_form_status = DungeonFormStatus.NewGameDifficulty;
                }

                 
                else if (settings_rectangle_buttons[2].Contains(e.Location))
                {
                    JustLoad();
                }

                 
                else if (settings_rectangle_buttons[4].Contains(e.Location))
                {
                    m_form_status = DungeonFormStatus.Menu;
                }
            }
            else if (m_form_status == DungeonFormStatus.NewGameDifficulty)
            {
                 
                if (choosed_difficulty_id > 0)
                {
                    if (new_game_difficulty_rectangle_arrow_left.Contains(e.Location))
                    {
                        choosed_difficulty_id--;
                    }
                }

                 
                if (choosed_difficulty_id < difficulties_number - 1)
                {
                    if (new_game_difficulty_rectangle_arrow_right.Contains(e.Location))
                    {
                        choosed_difficulty_id++;
                    }
                }

                 
                if (new_game_difficulty_rectangle_button_next.Contains(e.Location))
                {
                    m_form_status = DungeonFormStatus.NewGameCharacter;
                }

                 
                else if (new_game_difficulty_rectangle_button_back.Contains(e.Location))
                {
                    m_form_status = DungeonFormStatus.NewGameStart;
                }
            }
            else if (m_form_status == DungeonFormStatus.NewGameCharacter)
            {
                 
                if (choosed_character_id > 0)
                {
                    if (new_game_character_rectangle_arrow_left.Contains(e.Location))
                    {
                        choosed_character_id--;
                    }
                }

                 
                if (choosed_character_id < characters_number - 1)
                {
                    if (new_game_character_rectangle_arrow_right.Contains(e.Location))
                    {
                        choosed_character_id++;
                    }
                }

                 
                if (new_game_difficulty_rectangle_button_next.Contains(e.Location))
                {
                    m_form_status = DungeonFormStatus.NewGameLevelGenerate;
                }

                 
                else if (new_game_difficulty_rectangle_button_back.Contains(e.Location))
                {
                    m_form_status = DungeonFormStatus.NewGameDifficulty;
                }
            }
            else if (m_form_status == DungeonFormStatus.NewGameLevelGenerate)
            {
                 
                for (int i = 0; i < level_generation_nums_number; i++)
                {
                    if (new_game_level_generation_rectangle_arrows_up[i].Contains(e.Location))
                    {
                        if (choosed_level_generation_nums_values[i] < 9) choosed_level_generation_nums_values[i]++;
                        else choosed_level_generation_nums_values[i] = 0;
                    }
                    if (new_game_level_generation_rectangle_arrows_down[i].Contains(e.Location))
                    {
                        if (choosed_level_generation_nums_values[i] > 0) choosed_level_generation_nums_values[i]--;
                        else choosed_level_generation_nums_values[i] = 9;
                    }
                }

                 
                if (new_game_level_generation_rectangle_button.Contains(e.Location))
                {
                    Random rand = new Random();
                    for (int i = 0; i < level_generation_nums_number; i++)
                    {
                        choosed_level_generation_nums_values[i] = (byte)(rand.Next(0, 9 + 1));
                    }
                }

                 
                else if (new_game_level_generation_rectangle_button_reset.Contains(e.Location))
                {
                    for (int i = 0; i < level_generation_nums_number; i++)
                    {
                        choosed_level_generation_nums_values[i] = 0;
                    }
                }

                 
                else if (new_game_difficulty_rectangle_button_next.Contains(e.Location))
                {
                    StartNewGame();
                }

                 
                else if (new_game_difficulty_rectangle_button_back.Contains(e.Location))
                {
                    m_form_status = DungeonFormStatus.NewGameCharacter;
                }
            }
            else if (m_form_status == DungeonFormStatus.ConfirmExit)
            {
                 
                if (interface_confirm_exit_rectangle_confirm_button.Contains(e.Location))
                {
                    this.Hide();
                    Owner.Show();
                    Owner.Activate();
                    return;
                }

                 
                else if (interface_confirm_exit_rectangle_cancel_button.Contains(e.Location))
                {
                    m_form_status = DungeonFormStatus.Menu;
                }
            }
            else if (m_form_status == DungeonFormStatus.Game)
            {
                if (m_total_inventory_mode != DungeonInventoryStatus.None)
                {
                    int new_clicked_cell = -1;

                    if (m_total_inventory_mode == DungeonInventoryStatus.Inventory)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            for (int i2 = 0; i2 < 10; i2++)
                            {
                                int cell_id = i * 10 + i2;

                                if (interface_rectangle_inventory_cells[cell_id].Contains(e.Location))
                                {
                                    new_clicked_cell = cell_id;
                                    i2 = 10; i = 3;  
                                }
                            }
                        }

                         
                        for (int i = 0; i < special_items_number; i++)
                        {
                            if (interface_rectangle_inventory_special_cells[i].Contains(e.Location))
                            {
                                new_clicked_cell = 30 + i;
                            }
                        }

                        if (m_id_clicked_cell == -1)  
                        {
                            if (new_clicked_cell != -1)  
                            {
                                if (new_clicked_cell >= 30)
                                {
                                    DungeonItem item;
                                    item = m_hero.ContainerSpecialItems.Items[new_clicked_cell - 30];
                                    if (item != null)
                                    {
                                        m_id_clicked_cell = new_clicked_cell;
                                    }
                                }
                                else
                                {
                                    m_id_clicked_cell = new_clicked_cell;
                                }
                            }
                        }
                        else  
                        {
                            DungeonItem item;
                            if (m_id_clicked_cell >= 30)
                            {
                                item = m_hero.ContainerSpecialItems.Items[m_id_clicked_cell - 30];
                            }
                            else
                            {
                                item = m_hero.Container.Items[m_id_clicked_cell];
                            }
                            if (new_clicked_cell == -1)  
                            {
                                if (item != null)  
                                {
                                     
                                    if (interface_rectangle_inventory_item_info_button.Contains(e.Location))
                                    {
                                        is_show_item_info = !is_show_item_info;
                                    }

                                     
                                    if (item.CanUse)
                                    {
                                        if (interface_rectangle_inventory_item_buttons[0].Contains(e.Location))
                                        {
                                            m_hero.Use(item);
                                            if (m_id_clicked_cell >= 30)
                                            {
                                                m_id_clicked_cell = -1;
                                            }
                                        }
                                    }

                                     
                                    if (item.CanDrop)
                                    {
                                        if (interface_rectangle_inventory_item_buttons[1].Contains(e.Location))
                                        {
                                            m_hero.Drop(item);
                                            if (m_id_clicked_cell >= 30)
                                            {
                                                m_id_clicked_cell = -1;
                                            }
                                        }
                                    }

                                     
                                    if (item.CanDestroy)
                                    {
                                        if (interface_rectangle_inventory_item_buttons[2].Contains(e.Location))
                                        {
                                            m_hero.Destroy(item);
                                            if (m_id_clicked_cell >= 30)
                                            {
                                                m_id_clicked_cell = -1;
                                            }
                                        }
                                    }
                                }
                            }
                            else  
                            {
                                if (new_clicked_cell >= 30)
                                {
                                    if (new_clicked_cell == m_id_clicked_cell)  
                                    {
                                        m_id_clicked_cell = -1;  
                                    }
                                    else
                                    {
                                        if (item != null)
                                        {
                                            bool is_ok = false;
                                            if (new_clicked_cell == 30)  
                                            {
                                                if (item is DungeonItemHelmet)
                                                {
                                                    is_ok = true;
                                                }
                                            }
                                            else if (new_clicked_cell == 31)  
                                            {
                                                if (item is DungeonItemArmour)
                                                {
                                                    is_ok = true;
                                                }
                                            }
                                            else if (new_clicked_cell == 32)  
                                            {
                                                if (item is DungeonItemArtifact)
                                                {
                                                    is_ok = true;
                                                }
                                            }
                                            else if (new_clicked_cell == 33)  
                                            {
                                                if (item is DungeonItemSword)
                                                {
                                                    is_ok = true;
                                                }
                                            }
                                            else  
                                            {
                                                if (item is DungeonItemPotion)
                                                {
                                                    is_ok = true;
                                                }
                                            }
                                            if (is_ok)
                                            {
                                                if (m_hero.ContainerSpecialItems.Items[new_clicked_cell - 30] == null)
                                                {
                                                    m_hero.ContainerSpecialItems.Set(item, new_clicked_cell - 30);
                                                    (item as DungeonItemEquipment).Equip(m_hero);
                                                }
                                            }
                                        }
                                        if (m_hero.ContainerSpecialItems.Items[new_clicked_cell - 30] != null)
                                        {
                                            m_id_clicked_cell = new_clicked_cell;
                                        }
                                    }
                                }
                                else
                                {
                                    if (new_clicked_cell == m_id_clicked_cell)  
                                    {
                                        m_id_clicked_cell = -1;  
                                    }
                                    else   
                                    {
                                        if (item != null)
                                        {
                                            if (m_hero.Container.Items[new_clicked_cell] == null)
                                            {
                                                m_hero.Container.Set(item, new_clicked_cell);
                                                if (item is DungeonItemEquipment)
                                                {
                                                    (item as DungeonItemEquipment).UnEquip();
                                                }
                                            }
                                        }
                                        m_id_clicked_cell = new_clicked_cell;
                                    }
                                }
                            }
                        }

                        if (interface_rectangle_inventory_button.Contains(e.Location))  
                        {
                            m_total_inventory_mode = DungeonInventoryStatus.None;
                        }

                        if (interface_rectangle_statistic_button.Contains(e.Location))  
                        {
                            if (is_code_insert)
                            {
                                is_code_insert = false;
                            }
                            m_total_inventory_mode = DungeonInventoryStatus.Statistic;
                        }

                        if (interface_rectangle_map_button.Contains(e.Location))  
                        {
                            if (is_code_insert)
                            {
                                is_code_insert = false;
                            }
                            m_id_level_showing_map = m_hero.DungeonLevel.Id;
                            m_total_inventory_mode = DungeonInventoryStatus.Map;
                        }
                    }
                    else if (m_total_inventory_mode == DungeonInventoryStatus.Statistic)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            for (int i2 = 0; i2 < 3; i2++)
                            {
                                int id = i * 3 + i2;
                                DungeonStats stat = (DungeonStats)id;

                                bool can_up_stat = false;
                                if (m_hero.SkillPoints > 0)
                                {
                                    if (m_hero.GetStatValue(stat) < DungeonStatsInfo.Max(stat) - 0.25)
                                    {
                                        can_up_stat = true;
                                    }
                                }
                                if (can_up_stat)
                                {
                                    if (interface_rectangle_statistic_stats_buttons[id].Contains(e.Location))
                                    {
                                        m_hero.UpStatValue(stat);
                                    }
                                }
                            }
                        }

                        if (interface_rectangle_inventory_button.Contains(e.Location))  
                        {
                            if (is_code_insert)
                            {
                                is_code_insert = false;
                            }
                            m_total_inventory_mode = DungeonInventoryStatus.Inventory;
                        }

                        if (interface_rectangle_statistic_button.Contains(e.Location))  
                        {
                            m_total_inventory_mode = DungeonInventoryStatus.None;
                        }

                        if (interface_rectangle_map_button.Contains(e.Location))  
                        {
                            if (is_code_insert)
                            {
                                is_code_insert = false;
                            }
                            m_id_level_showing_map = m_hero.DungeonLevel.Id;
                            m_total_inventory_mode = DungeonInventoryStatus.Map;
                        }
                    }
                    else if (m_total_inventory_mode == DungeonInventoryStatus.Map)
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            for (int i2 = 0; i2 < 5; i2++)
                            {
                                if (interface_rectangle_map_levels[i, i2].Contains(e.Location))
                                {
                                    m_id_level_showing_map = i * 5 + i2;
                                }
                            }
                        }

                        if (interface_rectangle_inventory_button.Contains(e.Location))  
                        {
                            if (is_code_insert)
                            {
                                is_code_insert = false;
                            }
                            m_total_inventory_mode = DungeonInventoryStatus.Inventory;
                        }

                        if (interface_rectangle_statistic_button.Contains(e.Location))  
                        {
                            if (is_code_insert)
                            {
                                is_code_insert = false;
                            }
                            m_total_inventory_mode = DungeonInventoryStatus.Statistic;
                        }

                        if (interface_rectangle_map_button.Contains(e.Location))  
                        {
                            m_total_inventory_mode = DungeonInventoryStatus.None;
                        }
                    }
                }
                else
                {
                    if (interface_rectangle_inventory_button.Contains(e.Location))  
                    {
                        if (is_code_insert)
                        {
                            is_code_insert = false;
                        }
                        m_total_inventory_mode = DungeonInventoryStatus.Inventory;
                    }

                    if (interface_rectangle_statistic_button.Contains(e.Location))  
                    {
                        if (is_code_insert)
                        {
                            is_code_insert = false;
                        }
                        m_total_inventory_mode = DungeonInventoryStatus.Statistic;
                    }

                    if (interface_rectangle_map_button.Contains(e.Location))  
                    {
                        if (is_code_insert)
                        {
                            is_code_insert = false;
                        }
                        m_id_level_showing_map = m_hero.DungeonLevel.Id;
                        m_total_inventory_mode = DungeonInventoryStatus.Map;
                    }
                }

                if (is_code_insert)
                {
                     
                    for (int i = 0; i < code_nums_number; i++)
                    {
                        if (code_rectangle_arrows_up[i].Contains(e.Location))
                        {
                            if (choosed_code_nums_values[i] < 9) choosed_code_nums_values[i]++;
                            else choosed_code_nums_values[i] = 0;
                        }
                        if (code_rectangle_arrows_down[i].Contains(e.Location))
                        {
                            if (choosed_code_nums_values[i] > 0) choosed_code_nums_values[i]--;
                            else choosed_code_nums_values[i] = 9;
                        }
                    }

                     
                    if (code_rectangle_button_back.Contains(e.Location))
                    {
                        HideCodeInsert();
                    }

                     
                    if (code_rectangle_button_open.Contains(e.Location))
                    {
                        string code_entered = "";
                        for (int i = 0; i < code_nums_number; i++)
                        {
                            code_entered += choosed_code_nums_values[i].ToString();
                        }
                        if (code_entered == m_door_code_opened.Code)
                        {
                            m_door_code_opened.Open();
                            m_hero.DoorsCodeOpened++;
                            HideCodeInsert();
                            AudioEffect audio_effect = new AudioEffect(sounds[1]);
                            audio_effect.Play();
                        }
                        else
                        {
                            AudioEffect audio_effect = new AudioEffect(sounds[2]);
                            audio_effect.Play();
                        }
                        for (int i = 0; i < code_nums_number; i++)
                        {
                            choosed_code_nums_values[i] = 0;
                        }
                    }
                }
            }
            else if (m_form_status == DungeonFormStatus.GameEndWin || m_form_status == DungeonFormStatus.GameEndLoose)
            {
                if (result_rectangle_button_load.Contains(e.Location))
                {
                    if (m_form_status == DungeonFormStatus.GameEndWin)  
                    {
                        m_form_status = DungeonFormStatus.NewGameDifficulty;
                    }
                    else if (m_form_status == DungeonFormStatus.GameEndLoose)  
                    {
                        JustLoad();
                    }
                }

                 
                else if (result_rectangle_button_go_menu.Contains(e.Location))
                {
                    musics[m_total_music_id].Stop();
                    m_total_music_id = 0;
                    musics[m_total_music_id].Play();
                    m_form_status = DungeonFormStatus.Menu;
                }
            }

            Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y);  
        }

         
        private void GameForm_Paint(object sender, PaintEventArgs e)
        {
            Brush brush;

            if (m_form_status != DungeonFormStatus.Game && !is_game_menu)
            {
                 
                e.Graphics.DrawImage(Properties.Resources.BACKGROUND, interface_main_menu_rectangle_background);

                 
                e.Graphics.DrawImage(Properties.Resources.LOGO, interface_main_menu_rectangle_logo);
            }

            if (m_form_status == DungeonFormStatus.Game || is_game_menu)
            {
                DungeonLevel total_dungeon_level = m_hero.DungeonLevel;
                total_dungeon_level.Paint(sender, e, m_hero, showing_size);

                 
                 
                Rectangle rec_around_up = new Rectangle(0, 0, ClientSize.Width, (ClientSize.Height - vignette_size.Height) / 2);
                Rectangle rec_around_right = new Rectangle((ClientSize.Width + vignette_size.Width) / 2, 0, (ClientSize.Width - vignette_size.Width) / 2 + 1, ClientSize.Height);
                Rectangle rec_around_down = new Rectangle(0, (ClientSize.Height + vignette_size.Height) / 2, ClientSize.Width, (ClientSize.Height - vignette_size.Height) / 2 + 1);
                Rectangle rec_around_left = new Rectangle(0, 0, (ClientSize.Width - vignette_size.Width) / 2, ClientSize.Height);
                e.Graphics.FillRectangle(Brushes.Black, rec_around_up);
                e.Graphics.FillRectangle(Brushes.Black, rec_around_right);
                e.Graphics.FillRectangle(Brushes.Black, rec_around_down);
                e.Graphics.FillRectangle(Brushes.Black, rec_around_left);
                 
                if (is_code_insert)  
                {
                     
                    DrawRectagle(e, code_rectangle_background, Brushes.Black, Brushes.Gray, 2);

                     
                    DrawText(e, code_rectangle_text, Brushes.Black, "Кодовий замок");

                     
                    for (int i = 0; i < code_nums_number; i++)
                    {
                        if (code_is_entered_arrows_up[i])
                        {
                            DrawImage(e, code_rectangle_arrows_up[i], Properties.Resources.ARROW_UP_entered);
                        }
                        else
                        {
                            DrawImage(e, code_rectangle_arrows_up[i], Properties.Resources.ARROW_UP);
                        }
                        DrawRectagle(e, code_rectangle_values[i], Brushes.Black, Brushes.White, 2);
                        DrawText(e, code_rectangle_values[i], Brushes.Black, choosed_code_nums_values[i].ToString());
                        if (code_is_entered_arrows_down[i])
                        {
                            DrawImage(e, code_rectangle_arrows_down[i], Properties.Resources.ARROW_DOWN_entered);
                        }
                        else
                        {
                            DrawImage(e, code_rectangle_arrows_down[i], Properties.Resources.ARROW_DOWN);
                        }
                    }

                     
                    if (code_is_entered_button_back)
                    {
                        brush = Brushes.YellowGreen;
                    }
                    else
                    {
                        brush = Brushes.White;
                    }
                    DrawRectagle(e, code_rectangle_button_back, Brushes.Black, brush, 2);
                    DrawText(e, code_rectangle_button_back, Brushes.Black, "Назад");

                     
                    if (code_is_entered_button_open)
                    {
                        brush = Brushes.YellowGreen;
                    }
                    else
                    {
                        brush = Brushes.White;
                    }
                    DrawRectagle(e, code_rectangle_button_open, Brushes.Black, brush, 2);
                    DrawText(e, code_rectangle_button_open, Brushes.Black, "Відкрити");


                }

                if (m_interface_is_show)
                {
                    if (m_interface_is_show_vignette)  
                    {
                         
                         
                        Rectangle rec_up = new Rectangle((ClientSize.Width - vignette_size.Width) / 2, (ClientSize.Height - vignette_size.Height) / 2, vignette_size.Width, vignette_size.Height / 2);
                        Rectangle rec_right = new Rectangle((ClientSize.Width) / 2, (ClientSize.Height - vignette_size.Height) / 2, vignette_size.Width / 2, vignette_size.Height);
                        Rectangle rec_down = new Rectangle((ClientSize.Width - vignette_size.Width) / 2, (ClientSize.Height) / 2, vignette_size.Width, vignette_size.Height / 2);
                        Rectangle rec_left = new Rectangle((ClientSize.Width - vignette_size.Width) / 2, (ClientSize.Height - vignette_size.Height) / 2, vignette_size.Width / 2, vignette_size.Height);
                        LinearGradientBrush brush_up = new LinearGradientBrush(new Rectangle(rec_up.X - 1, rec_up.Y - 1, rec_up.Width + 2, rec_up.Height + 2), Color.Black, Color.Transparent, 90.0f);
                        LinearGradientBrush brush_right = new LinearGradientBrush(new Rectangle(rec_right.X - 1, rec_right.Y - 1, rec_right.Width + 2, rec_right.Height + 2), Color.Transparent, Color.Black, 0.0f);
                        LinearGradientBrush brush_down = new LinearGradientBrush(new Rectangle(rec_down.X - 1, rec_down.Y - 1, rec_down.Width + 2, rec_down.Height + 2), Color.Transparent, Color.Black, 90.0f);
                        LinearGradientBrush brush_left = new LinearGradientBrush(new Rectangle(rec_left.X - 1, rec_left.Y - 1, rec_left.Width + 2, rec_left.Height + 2), Color.Black, Color.Transparent, 0.0f);
                        e.Graphics.FillRectangle(brush_up, rec_up);
                        e.Graphics.FillRectangle(brush_right, rec_right);
                        e.Graphics.FillRectangle(brush_down, rec_down);
                        e.Graphics.FillRectangle(brush_left, rec_left);
                         
                    }

                    if (m_interface_is_show_buttons || m_total_inventory_mode != DungeonInventoryStatus.None)
                    {
                        DrawHeroButtons(sender, e);
                    }

                    if (m_total_inventory_mode != DungeonInventoryStatus.None)
                    {
                        DrawInventory(sender, e);
                        DrawHeroLevel(sender, e);
                        DrawHeroStats(sender, e);
                        DrawHeroDungeonLevel(sender, e);
                        DrawHeroPotions(sender, e);
                        DrawHeroMinimap(sender, e);
                    }
                    else
                    {
                        if (m_interface_is_show_hero_level)
                        {
                            DrawHeroLevel(sender, e);
                        }

                        if (m_interface_is_show_stats)
                        {
                            DrawHeroStats(sender, e);
                        }

                        if (m_interface_is_show_dungeon_level)
                        {
                            DrawHeroDungeonLevel(sender, e);
                        }

                        if (m_interface_is_show_potions)
                        {
                            DrawHeroPotions(sender, e);
                        }

                        if (m_interface_is_show_minimap)
                        {
                            DrawHeroMinimap(sender, e);
                        }
                    }
                }

                 
                if (is_game_menu)
                {
                     
                    brush = new SolidBrush(Color.FromArgb(210, 0, 0, 0));
                    DrawRectagle(e, in_game_menu_rectangle_background, brush);

                     
                    e.Graphics.DrawImage(Properties.Resources.LOGO, interface_main_menu_rectangle_logo);
                }

                 
                if (m_form_status == DungeonFormStatus.InGameMenu)
                {
                     
                    DrawText(e, interface_main_menu_rectangle_text, Brushes.White, "Ігрове меню");

                     
                    string[] buttons_texts = new string[6];
                    buttons_texts[0] = "Продовжити";
                    buttons_texts[1] = "Зберегти";
                    buttons_texts[2] = "Завантажити";
                    buttons_texts[3] = "Правила";
                    buttons_texts[4] = "Налаштування";
                    buttons_texts[5] = "Вийти в меню";
                    for (int i = 0; i < 6; i++)
                    {
                        DrawInterfaceButton(e, settings_rectangle_buttons[i], settings_is_entered_buttons[i], buttons_texts[i]);
                    }
                }
            }

            if (m_form_status == DungeonFormStatus.Menu)
            {
                 
                string text = "З поверненням, " + Nick + "!";
                if (IsRegistration)
                {
                    text = "Ласкаво просимо, " + Nick + "!";
                }
                DrawText(e, interface_main_menu_rectangle_text, Brushes.White, text);

                 
                DrawInterfaceButton(e, interface_main_menu_rectangle_game_button, interface_main_menu_is_entered_game_button, "Гра");

                 
                DrawInterfaceButton(e, interface_main_menu_rectangle_rules_button, interface_main_menu_is_entered_rules_button, "Правила");

                 
                DrawInterfaceButton(e, interface_main_menu_rectangle_records_button, interface_main_menu_is_entered_records_button, "Рекорди");

                 
                DrawInterfaceButton(e, interface_main_menu_rectangle_settings_button, interface_main_menu_is_entered_settings_button, "Налаштування");

                 
                DrawInterfaceButton(e, interface_main_menu_rectangle_exit_button, interface_main_menu_is_entered_exit_button, "Вихід");
            }
            else if (m_form_status == DungeonFormStatus.InGameMenuConfirmExit)
            {
                 
                DrawText(e, interface_main_menu_rectangle_text, Brushes.White, "Вихід у меню");

                 
                DrawText(e, interface_confirm_exit_rectangle_text, Brushes.White, "Ви дійсно бажаєте вийти в меню без збереження?", true, 0.35);

                 
                DrawInterfaceButton(e, interface_confirm_exit_rectangle_confirm_button, interface_confirm_exit_is_entered_confirm_button, "Вийти у меню");

                 
                DrawInterfaceButton(e, interface_confirm_exit_rectangle_cancel_button, interface_confirm_exit_is_entered_cancel_button, "Відміна");
            }
            else if (m_form_status == DungeonFormStatus.InGameMenuConfirmLoad)
            {
                 
                DrawText(e, interface_main_menu_rectangle_text, Brushes.White, "Завантаження збереження");

                 
                DrawText(e, interface_confirm_exit_rectangle_text, Brushes.White, "Прогрес поточної гри буде втрачено!", true, 0.4);

                 
                DrawInterfaceButton(e, interface_confirm_exit_rectangle_confirm_button, interface_confirm_exit_is_entered_confirm_button, "Завантажити");

                 
                DrawInterfaceButton(e, interface_confirm_exit_rectangle_cancel_button, interface_confirm_exit_is_entered_cancel_button, "Відміна");
            }
            else if (m_form_status == DungeonFormStatus.NewGameStart)
            {
                 
                DrawText(e, interface_main_menu_rectangle_text, Brushes.White, "Гра");

                 
                DrawInterfaceButton(e, settings_rectangle_buttons[0], settings_is_entered_buttons[0], "Нова гра");

                 
                DrawInterfaceButton(e, settings_rectangle_buttons[2], settings_is_entered_buttons[2], "Завантажити гру");

                 
                DrawInterfaceButton(e, settings_rectangle_buttons[4], settings_is_entered_buttons[4], "Назад");
            }
            else if (m_form_status == DungeonFormStatus.NewGameDifficulty)
            {
                 
                DrawText(e, interface_main_menu_rectangle_text, Brushes.White, "Нова гра");

                 
                DrawText(e, settings_controls_rectangle_main_text, Brushes.White, "Виберіть рівень складності:");

                if (choosed_difficulty_id < 0 || choosed_difficulty_id > difficulties_number) choosed_difficulty_id = 0;  

                 
                DrawRectagle(e, new_game_difficulty_rectangle_difficulty_header, Brushes.Black, Brushes.White, 2);
                DrawText(e, new_game_difficulty_rectangle_difficulty_header, Brushes.Black, difficulties_headers[choosed_difficulty_id]);

                 
                DrawRectagle(e, new_game_difficulty_rectangle_difficulty_description, Brushes.Black, Brushes.White, 2);
                DrawText(e, new_game_difficulty_rectangle_difficulty_description, Brushes.Black, difficulties_texts[choosed_difficulty_id], false, 0.08);

                 
                if (choosed_difficulty_id > 0)
                {
                    if (new_game_difficulty_is_entered_arrow_left)
                    {
                        DrawImage(e, new_game_difficulty_rectangle_arrow_left, Properties.Resources.ARROW_LEFT_entered);
                    }
                    else
                    {
                        DrawImage(e, new_game_difficulty_rectangle_arrow_left, Properties.Resources.ARROW_LEFT);
                    }
                }

                 
                if (choosed_difficulty_id < difficulties_number - 1)
                {
                    if (new_game_difficulty_is_entered_arrow_right)
                    {
                        DrawImage(e, new_game_difficulty_rectangle_arrow_right, Properties.Resources.ARROW_RIGHT_entered);
                    }
                    else
                    {
                        DrawImage(e, new_game_difficulty_rectangle_arrow_right, Properties.Resources.ARROW_RIGHT);
                    }
                }

                 
                DrawInterfaceButton(e, new_game_difficulty_rectangle_button_next, new_game_difficulty_is_entered_button_next, "Далі");

                 
                DrawInterfaceButton(e, new_game_difficulty_rectangle_button_back, new_game_difficulty_is_entered_button_back, "Назад");
            }
            else if (m_form_status == DungeonFormStatus.NewGameCharacter)
            {
                 
                DrawText(e, interface_main_menu_rectangle_text, Brushes.White, "Нова гра");

                 
                DrawText(e, settings_controls_rectangle_main_text, Brushes.White, "Виберіть персонажа:");

                 
                DrawRectagle(e, new_game_character_rectangle_character_header, Brushes.Black, Brushes.White, 2);
                DrawText(e, new_game_character_rectangle_character_header, Brushes.Black, characters_names[choosed_character_id]);

                 
                DrawRectagle(e, new_game_character_rectangle_character_image, Brushes.Black, Brushes.White, 2);
                Point image_pos = new Point(new_game_character_rectangle_character_image.X + new_game_character_rectangle_character_image.Width / 2 - characters[choosed_character_id].Image.Width / 2, new_game_character_rectangle_character_image.Y + new_game_character_rectangle_character_image.Height / 2 - characters[choosed_character_id].Image.Height / 2 + 15);
                characters[choosed_character_id].ResetMoveDirection();
                characters[choosed_character_id].Paint(sender, e, image_pos, new Size(0, 0));

                 
                DrawRectagle(e, new_game_character_rectangle_character_items_text, Brushes.Black, Brushes.White, 2);
                DrawText(e, new_game_character_rectangle_character_items_text, Brushes.Black, "Початкові предмети:");

                 
                for (int i = 0; i < character_start_items; i++)
                {
                    DrawRectagle(e, new_game_character_rectangle_character_items[i], Brushes.Black, Brushes.White, 2);
                    if (characters_items[choosed_character_id, i] != null)
                    {
                        DrawImage(e, new_game_character_rectangle_character_items[i], characters_items[choosed_character_id, i].Image);
                    }
                }

                 
                DrawRectagle(e, new_game_character_rectangle_character_description, Brushes.Black, Brushes.White, 2);
                DrawText(e, new_game_character_rectangle_character_description, Brushes.Black, characters_texts[choosed_character_id], true, 0.04);

                 
                DrawRectagle(e, new_game_character_rectangle_character_stats, Brushes.Black, Brushes.White, 2);
                string stats_text = "";
                for (int i = 0; i < 9; i++)
                {
                    stats_text += DungeonStatsInfo.Name((DungeonStats)i) + ": " + characters[choosed_character_id].GetStatValue((DungeonStats)i) + "\n";
                }
                DrawText(e, new_game_character_rectangle_character_stats, Brushes.Black, stats_text, true, 0.04);

                 
                if (choosed_character_id > 0)
                {
                    if (new_game_character_is_entered_arrow_left)
                    {
                        DrawImage(e, new_game_character_rectangle_arrow_left, Properties.Resources.ARROW_LEFT_entered);
                    }
                    else
                    {
                        DrawImage(e, new_game_character_rectangle_arrow_left, Properties.Resources.ARROW_LEFT);
                    }
                }

                 
                if (choosed_character_id < characters_number - 1)
                {
                    if (new_game_character_is_entered_arrow_right)
                    {
                        DrawImage(e, new_game_character_rectangle_arrow_right, Properties.Resources.ARROW_RIGHT_entered);
                    }
                    else
                    {
                        DrawImage(e, new_game_character_rectangle_arrow_right, Properties.Resources.ARROW_RIGHT);
                    }
                }

                 
                DrawInterfaceButton(e, new_game_difficulty_rectangle_button_next, new_game_difficulty_is_entered_button_next, "Далі");

                 
                DrawInterfaceButton(e, new_game_difficulty_rectangle_button_back, new_game_difficulty_is_entered_button_back, "Назад");
            }
            else if (m_form_status == DungeonFormStatus.NewGameLevelGenerate)
            {
                 
                DrawText(e, interface_main_menu_rectangle_text, Brushes.White, "Нова гра");

                 
                DrawText(e, settings_controls_rectangle_main_text, Brushes.White, "Введіть ключ генерації світу:");

                 
                for (int i = 0; i < level_generation_nums_number; i++)
                {
                    if (new_game_level_generation_is_entered_arrows_up[i])
                    {
                        DrawImage(e, new_game_level_generation_rectangle_arrows_up[i], Properties.Resources.ARROW_UP_entered);
                    }
                    else
                    {
                        DrawImage(e, new_game_level_generation_rectangle_arrows_up[i], Properties.Resources.ARROW_UP);
                    }
                    DrawRectagle(e, new_game_level_generation_rectangle_values[i], Brushes.Black, Brushes.White, 2);
                    DrawText(e, new_game_level_generation_rectangle_values[i], Brushes.Black, choosed_level_generation_nums_values[i].ToString());
                    if (new_game_level_generation_is_entered_arrows_down[i])
                    {
                        DrawImage(e, new_game_level_generation_rectangle_arrows_down[i], Properties.Resources.ARROW_DOWN_entered);
                    }
                    else
                    {
                        DrawImage(e, new_game_level_generation_rectangle_arrows_down[i], Properties.Resources.ARROW_DOWN);
                    }
                }

                 
                if (new_game_level_generation_is_entered_button)
                {
                    brush = Brushes.YellowGreen;
                }
                else
                {
                    brush = Brushes.White;
                }
                DrawRectagle(e, new_game_level_generation_rectangle_button, Brushes.Black, brush, 2);
                DrawText(e, new_game_level_generation_rectangle_button, Brushes.Black, "Згенерувати");

                 
                if (new_game_level_generation_is_entered_button_reset)
                {
                    brush = Brushes.YellowGreen;
                }
                else
                {
                    brush = Brushes.White;
                }
                DrawRectagle(e, new_game_level_generation_rectangle_button_reset, Brushes.Black, brush, 2);
                DrawText(e, new_game_level_generation_rectangle_button_reset, Brushes.Black, "Скинути");

                 
                DrawInterfaceButton(e, new_game_difficulty_rectangle_button_next, new_game_difficulty_is_entered_button_next, "Почати гру");

                 
                DrawInterfaceButton(e, new_game_difficulty_rectangle_button_back, new_game_difficulty_is_entered_button_back, "Назад");
            }
            else if (m_form_status == DungeonFormStatus.Loading ||
                     m_form_status == DungeonFormStatus.LoadingReady ||
                     m_form_status == DungeonFormStatus.MainLoading ||
                     m_form_status == DungeonFormStatus.MainLoadingReady ||
                     m_form_status == DungeonFormStatus.Saving)
            {
                 
                if (m_form_status == DungeonFormStatus.Saving)
                {
                    DrawText(e, interface_main_menu_rectangle_text, Brushes.White, "Збереження гри");
                }
                else
                {
                    DrawText(e, interface_main_menu_rectangle_text, Brushes.White, "Завантаження гри");
                }

                 
                DrawText(e, loading_rectangle_text, Brushes.White, loading_text_procent + "\n" + loading_text, true, 0.25);

                 
                DrawRectagle(e, loading_rectangle_bar, Brushes.White);
                DrawRectagle(e, loading_rectangle_bar_total, Brushes.Red);
            }
            else if (m_form_status == DungeonFormStatus.Rules)
            {
                 
                DrawText(e, interface_main_menu_rectangle_text, Brushes.White, "Правила");

                 
                DrawText(e, rules_rectangle_text, Brushes.White, rules_texts[rules_total_page], true, 0.0433);


                 
                if (rules_total_page > 0)
                {
                    if (rules_is_entered_arrow_left)
                    {
                        DrawImage(e, rules_rectangle_arrow_left, Properties.Resources.ARROW_LEFT_entered);
                    }
                    else
                    {
                        DrawImage(e, rules_rectangle_arrow_left, Properties.Resources.ARROW_LEFT);
                    }
                }

                 
                if (rules_total_page < rules_pages_number - 1)
                {
                    if (rules_is_entered_arrow_right)
                    {
                        DrawImage(e, rules_rectangle_arrow_right, Properties.Resources.ARROW_RIGHT_entered);
                    }
                    else
                    {
                        DrawImage(e, rules_rectangle_arrow_right, Properties.Resources.ARROW_RIGHT);
                    }
                }

                 
                DrawInterfaceButton(e, settings_rectangle_buttons[5], settings_is_entered_buttons[5], "Назад");
            }
            else if (m_form_status == DungeonFormStatus.Settings)
            {
                 
                DrawText(e, interface_main_menu_rectangle_text, Brushes.White, "Налаштування");

                 
                string[] buttons_texts = new string[6];
                buttons_texts[0] = "Екран";
                buttons_texts[1] = "Інше";
                buttons_texts[2] = "Аудіо";
                buttons_texts[3] = "Інтерфейс";
                buttons_texts[4] = "Управління";
                buttons_texts[5] = "Назад";
                for (int i = 0; i < 6; i++)
                {
                    DrawInterfaceButton(e, settings_rectangle_buttons[i], settings_is_entered_buttons[i], buttons_texts[i]);
                }
            }
            else if (m_form_status == DungeonFormStatus.SettingsScreen)
            {
                 
                DrawText(e, interface_main_menu_rectangle_text, Brushes.White, "Налаштування екрана");

                 
                DrawText(e, settings_screen_rectangle_main_text, Brushes.White, "Розмір вікна:");

                 
                int total_choosed_id = m_total_resolution_id;
                for (int i = 0; i < settings_screen_number; i++)
                {
                    if (settings_screen_is_entered_radiobuttons[i]) brush = Brushes.YellowGreen;
                    else brush = Brushes.White;
                    DrawEllipse(e, settings_screen_rectangle_radiobuttons[i], Brushes.Black, brush, 3);
                    if (i == total_choosed_id)
                    {
                        Size size = new Size(settings_screen_rectangle_radiobuttons[i].Width / 2, settings_screen_rectangle_radiobuttons[i].Height / 2);
                        Point location = new Point(settings_screen_rectangle_radiobuttons[i].X + (settings_screen_rectangle_radiobuttons[i].Width - size.Width) / 2, settings_screen_rectangle_radiobuttons[i].Y + (settings_screen_rectangle_radiobuttons[i].Height - size.Height) / 2);
                        Rectangle rec = new Rectangle(location, size);
                        DrawEllipse(e, rec, Brushes.Black);
                    }
                    string text;
                    if (i == 0) text = "Повний екран";
                    else text = resolutions_width[i].ToString() + " x " + resolutions_height[i].ToString();
                    DrawText(e, settings_screen_rectangle_texts[i], brush, text);
                }

                 
                DrawInterfaceButton(e, settings_rectangle_buttons[5], settings_is_entered_buttons[5], "Назад");
            }
            else if (m_form_status == DungeonFormStatus.SettingsOther)
            {
                 
                DrawText(e, interface_main_menu_rectangle_text, Brushes.White, "Додаткові налаштування");

                 
                if (settings_other_is_entered_checkboxes[0]) brush = Brushes.YellowGreen;
                else brush = Brushes.White;
                DrawRectagle(e, settings_other_rectangle_checkboxes[0], Brushes.Black, brush, 3);
                if (m_interface_is_show_vignette)
                {
                    Size size = new Size(settings_other_rectangle_checkboxes[0].Width / 2, settings_other_rectangle_checkboxes[0].Height / 2);
                    Point location = new Point(settings_other_rectangle_checkboxes[0].X + (settings_other_rectangle_checkboxes[0].Width - size.Width) / 2, settings_other_rectangle_checkboxes[0].Y + (settings_other_rectangle_checkboxes[0].Height - size.Height) / 2);
                    Rectangle rec = new Rectangle(location, size);
                    DrawRectagle(e, rec, Brushes.Black);
                }
                DrawText(e, settings_other_rectangle_texts[0], brush, "Віньєтка");

                 
                if (settings_other_is_entered_checkboxes[1]) brush = Brushes.YellowGreen;
                else brush = Brushes.White;
                DrawRectagle(e, settings_other_rectangle_checkboxes[1], Brushes.Black, brush, 3);
                if (m_is_show_blood_effect_on_hit)
                {
                    Size size = new Size(settings_other_rectangle_checkboxes[1].Width / 2, settings_other_rectangle_checkboxes[1].Height / 2);
                    Point location = new Point(settings_other_rectangle_checkboxes[1].X + (settings_other_rectangle_checkboxes[1].Width - size.Width) / 2, settings_other_rectangle_checkboxes[1].Y + (settings_other_rectangle_checkboxes[1].Height - size.Height) / 2);
                    Rectangle rec = new Rectangle(location, size);
                    DrawRectagle(e, rec, Brushes.Black);
                }
                DrawText(e, settings_other_rectangle_texts[1], brush, "Ефект крові під час удару");

                 
                if (settings_other_is_entered_checkboxes[2]) brush = Brushes.YellowGreen;
                else brush = Brushes.White;
                DrawRectagle(e, settings_other_rectangle_checkboxes[2], Brushes.Black, brush, 3);
                if (m_is_autosave)
                {
                    Size size = new Size(settings_other_rectangle_checkboxes[2].Width / 2, settings_other_rectangle_checkboxes[2].Height / 2);
                    Point location = new Point(settings_other_rectangle_checkboxes[2].X + (settings_other_rectangle_checkboxes[2].Width - size.Width) / 2, settings_other_rectangle_checkboxes[2].Y + (settings_other_rectangle_checkboxes[2].Height - size.Height) / 2);
                    Rectangle rec = new Rectangle(location, size);
                    DrawRectagle(e, rec, Brushes.Black);
                }
                DrawText(e, settings_other_rectangle_texts[2], brush, "Автозбереження");

                 
                DrawInterfaceButton(e, settings_rectangle_buttons[5], settings_is_entered_buttons[5], "Назад");
            }
            else if (m_form_status == DungeonFormStatus.SettingsAudio)
            {
                 
                DrawText(e, interface_main_menu_rectangle_text, Brushes.White, "Налаштування аудіо");

                DrawText(e, settings_audio_rectangle_texts[0], Brushes.White, "Гучність звуків: ");
                if (settings_audio_is_entered_buttons_minus[0]) brush = Brushes.YellowGreen;
                else brush = Brushes.White;
                DrawRectagle(e, settings_audio_rectangle_buttons_minus[0], Brushes.Black, brush, 3);
                DrawText(e, settings_audio_rectangle_buttons_minus[0], Brushes.Black, "-", true, 0.75);
                DrawText(e, settings_audio_rectangle_values[0], Brushes.White, Properties.Settings.Default.settings_audio_volume_sound.ToString());
                if (settings_audio_is_entered_buttons_plus[0]) brush = Brushes.YellowGreen;
                else brush = Brushes.White;
                DrawRectagle(e, settings_audio_rectangle_buttons_plus[0], Brushes.Black, brush, 3);
                DrawText(e, settings_audio_rectangle_buttons_plus[0], Brushes.Black, "+", true, 0.75);

                DrawText(e, settings_audio_rectangle_texts[1], Brushes.White, "Гучність музики:");
                if (settings_audio_is_entered_buttons_minus[1]) brush = Brushes.YellowGreen;
                else brush = Brushes.White;
                DrawRectagle(e, settings_audio_rectangle_buttons_minus[1], Brushes.Black, brush, 3);
                DrawText(e, settings_audio_rectangle_buttons_minus[1], Brushes.Black, "-", true, 0.75);
                DrawText(e, settings_audio_rectangle_values[1], Brushes.White, Properties.Settings.Default.settings_audio_volume_music.ToString());
                if (settings_audio_is_entered_buttons_plus[1]) brush = Brushes.YellowGreen;
                else brush = Brushes.White;
                DrawRectagle(e, settings_audio_rectangle_buttons_plus[1], Brushes.Black, brush, 3);
                DrawText(e, settings_audio_rectangle_buttons_plus[1], Brushes.Black, "+", true, 0.75);

                 
                DrawInterfaceButton(e, settings_rectangle_buttons[5], settings_is_entered_buttons[5], "Назад");
            }
            else if (m_form_status == DungeonFormStatus.SettingsInterface)
            {
                 
                DrawText(e, interface_main_menu_rectangle_text, Brushes.White, "Налаштування інтерфейсу");

                 
                for (int i = 0; i < settings_interface_number; i++)
                {
                    if (settings_interface_is_entered_checkboxes[i]) brush = Brushes.YellowGreen;
                    else brush = Brushes.White;
                    DrawRectagle(e, settings_interface_rectangle_checkboxes[i], Brushes.Black, brush, 3);
                    if (settings_interface_checkboxes_choosed_ids[i])
                    {
                        Size size = new Size(settings_interface_rectangle_checkboxes[i].Width / 2, settings_interface_rectangle_checkboxes[i].Height / 2);
                        Point location = new Point(settings_interface_rectangle_checkboxes[i].X + (settings_interface_rectangle_checkboxes[i].Width - size.Width) / 2, settings_interface_rectangle_checkboxes[i].Y + (settings_interface_rectangle_checkboxes[i].Height - size.Height) / 2);
                        Rectangle rec = new Rectangle(location, size);
                        DrawRectagle(e, rec, Brushes.Black);
                    }
                    DrawText(e, settings_interface_rectangle_texts[i], brush, settings_interface_checkboxes_texts[i]);
                }

                 
                DrawInterfaceButton(e, settings_rectangle_buttons[5], settings_is_entered_buttons[5], "Назад");
            }
            else if (m_form_status == DungeonFormStatus.SettingsControls)
            {
                 
                DrawText(e, interface_main_menu_rectangle_text, Brushes.White, "Налаштування керування");

                 
                string text = "";
                if (id_changing_key != -1)
                {
                    text = "Натисніть клавішу, яку потрібно призначити...";
                }
                DrawText(e, settings_controls_rectangle_main_text, Brushes.Green, text);

                 
                for (int i = 0; i < keys_number; i++)
                {
                    if (i == id_changing_key)
                    {
                        brush = Brushes.Green;
                    }
                    else
                    {
                        if (settings_controls_is_entered_buttons[i]) brush = Brushes.YellowGreen;
                        else brush = Brushes.White;
                    }
                    DrawRectagle(e, settings_controls_rectangle_buttons[i], Brushes.Black, brush);
                    DrawText(e, settings_controls_rectangle_buttons[i], Brushes.Black, settings_controls_keys_choosed_keys[i].ToString(), true, 0.35);  
                    DrawText(e, settings_controls_rectangle_texts[i], brush, settings_controls_keys_texts[i], true, 0.35);  
                }

                 
                DrawInterfaceButton(e, settings_rectangle_buttons[5], settings_is_entered_buttons[5], "Назад");
            }
            else if (m_form_status == DungeonFormStatus.ConfirmExit)
            {
                 
                DrawText(e, interface_main_menu_rectangle_text, Brushes.White, "Вихід");

                 
                DrawText(e, interface_confirm_exit_rectangle_text, Brushes.White, "Ви дійсно хочете вийти із гри?", true, 0.4);

                 
                DrawInterfaceButton(e, interface_confirm_exit_rectangle_confirm_button, interface_confirm_exit_is_entered_confirm_button, "Вийти");

                 
                DrawInterfaceButton(e, interface_confirm_exit_rectangle_cancel_button, interface_confirm_exit_is_entered_cancel_button, "Відміна");
            }
            else if (m_form_status == DungeonFormStatus.GameEndWin || m_form_status == DungeonFormStatus.GameEndLoose)
            {
                 
                if (m_form_status == DungeonFormStatus.GameEndWin)
                {
                    DrawText(e, result_rectangle_main_text, Brushes.YellowGreen, "Ви перемогли!");
                }
                else if (m_form_status == DungeonFormStatus.GameEndLoose)
                {
                    DrawText(e, result_rectangle_main_text, Brushes.OrangeRed, "Ви програли!");
                }

                 
                for (byte i = 0; i < result_number; i++)
                {
                    DrawText(e, result_rectangle_texts[i], Brushes.White, m_result_text[i]);
                }

                if (m_form_status == DungeonFormStatus.GameEndWin)
                {
                     
                    DrawInterfaceButton(e, result_rectangle_button_load, result_is_entered_button_load, "Играть ещё раз");
                }
                else if (m_form_status == DungeonFormStatus.GameEndLoose)
                {
                     
                    DrawInterfaceButton(e, result_rectangle_button_load, result_is_entered_button_load, "Завантаження");
                }

                 
                DrawInterfaceButton(e, result_rectangle_button_go_menu, result_is_entered_button_go_menu, "Вийти у меню");
            }
        }

         

         
        private void timer_game_Tick(object sender, EventArgs e)
        {
            if (m_form_status == DungeonFormStatus.Game)
            {
                if (m_enabled_keys.IndexOf(Properties.Settings.Default.settings_controls_move_up) >= 0)  
                {
                    m_hero.Move(DungeonCreatureMoveDirection.Up);
                }
                if (m_enabled_keys.IndexOf(Properties.Settings.Default.settings_controls_move_down) >= 0)  
                {
                    m_hero.Move(DungeonCreatureMoveDirection.Down);
                }
                if (m_enabled_keys.IndexOf(Properties.Settings.Default.settings_controls_move_left) >= 0)  
                {
                    m_hero.Move(DungeonCreatureMoveDirection.Left);
                }
                if (m_enabled_keys.IndexOf(Properties.Settings.Default.settings_controls_move_right) >= 0)  
                {
                    m_hero.Move(DungeonCreatureMoveDirection.Right);
                }
                if (m_enabled_keys.IndexOf(Properties.Settings.Default.settings_controls_action_hit) != -1)  
                {
                    m_hero.Hit();
                }

                CalculateInterfaceHeroStats();

                Refresh();
            }
        }

         
        private void timer_stats_update_Tick(object sender, EventArgs e)
        {
            if (m_form_status == DungeonFormStatus.Game)
            {
                for (int i = 0; i < dungeon_levels_number; i++)
                {
                    for (int i2 = 0; i2 < m_dungeon_levels[i].Creatures.Count; i2++)
                    {
                        m_dungeon_levels[i].Creatures[i2].Regenerate(((double)(timer_stats_update.Interval) / 1000));
                        if (i2 < m_dungeon_levels[i].Creatures.Count)
                        {
                            m_dungeon_levels[i].Creatures[i2].Restore(((double)(timer_stats_update.Interval) / 1000));
                        }
                    }
                }
            }
        }

         
        private void timer_loading_wait_Tick(object sender, EventArgs e)
        {
            timer_loading_wait.Stop();
            m_form_status = DungeonFormStatus.LoadingReady;
            Refresh();
        }

         
        private void timer_main_loading_wait_Tick(object sender, EventArgs e)
        {
            timer_main_loading_wait.Stop();
            m_form_status = DungeonFormStatus.MainLoadingReady;
            Refresh();
        }

         
        private void timer_saving_wait_Tick(object sender, EventArgs e)
        {
            timer_saving_wait.Stop();
            m_form_status = DungeonFormStatus.InGameMenu;
            Refresh();
        }

         
        private void timer_monsters_move_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < m_hero.DungeonLevel.Creatures.Count; i++)
            {
                if (m_hero.DungeonLevel.Creatures[i] is DungeonMonster)
                {
                    DungeonMonster monster = m_hero.DungeonLevel.Creatures[i] as DungeonMonster;
                    if (monster.IsInPlayerView(m_hero, new Size(ShowingSize.Width + 100, ShowingSize.Height + 100)))
                    {
                        if (monster.ActionStatus == DungeoMonsterActionStatus.Fighting || monster.ActionStatus == DungeoMonsterActionStatus.MovingToCell)
                        {
                            monster.MakeStepToTargetLocation();
                        }
                    }
                }
            }
        }

         
        private void timer_walkthrow_Tick(object sender, EventArgs e)
        {
            m_hero.WalkthrowTime++;
        }

         
        private void timer_change_music_Tick(object sender, EventArgs e)
        {
            if (musics[m_total_music_id].IsEnded)
            {
                musics[m_total_music_id].Stop();
                ChooseRandomMusicId();
                musics[m_total_music_id].Play();
            }
        }

         

         
         
         
        public void DrawRectagle(PaintEventArgs e, Rectangle external_rec, Brush external_brush)
        {
            e.Graphics.FillRectangle(external_brush, external_rec);
        }

         
         
         
         
         
        public void DrawRectagle(PaintEventArgs e, Rectangle external_rec, Brush external_brush, Brush internal_brush, int border_size = -1)
        {
            DrawRectagle(e, external_rec, external_brush);
            if (border_size == -1) border_size = length_20;
            Size internal_size = new Size(external_rec.Width - border_size * 2, external_rec.Height - border_size * 2);
            Point internal_location = new Point(external_rec.X + border_size, external_rec.Y + border_size);
            Rectangle internal_rec = new Rectangle(internal_location, internal_size);
            DrawRectagle(e, internal_rec, internal_brush);
        }

         
         
         
        private void DrawEllipse(PaintEventArgs e, Rectangle external_rec, Brush external_brush)
        {
            e.Graphics.FillEllipse(external_brush, external_rec);
        }

         
         
         
         
         
        private void DrawEllipse(PaintEventArgs e, Rectangle external_rec, Brush external_brush, Brush internal_brush, int border_size = -1)
        {
            DrawEllipse(e, external_rec, external_brush);
            if (border_size == -1) border_size = length_20;
            Size internal_size = new Size(external_rec.Width - border_size * 2, external_rec.Height - border_size * 2);
            Point internal_location = new Point(external_rec.X + border_size, external_rec.Y + border_size);
            Rectangle internal_rec = new Rectangle(internal_location, internal_size);
            DrawEllipse(e, internal_rec, internal_brush);
        }

         
         
         
        private void DrawImage(PaintEventArgs e, Rectangle external_rec, Bitmap image)
        {
            Size new_image_size = new Size(image.Width, image.Height);
            if (new_image_size.Width > external_rec.Width)
            {
                double k = (double)external_rec.Width / new_image_size.Width;
                new_image_size = new Size((int)(new_image_size.Width * k), (int)(new_image_size.Height * k));
            }
            if (new_image_size.Height > external_rec.Height)
            {
                double k = (double)external_rec.Height / new_image_size.Height;
                new_image_size = new Size((int)(new_image_size.Width * k), (int)(new_image_size.Height * k));
            }
            Rectangle rec = new Rectangle(external_rec.X + external_rec.Width / 2 - new_image_size.Width / 2, external_rec.Y + external_rec.Height / 2 - new_image_size.Height / 2, new_image_size.Width, new_image_size.Height);
            e.Graphics.DrawImage(image, rec);
        }

         
         
         
         
         
         
         
        public void DrawText(PaintEventArgs e, Rectangle external_rec, Brush text_brush, string text, bool is_center = true, double k = 0.5, bool is_right = false)
        {
            StringFormat format = new StringFormat();
            if (is_center)
            {
                if (is_right) format.Alignment = StringAlignment.Far;
                else format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
            }
            e.Graphics.DrawString(text, new Font(m_usage_font_name, (float)(external_rec.Height * k), System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))), text_brush, external_rec, format);
        }

         
         
         
         
        private void DrawInterfaceButton(PaintEventArgs e, Rectangle rectangle, bool is_entered, string text)
        {
            Brush brush;  
            if (is_entered)
            {
                e.Graphics.DrawImage(Properties.Resources.BUTTON_MENU_on, rectangle);
                brush = Brushes.Black;
            }
            else
            {
                e.Graphics.DrawImage(Properties.Resources.BUTTON_MENU_off, rectangle);
                brush = Brushes.White;
            }
            DrawText(e, rectangle, brush, text);
        }

         

         
        private static void ChooseRandomMusicId()
        {
            Random random = new Random();
            int new_music_id;
            do
            {
                new_music_id = random.Next(0, musics_number);

            } while (new_music_id == m_total_music_id);
            m_total_music_id = new_music_id;
        }

         
        private static void GenerateImagesFromTextureHelmets()
        {
            const int helmet_height = 73;
            const int helmet_width = 54;

            Bitmap texture = Properties.Resources.TEXTURE_item_helmets;

            Bitmap new_image_w;
            Bitmap new_image_s;
            Bitmap new_image_a;
            Bitmap new_image_d;
            Bitmap new_image;

            for (int quality_id = 0; quality_id < 10; quality_id++)
            {
                for (int style_id = 0; style_id < 3; style_id++)
                {
                    new_image_w = new Bitmap(helmet_width, helmet_height);
                    new_image_s = new Bitmap(helmet_width, helmet_height);
                    new_image_a = new Bitmap(helmet_width, helmet_height);
                    new_image_d = new Bitmap(helmet_width, helmet_height);
                    new_image = new Bitmap(helmet_width, helmet_height);

                    for (int i = helmet_height * 3 * quality_id + helmet_height * style_id; i < helmet_height * 3 * quality_id + helmet_height * style_id + helmet_height; i++)
                    {
                        for (int i2 = 0; i2 < texture.Width; i2++)
                        {
                            Color pixel = texture.GetPixel(i2, i);
                            if (i2 < helmet_width)
                            {
                                new_image_w.SetPixel(i2, i - (helmet_height * 3 * quality_id + helmet_height * style_id), pixel);
                            }
                            else if (i2 < helmet_width * 2)
                            {
                                new_image_s.SetPixel(i2 - helmet_width, i - (helmet_height * 3 * quality_id + helmet_height * style_id), pixel);
                            }
                            else if (i2 < helmet_width * 3)
                            {
                                new_image_a.SetPixel(i2 - helmet_width * 2, i - (helmet_height * 3 * quality_id + helmet_height * style_id), pixel);
                            }
                            else if (i2 < helmet_width * 4)
                            {
                                new_image_d.SetPixel(i2 - helmet_width * 3, i - (helmet_height * 3 * quality_id + helmet_height * style_id), pixel);
                            }
                            else if (i2 < helmet_width * 5)
                            {
                                new_image.SetPixel(i2 - helmet_width * 4, i - (helmet_height * 3 * quality_id + helmet_height * style_id), pixel);
                            }
                        }
                    }

                    ImagesHelmets[quality_id, style_id, 0] = new_image_w;
                    ImagesHelmets[quality_id, style_id, 1] = new_image_s;
                    ImagesHelmets[quality_id, style_id, 2] = new_image_a;
                    ImagesHelmets[quality_id, style_id, 3] = new_image_d;
                    ImagesHelmets[quality_id, style_id, 4] = new_image;
                }
            }
        }

         
        private static void GenerateImagesFromTextureArmours()
        {
            const int armour_height = 23;
            const int armour_width_w_s = 42;
            const int armour_width_a_d = 32;

            Bitmap texture = Properties.Resources.TEXTURE_item_armours;

            Bitmap new_image_w;
            Bitmap new_image_s;
            Bitmap new_image_a;
            Bitmap new_image_d;

            for (int quality_id = 0; quality_id < 10; quality_id++)
            {
                for (int style_id = 0; style_id < 3; style_id++)
                {

                    if (quality_id < 0) quality_id = 0;
                    else if (quality_id >= 10) quality_id = 9;

                    if (style_id < 0) style_id = 0;
                    else if (style_id >= 3) style_id = 2;

                    new_image_w = new Bitmap(armour_width_w_s, armour_height);
                    new_image_s = new Bitmap(armour_width_w_s, armour_height);
                    new_image_a = new Bitmap(armour_width_a_d, armour_height);
                    new_image_d = new Bitmap(armour_width_a_d, armour_height);

                    for (int i = armour_height * 3 * quality_id + armour_height * style_id; i < armour_height * 3 * quality_id + armour_height * style_id + armour_height; i++)
                    {
                        for (int i2 = 0; i2 < texture.Width; i2++)
                        {
                            Color pixel = texture.GetPixel(i2, i);
                            if (i2 < armour_width_w_s)
                            {
                                new_image_w.SetPixel(i2, i - (armour_height * 3 * quality_id + armour_height * style_id), pixel);
                            }
                            else if (i2 < armour_width_w_s * 2)
                            {
                                new_image_s.SetPixel(i2 - armour_width_w_s, i - (armour_height * 3 * quality_id + armour_height * style_id), pixel);
                            }
                            else if (i2 < armour_width_w_s * 2 + armour_width_a_d)
                            {
                                new_image_a.SetPixel(i2 - armour_width_w_s * 2, i - (armour_height * 3 * quality_id + armour_height * style_id), pixel);
                            }
                            else if (i2 < armour_width_w_s * 2 + armour_width_a_d * 2)
                            {
                                new_image_d.SetPixel(i2 - armour_width_w_s * 2 - armour_width_a_d, i - (armour_height * 3 * quality_id + armour_height * style_id), pixel);
                            }
                        }
                    }

                    ImagesArmours[quality_id, style_id, 0] = new_image_w;
                    ImagesArmours[quality_id, style_id, 1] = new_image_s;
                    ImagesArmours[quality_id, style_id, 2] = new_image_a;
                    ImagesArmours[quality_id, style_id, 3] = new_image_d;
                }
            }
        }

         
        private static void GenerateImagesFromTextureSwords()
        {
            const int sword_width = 70;
            const int sword_height = 18;
            const int sword_gif_height = 90;
            const int sword_gif_width_a_d = 90;
            const int sword_gif_width_w_s = 17;

            GifImage gif = new GifImage(Properties.Resources.TEXTURE_item_swords);
            GifImage gif2 = new GifImage(Properties.Resources.TEXTURE_item_swords_w_s);

            Bitmap new_image;
            Bitmap total_texture;
            Bitmap new_image_a;
            Bitmap new_image_d;
            Bitmap total_texture2;
            Bitmap new_image_w;
            Bitmap new_image_s;

            for (int quality_id = 0; quality_id < 30; quality_id++)
            {
                ImagesSwordsGif[quality_id, 0] = new GifAnimation();
                ImagesSwordsGif[quality_id, 1] = new GifAnimation();
                ImagesSwordsGif[quality_id, 2] = new GifAnimation();
                ImagesSwordsGif[quality_id, 3] = new GifAnimation();

                bool is_first = true;
                new_image = new Bitmap(sword_width, sword_height);
                for (int j = 0; j < gif.FramesCount; j++)
                {
                    total_texture = gif.GetNextFrame();

                    new_image_a = new Bitmap(sword_gif_width_a_d, sword_gif_height);
                    new_image_d = new Bitmap(sword_gif_width_a_d, sword_gif_height);

                    if (is_first)
                    {
                        for (int i = sword_gif_height * quality_id; i < sword_gif_height * quality_id + sword_height; i++)
                        {
                            for (int i2 = 0; i2 < sword_width; i2++)
                            {
                                Color pixel = total_texture.GetPixel(i2, i);
                                new_image.SetPixel(i2, i - sword_gif_height * quality_id, pixel);
                            }
                        }
                        is_first = false;
                    }

                    for (int i = sword_gif_height * quality_id; i < sword_gif_height * quality_id + sword_gif_height; i++)
                    {
                        for (int i2 = sword_width; i2 < sword_width + sword_gif_width_a_d; i2++)
                        {
                            Color pixel = total_texture.GetPixel(i2, i);
                            new_image_a.SetPixel(i2 - sword_width, i - sword_gif_height * quality_id, pixel);
                        }
                        for (int i2 = sword_width + sword_gif_width_a_d; i2 < sword_width + sword_gif_width_a_d * 2; i2++)
                        {
                            Color pixel = total_texture.GetPixel(i2, i);
                            new_image_d.SetPixel(i2 - sword_width - sword_gif_width_a_d, i - sword_gif_height * quality_id, pixel);
                        }
                    }

                    ImagesSwordsGif[quality_id, 2].AddFrame(new_image_a);
                    ImagesSwordsGif[quality_id, 3].AddFrame(new_image_d);
                }

                int k = quality_id % 3;
                for (int j = 0; j < gif2.FramesCount; j++)
                {
                    total_texture2 = gif2.GetNextFrame();

                    new_image_w = new Bitmap(sword_gif_width_w_s, sword_gif_height);
                    new_image_s = new Bitmap(sword_gif_width_w_s, sword_gif_height);

                    for (int i = sword_gif_height * k; i < sword_gif_height * (k + 1); i++)
                    {
                        for (int i2 = 0; i2 < sword_gif_width_w_s; i2++)
                        {
                            Color pixel = total_texture2.GetPixel(i2, i);
                            new_image_w.SetPixel(i2, i - sword_gif_height * k, pixel);
                        }
                        for (int i2 = sword_gif_width_w_s; i2 < sword_gif_width_w_s * 2; i2++)
                        {
                            Color pixel = total_texture2.GetPixel(i2, i);
                            new_image_s.SetPixel(i2 - sword_gif_width_w_s, i - sword_gif_height * k, pixel);
                        }
                    }

                    ImagesSwordsGif[quality_id, 0].AddFrame(new_image_w);
                    ImagesSwordsGif[quality_id, 1].AddFrame(new_image_s);
                }
                ImagesSwords[quality_id] = new_image;
            }
        }

         
        private static void LoadAudio()
        {
            musics[0] = new AudioEffect(Properties.Resources.MUSIC_GAME_1_c152___Arcade_Time, true);
            musics[1] = new AudioEffect(Properties.Resources.MUSIC_GAME_2_c152___Stranger, true);
            musics[2] = new AudioEffect(Properties.Resources.MUSIC_GAME_3_c152___Blue_Sun, true);
            m_total_music_id = 0;

            sounds[0] = new AudioEffect(Properties.Resources.SOUND_1_level_up, false);
            sounds[1] = new AudioEffect(Properties.Resources.SOUND_2_door_opened, false);
            sounds[2] = new AudioEffect(Properties.Resources.SOUND_3_door_not_opened, false);
            sounds[3] = new AudioEffect(Properties.Resources.SOUND_4_hit_start, false);
            sounds[4] = new AudioEffect(Properties.Resources.SOUND_5_hit_damage, false);
        }

         
        public void LoadForm()
        {
            CalculateInterface();
            m_form_status = DungeonFormStatus.Menu;
            m_enabled_keys.Clear();
            m_can_minimize = true;
            if (m_total_music_id != -1)
            {
                musics[m_total_music_id].Stop();
            }
            m_total_music_id = 0;
            musics[m_total_music_id].Play();
            rules_total_page = 0;
            is_game_menu = false;
        }

         
        private void LoadAll()
        {
            SetLoadingText("Завантаження налаштувань...");
            GetSettings();  
            CheckResolution();  
            PlusLoadingChaptersTotal(1);

            SetLoadingText("Завантаження музики...");
            LoadAudio();
            PlusLoadingChaptersTotal(1);

            SetLoadingText("Завантаження текстур...");
            GenerateImagesFromTextureHelmets();
            PlusLoadingChaptersTotal(1);
            GenerateImagesFromTextureArmours();
            PlusLoadingChaptersTotal(1);
            GenerateImagesFromTextureSwords();
            PlusLoadingChaptersTotal(1);

            SetLoadingText("Натисніть будь-яку клавішу, щоб продовжити...");
            timer_main_loading_wait.Start();  

            m_is_form_loaded = true;
        }

         
        private void ResetAllButtonsIsEntryValue()
        {
            for (int i = 0; i < 30; i++) interface_is_entered_inventory_cells[i] = false;
            for (int i = 0; i < special_items_number; i++) interface_is_entered_inventory_cells[i] = false;
            interface_is_entered_inventory_item_info_button = false;
            for (int i = 0; i < 3; i++) interface_is_entered_inventory_item_button[i] = false;
            interface_is_entered_inventory_button = false;
            interface_is_entered_statistic_button = false;
            interface_is_entered_map_button = false;
            interface_main_menu_is_entered_game_button = false;
            interface_main_menu_is_entered_rules_button = false;
            interface_main_menu_is_entered_records_button = false;
            interface_main_menu_is_entered_settings_button = false;
            interface_main_menu_is_entered_exit_button = false;
            interface_confirm_exit_is_entered_confirm_button = false;
            interface_confirm_exit_is_entered_cancel_button = false;
            for (int i = 0; i < 6; i++) settings_is_entered_buttons[i] = false;
            for (int i = 0; i < settings_screen_number; i++) settings_screen_is_entered_radiobuttons[i] = false;
            for (int i = 0; i < settings_other_number; i++) settings_other_is_entered_checkboxes[i] = false;
            for (int i = 0; i < settings_audio_number; i++) settings_audio_is_entered_buttons_minus[i] = false;
            for (int i = 0; i < settings_audio_number; i++) settings_audio_is_entered_buttons_plus[i] = false;
            for (int i = 0; i < settings_interface_number; i++) settings_interface_is_entered_checkboxes[i] = false;
            for (int i = 0; i < keys_number; i++) settings_controls_is_entered_buttons[i] = false;
            new_game_difficulty_is_entered_arrow_left = false;
            new_game_difficulty_is_entered_arrow_right = false;
            new_game_difficulty_is_entered_button_next = false;
            new_game_difficulty_is_entered_button_back = false;
            new_game_character_is_entered_arrow_left = false;
            new_game_character_is_entered_arrow_right = false;
            for (int i = 0; i < level_generation_nums_number; i++) new_game_level_generation_is_entered_arrows_up[i] = false;
            for (int i = 0; i < level_generation_nums_number; i++) new_game_level_generation_is_entered_arrows_down[i] = false;
            new_game_level_generation_is_entered_button = false;
            new_game_level_generation_is_entered_button_reset = false;
            for (int i = 0; i < code_nums_number; i++) code_is_entered_arrows_up[i] = false;
            for (int i = 0; i < code_nums_number; i++) code_is_entered_arrows_down[i] = false;
            code_is_entered_button_back = false;
            code_is_entered_button_open = false;
            result_is_entered_button_load = false;
            result_is_entered_button_go_menu = false;
        }

         
        private void CreateCharacters()
        {
             
             
             
            characters_textures[0] = Properties.Resources.TEXTURE_player_Rick;
            characters_names[0] = "Рік";
            characters_texts[0] = "Життя не наділило його ні силою, ні інтелектом, проте він знайшов себе дуже спритним, швидким і вкрай щасливим - навіть на шляху до Підземелля він випадково наткнувся на артефакт і взяв його з собою.";
            characters_items[0, 0] = new DungeonItemSword(8, "Меч Ріка");
            (characters_items[0, 0] as DungeonItemEquipment).AddEffect(DungeonStats.Power, 6);
            characters_items[0, 1] = new DungeonItemHelmet(2, 0, "Шолом Ріка", "Звичайний сталевий шолом. Збільшує максимальну енергію власника.");
            (characters_items[0, 1] as DungeonItemEquipment).AddEffect(DungeonStats.MaxEnergy, 20);
            characters_items[0, 2] = new DungeonItemArtifact(17, 3, "Артефакт Ріка", "Знайдений Ріком на шляху до Підземелля. Цей артефакт підвищує удачу свого власника, можливо, саме тому доля звела його з Ріком. Також трохи прискорює відновлення енергії.");  
            (characters_items[0, 2] as DungeonItemEquipment).AddEffect(DungeonStats.Luck, 15);
            (characters_items[0, 2] as DungeonItemEquipment).AddEffect(DungeonStats.Restore, 1);
            characters[0] = new DungeonHero(characters_textures[0], characters_names[0]);
            characters[0].UpStatValue(DungeonStats.Restore, true);
            characters[0].UpStatValue(DungeonStats.Speed, true);
            characters[0].UpStatValue(DungeonStats.Speed, true);
            characters[0].UpStatValue(DungeonStats.Mobility, true);
            characters[0].UpStatValue(DungeonStats.Mobility, true);
            characters[0].UpStatValue(DungeonStats.Luck, true);
            characters[0].UpStatValue(DungeonStats.Luck, true);
            characters[0].UpStatValue(DungeonStats.Luck, true);
            characters[0].UpStatValue(DungeonStats.Luck, true);
            characters[0].UpStatValue(DungeonStats.Luck, true);
            characters[0].Container.Add(characters_items[0, 0]);
            characters[0].Container.Add(characters_items[0, 1]);
            characters[0].Container.Add(characters_items[0, 2]);

            characters_textures[1] = Properties.Resources.TEXTURE_player_Liza;
            characters_names[1] = "Ліза";
            characters_texts[1] = "Юркая початківська шукачка пригод, що має непоганий інтелект і деякі знання з алхімії зіл, завдяки яким змогла взяти з собою пару зразків.";
            characters_items[1, 0] = new DungeonItemSword(2, "Меч Лізи");
            (characters_items[1, 0] as DungeonItemEquipment).AddEffect(DungeonStats.Power, 8);
            characters_items[1, 1] = new DungeonItemPotion(2, 0, "Зілля Лізи", "Зілля, зварене Лізою перед походом в підземелля. Має неймовірну здатність відновлювати здоров'я і енергію, але діє недовго і уповільнює використання");
            (characters_items[1, 1] as DungeonItemEquipment).AddEffect(DungeonStats.Regeneration, 10, 5);
            (characters_items[1, 1] as DungeonItemEquipment).AddEffect(DungeonStats.Restore, 10, 5);
            (characters_items[1, 1] as DungeonItemEquipment).AddEffect(DungeonStats.Speed, -1.5, 5);
            characters_items[1, 2] = new DungeonItemPotion(1, 1, "Зілля Лізи", "Зілля, зварене Лізою перед походом у підземелля. Збільшує максимальний запас здоров'я та енергії на тривалий час.");
            (characters_items[1, 2] as DungeonItemEquipment).AddEffect(DungeonStats.MaxHealth, 50, 90);
            (characters_items[1, 2] as DungeonItemEquipment).AddEffect(DungeonStats.MaxEnergy, 50, 90);
            characters[1] = new DungeonHero(characters_textures[1], characters_names[1]);
            characters[1].UpStatValue(DungeonStats.MaxEnergy, true);
            characters[1].UpStatValue(DungeonStats.Regeneration, true);
            characters[1].UpStatValue(DungeonStats.Restore, true);
            characters[1].UpStatValue(DungeonStats.Intelligence, true);
            characters[1].UpStatValue(DungeonStats.Intelligence, true);
            characters[1].UpStatValue(DungeonStats.Intelligence, true);
            characters[1].UpStatValue(DungeonStats.Intelligence, true);
            characters[1].UpStatValue(DungeonStats.Intelligence, true);
            characters[1].UpStatValue(DungeonStats.Mobility, true);
            characters[1].UpStatValue(DungeonStats.Luck, true);
            characters[1].Container.Add(characters_items[1, 0]);
            characters[1].Container.Add(characters_items[1, 1]);
            characters[1].Container.Add(characters_items[1, 2]);

            characters_textures[2] = Properties.Resources.TEXTURE_player_Sam;
            characters_names[2] = "Сем";
            characters_texts[2] = "Дуже сильний, але незграбний і вкрай невдачливий шукач пригод, який вирішив вирушити в підземелля відразу після купівлі нового спорядження.";
            characters_items[2, 0] = new DungeonItemSword(11, "Меч Сема");
            (characters_items[2, 0] as DungeonItemEquipment).AddEffect(DungeonStats.Power, 12);
            characters_items[2, 1] = new DungeonItemHelmet(4, 0, "Шолом Сема", "Хороший сталевий шолом. Збільшує запас і відновлення енергії власника.");
            (characters_items[2, 1] as DungeonItemEquipment).AddEffect(DungeonStats.MaxEnergy, 50);
            (characters_items[2, 1] as DungeonItemEquipment).AddEffect(DungeonStats.Restore, 3);
            characters_items[2, 2] = new DungeonItemArmour(4, 0, "Броня Сема", "Хороша броня. Збільшує максимальне здоров'я власника і прискорює його регенерацію.");
            (characters_items[2, 2] as DungeonItemEquipment).AddEffect(DungeonStats.MaxHealth, 50);
            (characters_items[2, 2] as DungeonItemEquipment).AddEffect(DungeonStats.Regeneration, 1.5);
            characters[2] = new DungeonHero(characters_textures[2], characters_names[2]);
            characters[2].UpStatValue(DungeonStats.MaxHealth, true);
            characters[2].UpStatValue(DungeonStats.MaxEnergy, true);
            characters[2].UpStatValue(DungeonStats.MaxEnergy, true);
            characters[2].UpStatValue(DungeonStats.Intelligence, true);
            characters[2].UpStatValue(DungeonStats.Restore, true);
            characters[2].UpStatValue(DungeonStats.Power, true);
            characters[2].UpStatValue(DungeonStats.Power, true);
            characters[2].UpStatValue(DungeonStats.Power, true);
            characters[2].UpStatValue(DungeonStats.Power, true);
            characters[2].UpStatValue(DungeonStats.Power, true);
            characters[2].Container.Add(characters_items[2, 0]);
            characters[2].Container.Add(characters_items[2, 1]);
            characters[2].Container.Add(characters_items[2, 2]);

            characters_textures[3] = Properties.Resources.TEXTURE_player_Ella;
            characters_names[3] = "Элла";
            characters_texts[3] = "Від народження вона має міцне здоров'я, а тому змогла набути хорошої фізичної форми, але не дуже цікавилася навчанням, тому має низький інтелект.";
            characters_items[3, 0] = new DungeonItemSword(5, "Меч Елли");
            (characters_items[3, 0] as DungeonItemEquipment).AddEffect(DungeonStats.Power, 10);
            characters_items[3, 1] = new DungeonItemArmour(2, 1, "Броня Елли", "Непогана броня. Збільшує максимальне здоров'я власника і прискорює його регенерацію.");
            (characters_items[3, 1] as DungeonItemEquipment).AddEffect(DungeonStats.MaxHealth, 35);
            (characters_items[3, 1] as DungeonItemEquipment).AddEffect(DungeonStats.Regeneration, 1);
            characters_items[3, 2] = new DungeonItemPotion(1, 1, "Зілля Елли", "Зілля, куплене Еллою перед походом у підземелля. Збільшує швидкість на тривалий час.");
            (characters_items[3, 2] as DungeonItemEquipment).AddEffect(DungeonStats.Speed, 2.5, 150);
            characters[3] = new DungeonHero(characters_textures[3], characters_names[3]);
            characters[3].UpStatValue(DungeonStats.MaxHealth, true);
            characters[3].UpStatValue(DungeonStats.MaxHealth, true);
            characters[3].UpStatValue(DungeonStats.MaxHealth, true);
            characters[3].UpStatValue(DungeonStats.MaxHealth, true);
            characters[3].UpStatValue(DungeonStats.MaxHealth, true);
            characters[3].UpStatValue(DungeonStats.Regeneration, true);
            characters[3].UpStatValue(DungeonStats.Restore, true);
            characters[3].UpStatValue(DungeonStats.Restore, true);
            characters[3].UpStatValue(DungeonStats.Power, true);
            characters[3].UpStatValue(DungeonStats.Power, true);
            characters[3].Container.Add(characters_items[3, 0]);
            characters[3].Container.Add(characters_items[3, 1]);
            characters[3].Container.Add(characters_items[3, 2]);
             
        }

         
        private void StartTimers()
        {
            timer_game.Start();
            timer_stats_update.Start();
            timer_monsters_move.Start();
            timer_walkthrow.Start();
            timer_change_music.Start();

            is_game_saved = false;
        }

         
        private void StopTimers()
        {
            timer_game.Stop();
            timer_stats_update.Stop();
            timer_monsters_move.Stop();
            timer_walkthrow.Stop();
            timer_change_music.Stop();
        }

         
        private void GetSettingsScreen()
        {
             
             
             
            m_total_resolution_id = Properties.Settings.Default.settings_screen_resolution_id;
             
        }

         
        private void GetSettingsOther()
        {
             
             
             
            m_interface_is_show_vignette = Properties.Settings.Default.settings_other_is_vignette;
            m_is_show_blood_effect_on_hit = Properties.Settings.Default.settings_other_is_blood_effect_on_hit;
            m_is_show_blood_effect_on_hit = Properties.Settings.Default.settings_other_is_blood_effect_on_hit;
            m_is_autosave = Properties.Settings.Default.settings_other_is_autosave;
             
        }

         
        private void GetSettingsAudio()
        {
             
             
             
            VolumeSound = Properties.Settings.Default.settings_audio_volume_sound;
            VolumeMusic = Properties.Settings.Default.settings_audio_volume_music;
             
        }

         
        private void GetSettingsInterface()
        {
             
             
             
            m_interface_is_show_hero_level = settings_interface_checkboxes_choosed_ids[0] = Properties.Settings.Default.settings_interface_is_hero_level;
            settings_interface_checkboxes_texts[0] = "Рівень персонажа";

            m_interface_is_show_stats = settings_interface_checkboxes_choosed_ids[1] = Properties.Settings.Default.settings_interface_is_stats;
            settings_interface_checkboxes_texts[1] = "Здоров'я, енергія";

            m_interface_is_show_dungeon_level = settings_interface_checkboxes_choosed_ids[2] = Properties.Settings.Default.settings_interface_is_dungeon_level;
            settings_interface_checkboxes_texts[2] = "Рівень підземелля";

            m_interface_is_show_potions = settings_interface_checkboxes_choosed_ids[3] = Properties.Settings.Default.settings_interface_is_potions;
            settings_interface_checkboxes_texts[3] = "Зілля";

            m_interface_is_show_buttons = settings_interface_checkboxes_choosed_ids[4] = Properties.Settings.Default.settings_interface_is_buttons;
            settings_interface_checkboxes_texts[4] = "Кнопки";

            m_interface_is_show_minimap = settings_interface_checkboxes_choosed_ids[5] = Properties.Settings.Default.settings_interface_is_minimap;
            settings_interface_checkboxes_texts[5] = "Міні-карта";
             
        }

         
        private void GetSettingsControls()
        {
             
             
             
            settings_controls_keys_choosed_keys[0] = Properties.Settings.Default.settings_controls_move_up;
            settings_controls_keys_texts[0] = "Іти вгору";

            settings_controls_keys_choosed_keys[1] = Properties.Settings.Default.settings_controls_move_down;
            settings_controls_keys_texts[1] = "Іти вниз";

            settings_controls_keys_choosed_keys[2] = Properties.Settings.Default.settings_controls_move_left;
            settings_controls_keys_texts[2] = "Іти вліво";

            settings_controls_keys_choosed_keys[3] = Properties.Settings.Default.settings_controls_move_right;
            settings_controls_keys_texts[3] = "Іти праворуч";

            settings_controls_keys_choosed_keys[4] = Properties.Settings.Default.settings_controls_inventory;
            settings_controls_keys_texts[4] = "Відкрити/закрити інвентар";

            settings_controls_keys_choosed_keys[5] = Properties.Settings.Default.settings_controls_statistic;
            settings_controls_keys_texts[5] = "Відкрити/закрити прокачування";

            settings_controls_keys_choosed_keys[6] = Properties.Settings.Default.settings_controls_map;
            settings_controls_keys_texts[6] = "Відкрити/закрити карту";

            settings_controls_keys_choosed_keys[7] = Properties.Settings.Default.settings_controls_open_ingame_menu;
            settings_controls_keys_texts[7] = "Відкрити/закрити меню";

            settings_controls_keys_choosed_keys[8] = Properties.Settings.Default.settings_controls_action_pickup_item;
            settings_controls_keys_texts[8] = "Підняти предмет";

            settings_controls_keys_choosed_keys[9] = Properties.Settings.Default.settings_controls_item_use;
            settings_controls_keys_texts[9] = "Використовувати предмет";

            settings_controls_keys_choosed_keys[10] = Properties.Settings.Default.settings_controls_item_drop;
            settings_controls_keys_texts[10] = "Викинути предмет";

            settings_controls_keys_choosed_keys[11] = Properties.Settings.Default.settings_controls_item_destroy;
            settings_controls_keys_texts[11] = "Знищити предмет";

            settings_controls_keys_choosed_keys[12] = Properties.Settings.Default.settings_controls_item_info;
            settings_controls_keys_texts[12] = "Інформація про предмет";

            settings_controls_keys_choosed_keys[13] = Properties.Settings.Default.settings_controls_use_potion_1;
            settings_controls_keys_texts[13] = "Використовувати зілля 1";

            settings_controls_keys_choosed_keys[14] = Properties.Settings.Default.settings_controls_use_potion_2;
            settings_controls_keys_texts[14] = "Використовувати зілля 2";

            settings_controls_keys_choosed_keys[15] = Properties.Settings.Default.settings_controls_use_potion_3;
            settings_controls_keys_texts[15] = "Використовувати зілля 3";

            settings_controls_keys_choosed_keys[16] = Properties.Settings.Default.settings_controls_action_hit;
            settings_controls_keys_texts[16] = "Завдати удару";

            settings_controls_keys_choosed_keys[17] = Properties.Settings.Default.settings_controls_choose_up;
            settings_controls_keys_texts[17] = "Вибрати осередок вище";

            settings_controls_keys_choosed_keys[18] = Properties.Settings.Default.settings_controls_choose_down;
            settings_controls_keys_texts[18] = "Вибрати осередок нижче";

            settings_controls_keys_choosed_keys[19] = Properties.Settings.Default.settings_controls_choose_left;
            settings_controls_keys_texts[19] = "Вибрати осередок лівіше";

            settings_controls_keys_choosed_keys[20] = Properties.Settings.Default.settings_controls_choose_right;
            settings_controls_keys_texts[20] = "Вибрати осередок правіше";

            settings_controls_keys_choosed_keys[21] = Properties.Settings.Default.settings_controls_game_save;
            settings_controls_keys_texts[21] = "Зберегти гру";

            settings_controls_keys_choosed_keys[22] = Properties.Settings.Default.settings_controls_game_load;
            settings_controls_keys_texts[22] = "Завантажити гру";

            settings_controls_keys_choosed_keys[23] = Properties.Settings.Default.settings_controls_hide_interface;
            settings_controls_keys_texts[23] = "Приховати/показати інтерфейс";
             
        }

         
        private void GetSettings()
        {
            GetSettingsScreen();
            GetSettingsInterface();
            GetSettingsControls();
            GetSettingsOther();
            GetSettingsAudio();
        }

         
        private void SetAndSaveSettingsScreen()
        {
             
             
             
            Properties.Settings.Default.settings_screen_resolution_id = m_total_resolution_id;
             
            Properties.Settings.Default.Save();
        }

         
        private void SetAndSaveSettingsOther()
        {
             
             
             
            Properties.Settings.Default.settings_other_is_vignette = m_interface_is_show_vignette;
            Properties.Settings.Default.settings_other_is_blood_effect_on_hit = m_is_show_blood_effect_on_hit;
            Properties.Settings.Default.settings_other_is_autosave = m_is_autosave;
             
            Properties.Settings.Default.Save();
        }

         
        private void SetAndSaveSettingsAudio()
        {
             
             
             
            Properties.Settings.Default.settings_audio_volume_sound = VolumeSound;
            Properties.Settings.Default.settings_audio_volume_music = VolumeMusic;
             
            Properties.Settings.Default.Save();
        }

         
        private void SetAndSaveSettingsInterface()
        {
             
             
             
            Properties.Settings.Default.settings_interface_is_hero_level = settings_interface_checkboxes_choosed_ids[0];
            Properties.Settings.Default.settings_interface_is_stats = settings_interface_checkboxes_choosed_ids[1];
            Properties.Settings.Default.settings_interface_is_dungeon_level = settings_interface_checkboxes_choosed_ids[2];
            Properties.Settings.Default.settings_interface_is_potions = settings_interface_checkboxes_choosed_ids[3];
            Properties.Settings.Default.settings_interface_is_buttons = settings_interface_checkboxes_choosed_ids[4];
            Properties.Settings.Default.settings_interface_is_minimap = settings_interface_checkboxes_choosed_ids[5];
             
            Properties.Settings.Default.Save();
        }

         
        private void SetAndSaveSettingsControls()
        {
             
             
             
            Properties.Settings.Default.settings_controls_move_up = settings_controls_keys_choosed_keys[0];
            Properties.Settings.Default.settings_controls_move_down = settings_controls_keys_choosed_keys[1];
            Properties.Settings.Default.settings_controls_move_left = settings_controls_keys_choosed_keys[2];
            Properties.Settings.Default.settings_controls_move_right = settings_controls_keys_choosed_keys[3];
            Properties.Settings.Default.settings_controls_inventory = settings_controls_keys_choosed_keys[4];
            Properties.Settings.Default.settings_controls_statistic = settings_controls_keys_choosed_keys[5];
            Properties.Settings.Default.settings_controls_map = settings_controls_keys_choosed_keys[6];
            Properties.Settings.Default.settings_controls_open_ingame_menu = settings_controls_keys_choosed_keys[7];
            Properties.Settings.Default.settings_controls_action_pickup_item = settings_controls_keys_choosed_keys[8];
            Properties.Settings.Default.settings_controls_item_use = settings_controls_keys_choosed_keys[9];
            Properties.Settings.Default.settings_controls_item_drop = settings_controls_keys_choosed_keys[10];
            Properties.Settings.Default.settings_controls_item_destroy = settings_controls_keys_choosed_keys[11];
            Properties.Settings.Default.settings_controls_item_info = settings_controls_keys_choosed_keys[12];
            Properties.Settings.Default.settings_controls_use_potion_1 = settings_controls_keys_choosed_keys[13];
            Properties.Settings.Default.settings_controls_use_potion_2 = settings_controls_keys_choosed_keys[14];
            Properties.Settings.Default.settings_controls_use_potion_3 = settings_controls_keys_choosed_keys[15];
            Properties.Settings.Default.settings_controls_action_hit = settings_controls_keys_choosed_keys[16];
            Properties.Settings.Default.settings_controls_choose_up = settings_controls_keys_choosed_keys[17];
            Properties.Settings.Default.settings_controls_choose_down = settings_controls_keys_choosed_keys[18];
            Properties.Settings.Default.settings_controls_choose_left = settings_controls_keys_choosed_keys[19];
            Properties.Settings.Default.settings_controls_choose_right = settings_controls_keys_choosed_keys[20];
            Properties.Settings.Default.settings_controls_game_save = settings_controls_keys_choosed_keys[21];
            Properties.Settings.Default.settings_controls_game_load = settings_controls_keys_choosed_keys[22];
            Properties.Settings.Default.settings_controls_hide_interface = settings_controls_keys_choosed_keys[23];
             
            Properties.Settings.Default.Save();
        }

         
        private void CheckResolution()
        {
            if (m_total_resolution_id < 0 || m_total_resolution_id >= settings_screen_number)
            {
                m_total_resolution_id = Properties.Settings.Default.settings_screen_resolution_id = 0;
            }
            if (m_total_resolution_id == 0)  
            {
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                FormBorderStyle = FormBorderStyle.Fixed3D;
                WindowState = FormWindowState.Normal;
                Width = resolutions_width[m_total_resolution_id];
                Height = resolutions_height[m_total_resolution_id];
                CenterToScreen();
            }
            CalculateInterface();
            if (is_game_menu)
            {
                CalculateInterfaceHeroStats();
                CalculateInterfaceHeroActivePotions();
            }
        }

         
        private void CalculateInterface()
        {
            if (ClientSize.Width > 0 && ClientSize.Height > 0)
            {
                if ((double)ClientSize.Width / ClientSize.Height > (double)4 / 3)
                {
                    length_1 = ClientSize.Height * 4 / 3 / 23;
                }
                else
                {
                    length_1 = ClientSize.Width / 23;
                }

                length_2 = length_1 / 2;
                length_4 = length_1 / 4;
                length_8 = length_1 / 8;
                length_20 = length_1 / 20;

                CalculateInterfaceMenu();

                if (m_hero != null)
                {
                    CalculateInterfaceGame();
                    CalculateInterfaceHeroActivePotions();
                    CalculateInterfaceHeroStats();
                    CalculateInterfaceHeroMap();
                }


                Refresh();
            }
        }

         
        private void CalculateInterfaceMenu()
        {
            Size button_size = Properties.Resources.BUTTON_MENU_off.Size;
            int between = (ClientSize.Height - Properties.Resources.LOGO.Size.Height - button_size.Height * 5) / 15;

             
             
             
             
            Size main_menu_size_background = Properties.Resources.BACKGROUND.Size;
            Point main_menu_location_background = new Point((ClientSize.Width - main_menu_size_background.Width) / 2, (ClientSize.Height - main_menu_size_background.Height) / 2);
            interface_main_menu_rectangle_background = new Rectangle(main_menu_location_background, main_menu_size_background);

             
            Size main_menu_size_logo = Properties.Resources.LOGO.Size;
            Point main_menu_location_logo = new Point((ClientSize.Width - main_menu_size_logo.Width) / 2, between * 2);
            interface_main_menu_rectangle_logo = new Rectangle(main_menu_location_logo, main_menu_size_logo);

             
            Size main_menu_size_text = new Size(ClientSize.Width, button_size.Height);
            Point main_menu_location_text = new Point(0, main_menu_location_logo.Y + main_menu_size_logo.Height + between);
            interface_main_menu_rectangle_text = new Rectangle(main_menu_location_text, main_menu_size_text);
             

             
             
             
             
            Point interface_main_menu_location_game_button = new Point((ClientSize.Width - button_size.Width) / 2, main_menu_location_text.Y + main_menu_size_text.Height + between * 2);
            interface_main_menu_rectangle_game_button = new Rectangle(interface_main_menu_location_game_button, button_size);

             
            Point interface_main_menu_location_rules_button = new Point(interface_main_menu_location_game_button.X, interface_main_menu_location_game_button.Y + button_size.Height + between);
            interface_main_menu_rectangle_rules_button = new Rectangle(interface_main_menu_location_rules_button, button_size);

             
            Point interface_main_menu_location_records_button = new Point(interface_main_menu_location_game_button.X, interface_main_menu_location_rules_button.Y + button_size.Height + between);
            interface_main_menu_rectangle_records_button = new Rectangle(interface_main_menu_location_records_button, button_size);

             
            Point interface_main_menu_location_settings_button = new Point(interface_main_menu_location_game_button.X, interface_main_menu_location_records_button.Y + button_size.Height + between);
            interface_main_menu_rectangle_settings_button = new Rectangle(interface_main_menu_location_settings_button, button_size);

             
            Point interface_main_menu_location_exit_button = new Point(interface_main_menu_location_game_button.X, interface_main_menu_location_settings_button.Y + button_size.Height + between);
            interface_main_menu_rectangle_exit_button = new Rectangle(interface_main_menu_location_exit_button, button_size);
             

             
             
             
             
            Size interface_confirm_exit_size_text = new Size(ClientSize.Width, button_size.Height);
            Point interface_confirm_exit_location_text = new Point(0, interface_main_menu_location_rules_button.Y);
            interface_confirm_exit_rectangle_text = new Rectangle(interface_confirm_exit_location_text, interface_confirm_exit_size_text);

             
            Point interface_confirm_exit_location_confirm_button = new Point(ClientSize.Width / 2 - button_size.Width - between, interface_confirm_exit_location_text.Y + interface_confirm_exit_size_text.Height + between);
            interface_confirm_exit_rectangle_confirm_button = new Rectangle(interface_confirm_exit_location_confirm_button, button_size);

             
            Point interface_confirm_exit_location_cancel_button = new Point(ClientSize.Width / 2 + between, interface_confirm_exit_location_confirm_button.Y);
            interface_confirm_exit_rectangle_cancel_button = new Rectangle(interface_confirm_exit_location_cancel_button, button_size);
             

             
             
             
             
            Point[] settings_location_buttons = new Point[6];
            settings_location_buttons[0] = interface_main_menu_location_game_button;
            settings_rectangle_buttons[0] = new Rectangle(settings_location_buttons[0], button_size);
            for (int i = 1; i < 6; i++)
            {
                settings_location_buttons[i] = new Point(settings_rectangle_buttons[0].X, settings_location_buttons[i - 1].Y + button_size.Height + length_8);
                settings_rectangle_buttons[i] = new Rectangle(settings_location_buttons[i], button_size);
            }
             

             
             
             
            int height = length_2 + length_8;
            Size settings_screen_size_radiobutton = new Size(height, height);
            int height_all = settings_location_buttons[5].Y - (main_menu_location_text.Y + main_menu_size_text.Height);
            int between3 = (height_all - (settings_screen_size_radiobutton.Height * (settings_screen_number + 1))) / (settings_screen_number + 2);

             
            Size settings_screen_size_main_text = new Size(height * 7, height);

             
            Size settings_screen_size_texts = new Size(settings_screen_size_main_text.Width - settings_screen_size_radiobutton.Width, height);

             
            Point settings_screen_location_main_text = new Point((ClientSize.Width - settings_screen_size_main_text.Width) / 2, main_menu_location_text.Y + main_menu_size_text.Height + between3);
            settings_screen_rectangle_main_text = new Rectangle(settings_screen_location_main_text, settings_screen_size_main_text);

             
            Point[] settings_screen_location_radiobuttons = new Point[settings_screen_number];
            Point[] settings_screen_location_texts = new Point[settings_screen_number];
            settings_screen_location_radiobuttons[0] = new Point(settings_screen_location_main_text.X, settings_screen_location_main_text.Y + settings_screen_size_main_text.Height + between3);
            settings_screen_rectangle_radiobuttons[0] = new Rectangle(settings_screen_location_radiobuttons[0], settings_screen_size_radiobutton);
            settings_screen_location_texts[0] = new Point(settings_screen_location_radiobuttons[0].X + settings_screen_size_radiobutton.Width, settings_screen_location_radiobuttons[0].Y);
            settings_screen_rectangle_texts[0] = new Rectangle(settings_screen_location_texts[0], settings_screen_size_texts);
            for (int i = 1; i < settings_screen_number; i++)
            {
                settings_screen_location_radiobuttons[i] = new Point(settings_screen_location_radiobuttons[0].X, settings_screen_location_radiobuttons[i - 1].Y + settings_screen_size_radiobutton.Height + between3);
                settings_screen_rectangle_radiobuttons[i] = new Rectangle(settings_screen_location_radiobuttons[i], settings_screen_size_radiobutton);
                settings_screen_location_texts[i] = new Point(settings_screen_location_radiobuttons[i].X + settings_screen_size_radiobutton.Width, settings_screen_location_radiobuttons[i].Y);
                settings_screen_rectangle_texts[i] = new Rectangle(settings_screen_location_texts[i], settings_screen_size_texts);
            }
             

             
             
             
            Size rules_size_text = new Size(height_all * 2, height_all);
            Point rules_location_text = new Point(ClientSize.Width / 2 - rules_size_text.Width / 2, main_menu_location_text.Y + main_menu_size_text.Height);
            rules_rectangle_text = new Rectangle(rules_location_text, rules_size_text);

            Size rules_size_arrow = Properties.Resources.ARROW_LEFT.Size;

             
            Point rules_location_arrow_left = new Point(rules_rectangle_text.X - length_8 - rules_size_arrow.Width, rules_rectangle_text.Y + rules_rectangle_text.Height / 2 - rules_size_arrow.Height / 2);
            rules_rectangle_arrow_left = new Rectangle(rules_location_arrow_left, rules_size_arrow);

             
            Point rules_location_arrow_right = new Point(rules_rectangle_text.X + rules_rectangle_text.Width + length_8, rules_location_arrow_left.Y);
            rules_rectangle_arrow_right = new Rectangle(rules_location_arrow_right, rules_size_arrow);
             

             
             
             
             
            Point[] settings_other_location_checkboxes = new Point[settings_other_number];

             
            Point[] settings_other_location_texts = new Point[settings_other_number];

            Size settings_other_size_checkbox = new Size(height, height);
            height_all = settings_location_buttons[5].Y - (main_menu_location_text.Y + main_menu_size_text.Height);
            int between4 = (height_all - (settings_other_size_checkbox.Height * (settings_other_number + 2))) / (settings_other_number + 3);

            Size settings_other_size_texts = new Size(height * 12 - settings_other_size_checkbox.Width, height);

            settings_other_location_checkboxes[0] = new Point((ClientSize.Width - settings_other_size_texts.Width) / 2, main_menu_location_text.Y + main_menu_size_text.Height + between4);
            settings_other_rectangle_checkboxes[0] = new Rectangle(settings_other_location_checkboxes[0], settings_other_size_checkbox);
            settings_other_location_texts[0] = new Point(settings_other_location_checkboxes[0].X + settings_other_size_checkbox.Width, settings_other_location_checkboxes[0].Y);
            settings_other_rectangle_texts[0] = new Rectangle(settings_other_location_texts[0], settings_other_size_texts);
            for (int i = 1; i < settings_other_number; i++)
            {
                settings_other_location_checkboxes[i] = new Point(settings_other_location_checkboxes[0].X, settings_other_location_checkboxes[i - 1].Y + settings_other_size_checkbox.Height + between4);
                settings_other_rectangle_checkboxes[i] = new Rectangle(settings_other_location_checkboxes[i], settings_other_size_checkbox);
                settings_other_location_texts[i] = new Point(settings_other_location_checkboxes[i].X + settings_other_size_checkbox.Width, settings_other_location_checkboxes[i].Y);
                settings_other_rectangle_texts[i] = new Rectangle(settings_other_location_texts[i], settings_other_size_texts);
            }
             

             
             
             
             
            Point[] settings_audio_location_texts = new Point[settings_audio_number];

             
            Point[] settings_audio_location_buttons_minus = new Point[settings_audio_number];
            Point[] settings_audio_location_buttons_plus = new Point[settings_audio_number];
            Size settings_audio_size_button = new Size(height, height);

             
            Point[] settings_audio_location_values = new Point[settings_audio_number];
            Size settings_audio_size_value = new Size(settings_audio_size_button.Width * 2, settings_audio_size_button.Height);

            int between6 = (height_all - (settings_audio_size_button.Height * (settings_audio_number + 2))) / (settings_audio_number + 3);

            Size settings_audio_size_texts = new Size(height * 12 - settings_audio_size_button.Width * 2 - settings_audio_size_value.Width, height);

             
            settings_audio_location_texts[0] = new Point((ClientSize.Width - height * 12) / 2, main_menu_location_text.Y + main_menu_size_text.Height + between6);
            settings_audio_rectangle_texts[0] = new Rectangle(settings_audio_location_texts[0], settings_audio_size_texts);

             
            settings_audio_location_buttons_minus[0] = new Point(settings_audio_location_texts[0].X + settings_audio_size_texts.Width, settings_audio_location_texts[0].Y);
            settings_audio_rectangle_buttons_minus[0] = new Rectangle(settings_audio_location_buttons_minus[0], settings_audio_size_button);

             
            settings_audio_location_values[0] = new Point(settings_audio_location_buttons_minus[0].X + settings_audio_size_button.Width, settings_audio_location_texts[0].Y);
            settings_audio_rectangle_values[0] = new Rectangle(settings_audio_location_values[0], settings_audio_size_value);

             
            settings_audio_location_buttons_plus[0] = new Point(settings_audio_location_values[0].X + settings_audio_size_value.Width, settings_audio_location_texts[0].Y);
            settings_audio_rectangle_buttons_plus[0] = new Rectangle(settings_audio_location_buttons_plus[0], settings_audio_size_button);
            for (int i = 1; i < settings_audio_number; i++)
            {
                 
                settings_audio_location_texts[i] = new Point(settings_audio_location_texts[0].X, settings_audio_location_texts[i - 1].Y + settings_audio_size_texts.Height + between6);
                settings_audio_rectangle_texts[i] = new Rectangle(settings_audio_location_texts[i], settings_audio_size_texts);

                 
                settings_audio_location_buttons_minus[i] = new Point(settings_audio_location_buttons_minus[0].X, settings_audio_location_texts[i].Y);
                settings_audio_rectangle_buttons_minus[i] = new Rectangle(settings_audio_location_buttons_minus[i], settings_audio_size_button);

                 
                settings_audio_location_values[i] = new Point(settings_audio_location_values[0].X, settings_audio_location_texts[i].Y);
                settings_audio_rectangle_values[i] = new Rectangle(settings_audio_location_values[i], settings_audio_size_value);

                 
                settings_audio_location_buttons_plus[i] = new Point(settings_audio_location_buttons_plus[0].X, settings_audio_location_texts[i].Y);
                settings_audio_rectangle_buttons_plus[i] = new Rectangle(settings_audio_location_buttons_plus[i], settings_audio_size_button);
            }
             

             
             
             
            height = length_2 + length_8;
            Size settings_interface_size_checkbox = new Size(height, height);
            height_all = settings_location_buttons[5].Y - (main_menu_location_text.Y + main_menu_size_text.Height);
            int between5 = (height_all - (settings_interface_size_checkbox.Height * (settings_interface_number))) / (settings_interface_number + 1);

            Size settings_interface_size_texts = new Size(height * 10 - settings_interface_size_checkbox.Width, height);

             
            Point[] settings_interface_location_checkboxes = new Point[settings_interface_number];
            Point[] settings_interface_location_texts = new Point[settings_interface_number];
            settings_interface_location_checkboxes[0] = new Point((ClientSize.Width - settings_interface_size_texts.Width) / 2, main_menu_location_text.Y + main_menu_size_text.Height + between5);
            settings_interface_rectangle_checkboxes[0] = new Rectangle(settings_interface_location_checkboxes[0], settings_interface_size_checkbox);
            settings_interface_location_texts[0] = new Point(settings_interface_location_checkboxes[0].X + settings_interface_size_checkbox.Width, settings_interface_location_checkboxes[0].Y);
            settings_interface_rectangle_texts[0] = new Rectangle(settings_interface_location_texts[0], settings_interface_size_texts);
            for (int i = 1; i < settings_interface_number; i++)
            {
                settings_interface_location_checkboxes[i] = new Point(settings_interface_location_checkboxes[0].X, settings_interface_location_checkboxes[i - 1].Y + settings_interface_size_checkbox.Height + between5);
                settings_interface_rectangle_checkboxes[i] = new Rectangle(settings_interface_location_checkboxes[i], settings_interface_size_checkbox);
                settings_interface_location_texts[i] = new Point(settings_interface_location_checkboxes[i].X + settings_interface_size_checkbox.Width, settings_interface_location_checkboxes[i].Y);
                settings_interface_rectangle_texts[i] = new Rectangle(settings_interface_location_texts[i], settings_interface_size_texts);
            }
             

             
             
             
            height = length_2 + length_4;
            Size settings_controls_size_button = new Size(height * 2, height);
            between3 = (height_all - (settings_controls_size_button.Height * (settings_screen_number + 1))) / (settings_screen_number + 2);

            Size settings_controls_size_main_text = new Size(ClientSize.Width, height);
            Size settings_controls_size_texts = new Size(height * 10 - settings_controls_size_button.Width, height);

            Point settings_controls_location_main_text = new Point((ClientSize.Width - settings_controls_size_main_text.Width) / 2, main_menu_location_text.Y + main_menu_size_text.Height + between3);
            settings_controls_rectangle_main_text = new Rectangle(settings_controls_location_main_text, settings_controls_size_main_text);

             
            Point[] settings_controls_location_buttons = new Point[keys_number];
            Point[] settings_controls_location_texts = new Point[keys_number];
            settings_controls_location_buttons[0] = new Point(ClientSize.Width / 2 - (settings_controls_size_texts.Width + settings_controls_size_button.Width) / 2 - between3 - (settings_controls_size_texts.Width + settings_controls_size_button.Width), settings_controls_location_main_text.Y + settings_controls_size_main_text.Height + between3);
            settings_controls_rectangle_buttons[0] = new Rectangle(settings_controls_location_buttons[0], settings_controls_size_button);
            for (int i = 0; i < 8; i++)  
            {
                for (int i2 = 0; i2 < 3; i2++)  
                {
                    int id = i2 * 8 + i;
                    if (id < keys_number)
                    {
                        if (id != 0)
                        {
                            settings_controls_location_buttons[id] = new Point(settings_controls_location_buttons[0].X + (settings_controls_size_button.Width + settings_controls_size_texts.Width + between3) * i2, settings_controls_location_buttons[0].Y + (settings_controls_size_button.Height + between3) * i);
                            settings_controls_rectangle_buttons[id] = new Rectangle(settings_controls_location_buttons[id], settings_controls_size_button);
                        }
                        settings_controls_location_texts[id] = new Point(settings_controls_location_buttons[id].X + settings_controls_size_button.Width, settings_controls_location_buttons[id].Y);
                        settings_controls_rectangle_texts[id] = new Rectangle(settings_controls_location_texts[id], settings_controls_size_texts);
                    }
                }
            }
             

             
             
             
            height_all = settings_location_buttons[5].Y - (settings_controls_location_main_text.Y + settings_controls_size_main_text.Height);
            height = height_all * 10 / 100;
            Size new_game_difficulty_size_difficulty_header = new Size(height * 18, height * 2);
            Size new_game_difficulty_size_difficulty_description = new Size(new_game_difficulty_size_difficulty_header.Width, height * 7);
            int between7 = (height_all - new_game_difficulty_size_difficulty_header.Height - new_game_difficulty_size_difficulty_description.Height) / 2;
            Size new_game_difficulty_size_arrow = Properties.Resources.ARROW_LEFT.Size;

             
            Point new_game_difficulty_location_difficulty_header = new Point(ClientSize.Width / 2 - new_game_difficulty_size_difficulty_header.Width / 2, settings_controls_location_main_text.Y + settings_controls_size_main_text.Height + between7);
            new_game_difficulty_rectangle_difficulty_header = new Rectangle(new_game_difficulty_location_difficulty_header, new_game_difficulty_size_difficulty_header);

             
            Point new_game_difficulty_location_difficulty_description = new Point(new_game_difficulty_location_difficulty_header.X, new_game_difficulty_location_difficulty_header.Y + new_game_difficulty_size_difficulty_header.Height);
            new_game_difficulty_rectangle_difficulty_description = new Rectangle(new_game_difficulty_location_difficulty_description, new_game_difficulty_size_difficulty_description);

             
            Point new_game_difficulty_location_button_next = new Point(ClientSize.Width / 2 - between7 * 2 - button_size.Width, settings_location_buttons[5].Y);
            new_game_difficulty_rectangle_button_next = new Rectangle(new_game_difficulty_location_button_next, button_size);

             
            Point new_game_difficulty_location_button_back = new Point(ClientSize.Width / 2 + between7 * 2, new_game_difficulty_location_button_next.Y);
            new_game_difficulty_rectangle_button_back = new Rectangle(new_game_difficulty_location_button_back, button_size);
             

             
             
             
            Size big_block_size = new Size(height * 6, height_all - height * 2);

             
            Point new_game_character_location_character_header = new Point(ClientSize.Width / 2 - big_block_size.Width / 2 - big_block_size.Width, settings_controls_location_main_text.Y + settings_controls_size_main_text.Height + between7);
            Size new_game_character_size_character_header = new Size(big_block_size.Width * 3, height);
            new_game_character_rectangle_character_header = new Rectangle(new_game_character_location_character_header, new_game_character_size_character_header);

             
            Size new_game_character_size_character_item = new Size(big_block_size.Width / 3, big_block_size.Width / 3);
            Point[] new_game_character_location_character_items = new Point[character_start_items];

             
            Size new_game_character_size_character_items_text = new Size(big_block_size.Width, new_game_character_size_character_header.Height / 2);

             
            Point new_game_character_location_character_image = new Point(new_game_character_location_character_header.X, new_game_character_location_character_header.Y + new_game_character_size_character_header.Height);
            Size new_game_character_size_character_image = new Size(big_block_size.Width, big_block_size.Height - new_game_character_size_character_items_text.Height - new_game_character_size_character_item.Height);
            new_game_character_rectangle_character_image = new Rectangle(new_game_character_location_character_image, new_game_character_size_character_image);

             
            Point new_game_character_location_character_items_text = new Point(new_game_character_location_character_image.X, new_game_character_location_character_image.Y + new_game_character_size_character_image.Height);
            new_game_character_rectangle_character_items_text = new Rectangle(new_game_character_location_character_items_text, new_game_character_size_character_items_text);

             
            for (int i = 0; i < character_start_items; i++)
            {
                new_game_character_location_character_items[i] = new Point(new_game_character_location_character_items_text.X + new_game_character_size_character_item.Width * i, new_game_character_location_character_items_text.Y + new_game_character_size_character_items_text.Height);
                new_game_character_rectangle_character_items[i] = new Rectangle(new_game_character_location_character_items[i], new_game_character_size_character_item);
            }

             
            Point new_game_character_location_character_description = new Point(new_game_character_location_character_image.X + big_block_size.Width, new_game_character_location_character_image.Y);
            Size new_game_character_size_character_description = big_block_size;
            new_game_character_rectangle_character_description = new Rectangle(new_game_character_location_character_description, new_game_character_size_character_description);

             
            Point new_game_character_location_character_stats = new Point(new_game_character_location_character_description.X + big_block_size.Width, new_game_character_location_character_image.Y);
            Size new_game_character_size_character_stats = big_block_size;
            new_game_character_rectangle_character_stats = new Rectangle(new_game_character_location_character_stats, new_game_character_size_character_stats);

             
            Size new_game_character_size_arrow = new_game_difficulty_size_arrow;
            Point new_game_character_location_arrow_left = new Point(new_game_character_location_character_image.X - between7 - new_game_character_size_arrow.Width, new_game_character_location_character_header.Y + (new_game_character_size_character_header.Height + big_block_size.Height) / 2 - new_game_character_size_arrow.Height / 2);
            new_game_character_rectangle_arrow_left = new Rectangle(new_game_character_location_arrow_left, new_game_character_size_arrow);

             
            Point new_game_character_location_arrow_right = new Point(new_game_character_location_character_stats.X + big_block_size.Width + between7, new_game_character_location_arrow_left.Y);
            new_game_character_rectangle_arrow_right = new Rectangle(new_game_character_location_arrow_right, new_game_character_size_arrow);
             

             
             
             
             
            Size new_game_level_generation_size_arrow_up = Properties.Resources.ARROW_UP.Size;

             
            Size new_game_level_generation_size_value = new Size(height * 2, height * 3);

             
            Size new_game_level_generation_size_arrow_down = Properties.Resources.ARROW_DOWN.Size;

             
            Size new_game_level_generation_size_button = new Size(((new_game_level_generation_size_value.Width + between7) * level_generation_nums_number - between7) / 2 - between7, new_game_level_generation_size_value.Height / 2);

             
            Point[] new_game_level_generation_location_arrows_up = new Point[level_generation_nums_number];
            for (int i = 0; i < level_generation_nums_number; i++)
            {
                new_game_level_generation_location_arrows_up[i] = new Point(ClientSize.Width / 2 - between7 / 2 - (new_game_level_generation_size_value.Width + between7) * (level_generation_nums_number / 2) + between7 + (new_game_level_generation_size_value.Width + between7) * i + (new_game_level_generation_size_value.Width - new_game_level_generation_size_arrow_up.Width) / 2, settings_controls_location_main_text.Y + settings_controls_size_main_text.Height + (height_all - (new_game_level_generation_size_arrow_up.Height + between7 + new_game_level_generation_size_value.Height + between7 + new_game_level_generation_size_arrow_down.Height)) / 2 - 15);
                new_game_level_generation_rectangle_arrows_up[i] = new Rectangle(new_game_level_generation_location_arrows_up[i], new_game_level_generation_size_arrow_up);
            }

             
            Point[] new_game_level_generation_location_values = new Point[level_generation_nums_number];
            for (int i = 0; i < level_generation_nums_number; i++)
            {
                new_game_level_generation_location_values[i] = new Point(new_game_level_generation_location_arrows_up[i].X - (new_game_level_generation_size_value.Width - new_game_level_generation_size_arrow_up.Width) / 2, new_game_level_generation_location_arrows_up[i].Y + new_game_level_generation_size_arrow_up.Height + between7);
                new_game_level_generation_rectangle_values[i] = new Rectangle(new_game_level_generation_location_values[i], new_game_level_generation_size_value);
            }

             
            Point[] new_game_level_generation_location_arrows_down = new Point[level_generation_nums_number];
            for (int i = 0; i < level_generation_nums_number; i++)
            {
                new_game_level_generation_location_arrows_down[i] = new Point(new_game_level_generation_location_arrows_up[i].X, new_game_level_generation_location_values[i].Y + new_game_level_generation_size_value.Height + between7);
                new_game_level_generation_rectangle_arrows_down[i] = new Rectangle(new_game_level_generation_location_arrows_down[i], new_game_level_generation_size_arrow_down);
            }

             
            Point new_game_level_generation_location_button = new Point(new_game_level_generation_location_values[0].X, new_game_level_generation_location_arrows_down[0].Y + new_game_level_generation_size_arrow_down.Height + between7);
            new_game_level_generation_rectangle_button = new Rectangle(new_game_level_generation_location_button, new_game_level_generation_size_button);

             
            Point new_game_level_generation_location_button_reset = new Point(new_game_level_generation_location_button.X + new_game_level_generation_size_button.Width + between7 * 2, new_game_level_generation_location_button.Y);
            new_game_level_generation_rectangle_button_reset = new Rectangle(new_game_level_generation_location_button_reset, new_game_level_generation_size_button);
             

             
             
             
             
            Point in_game_menu_location_background = new Point(0, 0);
            Size in_game_menu_size_background = ClientSize;
            in_game_menu_rectangle_background = new Rectangle(in_game_menu_location_background, in_game_menu_size_background);
             

             
             
             
            Size loading_size_text = new Size(ClientSize.Width, height * 2);
            Size loading_size_bar = new Size(ClientSize.Width * 8 / 10, height);

            int between8 = (height_all - loading_size_text.Height - loading_size_bar.Height) / 2;

             
            Point loading_location_text = new Point(ClientSize.Width / 2 - loading_size_text.Width / 2, settings_controls_location_main_text.Y + settings_controls_size_main_text.Height + between8);
            loading_rectangle_text = new Rectangle(loading_location_text, loading_size_text);

             
            Point loading_location_bar = new Point(ClientSize.Width / 2 - loading_size_bar.Width / 2, loading_location_text.Y + loading_size_text.Height);
            loading_rectangle_bar = new Rectangle(loading_location_bar, loading_size_bar);
             

             
             
             
             
            result_rectangle_button_load = new_game_difficulty_rectangle_button_next;

             
            result_rectangle_button_go_menu = new_game_difficulty_rectangle_button_back;

            int height2 = (result_rectangle_button_load.Y - (interface_main_menu_rectangle_logo.Y + interface_main_menu_rectangle_logo.Height) - length_20 * 2);
            Size result_size_main_text = new Size(ClientSize.Width, height2 / 5);

             
            Point result_location_main_text = new Point(0, interface_main_menu_rectangle_logo.Y + interface_main_menu_rectangle_logo.Height + length_20);
            result_rectangle_main_text = new Rectangle(result_location_main_text, result_size_main_text);

             
            Point[] result_location_texts = new Point[result_number];
            Size result_size_text = new Size(ClientSize.Width, height2 * 4 / 5 / result_number - length_20);
            for (byte i = 0; i < result_number; i++)
            {
                result_location_texts[i] = new Point(0, result_location_main_text.Y + result_size_main_text.Height + length_20 + (result_size_text.Height + length_20) * i);
                result_rectangle_texts[i] = new Rectangle(result_location_texts[i], result_size_text);
            }
             

            CalculateLoadingBar();
        }

         
        private void CalculateInterfaceGame()
        {
             
             
             
             
            Point[] interface_location_level_text = new Point[3];
            Size[] interface_size_level_text = new Size[3];

             
            Point[] interface_location_inventory_cells = new Point[30];

             
            Point[] interface_location_inventory_special_cells_texts = new Point[special_items_number_first_row + special_items_number_second_row * 2];

             
            Point[] interface_location_inventory_special_cells = new Point[special_items_number];

             
            Point[] interface_location_inventory_item_buttons = new Point[3];

             
            Point[] interface_location_statistic_texts = new Point[3];

             
            Point[] interface_location_statistic_stats_texts = new Point[9];

             
            Point[] interface_location_statistic_stats_values = new Point[9];

             
            Point[,] interface_location_statistic_stats_blocks = new Point[9, 10];

             
            Point[] interface_location_statistic_stats_buttons = new Point[9];

             
            Point interface_location_level = new Point(length_2, length_4);
            Size interface_size_level = new Size(length_1 * 2 + length_2, length_1 * 2 + length_2);
            interface_rectangle_level = new Rectangle(interface_location_level, interface_size_level);

             
            interface_location_level_text[0] = new Point(interface_location_level.X, interface_location_level.Y - length_20);
            interface_size_level_text[0] = new Size(interface_size_level.Width, interface_size_level.Height * 2 / 3);
            interface_rectangle_level_text[0] = new Rectangle(interface_location_level_text[0], interface_size_level_text[0]);

             
            interface_location_level_text[1] = new Point(interface_location_level.X, interface_location_level_text[0].Y + interface_size_level_text[0].Height * 2 / 3);
            interface_size_level_text[1] = new Size(interface_size_level.Width, interface_size_level.Height / 4);
            interface_rectangle_level_text[1] = new Rectangle(interface_location_level_text[1], interface_size_level_text[1]);

             
            interface_location_level_text[2] = new Point(interface_location_level.X, interface_location_level_text[1].Y + interface_size_level_text[1].Height);
            interface_size_level_text[2] = interface_size_level_text[1];
            interface_rectangle_level_text[2] = new Rectangle(interface_location_level_text[2], interface_size_level_text[2]);

             
            Point interface_location_stats_health = new Point(interface_location_level.X + interface_size_level.Width + length_4, interface_location_level.Y);
            Size interface_size_stats_health = new Size(length_2 * 9, length_2);
            interface_rectangle_stats_health = new Rectangle(interface_location_stats_health, interface_size_stats_health);

             
            Point interface_location_stats_number_health = new Point(interface_location_stats_health.X + interface_size_stats_health.Width + length_4, interface_location_stats_health.Y);
            Size interface_size_stats_number_health = new Size(length_2 * 4, interface_size_stats_health.Height);
            interface_rectangle_stats_number_health = new Rectangle(interface_location_stats_number_health, interface_size_stats_number_health);

             
            Point interface_location_stats_energy = new Point(interface_location_stats_health.X, interface_location_stats_health.Y + interface_size_stats_health.Height + length_4);
            Size interface_size_stats_energy = interface_size_stats_health;
            interface_rectangle_stats_energy = new Rectangle(interface_location_stats_energy, interface_size_stats_energy);

             
            Point interface_location_stats_number_energy = new Point(interface_location_stats_number_health.X, interface_location_stats_energy.Y);
            Size interface_size_stats_number_energy = interface_size_stats_number_health;
            interface_rectangle_stats_number_energy = new Rectangle(interface_location_stats_number_energy, interface_size_stats_number_energy);

             
            Size interface_size_dungeon_level = new Size(length_2 * 11, length_2);
            Point interface_location_dungeon_level = new Point(ClientSize.Width - length_2 - interface_size_dungeon_level.Width, length_4);
            interface_rectangle_dungeon_level = new Rectangle(interface_location_dungeon_level, interface_size_dungeon_level);

             
            Size interface_size_inventory_cell = new Size(length_1, length_1);

             
            Size interface_size_inventory_button = new Size(length_1 * 3, length_1 * 3 / 4);

             
            Size interface_size_inventory = new Size((interface_size_inventory_cell.Width) * 10, (interface_size_inventory_cell.Height) * 3);
            Point interface_location_inventory = new Point(ClientSize.Width / 2 - interface_size_inventory.Width / 2, ClientSize.Height - length_4 - interface_size_inventory_button.Height - length_4 - interface_size_inventory.Height);
            interface_rectangle_inventory = new Rectangle(interface_location_inventory, interface_size_inventory);

             
            Size interface_size_statistic = new Size(interface_size_inventory.Width, interface_size_inventory.Height + length_4 + interface_size_inventory_button.Height);
            Point interface_location_statistic = new Point(ClientSize.Width / 2 - interface_size_statistic.Width / 2, ClientSize.Height - length_4 - interface_size_inventory_button.Height - length_4 - interface_size_statistic.Height);
            interface_rectangle_statistic = new Rectangle(interface_location_statistic, interface_size_statistic);

             
            Size interface_size_map = new Size(interface_size_statistic.Width + interface_size_inventory_cell.Height, interface_size_statistic.Width + interface_size_inventory_cell.Height);
            Point interface_location_map = new Point(ClientSize.Width / 2 - interface_size_map.Width / 2, ClientSize.Height - length_4 - interface_size_inventory_button.Height - length_4 - interface_size_map.Height);
            interface_rectangle_map = new Rectangle(interface_location_map, interface_size_map);

             
            for (int i = 0; i < 3; i++)
            {
                for (int i2 = 0; i2 < 10; i2++)
                {
                    int cell_id = i * 10 + i2;
                    interface_location_inventory_cells[cell_id] = new Point(interface_location_inventory.X + interface_size_inventory_cell.Width * i2, interface_location_inventory.Y + interface_size_inventory_cell.Height * i);
                    interface_rectangle_inventory_cells[cell_id] = new Rectangle(interface_location_inventory_cells[cell_id], interface_size_inventory_cell);
                }
            }

             
            Size interface_size_inventory_special_cells_text = new Size(length_1, length_2);
            for (int i = 0; i < special_items_number_first_row; i++)
            {
                interface_location_inventory_special_cells_texts[i] = new Point(length_2 + (interface_size_inventory_special_cells_text.Width + length_2) * i, interface_location_inventory.Y);
                interface_rectangle_inventory_special_cells_texts[i] = new Rectangle(interface_location_inventory_special_cells_texts[i], interface_size_inventory_special_cells_text);
            }
            for (int i = 0; i < 2; i++)
            {
                for (int i2 = 0; i2 < special_items_number_second_row; i2++)
                {
                    int cell_id = special_items_number_first_row + i * special_items_number_second_row + i2;
                    interface_location_inventory_special_cells_texts[cell_id] = new Point(length_2 * 5 / 2 + (interface_size_inventory_special_cells_text.Width + length_2) * i2, interface_location_inventory_special_cells_texts[0].Y + interface_size_inventory_special_cells_text.Height + interface_size_inventory_cell.Height + length_2 + (interface_size_inventory_special_cells_text.Height) * i);
                    interface_rectangle_inventory_special_cells_texts[cell_id] = new Rectangle(interface_location_inventory_special_cells_texts[cell_id], interface_size_inventory_special_cells_text);
                }
            }

             
            for (int i = 0; i < special_items_number_first_row; i++)
            {
                interface_location_inventory_special_cells[i] = new Point(interface_location_inventory_special_cells_texts[i].X, interface_location_inventory_special_cells_texts[i].Y + interface_size_inventory_special_cells_text.Height);
                interface_rectangle_inventory_special_cells[i] = new Rectangle(interface_location_inventory_special_cells[i], interface_size_inventory_cell);
            }
            for (int i = special_items_number_first_row; i < special_items_number; i++)
            {
                interface_location_inventory_special_cells[i] = new Point(interface_location_inventory_special_cells_texts[i + special_items_number_second_row].X, interface_location_inventory_special_cells_texts[i + special_items_number_second_row].Y + interface_size_inventory_special_cells_text.Height);
                interface_rectangle_inventory_special_cells[i] = new Rectangle(interface_location_inventory_special_cells[i], interface_size_inventory_cell);
            }

             
            Size interface_size_inventory_item_button = new Size(length_1 * 3, length_1 * 3 / 4);
            interface_location_inventory_item_buttons[0] = new Point(interface_location_inventory.X, interface_location_inventory.Y - length_4 - interface_size_inventory_item_button.Height);
            interface_location_inventory_item_buttons[1] = new Point(interface_location_inventory_item_buttons[0].X + interface_size_inventory_item_button.Width + length_2, interface_location_inventory_item_buttons[0].Y);
            interface_location_inventory_item_buttons[2] = new Point(interface_location_inventory_item_buttons[1].X + interface_size_inventory_item_button.Width + length_2, interface_location_inventory_item_buttons[1].Y);
            for (int i = 0; i < 3; i++)
            {
                interface_rectangle_inventory_item_buttons[i] = new Rectangle(interface_location_inventory_item_buttons[i], interface_size_inventory_item_button);
            }

             
            Size interface_size_inventory_item_info_button = new Size(length_2 * 11, interface_size_inventory_item_button.Height);
            Point interface_location_inventory_item_info_button = new Point(interface_location_level.X, interface_location_inventory_item_buttons[0].Y);
            interface_rectangle_inventory_item_info_button = new Rectangle(interface_location_inventory_item_info_button, interface_size_inventory_item_info_button);

             
            Point interface_location_inventory_item_name = new Point(interface_location_level.X, interface_location_level.Y + interface_size_level.Height + length_2);
            Size interface_size_inventory_item_name = interface_size_inventory_item_info_button;
            interface_rectangle_inventory_item_name = new Rectangle(interface_location_inventory_item_name, interface_size_inventory_item_name);

             
            Size interface_size_inventory_item_description = new Size(interface_size_inventory_item_name.Width, interface_location_inventory_item_info_button.Y - interface_location_inventory_item_name.Y - interface_size_inventory_item_info_button.Height);
            Point interface_location_inventory_item_description = new Point(interface_location_inventory_item_name.X, interface_location_inventory_item_name.Y + interface_size_inventory_item_name.Height);
            interface_rectangle_inventory_item_description = new Rectangle(interface_location_inventory_item_description, interface_size_inventory_item_description);

             
            Point interface_location_inventory_button = new Point(interface_location_inventory.X, ClientSize.Height - length_4 - interface_size_inventory_button.Height);
            interface_rectangle_inventory_button = new Rectangle(interface_location_inventory_button, interface_size_inventory_button);

             
            Size interface_size_statistic_button = interface_size_inventory_button;
            Point interface_location_statistic_button = new Point(interface_location_inventory_button.X + interface_size_inventory_button.Width + length_2, interface_location_inventory_button.Y);
            interface_rectangle_statistic_button = new Rectangle(interface_location_statistic_button, interface_size_statistic_button);

             
            Size interface_size_map_button = interface_size_statistic_button;
            Point interface_location_map_button = new Point(interface_location_statistic_button.X + interface_size_statistic_button.Width + length_2, interface_location_statistic_button.Y);
            interface_rectangle_map_button = new Rectangle(interface_location_map_button, interface_size_map_button);

             
            Size interface_size_left_side_window = new Size(length_2 * 11, interface_size_statistic.Height - interface_size_inventory_cell.Height);
            Point interface_location_left_side_window = new Point(interface_location_level.X, interface_location_statistic.Y);
            interface_rectangle_left_side_window = new Rectangle(interface_location_left_side_window, interface_size_left_side_window);

             
            Size interface_size_map_levels;
            interface_size_map_levels = new Size(interface_size_left_side_window.Width / 5, interface_size_left_side_window.Height / 2);
            for (int i = 0; i < 2; i++)
            {
                for (int i2 = 0; i2 < 5; i2++)
                {
                    Point interface_location_map_levels = new Point(interface_location_left_side_window.X + i2 * interface_size_map_levels.Width, interface_location_left_side_window.Y + i * interface_size_map_levels.Height);
                    interface_rectangle_map_levels[i, i2] = new Rectangle(interface_location_map_levels, interface_size_map_levels);
                }
            }

             
            Size interface_size_statistic_text = new Size(interface_size_left_side_window.Width - length_20 * 2, interface_size_left_side_window.Height / 3 - length_20 * 2);
            interface_location_statistic_texts[0] = new Point(interface_location_left_side_window.X + length_20, interface_location_left_side_window.Y + length_8);
            interface_location_statistic_texts[1] = new Point(interface_location_statistic_texts[0].X, interface_location_statistic_texts[0].Y + interface_size_statistic_text.Height);
            interface_location_statistic_texts[2] = new Point(interface_location_statistic_texts[1].X, interface_location_statistic_texts[1].Y + interface_size_statistic_text.Height);
            for (int i = 0; i < 3; i++)
            {
                interface_rectangle_statistic_texts[i] = new Rectangle(interface_location_statistic_texts[i], interface_size_statistic_text);
            }

             
            Size interface_size_statistic_stats_text = new Size((interface_size_statistic.Width - length_20 * 5) / 3, (interface_size_statistic.Height - length_20 * 5) / 6);
             
            Size interface_size_statistic_stats_value = new Size(interface_size_statistic_stats_text.Width * 5 / 20, interface_size_statistic_stats_text.Height);
             
            Size interface_size_statistic_stats_block = new Size(interface_size_statistic_stats_text.Width / 20, interface_size_statistic_stats_value.Height);
             
            Size interface_size_statistic_stats_button = new Size(interface_size_statistic_stats_text.Width * 5 / 20, interface_size_statistic_stats_block.Height);
            for (int i = 0; i < 3; i++)
            {
                for (int i2 = 0; i2 < 3; i2++)
                {
                    int id = i * 3 + i2;
                     
                    interface_location_statistic_stats_texts[id] = new Point(interface_location_statistic.X + (interface_size_statistic_stats_text.Width + length_20) * i2 + length_20, interface_location_statistic.Y + (interface_size_statistic_stats_text.Height + interface_size_statistic_stats_value.Height + length_20) * i + length_20);
                    interface_rectangle_statistic_stats_texts[id] = new Rectangle(interface_location_statistic_stats_texts[id], interface_size_statistic_stats_text);
                     
                    interface_location_statistic_stats_values[id] = new Point(interface_location_statistic_stats_texts[id].X, interface_location_statistic_stats_texts[id].Y + interface_size_statistic_stats_text.Height);
                    interface_rectangle_statistic_stats_values[id] = new Rectangle(interface_location_statistic_stats_values[id], new Size(interface_size_statistic_stats_value.Width * 11 / 10, interface_size_statistic_stats_value.Height));
                     
                    for (int i3 = 0; i3 < 10; i3++)
                    {
                        interface_location_statistic_stats_blocks[id, i3] = new Point(interface_location_statistic_stats_values[id].X + interface_size_statistic_stats_value.Width + interface_size_statistic_stats_block.Width * i3, interface_location_statistic_stats_values[id].Y);
                        interface_rectangle_statistic_stats_blocks[id, i3] = new Rectangle(interface_location_statistic_stats_blocks[id, i3], interface_size_statistic_stats_block);
                    }
                     
                    interface_location_statistic_stats_buttons[id] = new Point(interface_location_statistic_stats_blocks[id, 9].X + interface_size_statistic_stats_block.Width, interface_location_statistic_stats_values[id].Y);
                    interface_rectangle_statistic_stats_buttons[id] = new Rectangle(interface_location_statistic_stats_buttons[id], interface_size_statistic_stats_button);
                }
            }

             
            Size interface_size_minimap = new Size(interface_size_left_side_window.Width, interface_size_left_side_window.Width);
            Point interface_location_minimap = new Point(ClientSize.Width - length_2 - interface_size_minimap.Width, ClientSize.Height - length_4 - interface_size_minimap.Height);
            interface_rectangle_minimap = new Rectangle(interface_location_minimap, interface_size_minimap);
            int minimap_block_length = interface_rectangle_minimap.Width / minimap_cells_number_in_line;
            int offset_length = (interface_rectangle_minimap.Width - minimap_block_length * minimap_cells_number_in_line) / 2;
            for (int i = 0; i < minimap_cells_number_in_line; i++)
            {
                for (int i2 = 0; i2 < minimap_cells_number_in_line; i2++)
                {
                    interface_rectangle_minimap_cells[i, i2] = new Rectangle(interface_rectangle_minimap.X + offset_length + i2 * minimap_block_length, interface_rectangle_minimap.Y + offset_length + i * minimap_block_length, minimap_block_length, minimap_block_length);
                }
            }
             

             
             
             
            int between = length_4;

             
            Size code_size_background = new Size(interface_rectangle_inventory.Width, (interface_rectangle_minimap.Y - length_2) - interface_rectangle_inventory_item_name.Y);
            Point code_location_background = new Point(ClientSize.Width / 2 - code_size_background.Width / 2, interface_rectangle_inventory_item_name.Y);
            code_rectangle_background = new Rectangle(code_location_background, code_size_background);

             
            Size code_size_text = new Size(code_size_background.Width, code_rectangle_background.Height / 8);
            Point code_location_text = new Point(code_location_background.X, code_location_background.Y + between);
            code_rectangle_text = new Rectangle(code_location_text, code_size_text);

            Size code_size_button = new Size((code_rectangle_background.Width - between * 3) / 2, code_size_text.Height);

            Size code_size_arrow_up = Properties.Resources.ARROW_UP.Size;
            Size code_size_arrow_down = Properties.Resources.ARROW_DOWN.Size;
            Size code_size_value = new Size((code_rectangle_background.Width - between) / 4 - between, code_rectangle_background.Height - between * 6 - code_size_button.Height - code_size_arrow_up.Height - code_size_arrow_down.Height - code_size_text.Height);

             

             
            Point[] code_location_arrows_up = new Point[code_nums_number];
            for (int i = 0; i < code_nums_number; i++)
            {
                code_location_arrows_up[i] = new Point(ClientSize.Width / 2 - between / 2 - (code_size_value.Width + between) * (code_nums_number / 2) + between + (code_size_value.Width + between) * i + (code_size_value.Width - code_size_arrow_up.Width) / 2, code_rectangle_text.Y + code_rectangle_text.Height + between);
                code_rectangle_arrows_up[i] = new Rectangle(code_location_arrows_up[i], code_size_arrow_up);
            }

             
            Point[] code_location_values = new Point[code_nums_number];
            for (int i = 0; i < code_nums_number; i++)
            {
                code_location_values[i] = new Point(code_location_arrows_up[i].X - (code_size_value.Width - code_size_arrow_up.Width) / 2, code_location_arrows_up[i].Y + code_size_arrow_up.Height + between);
                code_rectangle_values[i] = new Rectangle(code_location_values[i], code_size_value);
            }

             
            Point[] code_location_arrows_down = new Point[code_nums_number];
            for (int i = 0; i < code_nums_number; i++)
            {
                code_location_arrows_down[i] = new Point(code_location_arrows_up[i].X, code_location_values[i].Y + code_size_value.Height + between);
                code_rectangle_arrows_down[i] = new Rectangle(code_location_arrows_down[i], code_size_arrow_down);
            }

             
            Point code_location_button_back = new Point(code_rectangle_background.X + between, code_rectangle_arrows_down[code_nums_number - 1].Y + code_rectangle_arrows_down[code_nums_number - 1].Height + between);
            code_rectangle_button_back = new Rectangle(code_location_button_back, code_size_button);

             
            Point code_location_button_open = new Point(ClientSize.Width / 2 + between, code_location_button_back.Y);
            code_rectangle_button_open = new Rectangle(code_location_button_open, code_size_button);
             
        }

         
        private void CalculateInterfaceHeroMap()
        {
            m_showing_map_cells_number_in_line = m_hero.DungeonLevel.CellsNumberInLine;
            interface_rectangle_map_cells = new Rectangle[m_showing_map_cells_number_in_line, m_showing_map_cells_number_in_line];
            int map_block_length = interface_rectangle_map.Width / m_showing_map_cells_number_in_line;
            int offset_length = (interface_rectangle_map.Width - map_block_length * m_showing_map_cells_number_in_line) / 2;
            for (int i = 0; i < m_showing_map_cells_number_in_line; i++)
            {
                for (int i2 = 0; i2 < m_showing_map_cells_number_in_line; i2++)
                {
                    interface_rectangle_map_cells[i, i2] = new Rectangle(interface_rectangle_map.X + offset_length + i2 * map_block_length, interface_rectangle_map.Y + offset_length + i * map_block_length, map_block_length, map_block_length);
                }
            }
        }

         
        private void CalculateInterfaceHeroStats()
        {
            interface_rectangle_stats_total_health = new Rectangle(interface_rectangle_stats_health.X + length_20, interface_rectangle_stats_health.Y + length_20, (int)((interface_rectangle_stats_health.Width - length_20 * 2) * (m_hero.TotalHealth / m_hero.GetStatValueWithEffectValue(DungeonStats.MaxHealth))), interface_rectangle_stats_health.Height - length_20 * 2);
            interface_rectangle_stats_total_energy = new Rectangle(interface_rectangle_stats_energy.X + length_20, interface_rectangle_stats_energy.Y + length_20, (int)((interface_rectangle_stats_energy.Width - length_20 * 2) * (m_hero.TotalEnergy / m_hero.GetStatValueWithEffectValue(DungeonStats.MaxEnergy))), interface_rectangle_stats_energy.Height - length_20 * 2);
        }

         
        public void CalculateInterfaceHeroActivePotions()
        {
            Size interface_size_active_potions_headers = new Size(interface_rectangle_minimap.Width * 2, interface_rectangle_dungeon_level.Height);
            Point interface_location_active_potions_headers = new Point(ClientSize.Width - length_2 - interface_size_active_potions_headers.Width, interface_rectangle_dungeon_level.Y + interface_rectangle_dungeon_level.Height + length_4);
            interface_rectangle_active_potions_headers = new Rectangle(interface_location_active_potions_headers, interface_size_active_potions_headers);

            int max_showing_potions = (interface_rectangle_minimap.Y - length_4 * 2 - interface_rectangle_dungeon_level.Y - interface_rectangle_dungeon_level.Height) / interface_size_active_potions_headers.Height;
            interface_rectangle_active_potions_text = new Rectangle[max_showing_potions];
            showing_potions_text = new string[max_showing_potions];
            effects_stats = new DungeonStats[max_showing_potions];
            int showing_potions = 0;
            Size interface_size_active_potions_text = interface_size_active_potions_headers;
            for (int i = 0; i < m_hero.EffectsPotions.Count; i++)
            {
                bool is_break = false;
                string effect_text = "";
                if (i < max_showing_potions - 1)
                {
                    DungeonEffect effect = m_hero.EffectsPotions[i];
                    if (effect != null)
                    {
                        if (effect.Duration != -1)  
                        {
                            effect_text += DungeonStatsInfo.Name(effect.Stat) + " (";
                            if (effect.Value > 0) effect_text += "+";
                            effect_text += effect.Value.ToString() + "): " + (effect.UsageTime / 1000 + 1).ToString() + " секунд";
                            showing_potions++;
                        }
                        effects_stats[i] = effect.Stat;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    effect_text = "...";
                    showing_potions++;
                    is_break = true;
                }
                Point interface_location_active_potions_text = new Point(interface_location_active_potions_headers.X, interface_location_active_potions_headers.Y + interface_size_active_potions_headers.Height + interface_size_active_potions_text.Height * (i));
                interface_rectangle_active_potions_text[i] = new Rectangle(interface_location_active_potions_text, interface_size_active_potions_text);
                showing_potions_text[i] = effect_text;
                if (is_break) break;
            }

            showing_potions_number = showing_potions;
        }

         

         
        private void DrawHeroButtons(object sender, PaintEventArgs e)
        {
            Brush brush;
             
            if (interface_is_entered_inventory_button)
            {
                brush = Brushes.YellowGreen;
            }
            else
            {
                brush = Brushes.White;
            }
            DrawRectagle(e, interface_rectangle_inventory_button, Brushes.Black, brush);
            DrawText(e, interface_rectangle_inventory_button, Brushes.Black, "Інвентар", true, 0.35);

             
            if (interface_is_entered_statistic_button)
            {
                brush = Brushes.YellowGreen;
            }
            else
            {
                brush = Brushes.White;
            }
            DrawRectagle(e, interface_rectangle_statistic_button, Brushes.Black, brush);
            DrawText(e, interface_rectangle_statistic_button, Brushes.Black, "Прокачка", true, 0.35);

             
            if (interface_is_entered_map_button)
            {
                brush = Brushes.YellowGreen;
            }
            else
            {
                brush = Brushes.White;
            }
            DrawRectagle(e, interface_rectangle_map_button, Brushes.Black, brush);
            DrawText(e, interface_rectangle_map_button, Brushes.Black, "Карта", true, 0.35);
        }

         
        private void DrawHeroLevel(object sender, PaintEventArgs e)
        {
             
            DrawEllipse(e, interface_rectangle_level, Brushes.Black, Brushes.White);

             
            DrawText(e, interface_rectangle_level_text[0], Brushes.Black, "" + m_hero.Level);
            DrawText(e, interface_rectangle_level_text[1], Brushes.Black, "Рівень");
            DrawText(e, interface_rectangle_level_text[2], Brushes.Black, "" + m_hero.Exp + "/10");
        }

         
        private void DrawHeroStats(object sender, PaintEventArgs e)
        {
             
            DrawRectagle(e, interface_rectangle_stats_health, Brushes.Black, Brushes.White);

             
            DrawRectagle(e, interface_rectangle_stats_total_health, Brushes.Red);

             
            DrawRectagle(e, interface_rectangle_stats_number_health, Brushes.Black, Brushes.White);
            DrawText(e, interface_rectangle_stats_number_health, Brushes.Black, "" + (int)(m_hero.TotalHealth) + "/" + m_hero.GetStatValueWithEffectValue(DungeonStats.MaxHealth));

             
            DrawRectagle(e, interface_rectangle_stats_energy, Brushes.Black, Brushes.White);

             
            DrawRectagle(e, interface_rectangle_stats_total_energy, Brushes.Aqua);

             
            DrawRectagle(e, interface_rectangle_stats_number_energy, Brushes.Black, Brushes.White);
            DrawText(e, interface_rectangle_stats_number_energy, Brushes.Black, "" + (int)(m_hero.TotalEnergy) + "/" + m_hero.GetStatValueWithEffectValue(DungeonStats.MaxEnergy));
        }

         
        private void DrawHeroDungeonLevel(object sender, PaintEventArgs e)
        {
             
            DrawRectagle(e, interface_rectangle_dungeon_level, Brushes.Black, Brushes.White);
            DrawText(e, interface_rectangle_dungeon_level, Brushes.Black, "Рівень підземелля:" + (m_hero.DungeonLevel.Id + 1));
        }

         
        private void DrawHeroPotions(object sender, PaintEventArgs e)
        {
            Brush brush;
            for (int i = 0; i < special_items_number_second_row; i++)
            {
                DrawRectagle(e, interface_rectangle_inventory_special_cells_texts[i + special_items_number_first_row + special_items_number_second_row], Brushes.Black, Brushes.White);
                DrawText(e, interface_rectangle_inventory_special_cells_texts[i + special_items_number_first_row + special_items_number_second_row], Brushes.Black, settings_controls_keys_choosed_keys[keys_id_first_potion + i].ToString(), true, 0.35);

                brush = Brushes.White;
                if (m_total_inventory_mode == DungeonInventoryStatus.Inventory)
                {
                    if (i + special_items_number_first_row + 30 == m_id_clicked_cell)
                    {
                        brush = Brushes.Green;
                    }
                    else if (interface_is_entered_inventory_special_cells[i + special_items_number_first_row])
                    {
                        brush = Brushes.YellowGreen;
                    }
                }
                DrawRectagle(e, interface_rectangle_inventory_special_cells[i + special_items_number_first_row], Brushes.Black, brush);
                DungeonItem item = m_hero.ContainerSpecialItems.Items[i + special_items_number_first_row];
                if (item != null)
                {
                    DrawImage(e, interface_rectangle_inventory_special_cells[i + special_items_number_first_row], item.Image);
                }
            }
            if (showing_potions_number != 0)
            {
                 
                DrawText(e, interface_rectangle_active_potions_headers, Brushes.White, "Діючі зілля:", true, 0.5, true);

                for (int i = 0; i < showing_potions_number; i++)
                {
                    if (showing_potions_text[i] == "...") brush = Brushes.White;
                    else brush = new SolidBrush(DungeonStatsInfo.GetColor(effects_stats[i]));
                    DrawText(e, interface_rectangle_active_potions_text[i], brush, showing_potions_text[i], true, 0.5, true);
                }
            }
        }

         
        private void DrawInventory(object sender, PaintEventArgs e)
        {
            Brush brush;

            if (m_total_inventory_mode == DungeonInventoryStatus.Inventory)
            {
                 
                DrawRectagle(e, interface_rectangle_inventory, Brushes.Black);

                DungeonItem item;  
                for (int i = 0; i < 3; i++)
                {
                    for (int i2 = 0; i2 < 10; i2++)
                    {
                        int cell_id = i * 10 + i2;
                        brush = Brushes.White;
                        if (interface_is_entered_inventory_cells[cell_id])
                        {
                            brush = Brushes.YellowGreen;
                        }
                        if (cell_id == m_id_clicked_cell)
                        {
                            brush = Brushes.Green;
                        }
                        DrawRectagle(e, interface_rectangle_inventory_cells[cell_id], Brushes.Black, brush);

                        item = m_hero.Container.Items[cell_id];
                        if (item != null)
                        {
                            DrawImage(e, interface_rectangle_inventory_cells[cell_id], item.Image);
                        }
                    }
                }

                if (m_id_clicked_cell != -1)
                {
                    if (m_id_clicked_cell >= 30)
                    {
                        item = m_hero.ContainerSpecialItems.Items[m_id_clicked_cell - 30];
                    }
                    else
                    {
                        item = m_hero.Container.Items[m_id_clicked_cell];
                    }
                    if (item != null)
                    {
                         
                         
                         
                        brush = Brushes.White;
                        if (interface_is_entered_inventory_item_info_button)
                        {
                            brush = Brushes.YellowGreen;
                        }
                        DrawRectagle(e, interface_rectangle_inventory_item_info_button, Brushes.Black, brush);
                        string text = "";
                        if (is_show_item_info) text = "Приховати інформацію";
                        else text = "Показати інформацію";
                        DrawText(e, interface_rectangle_inventory_item_info_button, Brushes.Black, text, true, 0.28);
                         

                        if (is_show_item_info)
                        {
                             
                            DrawRectagle(e, interface_rectangle_inventory_item_name, Brushes.Black, Brushes.Aquamarine);
                            DrawText(e, interface_rectangle_inventory_item_name, Brushes.Black, item.Name);

                             
                            DrawRectagle(e, interface_rectangle_inventory_item_description, Brushes.Black, Brushes.White);
                            DrawText(e, interface_rectangle_inventory_item_description, Brushes.Black, item.FullDescription, false, 0.03);
                        }

                        if (item.CanUse)
                        {
                             
                             
                             
                            brush = Brushes.White;
                            if (interface_is_entered_inventory_item_button[0])
                            {
                                brush = Brushes.YellowGreen;
                            }
                            DrawRectagle(e, interface_rectangle_inventory_item_buttons[0], Brushes.Black, brush);
                            DrawText(e, interface_rectangle_inventory_item_buttons[0], Brushes.Black, "Використати", true, 0.28);
                             
                        }
                        if (item.CanDrop)
                        {
                             
                             
                             
                            brush = Brushes.White;
                            if (interface_is_entered_inventory_item_button[1])
                            {
                                brush = Brushes.YellowGreen;
                            }
                            DrawRectagle(e, interface_rectangle_inventory_item_buttons[1], Brushes.Black, brush);
                            DrawText(e, interface_rectangle_inventory_item_buttons[1], Brushes.Black, "Викинути", true, 0.28);
                             
                        }
                        if (item.CanDestroy)
                        {
                             
                             
                             
                            brush = Brushes.White;
                            if (interface_is_entered_inventory_item_button[2])
                            {
                                brush = Brushes.YellowGreen;
                            }
                            DrawRectagle(e, interface_rectangle_inventory_item_buttons[2], Brushes.Black, brush);
                            DrawText(e, interface_rectangle_inventory_item_buttons[2], Brushes.Black, "Знищити", true, 0.28);
                             
                        }
                    }
                }


                string[] texts = new string[special_items_number];
                texts[0] = "Шолом";
                texts[1] = "Броня";
                texts[2] = "Артефакт";
                texts[3] = "Зброя";
                texts[4] = "Зілля 1";
                texts[5] = "Зілля 2";
                texts[6] = "Зілля 3";

                for (int i = 0; i < special_items_number; i++)
                {
                    if (i < special_items_number_first_row)
                    {
                        DrawRectagle(e, interface_rectangle_inventory_special_cells_texts[i], Brushes.Black, Brushes.White);
                        DrawText(e, interface_rectangle_inventory_special_cells_texts[i], Brushes.Black, texts[i], true, 0.25);
                    }
                    else
                    {
                        DrawRectagle(e, interface_rectangle_inventory_special_cells_texts[i], Brushes.Black, Brushes.White);
                        DrawText(e, interface_rectangle_inventory_special_cells_texts[i], Brushes.Black, texts[i], true, 0.25);
                    }


                    brush = Brushes.White;
                    if (interface_is_entered_inventory_special_cells[i])
                    {
                        brush = Brushes.YellowGreen;
                    }
                    if (i + 30 == m_id_clicked_cell)
                    {
                        brush = Brushes.Green;
                    }
                    DrawRectagle(e, interface_rectangle_inventory_special_cells[i], Brushes.Black, brush);
                    item = m_hero.ContainerSpecialItems.Items[i];
                    if (item != null)
                    {
                        DrawImage(e, interface_rectangle_inventory_special_cells[i], item.Image);
                    }
                }
            }
            else if (m_total_inventory_mode == DungeonInventoryStatus.Statistic)
            {
                 
                DrawRectagle(e, interface_rectangle_statistic, Brushes.Black, Brushes.White);

                 
                DrawRectagle(e, interface_rectangle_left_side_window, Brushes.Black, Brushes.White);
                DrawText(e, interface_rectangle_statistic_texts[0], Brushes.Black, "Рівень:" + m_hero.Level, true, 0.4);
                DrawText(e, interface_rectangle_statistic_texts[1], Brushes.Black, "Очки досвіду:" + m_hero.Exp + "/10", true, 0.4);
                DrawText(e, interface_rectangle_statistic_texts[2], Brushes.Black, "Очки умінь:" + m_hero.SkillPoints, true, 0.4);

                for (int i = 0; i < 3; i++)
                {
                    for (int i2 = 0; i2 < 3; i2++)
                    {
                        int id = i * 3 + i2;
                        DungeonStats stat = (DungeonStats)id;
                        DrawText(e, interface_rectangle_statistic_stats_texts[id], Brushes.Black, DungeonStatsInfo.Name(stat), true, 0.4);
                        string effects_text = "";
                        if (m_hero.GetEffectValue(stat) > 0.005)
                        {
                            effects_text = "\n+" + m_hero.GetEffectValue(stat) + "";
                        }
                        else if (m_hero.GetEffectValue(stat) < -0.005)
                        {
                            effects_text = "\n" + m_hero.GetEffectValue(stat) + "";
                        }
                        DrawText(e, interface_rectangle_statistic_stats_values[id], Brushes.Black, m_hero.GetStatValue(stat).ToString() + effects_text, true, 0.35);

                        for (int i3 = 0; i3 < 10; i3++)
                        {
                            if (m_hero.GetStatValue(stat) >= (i3 + 1) * DungeonStatsInfo.Plus(stat) - 0.25)
                            {
                                brush = Brushes.Blue;
                            }
                            else
                            {
                                brush = Brushes.Gray;
                            }
                            DrawRectagle(e, interface_rectangle_statistic_stats_blocks[id, i3], Brushes.White, brush, 1);
                        }

                        bool can_up_stat = false;
                        if (m_hero.SkillPoints > 0)
                        {
                            if (m_hero.GetStatValue(stat) < DungeonStatsInfo.Max(stat) - 0.25)
                            {
                                can_up_stat = true;
                            }
                        }
                        if (can_up_stat)
                        {
                            brush = Brushes.Red;
                        }
                        else
                        {
                            brush = Brushes.Gray;
                        }
                        DrawRectagle(e, interface_rectangle_statistic_stats_buttons[id], Brushes.White, brush);
                        if (can_up_stat)
                        {
                            brush = Brushes.Yellow;
                        }
                        else
                        {
                            brush = Brushes.White;
                        }
                        string text = "+";

                        text = "+ " + DungeonStatsInfo.Plus(stat).ToString();

                        DrawText(e, interface_rectangle_statistic_stats_buttons[id], brush, text, true, 0.3);
                    }
                }
            }
            else if (m_total_inventory_mode == DungeonInventoryStatus.Map)
            {
                if (m_id_level_showing_map == -1) m_id_level_showing_map = m_hero.DungeonLevel.Id;

                 
                DrawRectagle(e, interface_rectangle_map, Brushes.White, Brushes.Black);
                for (int i = 0; i < m_showing_map_cells_number_in_line; i++)
                {
                    for (int i2 = 0; i2 < m_showing_map_cells_number_in_line; i2++)
                    {
                        DungeonMapCell map_cell = m_dungeon_levels[m_id_level_showing_map].GetCell(new Point(i2, i));
                        if (map_cell != DungeonMapCell.Nothing) DrawRectagle(e, interface_rectangle_map_cells[i, i2], DungeonLevel.GetBrushForMapCell(map_cell));
                    }
                }
                if (m_hero.DungeonLevel.Id == m_id_level_showing_map)
                {
                    Size player_cell_size = new Size(length_4, length_4);
                    Point player_cell_location = new Point((int)(interface_rectangle_map_cells[0, 0].X + (m_hero.Location.X) * ((double)(interface_rectangle_map_cells[0, 0].Width * m_showing_map_cells_number_in_line) / m_hero.DungeonLevel.MapLengthInLine) - player_cell_size.Width / 2), (int)(interface_rectangle_map_cells[0, 0].Y + (m_hero.Location.Y) * ((double)(interface_rectangle_map_cells[0, 0].Height * m_showing_map_cells_number_in_line) / m_hero.DungeonLevel.MapLengthInLine) - player_cell_size.Height / 2));
                    Rectangle player_cell_rectangle = new Rectangle(player_cell_location, player_cell_size);
                    DrawEllipse(e, player_cell_rectangle, Brushes.Red);
                }

                 
                DrawRectagle(e, interface_rectangle_left_side_window, Brushes.Black, Brushes.White);
                for (int i = 0; i < 2; i++)
                {
                    for (int i2 = 0; i2 < 5; i2++)
                    {
                        if (m_id_level_showing_map == i * 5 + i2) brush = Brushes.Green;
                        else if (interface_is_entered_map_levels[i, i2]) brush = Brushes.YellowGreen;
                        else brush = Brushes.White;
                        DrawRectagle(e, interface_rectangle_map_levels[i, i2], Brushes.Black, brush);
                        DrawText(e, interface_rectangle_map_levels[i, i2], Brushes.Black, (i * 5 + i2 + 1).ToString(), true, 0.3);
                    }
                }
            }
        }

         
        private void DrawHeroMinimap(object sender, PaintEventArgs e)
        {
            DrawRectagle(e, interface_rectangle_minimap, Brushes.White, Brushes.Black);
            for (int i = 0; i < minimap_cells_number_in_line; i++)
            {
                for (int i2 = 0; i2 < minimap_cells_number_in_line; i2++)
                {
                    DungeonMapCell map_cell = m_hero.DungeonLevel.GetCellWhereIsCreature(m_hero, -minimap_cells_number_in_line / 2 + i2, -minimap_cells_number_in_line / 2 + i);

                    if (map_cell != DungeonMapCell.Nothing) DrawRectagle(e, interface_rectangle_minimap_cells[i, i2], DungeonLevel.GetBrushForMapCell(map_cell));
                }
            }
            DrawEllipse(e, interface_rectangle_minimap_cells[minimap_cells_number_in_line / 2, minimap_cells_number_in_line / 2], Brushes.Red);  
        }

         

         
        private void PauseGame()
        {
            m_enabled_keys.Clear();
            is_game_menu = true;
            StopTimers();
            m_form_status = DungeonFormStatus.InGameMenu;
            Refresh();
            m_hero.DungeonLevel.PauseMonstersActions();
        }

         
        private void ResumeGame()
        {
            is_game_menu = false;
            StartTimers();
            m_form_status = DungeonFormStatus.Game;
            m_hero.DungeonLevel.ResumeMonstersActions();
        }

         
        public void UpdateMapDungeonLevelShowing()
        {
            m_id_level_showing_map = m_hero.DungeonLevel.Id;
        }

         
        public void ShowCodeInsert(DungeonDoor door_code_opened)
        {
            if (!is_code_insert)
            {
                m_last_inventory_mode = m_total_inventory_mode;
                m_total_inventory_mode = DungeonInventoryStatus.None;
                m_door_code_opened = door_code_opened;
                is_code_insert = true;
            }
        }

         
        public void HideCodeInsert()
        {
            if (is_code_insert)
            {
                is_code_insert = false;
                m_total_inventory_mode = m_last_inventory_mode;
            }
        }
         
         
        private void StartNewGame()
        {
            total_difficulty_id = choosed_difficulty_id;
            m_form_status = DungeonFormStatus.Loading;
            SetLoadingText("Завантаження гри...");
            if (total_difficulty_id == 3)  
            {
                SetLoadingChapters(dungeon_levels_number);
            }
            else
            {
                SetLoadingChapters(dungeon_levels_number + 15);
            }
            SetLoadingText("Генерація рівнів...");

            m_total_level_generation_number = "";
            int key_number = 0;
            for (int i = level_generation_nums_number - 1; i >= 0; i--)
            {
                m_total_level_generation_number += choosed_level_generation_nums_values[i].ToString();
                key_number += (int)(choosed_level_generation_nums_values[i] * Math.Pow(10, (level_generation_nums_number - 1) - i));
            }
            world_key_number = key_number;
            world_key = new Random(world_key_number);

            DungeonLevel.LevelsNumber = 0;
            m_dungeon_levels = new DungeonLevel[dungeon_levels_number];
            for (int i = 0; i < dungeon_levels_number; i++)
            {
                DungeonLadderType ladder_entrance_direction = DungeonLadderType.NoLadder;
                Point ladder_entrance_position = new Point(0, 0);
                if (i != 0)
                {
                    DungeonLadderType ladder_exit_direction_previous_map = m_dungeon_levels[i - 1].LadderExitDirection;
                    if (ladder_exit_direction_previous_map == DungeonLadderType.Up) ladder_entrance_direction = DungeonLadderType.Down;
                    else if (ladder_exit_direction_previous_map == DungeonLadderType.Down) ladder_entrance_direction = DungeonLadderType.Up;
                    else if (ladder_exit_direction_previous_map == DungeonLadderType.Left) ladder_entrance_direction = DungeonLadderType.Right;
                    else if (ladder_exit_direction_previous_map == DungeonLadderType.Right) ladder_entrance_direction = DungeonLadderType.Left;
                    ladder_entrance_position = m_dungeon_levels[i - 1].LadderExitLocation;
                }
                DungeonLadderType ladder_exit_direction = DungeonLadderType.NoLadder;
                if (i != dungeon_levels_number - 1)
                {
                    do
                    {
                        ladder_exit_direction = (DungeonLadderType)(world_key.Next(0, 3 + 1));
                    }
                    while (ladder_exit_direction == ladder_entrance_direction);
                }
                m_dungeon_levels[i] = new DungeonLevel(this, ref world_key, (DungeonDifficulty)(choosed_difficulty_id), ladder_entrance_direction, ladder_entrance_position, ladder_exit_direction);
                PlusLoadingChaptersTotal(1);
            }
            for (int i = 0; i < dungeon_levels_number; i++)
            {
                if (i != 0)
                {
                    m_dungeon_levels[i].PreviousDungeonLevel = m_dungeon_levels[i - 1];
                }
                if (i != dungeon_levels_number - 1)
                {
                    m_dungeon_levels[i].NextDungeonLevel = m_dungeon_levels[i + 1];
                }
            }

            m_hero = characters[choosed_character_id];  
            m_hero.FullStats();

            m_dungeon_levels[0].Add(m_hero);  
            m_hero.Location = new DungeonPoint(m_dungeon_levels[0].EntranceLocation.X, m_dungeon_levels[0].EntranceLocation.Y);

            if (total_difficulty_id == 3)  
            {
                CalculateInterface();
                loading_text = "Натисніть будь-яку клавішу, щоб продовжити...";
                timer_loading_wait.Start();  
            }
            else
            {
                SetLoadingText("Збереження світу...");
                string filename = Nick + " " + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss") + " (новая игра)";
                SaveGame(filename);

                SetLoadingText("Завантаження світу...");
                LoadGame(filename);
            }
        }

         
        private void JustLoad()
        {
            m_can_minimize = false;
            LoadForm load_form = new LoadForm();
            load_form.Owner = this;
            load_form.ShowDialog();
            m_can_minimize = true;
            if (load_form.GoLoad)
            {
                m_form_status = DungeonFormStatus.Loading;
                SetLoadingChapters(12);
                SetLoadingText("Завантаження світу...");
                LoadGame(SaveName);
            }
        }

         
        private void JustSave()
        {
            if (total_difficulty_id == 3)
            {
                m_can_minimize = false;
                MessageBox.Show("На рівні складності 'Хардкор' не можна зберігатися!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                m_can_minimize = true;
            }
            else
            {
                m_can_minimize = false;
                SaveForm save_form = new SaveForm();
                save_form.Owner = this;
                SaveName = Nick + " " + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss");
                save_form.ShowDialog();
                m_can_minimize = true;
                if (save_form.GoSave)
                {
                    m_form_status = DungeonFormStatus.Saving;
                    SetLoadingChapters(3);
                    SetLoadingText("Збереження світу...");
                    SaveGame(SaveName);
                }
                timer_saving_wait.Start();
            }
        }

         
         
        private void LoadGame(string filename)
        {
            try
            {
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
                folder_path = Path.GetDirectoryName(Application.ExecutablePath) + "\\data\\saves\\" + Nick;
                if (!Directory.Exists(folder_path))
                {
                    Directory.CreateDirectory(folder_path);
                }
                DungeonLevel.LevelsNumber = 0;
                m_dungeon_levels = new DungeonLevel[dungeon_levels_number];
                BinaryFormatter formatter = new BinaryFormatter();  
                using (FileStream fs = new FileStream("data\\saves\\" + Nick + "\\" + filename + ".bin", FileMode.Open))  
                {
                    formatter.Deserialize(fs);  
                    formatter.Deserialize(fs);  
                    formatter.Deserialize(fs);  
                    formatter.Deserialize(fs);  
                    m_total_level_generation_number = (string)formatter.Deserialize(fs);  
                    formatter.Deserialize(fs);  
                    formatter.Deserialize(fs);  
                    m_hero = (DungeonHero)formatter.Deserialize(fs);
                    m_total_inventory_mode = (DungeonInventoryStatus)formatter.Deserialize(fs);
                    m_id_clicked_cell = (int)formatter.Deserialize(fs);
                    m_dungeon_levels[m_hero.DungeonLevel.Id] = m_hero.DungeonLevel;
                    for (int i = m_hero.DungeonLevel.Id + 1; i < dungeon_levels_number; i++)
                    {
                        m_dungeon_levels[i] = m_dungeon_levels[i - 1].NextDungeonLevel;
                    }
                    for (int i = m_hero.DungeonLevel.Id - 1; i >= 0; i--)
                    {
                        m_dungeon_levels[i] = m_dungeon_levels[i + 1].PreviousDungeonLevel;
                    }
                    PlusLoadingChaptersTotal(1);
                    for (int i = 0; i < dungeon_levels_number; i++)
                    {
                        for (int i2 = 0; i2 < m_dungeon_levels[i].Creatures.Count; i2++)
                        {
                            m_dungeon_levels[i].Creatures[i2].InitializeTimers();
                            if (m_dungeon_levels[i].Creatures[i2] is DungeonMonster)
                            {
                                (m_dungeon_levels[i].Creatures[i2] as DungeonMonster).InitializeTimers();
                            }
                            for (int i3 = 0; i3 < m_dungeon_levels[i].Creatures[i2].Container.Items.Count; i3++)
                            {
                                if (m_dungeon_levels[i].Creatures[i2].Container.Items[i3] != null)
                                {
                                    if (m_dungeon_levels[i].Creatures[i2].Container.Items[i3] is DungeonItemEquipment)
                                    {
                                        for (int i4 = 0; i4 < (m_dungeon_levels[i].Creatures[i2].Container.Items[i3] as DungeonItemEquipment).Effects.Count; i4++)
                                        {
                                            (m_dungeon_levels[i].Creatures[i2].Container.Items[i3] as DungeonItemEquipment).Effects[i4].InitializeTimers();
                                        }
                                    }
                                }
                            }
                            for (int i3 = 0; i3 < m_dungeon_levels[i].Creatures[i2].ContainerSpecialItems.Items.Count; i3++)
                            {
                                if (m_dungeon_levels[i].Creatures[i2].ContainerSpecialItems.Items[i3] != null)
                                {
                                    if (m_dungeon_levels[i].Creatures[i2].ContainerSpecialItems.Items[i3] is DungeonItemEquipment)
                                    {
                                        for (int i4 = 0; i4 < (m_dungeon_levels[i].Creatures[i2].ContainerSpecialItems.Items[i3] as DungeonItemEquipment).Effects.Count; i4++)
                                        {
                                            (m_dungeon_levels[i].Creatures[i2].ContainerSpecialItems.Items[i3] as DungeonItemEquipment).Effects[i4].InitializeTimers();
                                        }
                                    }
                                }
                            }
                        }
                        for (int i2 = 0; i2 < m_dungeon_levels[i].Chests.Count; i2++)
                        {
                            for (int i3 = 0; i3 < m_dungeon_levels[i].Chests[i2].Container.Items.Count; i3++)
                            {
                                if (m_dungeon_levels[i].Chests[i2].Container.Items[i3] is DungeonItemEquipment)
                                {
                                    for (int i4 = 0; i4 < (m_dungeon_levels[i].Chests[i2].Container.Items[i3] as DungeonItemEquipment).Effects.Count; i4++)
                                    {
                                        (m_dungeon_levels[i].Chests[i2].Container.Items[i3] as DungeonItemEquipment).Effects[i4].InitializeTimers();
                                    }
                                }
                            }
                        }
                        for (int i2 = 0; i2 < m_dungeon_levels[i].GraphicEffects.Count; i2++)
                        {
                            m_dungeon_levels[i].GraphicEffects[i2].InitializeTimers();
                        }
                        for (int i2 = 0; i2 < m_dungeon_levels[i].Container.Items.Count; i2++)
                        {
                            if (m_dungeon_levels[i].Container.Items[i2] is DungeonItemEquipment)
                            {
                                for (int i3 = 0; i3 < (m_dungeon_levels[i].Container.Items[i2] as DungeonItemEquipment).Effects.Count; i3++)
                                {
                                    (m_dungeon_levels[i].Container.Items[i2] as DungeonItemEquipment).Effects[i3].InitializeTimers();
                                }
                            }
                        }
                        m_dungeon_levels[i].InitializeTimers();
                        m_dungeon_levels[i].Form = this;
                        PlusLoadingChaptersTotal(1);
                    }
                }
                CalculateInterface();
                total_difficulty_id = (byte)m_dungeon_levels[0].Difficulty;
                PlusLoadingChaptersTotal(1);
                loading_text = "Натисніть будь-яку клавішу, щоб продовжити...";
                timer_loading_wait.Start();  
            }
            catch
            {
                if (is_game_menu)
                {
                    m_form_status = DungeonFormStatus.InGameMenu;
                }
                else
                {
                    m_form_status = DungeonFormStatus.Menu;
                }
                is_game_menu = false;
                m_can_minimize = false;
                MessageBox.Show("Помилка при завантаженні збереження!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                m_can_minimize = true;
                return;
            }
        }

         
         
        private void SaveGame(string filename)
        {
            try
            {
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
                folder_path = Path.GetDirectoryName(Application.ExecutablePath) + "\\data\\saves\\" + Nick;
                if (!Directory.Exists(folder_path))
                {
                    Directory.CreateDirectory(folder_path);
                }
                BinaryFormatter formatter = new BinaryFormatter();  
                using (FileStream fs = new FileStream("data\\saves\\" + Nick + "\\" + filename + ".bin", FileMode.OpenOrCreate))  
                {
                    formatter.Serialize(fs, Path.GetFileNameWithoutExtension(filename));
                    formatter.Serialize(fs, total_difficulty_id);
                    formatter.Serialize(fs, m_hero.Name);
                    formatter.Serialize(fs, m_hero.Level);
                    PlusLoadingChaptersTotal(1);
                    formatter.Serialize(fs, m_total_level_generation_number);
                    formatter.Serialize(fs, m_hero.DungeonLevel.Id);
                    formatter.Serialize(fs, DateTime.Now.ToString());
                    PlusLoadingChaptersTotal(1);
                    formatter.Serialize(fs, m_hero);
                    formatter.Serialize(fs, m_total_inventory_mode);
                    formatter.Serialize(fs, m_id_clicked_cell);
                    PlusLoadingChaptersTotal(1);
                }
                is_game_saved = true;
            }
            catch
            {
                if (is_game_menu)
                {
                    m_form_status = DungeonFormStatus.InGameMenu;
                }
                else
                {
                    m_form_status = DungeonFormStatus.Menu;
                    is_game_menu = false;
                }
                is_game_saved = false;
                m_can_minimize = false;
                MessageBox.Show("Помилка при створенні збереження!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                m_can_minimize = true;
                return;
            }
        }

         
         
        private void SetLoadingChapters(int chapters)
        {
            loading_chapters = chapters;
            loading_chapters_total = 0;
            SetLoadingText("Загрузка...");
            loading_text_procent = "0%";
            CalculateLoadingBar();
        }

         
         
        private void PlusLoadingChaptersTotal(int chapters)
        {
            loading_chapters_total += chapters;
            if (loading_chapters_total >= loading_chapters)
            {
                loading_chapters_total = loading_chapters;
            }
            loading_text_procent = Math.Round((double)loading_chapters_total / loading_chapters * 100).ToString() + "%";
            CalculateLoadingBar();
        }

         
         
        private void SetLoadingText(string new_text)
        {
            loading_text = new_text;
            Refresh();
        }

         
        private void CalculateLoadingBar()
        {
            if (loading_chapters != 0)
            {
                loading_rectangle_bar_total = new Rectangle(loading_rectangle_bar.X, loading_rectangle_bar.Y, (int)(loading_rectangle_bar.Width * ((double)loading_chapters_total / loading_chapters)), loading_rectangle_bar.Height);
            }
            else
            {
                loading_rectangle_bar_total = loading_rectangle_bar;
            }
            Refresh();
        }

         
         
        private void EndGame(DungeonFormStatus form_status)
        {
            is_game_menu = false;
            StopTimers();
            m_result_text[0] = "Час проходження: " + (m_hero.WalkthrowTime / 60) + "хв." + (m_hero.WalkthrowTime - (m_hero.WalkthrowTime / 60) * 60) + "сек.";
            string difficulty_name = "Легко";
            m_result_text[1] = "Рівень складності:" + difficulty_name;
            m_result_text[2] = "Персонаж: " + m_hero.Name;
            m_result_text[3] = "Пройдена відстань: " + m_hero.DistanceWalked;
            m_result_text[4] = "Монстрів убито:" + m_hero.MonstersKilled;
            m_result_text[5] = "Босів убито: " + m_hero.MonstersBossesKilled;
            m_result_text[6] = "Двері відчинені: " + m_hero.DoorsOpened;
            m_result_text[7] = "Двері з кодовим замком відкрито: " + m_hero.DoorsCodeOpened;
            m_result_text[8] = "Скрині відкрито: " + m_hero.ChestsOpened;
            m_form_status = form_status;
            Refresh();

            if (form_status == DungeonFormStatus.GameEndWin || form_status == DungeonFormStatus.GameEndLoose)
            {
                 
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
                        int records_number = 0;  
                        if (fs.Length != 0)  
                        {
                            fs.Position = 0;  
                            records_number = (int)formatter.Deserialize(fs);  
                            records_number++;  
                            fs.Position = 0;  
                            formatter.Serialize(fs, records_number);  
                            fs.Position = fs.Length;  
                            formatter.Serialize(fs, records_number);  
                        }
                        else  
                        {
                            formatter.Serialize(fs, 1);  
                            formatter.Serialize(fs, 1);  
                        }

                        if (form_status == DungeonFormStatus.GameEndWin)
                        {
                            formatter.Serialize(fs, true);  
                        }
                        else
                        {
                            formatter.Serialize(fs, false);  
                        }
                        formatter.Serialize(fs, m_hero.WalkthrowTime);  
                        formatter.Serialize(fs, Nick);  
                        formatter.Serialize(fs, total_difficulty_id);  
                        formatter.Serialize(fs, m_hero.Name);  
                        formatter.Serialize(fs, m_hero.Level);  
                        formatter.Serialize(fs, m_total_level_generation_number);  
                        formatter.Serialize(fs, m_hero.DistanceWalked);  
                        formatter.Serialize(fs, m_hero.MonstersKilled);  
                        formatter.Serialize(fs, m_hero.MonstersBossesKilled);  
                    }
                    m_hero.Destroy();
                }
                catch
                {
                    m_can_minimize = false;
                    MessageBox.Show("Помилка під час запису у файл!", "Рекорди", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    m_can_minimize = true;
                }
            }
        }

         
        public void WinGame()
        {
            EndGame(DungeonFormStatus.GameEndWin);
            musics[m_total_music_id].Stop();
            m_total_music_id = 1;
            musics[m_total_music_id].Play();
        }

         
        public void LooseGame()
        {
            EndGame(DungeonFormStatus.GameEndLoose);
            musics[m_total_music_id].Stop();
            m_total_music_id = 2;
            musics[m_total_music_id].Play();
        }

         
        public void GoAutoSave()
        {
            if (m_is_autosave)
            {
                if (total_difficulty_id != 3)
                {
                    PauseGame();
                    m_can_minimize = false;
                    SaveForm save_form = new SaveForm();
                    save_form.Owner = this;
                    SaveName = Nick + " " + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss") + " (автозбереження)";
                    save_form.ShowDialog();
                    m_can_minimize = true;
                    if (save_form.GoSave)
                    {
                        m_form_status = DungeonFormStatus.Saving;
                        SetLoadingChapters(3);
                        SetLoadingText("Збереження світу...");
                        SaveGame(SaveName);
                    }
                    timer_saving_wait.Start();
                }
            }
        }

         
    }
}