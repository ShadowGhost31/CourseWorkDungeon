using System;
using System.Drawing;
using System.Windows.Forms;

namespace Dungeon
{
      
    [Serializable]
    public class DungeonMonster : DungeonCreature
    {
          
        private static readonly int thinking_time = 1500;

          
        private bool m_is_boss;

          
        public bool IsBoss
        {
            get
            {
                return m_is_boss;
            }
        }

          
        private DungeoMonsterActionStatus m_action_status;

          
        public DungeoMonsterActionStatus ActionStatus
        {
            get
            {
                return m_action_status;
            }
        }

          
        private bool m_is_attached_to_room;

          
        public bool IsAttachedToRoom
        {
            get
            {
                return m_is_attached_to_room;
            }
        }

          
        private Point m_connection_point_attached;

          
        public Point ConnectionPointAttached
        {
            get
            {
                return m_connection_point_attached;
            }
        }

          
        private Point m_target_location;

          
        private bool m_thinking_timer_is_working;

          
        [NonSerialized]
        private Timer m_thinking_timer;

          
        private bool m_moving_timer_is_working;

          
        [NonSerialized]
        private Timer m_moving_timer;

          
          
          
          
          
          
          
          
          
          
          
        public DungeonMonster(Bitmap texture, string name = "Монстр", bool is_boss = false, double max_health = 50, double regeneration = 0, double max_energy = 25, double restore = 0.5, double power = 5, double mobility = 0, double speed = 1)
            : base(texture, name, max_health, regeneration, max_energy, restore, power, mobility, speed, 0, 0)
        {
            m_is_boss = is_boss;
            m_is_attached_to_room = false;

            m_action_status = DungeoMonsterActionStatus.AFK;

            m_thinking_timer_is_working = false;
            m_moving_timer_is_working = false;
            InitializeTimers();
        }

          
        new public void InitializeTimers()
        {
            m_thinking_timer = new Timer();
            m_thinking_timer.Interval = thinking_time;
            m_thinking_timer.Tick += m_thinking_timer_Tick;
            if (m_thinking_timer_is_working)
            {
                m_thinking_timer.Start();
                m_thinking_timer_is_working = true;
            }

            m_moving_timer = new Timer();
            Random random = new Random();
            m_moving_timer.Interval = 100 * random.Next(10, 51);
            m_moving_timer.Tick += m_moving_timer_Tick;
            if (m_moving_timer_is_working)
            {
                m_moving_timer.Start();
                m_moving_timer_is_working = true;
            }
        }

          
          
        public void DistributeAllSkillPoints(ref Random world_key)
        {
            while (SkillPoints > 0)
            {
                DungeonStats stat = (DungeonStats)(world_key.Next(0, 10));
                while (stat == DungeonStats.Intelligence || stat == DungeonStats.Luck)   
                {
                    stat = (DungeonStats)(world_key.Next(0, 10));
                }
                UpStatValue(stat);
            }
        }

          
          
        public void GiveKey(DungeonItemKey key)
        {
            Container.Add(key);
            if (!m_is_boss)
            {
                m_name = "Зберігач ключа";
            }
        }

          
          
        public void AttachToConnectionPoint(Point connection_point_location)
        {
            m_connection_point_attached = connection_point_location;
            m_is_attached_to_room = true;
        }

          
          
