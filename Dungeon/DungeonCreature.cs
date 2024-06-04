using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Dungeon
{

    [Serializable]
    public class DungeonCreature : DungeonObject
    {

        private static readonly double minus_energy_on_move_step = 0.01;


        private static readonly double minus_energy_on_hit = 2;


        private static readonly double max_closest_length_to_pickup_item = 70;


        private static readonly double hit_radius = 70;


        private static readonly int hit_timer_frames_sword_back = 10;


        private static readonly int hit_timer_frames_sword_forward = 10;


        private static readonly int hit_timer_frames_sword_pause = 5;


        private static readonly int hit_timer_frames_sword_default = 10;


        private static readonly int hit_timer_frames_all = 1 + hit_timer_frames_sword_back + hit_timer_frames_sword_forward + hit_timer_frames_sword_pause + hit_timer_frames_sword_default;


        private Bitmap m_image_head_w;


        private Bitmap m_image_head_s;


        private Bitmap m_image_head_a;


        private Bitmap m_image_head_d;


        private Bitmap m_image_body_w;


        private Bitmap m_image_body_s;


        private Bitmap m_image_body_a;


        private Bitmap m_image_body_d;

        private Bitmap m_image_left_arm;


        private Bitmap m_image_right_arm;


        protected string m_name;


        public string Name
        {
            get
            {
                return m_name;
            }
        }


        private double m_max_health;


        private double m_intelligence;


        private double m_regeneration;


        private double m_max_energy;


        private double m_restore;


        private double m_power;


        private double m_mobility;


        private double m_speed;


        private double m_luck;


        private int m_level;


        public int Level
        {
            get
            {
                return m_level;
            }
        }


        private double m_exp;


        public double Exp
        {
            get
            {
                return m_exp;
            }
        }

     
        private int m_skill_points;


        public int SkillPoints
        {
            get
            {
                return m_skill_points;
            }
        }

       
        private double m_total_health;

      
        public double TotalHealth
        {
            get
            {
                return m_total_health;
            }
        }

    
        private double m_total_energy;

    
        public double TotalEnergy
        {
            get
            {
                return m_total_energy;
            }
        }

  
        private DungeonCreatureMoveDirection m_move_direction;

 
        public DungeonContainer Container;

      
        public DungeonContainer ContainerSpecialItems;

     
        private List<DungeonEffect> m_effects;

     
        public List<DungeonEffect> EffectsPotions
        {
            get
            {
                List<DungeonEffect> effect_potions = new List<DungeonEffect>();
                for (int i = 0; i < m_effects.Count; i++)
                {
                    if (m_effects[i].Duration != -1) effect_potions.Add(m_effects[i]);
                }
                return effect_potions;
            }
        }

  
        private int m_hit_total_frames;

 
        private double m_hit_power;

     
        private bool m_timer_hit_is_working;

    
        [NonSerialized]
        private Timer m_timer_hit;

   
   


  

  
    
    
     
   

        protected DungeonCreature(Bitmap texture, string name, double max_health, double regeneration, double max_energy, double restore, double power, double mobility, double speed, double intelligence, double luck)
            : base(Properties.Resources.error, DungeonObjectCollision.StaticCollision)
        {
            SetImagesFromTexture(texture);

            m_name = name;

            m_max_health = max_health;
            m_max_energy = max_energy;
            m_intelligence = intelligence;
            m_regeneration = regeneration;
            m_restore = restore;
            m_speed = speed;
            m_power = power;
            m_mobility = mobility;
            m_luck = luck;

            m_level = 1;
            m_exp = 0;
            m_skill_points = 0;

            m_total_health = m_max_health;
            m_total_energy = m_max_energy;

            m_move_direction = DungeonCreatureMoveDirection.Down;

            Container = new DungeonContainer(this, 30);

            ContainerSpecialItems = new DungeonContainer(this, 7);

            m_effects = new List<DungeonEffect>();

            m_hit_total_frames = 0;
            m_hit_power = m_power;

            m_timer_hit_is_working = false;
            InitializeTimers();
        }

 
        public void InitializeTimers()
        {
            m_timer_hit = new Timer();
            m_timer_hit.Interval = 10;
            m_timer_hit.Tick += timer_hit_Tick;
            if (m_timer_hit_is_working)
            {
                m_timer_hit.Start();
            }
        }

  
 
        private void SetImagesFromTexture(Bitmap texture)
        {
            const int head_width = 54;
            const int head_height = 60;
            const int body_width = 32;
            const int body_height = 40;
            const int left_arm_width = 11;
            const int left_arm_height = 11;
            const int right_arm_width = 11;
            const int right_arm_height = 11;

            Bitmap new_image_head_w = new Bitmap(head_width, head_height);
            Bitmap new_image_head_s = new Bitmap(head_width, head_height);
            Bitmap new_image_head_a = new Bitmap(head_width, head_height);
            Bitmap new_image_head_d = new Bitmap(head_width, head_height);

            Bitmap new_image_body_w = new Bitmap(body_width, body_height);
            Bitmap new_image_body_s = new Bitmap(body_width, body_height);
            Bitmap new_image_body_a = new Bitmap(body_width, body_height);
            Bitmap new_image_body_d = new Bitmap(body_width, body_height);

            Bitmap new_image_left_arm = new Bitmap(left_arm_width, left_arm_height);
            Bitmap new_image_right_arm = new Bitmap(right_arm_width, right_arm_height);

            for (int i = 0; i < texture.Height; i++)
            {
                for (int i2 = 0; i2 < texture.Width; i2++)
                {
                    Color pixel = texture.GetPixel(i2, i);
                    if (i < head_height)
                    {
                        if (i2 < head_width)
                        {
                            new_image_head_w.SetPixel(i2, i, pixel);
                        }
                        else if (i2 < head_width * 2)
                        {
                            new_image_head_s.SetPixel(i2 - head_width, i, pixel);
                        }
                        else if (i2 < head_width * 3)
                        {
                            new_image_head_a.SetPixel(i2 - head_width * 2, i, pixel);
                        }
                        else if (i2 < head_width * 4)
                        {
                            new_image_head_d.SetPixel(i2 - head_width * 3, i, pixel);
                        }
                    }
                    else if (i < head_height + body_height)
                    {
                        if (i2 < body_width)
                        {
                            new_image_body_w.SetPixel(i2, i - head_height, pixel);
                        }
                        else if (i2 < body_width * 2)
                        {
                            new_image_body_s.SetPixel(i2 - body_width, i - head_height, pixel);
                        }
                        else if (i2 < body_width * 3)
                        {
                            new_image_body_a.SetPixel(i2 - body_width * 2, i - head_height, pixel);
                        }
                        else if (i2 < body_width * 4)
                        {
                            new_image_body_d.SetPixel(i2 - body_width * 3, i - head_height, pixel);
                        }
                        else if (i2 < body_width * 4 + left_arm_width)
                        {
                            if (i < head_height + left_arm_height)
                            {
                                new_image_left_arm.SetPixel(i2 - body_width * 4, i - head_height, pixel);
                            }
                        }
                        else if (i2 < body_width * 4 + left_arm_width + right_arm_width)
                        {
                            if (i < head_height + right_arm_height)
                            {
                                new_image_right_arm.SetPixel(i2 - body_width * 4 - left_arm_width, i - head_height, pixel);
                            }
                        }
                    }
                }
            }

            m_image_head_w = new_image_head_w;
            m_image_head_s = new_image_head_s;
            m_image_head_a = new_image_head_a;
            m_image_head_d = new_image_head_d;

            m_image_body_w = new_image_body_w;
            m_image_body_s = new_image_body_s;
            m_image_body_a = new_image_body_a;
            m_image_body_d = new_image_body_d;

            m_image_left_arm = new_image_left_arm;
            m_image_right_arm = new_image_right_arm;

            Image = m_image_body_s;
        }

   

        private void PlusLocationX(double x)
        {
            Location = new DungeonPoint(Location.X + x, Location.Y);
        }

 

        private void PlusLocationY(double y)
        {
            Location = new DungeonPoint(Location.X, Location.Y + y);
        }

    
   

        private DungeonPoint PlusLocationXValue(double x)
        {
            return new DungeonPoint(Location.X + x, Location.Y);
        }


  

        private DungeonPoint PlusLocationYValue(double y)
        {
            return new DungeonPoint(Location.X, Location.Y + y);
        }

   
        public void FullStats()
        {
            m_total_health = m_max_health;
            m_total_energy = m_max_energy;
        }

     
   

        private void MoveWithSpeed(DungeonCreatureMoveDirection move_direction, double step = 1)
        {
            bool need_hide_code_buttons = true;

            DungeonPoint creature_location = new DungeonPoint();

            if (move_direction == DungeonCreatureMoveDirection.Up)
            {
                creature_location = PlusLocationYValue(-step);
            }
            else if (move_direction == DungeonCreatureMoveDirection.Down)
            {
                creature_location = PlusLocationYValue(step);
            }
            else if (move_direction == DungeonCreatureMoveDirection.Left)
            {
                creature_location = PlusLocationXValue(-step);
            }
            else if (move_direction == DungeonCreatureMoveDirection.Right)
            {
                creature_location = PlusLocationXValue(step);
            }

            DungeonPoint creature_rect_location = new DungeonPoint(creature_location.X - CollisionSize.Width / 2 + CollisionOffset.X, creature_location.Y - CollisionSize.Height / 2 + CollisionOffset.Y);
            Rectangle creature_rect = new Rectangle(creature_rect_location.Point, CollisionSize);

  

            List<DungeonObject> objects_with_static_collision = DungeonLevel.ObjectsWithStaticCollision;

            for (int i = 0; i < objects_with_static_collision.Count; i++)
            {
                DungeonObject obj = objects_with_static_collision[i];

                if (obj.CollisionType == DungeonObjectCollision.StaticCollision)
                {
                    if (obj != this)
                    {
                        Size object_size = obj.CollisionSize;
                        Point object_location = obj.Location.Point;
                        Point object_collision_offset = obj.CollisionOffset;
                        Point object_rect_location = new Point(object_location.X - object_size.Width / 2 + object_collision_offset.X, object_location.Y - object_size.Height / 2 + object_collision_offset.Y);
                        Rectangle object_rect = new Rectangle(object_rect_location, object_size);

                        if (creature_rect.IntersectsWith(object_rect))
                        {
                            bool can_go = false;
                            if (this is DungeonHero)
                            {
                                if (obj is DungeonDoor)
                                {
                                    if ((obj as DungeonDoor).IsCode)
                                    {
                                        DungeonLevel.Form.ShowCodeInsert(obj as DungeonDoor);
                                        need_hide_code_buttons = false;
                                    }
                                    else
                                    {
                                        DungeonItemKey key = (obj as DungeonDoor).Key;
                                        if (Have(key))
                                        {
                                            (obj as DungeonDoor).Open();
                                            can_go = true;
                                            PlusExp(1);
                                            (this as DungeonHero).DoorsOpened++;
                                            AudioEffect audio_effect = new AudioEffect(MainForm.GetSound(1));
                                            audio_effect.Play();
                                        }
                                        else
                                        {
                                            AudioEffect audio_effect = new AudioEffect(MainForm.GetSound(2));
                                            audio_effect.Play();
                                        }
                                    }
                                }
                                else if (obj is DungeonChest)
                                {
                                    if (!((obj as DungeonChest).IsOpened))
                                    {
                                        (obj as DungeonChest).Open(this);
                                        (this as DungeonHero).ChestsOpened++;
                                        PlusExp(1);
                                    }
                                }
                            }
                            if (!can_go)
                            {
                                double new_speed = step; 
                                if (move_direction == DungeonCreatureMoveDirection.Up)
                                {
                                    new_speed = creature_rect.Y - (object_rect.Y + object_rect.Height - step);
                                }
                                else if (move_direction == DungeonCreatureMoveDirection.Down)
                                {
                                    new_speed = object_rect.Y - (creature_rect.Y + creature_rect.Height - step);
                                }
                                else if (move_direction == DungeonCreatureMoveDirection.Left)
                                {
                                    new_speed = creature_rect.X - (object_rect.X + object_rect.Width - step);
                                }
                                else if (move_direction == DungeonCreatureMoveDirection.Right)
                                {
                                    new_speed = object_rect.X - (creature_rect.X + creature_rect.Width - step);
                                }
                                if (new_speed < 0) new_speed = 0;
                                if (new_speed < step) step = new_speed;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    DungeonLevel.ObjectsWithStaticCollision.Remove(obj);
                    i--;
                }
            }


            if (DungeonLevel.LadderEntranceBlock != null)
            {
                Point ladder_entrance_location = new Point(0, 0);
                Size ladder_entrance_size = new Size(100, 100);

                if (DungeonLevel.LadderEntranceDirection == DungeonLadderType.Up)
                {
                    ladder_entrance_size = new Size(DungeonLevel.LadderEntranceBlock.Image.Size.Width, DungeonLevel.LadderEntranceBlock.Image.Size.Height / 4);
                    ladder_entrance_location = new Point(DungeonLevel.LadderEntranceBlock.Location.Point.X - DungeonLevel.LadderEntranceBlock.Image.Size.Width / 2, DungeonLevel.LadderEntranceBlock.Location.Point.Y - DungeonLevel.LadderEntranceBlock.Image.Size.Height / 2);
                }
                else if (DungeonLevel.LadderEntranceDirection == DungeonLadderType.Down)
                {
                    ladder_entrance_size = new Size(DungeonLevel.LadderEntranceBlock.Image.Size.Width, DungeonLevel.LadderEntranceBlock.Image.Size.Height / 4);
                    ladder_entrance_location = new Point(DungeonLevel.LadderEntranceBlock.Location.Point.X - DungeonLevel.LadderEntranceBlock.Image.Size.Width / 2, DungeonLevel.LadderEntranceBlock.Location.Point.Y - DungeonLevel.LadderEntranceBlock.Image.Size.Height / 2 + (DungeonLevel.LadderEntranceBlock.Image.Size.Height - ladder_entrance_size.Height));
                }
                else if (DungeonLevel.LadderEntranceDirection == DungeonLadderType.Left)
                {
                    ladder_entrance_size = new Size(DungeonLevel.LadderEntranceBlock.Image.Size.Width / 4, DungeonLevel.LadderEntranceBlock.Image.Size.Height);
                    ladder_entrance_location = new Point(DungeonLevel.LadderEntranceBlock.Location.Point.X - DungeonLevel.LadderEntranceBlock.Image.Size.Width / 2, DungeonLevel.LadderEntranceBlock.Location.Point.Y - DungeonLevel.LadderEntranceBlock.Image.Size.Height / 2);
                }
                else if (DungeonLevel.LadderEntranceDirection == DungeonLadderType.Right)
                {
                    ladder_entrance_size = new Size(DungeonLevel.LadderEntranceBlock.Image.Size.Width / 4, DungeonLevel.LadderEntranceBlock.Image.Size.Height);
                    ladder_entrance_location = new Point(DungeonLevel.LadderEntranceBlock.Location.Point.X - DungeonLevel.LadderEntranceBlock.Image.Size.Width / 2 + (DungeonLevel.LadderEntranceBlock.Image.Size.Width - ladder_entrance_size.Width), DungeonLevel.LadderEntranceBlock.Location.Point.Y - DungeonLevel.LadderEntranceBlock.Image.Size.Height / 2);
                }
                Rectangle ladder_entrance_rect = new Rectangle(ladder_entrance_location, ladder_entrance_size);

                if (creature_rect.IntersectsWith(ladder_entrance_rect))
                {
                    DungeonLevel.PreviousDungeonLevel.Add(this);
                    DungeonLevel old_dungeon_level = DungeonLevel.NextDungeonLevel;
                    DungeonLevel new_dungeon_level = DungeonLevel;
                    Location = new DungeonPoint(Location.X - old_dungeon_level.LadderEntranceBlock.Location.X + new_dungeon_level.LadderExitBlock.Location.X, Location.Y - old_dungeon_level.LadderEntranceBlock.Location.Y + new_dungeon_level.LadderExitBlock.Location.Y);
                    DungeonLevel.Form.UpdateMapDungeonLevelShowing();

                    if (this is DungeonHero)
                    {
                        old_dungeon_level.PauseMonstersActions();
                        new_dungeon_level.ResumeMonstersActions();
                    }
                }
            }


            if (DungeonLevel.LadderExitBlock != null)
            {
                Point ladder_exit_location = new Point(0, 0);
                Size ladder_exit_size = new Size(100, 100);

                if (DungeonLevel.LadderExitDirection == DungeonLadderType.Up)
                {
                    ladder_exit_size = new Size(DungeonLevel.LadderExitBlock.Image.Size.Width, DungeonLevel.LadderExitBlock.Image.Size.Height / 4);
                    ladder_exit_location = new Point(DungeonLevel.LadderExitBlock.Location.Point.X - DungeonLevel.LadderExitBlock.Image.Size.Width / 2, DungeonLevel.LadderExitBlock.Location.Point.Y - DungeonLevel.LadderExitBlock.Image.Size.Height / 2);
                }
                else if (DungeonLevel.LadderExitDirection == DungeonLadderType.Down)
                {
                    ladder_exit_size = new Size(DungeonLevel.LadderExitBlock.Image.Size.Width, DungeonLevel.LadderExitBlock.Image.Size.Height / 4);
                    ladder_exit_location = new Point(DungeonLevel.LadderExitBlock.Location.Point.X - DungeonLevel.LadderExitBlock.Image.Size.Width / 2, DungeonLevel.LadderExitBlock.Location.Point.Y - DungeonLevel.LadderExitBlock.Image.Size.Height / 2 + (DungeonLevel.LadderExitBlock.Image.Size.Height - ladder_exit_size.Height));
                }
                else if (DungeonLevel.LadderExitDirection == DungeonLadderType.Left)
                {
                    ladder_exit_size = new Size(DungeonLevel.LadderExitBlock.Image.Size.Width / 4, DungeonLevel.LadderExitBlock.Image.Size.Height);
                    ladder_exit_location = new Point(DungeonLevel.LadderExitBlock.Location.Point.X - DungeonLevel.LadderExitBlock.Image.Size.Width / 2, DungeonLevel.LadderExitBlock.Location.Point.Y - DungeonLevel.LadderExitBlock.Image.Size.Height / 2);
                }
                else if (DungeonLevel.LadderExitDirection == DungeonLadderType.Right)
                {
                    ladder_exit_size = new Size(DungeonLevel.LadderExitBlock.Image.Size.Width / 4, DungeonLevel.LadderExitBlock.Image.Size.Height);
                    ladder_exit_location = new Point(DungeonLevel.LadderExitBlock.Location.Point.X - DungeonLevel.LadderExitBlock.Image.Size.Width / 2 + (DungeonLevel.LadderExitBlock.Image.Size.Width - ladder_exit_size.Width), DungeonLevel.LadderExitBlock.Location.Point.Y - DungeonLevel.LadderExitBlock.Image.Size.Height / 2);
                }
                Rectangle ladder_exit_rect = new Rectangle(ladder_exit_location, ladder_exit_size);

                if (creature_rect.IntersectsWith(ladder_exit_rect))
                {
                    DungeonLevel.NextDungeonLevel.Add(this);
                    DungeonLevel old_dungeon_level = DungeonLevel.PreviousDungeonLevel;
                    DungeonLevel new_dungeon_level = DungeonLevel;
                    Location = new DungeonPoint(Location.X - old_dungeon_level.LadderExitBlock.Location.X + new_dungeon_level.LadderEntranceBlock.Location.X, Location.Y - old_dungeon_level.LadderExitBlock.Location.Y + new_dungeon_level.LadderEntranceBlock.Location.Y);
                    DungeonLevel.Form.UpdateMapDungeonLevelShowing();
                    if (this is DungeonHero)
                    {
                        old_dungeon_level.PauseMonstersActions();
                        new_dungeon_level.ResumeMonstersActions();
                        DungeonLevel.Form.GoAutoSave();
                    }
                }
            }
            // =========================================================

            double minus_energy = 0;
            if (step != 0)
            {
                minus_energy = minus_energy_on_move_step;
                minus_energy = minus_energy * (1 - GetStatValueWithEffectValue(DungeonStats.Mobility) / 100); 
                if (minus_energy < 0) minus_energy = 0;
                if (m_total_energy < minus_energy)
                {
                    minus_energy = (m_total_energy / minus_energy) * minus_energy;
                    step = (m_total_energy / minus_energy) * step;
                }
                if (this is DungeonHero)
                {
                    (this as DungeonHero).DistanceWalked += step;
                }

                if (need_hide_code_buttons)
                {
                    DungeonLevel.Form.HideCodeInsert();
                }
            }
            if (m_total_energy > 0)
            {
                if (move_direction == DungeonCreatureMoveDirection.Up)
                {
                    PlusLocationY(-step);
                }
                else if (move_direction == DungeonCreatureMoveDirection.Down)
                {
                    PlusLocationY(step);
                }
                else if (move_direction == DungeonCreatureMoveDirection.Left)
                {
                    PlusLocationX(-step);
                }
                else if (move_direction == DungeonCreatureMoveDirection.Right)
                {
                    PlusLocationX(step);
                }
                m_total_energy -= minus_energy;
            }
            if (m_total_energy < 0) m_total_energy = 0; // на всякий случай
        }

    
    
   
        public void Move(DungeonCreatureMoveDirection move_direction, bool dont_change_move_direction = false)
        {
            if (ObjectStatus == DungeonObjectStatus.AddedNotDestroyed)
            {
                double value = minus_energy_on_move_step;
                while (m_total_energy < value && value > 0)
                {
                    value -= 0.5;
                }
                if (value > 0)
                {
                    MoveWithSpeed(move_direction, GetStatValueWithEffectValue(DungeonStats.Speed));
                    if (this is DungeonHero)
                    {
                        DungeonLevel.FindCells(this, new Size(950, 950));
                    }
                }

                if (!dont_change_move_direction)
                {
                    m_move_direction = move_direction;
                }
            }
        }


    
   
     
        new public bool Paint(object sender, PaintEventArgs e, Point player_location, Size showing_size)
        {
            bool can_go_next_code = false;
            if (showing_size.Width == 0 && showing_size.Height == 0) can_go_next_code = true;
            if (can_go_next_code || ObjectStatus == DungeonObjectStatus.AddedNotDestroyed)
            {
                Size form_size = ((MainForm)sender).Size;
                Point center_screen = new Point(((MainForm)sender).ClientSize.Width / 2, ((MainForm)sender).ClientSize.Height / 2);
                Point screen_location;
                if (can_go_next_code) screen_location = player_location;
                else screen_location = new DungeonPoint(Location.X - player_location.X + form_size.Width / 2 - Size.Width / 2, Location.Y - player_location.Y + form_size.Height / 2 - Size.Height / 2).Point;
                if (can_go_next_code || IsInView(screen_location, center_screen, showing_size))
                {
                    Point left_arm_location = new Point(0, 0);
                    Point right_arm_location = new Point(0, 0);

                    
                    if (m_move_direction == DungeonCreatureMoveDirection.Up)
                    {
                        left_arm_location = new Point(screen_location.X - m_image_left_arm.Width / 2, screen_location.Y + m_image_body_w.Height * 3 / 5);
                        right_arm_location = new Point(screen_location.X + m_image_body_w.Width - m_image_right_arm.Width / 2 - 1, screen_location.Y + m_image_body_w.Height * 3 / 5);
                    }
                    else if (m_move_direction == DungeonCreatureMoveDirection.Down)
                    {
                        left_arm_location = new Point(screen_location.X - m_image_left_arm.Width / 2, screen_location.Y + m_image_body_s.Height * 3 / 5);
                        right_arm_location = new Point(screen_location.X + m_image_body_s.Width - m_image_right_arm.Width / 2 - 1, screen_location.Y + m_image_body_s.Height * 3 / 5);
                    }
                    else if (m_move_direction == DungeonCreatureMoveDirection.Left)
                    {
                        left_arm_location = new Point(screen_location.X + m_image_body_a.Width / 2 - m_image_left_arm.Width / 2, screen_location.Y + m_image_body_a.Height * 3 / 5);
                    }
                    else if (m_move_direction == DungeonCreatureMoveDirection.Right)
                    {
                        right_arm_location = new Point(screen_location.X + m_image_body_d.Width / 2 - m_image_right_arm.Width / 2, screen_location.Y + m_image_body_d.Height * 3 / 5);
                    }

                   
                    if (m_move_direction != DungeonCreatureMoveDirection.Left && m_move_direction != DungeonCreatureMoveDirection.Up)
                    {
                       
                        if (m_move_direction == DungeonCreatureMoveDirection.Up)
                        {
                            e.Graphics.DrawImage(m_image_body_w, screen_location);
                        }
                        else if (m_move_direction == DungeonCreatureMoveDirection.Down)
                        {
                            e.Graphics.DrawImage(m_image_body_s, screen_location);
                        }
                        else if (m_move_direction == DungeonCreatureMoveDirection.Left)
                        {
                            e.Graphics.DrawImage(m_image_body_a, screen_location);
                        }
                        else if (m_move_direction == DungeonCreatureMoveDirection.Right)
                        {
                            e.Graphics.DrawImage(m_image_body_d, screen_location);
                        }

                       
                        DungeonItem item_armour = ContainerSpecialItems.Items[1];
                        if (item_armour != null)
                        {
                            Bitmap image = (item_armour as DungeonItemArmour).GetImageOnDirection(m_move_direction);
                            Point image_location = new Point(screen_location.X + m_image_body_w.Width / 2 - image.Width / 2, screen_location.Y + m_image_body_w.Height - image.Height);
                            e.Graphics.DrawImage(image, image_location);
                        }

                       
                        Point head_location = new Point();
                        if (m_move_direction == DungeonCreatureMoveDirection.Up)
                        {
                            head_location = new Point(screen_location.X + m_image_body_w.Width / 2 - m_image_head_w.Width / 2, screen_location.Y - m_image_head_w.Height / 2);
                            e.Graphics.DrawImage(m_image_head_w, head_location);
                        }
                        else if (m_move_direction == DungeonCreatureMoveDirection.Down)
                        {
                            head_location = new Point(screen_location.X + m_image_body_s.Width / 2 - m_image_head_s.Width / 2, screen_location.Y - m_image_head_s.Height / 2);
                            e.Graphics.DrawImage(m_image_head_s, head_location);
                        }
                        else if (m_move_direction == DungeonCreatureMoveDirection.Left)
                        {
                            head_location = new Point(screen_location.X + m_image_body_a.Width / 2 - m_image_head_a.Width / 2, screen_location.Y - m_image_head_a.Height / 2);
                            e.Graphics.DrawImage(m_image_head_a, head_location);
                        }
                        else if (m_move_direction == DungeonCreatureMoveDirection.Right)
                        {
                            head_location = new Point(screen_location.X + m_image_body_d.Width / 2 - m_image_head_d.Width / 2, screen_location.Y - m_image_head_d.Height / 2);
                            e.Graphics.DrawImage(m_image_head_d, head_location);
                        }

                        
                        DungeonItem item_helmet = ContainerSpecialItems.Items[0];
                        if (item_helmet != null)
                        {
                            Bitmap image = (item_helmet as DungeonItemHelmet).GetImageOnDirection(m_move_direction);
                            Point image_location = new Point(head_location.X + m_image_head_s.Width / 2 - image.Width / 2, head_location.Y + m_image_head_s.Height / 2 - image.Height / 2 - 7);
                            e.Graphics.DrawImage(image, image_location);
                        }
                    }

                    
                    DungeonItem item_sword = ContainerSpecialItems.Items[3];
                    if (item_sword != null)
                    {
                        Bitmap image = (item_sword as DungeonItemSword).GetImageOnDirection(m_move_direction);
                        Point image_location = new Point();
                        if (m_move_direction == DungeonCreatureMoveDirection.Up)
                        {
                            image_location = new Point(screen_location.X + m_image_body_w.Width - m_image_right_arm.Width / 2 + 1 - 3, screen_location.Y + m_image_body_w.Height * 3 / 5 - image.Height + 30 - image.Width / 2);
                        }
                        else if (m_move_direction == DungeonCreatureMoveDirection.Down)
                        {
                            image_location = new Point(screen_location.X - m_image_left_arm.Width / 2 + 1 - 3, screen_location.Y + m_image_body_w.Height * 3 / 5 - image.Height + 30 - image.Width / 2);
                        }
                        else if (m_move_direction == DungeonCreatureMoveDirection.Left)
                        {
                            image_location = new Point(screen_location.X + m_image_body_d.Width / 2 - m_image_right_arm.Width / 2 - image.Width + 12 + 5, screen_location.Y + m_image_body_d.Height * 3 / 5 - image.Height + 16 + 5);
                        }
                        else if (m_move_direction == DungeonCreatureMoveDirection.Right)
                        {
                            image_location = new Point(screen_location.X + m_image_body_d.Width / 2 - m_image_right_arm.Width / 2 - image.Width + 78 + 5, screen_location.Y + m_image_body_d.Height * 3 / 5 - image.Height + 16 + 5);
                        }
                        e.Graphics.DrawImage(image, image_location);
                    }

                    
                    if (m_move_direction == DungeonCreatureMoveDirection.Left || m_move_direction == DungeonCreatureMoveDirection.Up)
                    {
                     
                        if (m_move_direction == DungeonCreatureMoveDirection.Up)
                        {
                            e.Graphics.DrawImage(m_image_left_arm, left_arm_location);
                            e.Graphics.DrawImage(m_image_right_arm, right_arm_location);
                        }

                      
                        if (m_move_direction == DungeonCreatureMoveDirection.Up)
                        {
                            e.Graphics.DrawImage(m_image_body_w, screen_location);
                        }
                        else if (m_move_direction == DungeonCreatureMoveDirection.Down)
                        {
                            e.Graphics.DrawImage(m_image_body_s, screen_location);
                        }
                        else if (m_move_direction == DungeonCreatureMoveDirection.Left)
                        {
                            e.Graphics.DrawImage(m_image_body_a, screen_location);
                        }
                        else if (m_move_direction == DungeonCreatureMoveDirection.Right)
                        {
                            e.Graphics.DrawImage(m_image_body_d, screen_location);
                        }

                        
                        DungeonItem item_armour = ContainerSpecialItems.Items[1];
                        if (item_armour != null)
                        {
                            Bitmap image = (item_armour as DungeonItemArmour).GetImageOnDirection(m_move_direction);
                            Point image_location = new Point(screen_location.X + m_image_body_w.Width / 2 - image.Width / 2, screen_location.Y + m_image_body_w.Height - image.Height);
                            e.Graphics.DrawImage(image, image_location);
                        }

                       
                        Point head_location = new Point();
                        if (m_move_direction == DungeonCreatureMoveDirection.Up)
                        {
                            head_location = new Point(screen_location.X + m_image_body_w.Width / 2 - m_image_head_w.Width / 2, screen_location.Y - m_image_head_w.Height / 2);
                            e.Graphics.DrawImage(m_image_head_w, head_location);
                        }
                        else if (m_move_direction == DungeonCreatureMoveDirection.Down)
                        {
                            head_location = new Point(screen_location.X + m_image_body_s.Width / 2 - m_image_head_s.Width / 2, screen_location.Y - m_image_head_s.Height / 2);
                            e.Graphics.DrawImage(m_image_head_s, head_location);
                        }
                        else if (m_move_direction == DungeonCreatureMoveDirection.Left)
                        {
                            head_location = new Point(screen_location.X + m_image_body_a.Width / 2 - m_image_head_a.Width / 2, screen_location.Y - m_image_head_a.Height / 2);
                            e.Graphics.DrawImage(m_image_head_a, head_location);
                        }
                        else if (m_move_direction == DungeonCreatureMoveDirection.Right)
                        {
                            head_location = new Point(screen_location.X + m_image_body_d.Width / 2 - m_image_head_d.Width / 2, screen_location.Y - m_image_head_d.Height / 2);
                            e.Graphics.DrawImage(m_image_head_d, head_location);
                        }

                       
                        DungeonItem item_helmet = ContainerSpecialItems.Items[0];
                        if (item_helmet != null)
                        {
                            Bitmap image = (item_helmet as DungeonItemHelmet).GetImageOnDirection(m_move_direction);
                            Point image_location = new Point(head_location.X + m_image_head_s.Width / 2 - image.Width / 2, head_location.Y + m_image_head_s.Height / 2 - image.Height / 2 - 7);
                            e.Graphics.DrawImage(image, image_location);
                        }

                       
                        if (m_move_direction == DungeonCreatureMoveDirection.Left)
                        {
                            e.Graphics.DrawImage(m_image_left_arm, left_arm_location);
                        }
                    }
                    else
                    {
                        
                        if (m_move_direction == DungeonCreatureMoveDirection.Down)
                        {
                            e.Graphics.DrawImage(m_image_left_arm, left_arm_location);
                            e.Graphics.DrawImage(m_image_right_arm, right_arm_location);
                        }
                        else if (m_move_direction == DungeonCreatureMoveDirection.Right)
                        {
                            e.Graphics.DrawImage(m_image_right_arm, right_arm_location);
                        }
                    }

                    return true;
                }
            }
            return false;
        }

       
        
        
        public bool Have(DungeonItem item)
        {
            List<DungeonItem> creature_items = Container.Items;
            for (int i = 0; i < creature_items.Count; i++)
            {
                DungeonItem total_item = creature_items[i];
                if (total_item == item)
                {
                    return true;
                }
            }
            return false;
        }

      
       
        public bool PickUpItem()
        {
            List<DungeonItem> items_on_floor = DungeonLevel.Container.Items;
            DungeonItem closest_item = null;
            double closest_length = max_closest_length_to_pickup_item + 1;
            for (int i = 0; i < items_on_floor.Count; i++)
            {
                DungeonItem item = items_on_floor[i];
                double length = Math.Sqrt(Math.Pow(item.Location.X - Location.X, 2) + Math.Pow(item.Location.Y - Location.Y, 2));
                if (length < max_closest_length_to_pickup_item && length < closest_length)
                {
                    closest_item = item;
                    closest_length = length;
                }
            }
            if (closest_item != null)
            {
                bool can_add = false;
                List<DungeonItem> items = Container.Items;
                int i = 0;
                for (i = 0; i < 30; i++)
                {
                    if (items[i] == null)
                    {
                        can_add = true;
                        break;
                    }
                }
                if (can_add)
                {
                    bool result = Container.Set(closest_item, i);
                    if (result == true)
                    {
                        if (this is DungeonHero)
                        {
                            if (closest_item is DungeonItemArtifact)
                            {
                                if ((closest_item as DungeonItemArtifact).IsSpecial)
                                {
                                    DungeonLevel.Form.WinGame();
                                }
                            }
                        }
                        return true;
                    }
                }
            }
            return false;
        }

   
    
        public void Drop(DungeonItem item)
        {
            if (item.CanDrop)
            {
                item.Location = Location;
                DungeonLevel.Container.Add(item);
            }
        }

   
   
        public void Use(DungeonItem item)
        {
            if (item != null)
            {
                if (item.CanUse)
                {
                    if (item is DungeonItemPotion)
                    {
                        (item as DungeonItemPotion).StartEffects(this);
                    }
                    else if (item is DungeonItemPaper)
                    {
                        // ...
                    }
                }
            }
        }

    
    
        public void Destroy(DungeonItem item)
        {
            if (item != null)
            {
                if (item.CanDestroy)
                {
                    item.Destroy();
                }
            }
        }

  
 
        public void PlusExp(double exp)
        {
            m_exp += exp * (1 + m_intelligence / 100);
            while (m_exp >= 10)
            {
                m_level++;
                if (this is DungeonHero)
                {
                    AudioEffect audio_effect = new AudioEffect(MainForm.GetSound(0));
                    audio_effect.Play();
                }
                m_exp -= 10;
                Random random = new Random();
                do
                {
                    m_skill_points++;
                } while (random.Next(0, 101) <= m_luck);
            }
        }

    
      
     
        public double GetEffectValue(DungeonStats stat)
        {
            double result_effect_value = 0;
            for (int i = 0; i < m_effects.Count; i++)
            {
                if (m_effects[i].Stat == stat)
                {
                    result_effect_value += m_effects[i].Value;
                }
            }
            return result_effect_value;
        }

  
  
        public void AddEffect(DungeonEffect effect)
        {
            m_effects.Add(effect);
        }

    
      
        public void RemoveEffect(DungeonEffect effect)
        {
            m_effects.Remove(effect);
        }

    
    
   
        public double GetStatValue(DungeonStats stat)
        {
            if (stat == DungeonStats.MaxHealth) return m_max_health;
            else if (stat == DungeonStats.MaxEnergy) return m_max_energy;
            else if (stat == DungeonStats.Intelligence) return m_intelligence;
            else if (stat == DungeonStats.Regeneration) return m_regeneration;
            else if (stat == DungeonStats.Restore) return m_restore;
            else if (stat == DungeonStats.Speed) return m_speed;
            else if (stat == DungeonStats.Power) return m_power;
            else if (stat == DungeonStats.Mobility) return m_mobility;
            else if (stat == DungeonStats.Luck) return m_luck;
            return -1;
        }

   
     
    
        public void UpStatValue(DungeonStats stat, bool is_free = false)
        {
            if (m_skill_points > 0 || is_free)
            {
                double value = GetStatValue(stat);
                if (value < DungeonStatsInfo.Max(stat) - 0.25)
                {
                    value += DungeonStatsInfo.Plus(stat);
                    if (!is_free) m_skill_points--;
                    if (stat == DungeonStats.MaxHealth) m_max_health = value;
                    else if (stat == DungeonStats.MaxEnergy) m_max_energy = value;
                    else if (stat == DungeonStats.Regeneration) m_regeneration = value;
                    else if (stat == DungeonStats.Restore) m_restore = value;
                    else if (stat == DungeonStats.Speed) m_speed = value;
                    else if (stat == DungeonStats.Power) m_power = value;
                    else if (stat == DungeonStats.Mobility) m_mobility = value;
                    if (this is DungeonHero)
                    {
                        if (stat == DungeonStats.Intelligence) m_intelligence = value;
                        else if (stat == DungeonStats.Luck) m_luck = value;
                    }
                }
            }
        }

 


        public double GetStatValueWithEffectValue(DungeonStats stat)
        {
            return GetStatValue(stat) + GetEffectValue(stat);
        }


 
        public void Regenerate(double k = 1)
        {
            if (ObjectStatus == DungeonObjectStatus.AddedNotDestroyed)
            {
                double new_value = m_total_health + GetStatValueWithEffectValue(DungeonStats.Regeneration) * k;
                if (new_value <= GetStatValueWithEffectValue(DungeonStats.MaxHealth))
                {
                    if (new_value >= 0)
                    {
                        m_total_health = new_value;
                    }
                    else
                    {
                        m_total_health = 0;
                        GoDeath(); 
                    }
                }
                else
                {
                    m_total_health = GetStatValueWithEffectValue(DungeonStats.MaxHealth);
                }
            }
        }

   

        public void Restore(double k = 1)
        {
            if (ObjectStatus == DungeonObjectStatus.AddedNotDestroyed)
            {
                double new_value = m_total_energy + GetStatValueWithEffectValue(DungeonStats.Restore) * k;
                if (new_value <= GetStatValueWithEffectValue(DungeonStats.MaxEnergy))
                {
                    if (new_value >= 0)
                    {
                        m_total_energy = new_value;
                    }
                    else
                    {
                        m_total_energy = 0;
                    }
                }
                else
                {
                    m_total_energy = GetStatValueWithEffectValue(DungeonStats.MaxEnergy);
                }
            }
        }

  
        public void Hit()
        {
            if (!m_timer_hit_is_working)
            {
                if (ObjectStatus == DungeonObjectStatus.AddedNotDestroyed)
                {
                    if (m_hit_total_frames == 0)
                    {
                        m_hit_power = GetStatValueWithEffectValue(DungeonStats.Power);
                        double energy_with_mobility = minus_energy_on_hit * (1 - GetStatValueWithEffectValue(DungeonStats.Mobility) / 100);
                        if (energy_with_mobility < 0) energy_with_mobility = 0;
                        if (m_total_energy > energy_with_mobility / 2) 
                        {
                            if (m_total_energy < energy_with_mobility) 
                            {
                                m_hit_power = (m_total_energy / energy_with_mobility) * m_hit_power; 
                            }
                            m_timer_hit.Start();
                            m_timer_hit_is_working = true;
                        }

                    }
                }
            }
        }

    
        private void timer_hit_Tick(object sender, EventArgs e)
        {
            if (ObjectStatus == DungeonObjectStatus.Destroyed || DungeonLevel == null)
            {
                m_timer_hit.Stop();
                m_timer_hit_is_working = false;
                return;
            }
            m_hit_total_frames++;

  
            if (m_hit_total_frames <= 1 + hit_timer_frames_sword_back + hit_timer_frames_sword_forward)
            {
                if (m_hit_total_frames > hit_timer_frames_sword_back + 3)
                {
                    AudioEffect audio_effect = new AudioEffect(MainForm.GetSound(3));
                    audio_effect.Play();
                }
                if (ContainerSpecialItems.Items[3] != null)
                {
                    if ((ContainerSpecialItems.Items[3] as DungeonItemSword).TotalFrame <= 1 + hit_timer_frames_sword_back + hit_timer_frames_sword_forward)
                    {
                        (ContainerSpecialItems.Items[3] as DungeonItemSword).TotalFrame++;
                    }
                }
                double minus_energy = (minus_energy_on_hit / (1 + hit_timer_frames_sword_back + hit_timer_frames_sword_forward)); 
                minus_energy = minus_energy * (1 - GetStatValueWithEffectValue(DungeonStats.Mobility) / 100); 
                if (minus_energy > 0)
                {
                    m_total_energy -= minus_energy;
                }
                if (m_total_energy < 0) m_total_energy = 0; // на всякий случай
            }

           
            else if (m_hit_total_frames == 1 + hit_timer_frames_sword_back + hit_timer_frames_sword_forward + 1)
            {
                // =======================
               
                double x0, y0; 
                x0 = Location.X + Image.Width / 2;
                y0 = Location.Y + Image.Height / 2;
                DungeonCreatureMoveDirection d0 = m_move_direction; 
                for (int i = 0; i < DungeonLevel.Creatures.Count; i++) 
                {
                    if (DungeonLevel.Creatures[i].ObjectStatus == DungeonObjectStatus.AddedNotDestroyed &&
                        DungeonLevel.Creatures[i] != this &&
                        (((this is DungeonMonster) && (DungeonLevel.Creatures[i] is DungeonHero)) || ((this is DungeonHero) && (DungeonLevel.Creatures[i] is DungeonMonster))))
                    {
                        double x, y; 
                        x = DungeonLevel.Creatures[i].Location.X + DungeonLevel.Creatures[i].Image.Width / 2;
                        y = DungeonLevel.Creatures[i].Location.Y + DungeonLevel.Creatures[i].Image.Height / 2;

                        if (Math.Pow(x - x0, 2) + Math.Pow(y - y0, 2) <= Math.Pow(hit_radius, 2)) 
                        {
                            DungeonCreatureMoveDirection d; 
                            DungeonCreatureMoveDirection d2;
                            if (y >= y0) d = DungeonCreatureMoveDirection.Down;
                            else d = DungeonCreatureMoveDirection.Up;
                            if (x >= x0) d2 = DungeonCreatureMoveDirection.Right;
                            else d2 = DungeonCreatureMoveDirection.Left;

                            if (d == d0 || d2 == d0) 
                            {
                                AudioEffect audio_effect = new AudioEffect(MainForm.GetSound(4));
                                audio_effect.Play();
                                if (DungeonLevel.Creatures[i].HitByAnother(this))
                                { 

                                }
                            }
                        }
                    }
                }
                
                if (ContainerSpecialItems.Items[3] != null)
                {
                    (ContainerSpecialItems.Items[3] as DungeonItemSword).TotalFrame++;
                }
            }
            else if (m_hit_total_frames < hit_timer_frames_all) 
            {
                if (ContainerSpecialItems.Items[3] != null)
                {
                    if ((ContainerSpecialItems.Items[3] as DungeonItemSword).TotalFrame < hit_timer_frames_all)
                    {
                        (ContainerSpecialItems.Items[3] as DungeonItemSword).TotalFrame++;
                    }
                }
            }
            else 
            {
                if (ContainerSpecialItems.Items[3] != null)
                {
                    (ContainerSpecialItems.Items[3] as DungeonItemSword).TotalFrame = 0;
                }
                m_hit_total_frames = 0;
                m_timer_hit.Stop();
                m_timer_hit_is_working = false;
            }
        }

       
      
        private bool HitByAnother(DungeonCreature another)
        {
            if (ObjectStatus == DungeonObjectStatus.AddedNotDestroyed)
            {
                m_total_health -= another.GetStatValueWithEffectValue(DungeonStats.Power);
                if (m_total_health <= 0)
                {
                    GoDeath(another);
                    return false;
                }
            }
            return true;
        }

   
     
        private void GoDeath(DungeonCreature killer = null)
        {
            if (killer != null)
            {
                killer.PlusExp(Level); 
                Container.DropAllItems(Location.Point, 50 + killer.m_luck); 
                ContainerSpecialItems.DropAllItems(Location.Point, 50 + killer.m_luck); 

                if (killer is DungeonHero)
                {
                    if (this is DungeonMonster)
                    {
                        if ((this as DungeonMonster).IsBoss)
                        {
                            (killer as DungeonHero).MonstersBossesKilled++;
                        }
                        else
                        {
                            (killer as DungeonHero).MonstersKilled++;
                        }
                    }
                }
            }
            else
            {
                Container.DropAllItems(Location.Point); 
                ContainerSpecialItems.DropAllItems(Location.Point); 
            }
            if (this is DungeonMonster)
            {
                DungeonLevel.TryToSpawnMonsters();
                (this as DungeonMonster).StopThinking();
                if ((this as DungeonMonster).IsBoss)
                {
                    DungeonLevel.IsBossAlive = false;
                }

                Destroy();
            }
            if (this is DungeonHero)
            {
                DungeonLevel.Form.LooseGame();
            }
        }
       
        public void ResetMoveDirection()
        {
            m_move_direction = DungeonCreatureMoveDirection.Down;
        }
    }
}