        public void SetTargetLocation(Point target_location)
        {
            m_target_location = target_location;
        }

          
        public void MakeStepToTargetLocation()
        {
            if (DungeonLevel.GetCellLocationFromGlobalLocation(Location.Point) == DungeonLevel.GetCellLocationFromGlobalLocation(m_target_location) &&
                m_action_status != DungeoMonsterActionStatus.Fighting)
            {
                StopThinking();
                StartThinking();
            }

            DungeonCreatureMoveDirection move_direction = DungeonCreatureMoveDirection.Right;
            double length_y = Math.Abs(Location.Y - m_target_location.Y);
            double length_x = Math.Abs(Location.X - m_target_location.X);
            bool is_move = false;
            if (length_y > length_x)
            {
                if (Location.Y > m_target_location.Y)
                {
                    move_direction = DungeonCreatureMoveDirection.Up;
                    is_move = true;
                }
                else if (Location.Y < m_target_location.Y)
                {
                    move_direction = DungeonCreatureMoveDirection.Down;
                    is_move = true;
                }
                else if (Location.X > m_target_location.X)
                {
                    move_direction = DungeonCreatureMoveDirection.Left;
                    is_move = true;
                }
                else if (Location.X < m_target_location.X)
                {
                    move_direction = DungeonCreatureMoveDirection.Right;
                    is_move = true;
                }
            }
            else
            {
                if (Location.X > m_target_location.X)
                {
                    move_direction = DungeonCreatureMoveDirection.Left;
                    is_move = true;
                }
                else if (Location.X < m_target_location.X)
                {
                    move_direction = DungeonCreatureMoveDirection.Right;
                    is_move = true;
                }
                else if (Location.Y > m_target_location.Y)
                {
                    move_direction = DungeonCreatureMoveDirection.Up;
                    is_move = true;
                }
                else if (Location.Y < m_target_location.Y)
                {
                    move_direction = DungeonCreatureMoveDirection.Down;
                    is_move = true;
                }
            }
            if (is_move)
            {
                if (Math.Abs(length_y - length_x) < GetStatValueWithEffectValue(DungeonStats.Speed) * 2)   
                {
                    Move(move_direction, true);
                }
                else
                {
                    Move(move_direction);
                }
            }
        }

          
        public void StartThinking()
        {
            if (m_action_status != DungeoMonsterActionStatus.Thinking)
            {
                m_action_status = DungeoMonsterActionStatus.Thinking;
                m_thinking_timer.Start();
                m_thinking_timer_is_working = true;
            }
        }

          
        public void StopThinking()
        {
            if (m_action_status != DungeoMonsterActionStatus.AFK)
            {
                m_action_status = DungeoMonsterActionStatus.AFK;
                m_thinking_timer.Stop();
                m_thinking_timer_is_working = false;
                m_moving_timer.Stop();
                m_moving_timer_is_working = false;
            }
        }

          
        public void StartFighting()
        {
            m_action_status = DungeoMonsterActionStatus.Fighting;
        }

          
        private void m_thinking_timer_Tick(object sender, EventArgs e)
        {
            if (DungeonLevel != DungeonLevel.Form.Hero.DungeonLevel)
            {
                return;
            }

            Point total_connection_point_where_is_monster;
            if (m_is_attached_to_room)
            {
                total_connection_point_where_is_monster = m_connection_point_attached;
            }
            else
            {
                total_connection_point_where_is_monster = DungeonLevel.GetConnectionPointLocationFromGlobalLocation(Location.Point);
            }

            m_target_location = DungeonLevel.GetGlobalLocationRandomCellInConnectionPointNotWall(total_connection_point_where_is_monster);

            m_action_status = DungeoMonsterActionStatus.MovingToCell;

            m_thinking_timer.Stop();
            m_thinking_timer_is_working = false;

            Random random = new Random();
            m_moving_timer.Interval = 100 * random.Next(10, 51);
            m_moving_timer.Start();
            m_moving_timer_is_working = true;
        }

          
        private void m_moving_timer_Tick(object sender, EventArgs e)
        {
            m_moving_timer.Stop();
            m_moving_timer_is_working = false;
            StartThinking();
        }

          
                   
          
        public bool IsInPlayerView(DungeonHero hero, Size showing_size)
        {
            if (DungeonLevel == hero.DungeonLevel || m_action_status == DungeoMonsterActionStatus.Fighting)
            {
                Point calculating_location = new Point(hero.Location.Point.X - showing_size.Width / 2, hero.Location.Point.Y - showing_size.Height / 2);
                Rectangle calculating_size_rectangle = new Rectangle(calculating_location, showing_size);
                if (calculating_size_rectangle.Contains(Location.Point))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
