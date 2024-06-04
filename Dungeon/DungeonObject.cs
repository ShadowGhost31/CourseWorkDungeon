using System;
using System.Drawing;
using System.Windows.Forms;

namespace Dungeon
{
      
    [Serializable]
    public class DungeonObject : DungeonOwner
    {
          
        protected Bitmap m_image;

          
        public Bitmap Image
        {
            get
            {
                return m_image;
            }
            set
            {
                m_image = value;
                m_size = m_image.Size;
                m_collision_size = m_size;
                m_collision_offset = new Point(0, 0);
            }
        }

          
        protected Size m_size;

          
        public Size Size
        {
            get
            {
                return m_size;
            }
        }

          
        protected DungeonObjectCollision m_collision_type;

          
        public DungeonObjectCollision CollisionType
        {
            get
            {
                return m_collision_type;
            }
        }

          
        protected Size m_collision_size;

          
        public Size CollisionSize
        {
            get
            {
                return m_collision_size;
            }
        }

          
        protected Point m_collision_offset;

          
        public Point CollisionOffset
        {
            get
            {
                return m_collision_offset;
            }
        }

          
        public DungeonLevel DungeonLevel;

          
        private DungeonPoint m_location;

          
        public DungeonPoint Location
        {
            get
            {
                return m_location;
            }
            set
            {
                m_location = value;
                if (this is DungeonHero)
                {
                    if (DungeonLevel != null)
                    {
                        DungeonLevel.FindCells(this as DungeonHero, MainForm.ShowingSize);
                    }
                }
            }
        }

          
        public DungeonObjectStatus ObjectStatus;

          
          
          
        protected DungeonObject(Bitmap image, DungeonObjectCollision collision_type)
            : base()
        {
            m_image = image;
            m_size = image.Size;
            m_collision_type = collision_type;
            m_collision_size = m_size;
            m_collision_offset = new Point(0, 0);

            DungeonLevel = null;
            m_location = new DungeonPoint(0, 0);
            ObjectStatus = DungeonObjectStatus.CreatedNotAdded;
        }

          
          
          
        public void SetLocation(double x, double y = 0)
        {
            Location = new DungeonPoint(x, y);
        }

          
        public void Destroy()
        {
            if (ObjectStatus == DungeonObjectStatus.AddedNotDestroyed)
            {
                ObjectStatus = DungeonObjectStatus.Destroyed;
            }
            m_collision_type = DungeonObjectCollision.NoCollision;
            if (this is DungeonItem)
            {
                if ((this as DungeonItem).Container != null)
                {
                    (this as DungeonItem).Container.Remove(this as DungeonItem);
                }
            }
            if (DungeonLevel != null)
            {
                DungeonLevel.Remove(this);
            }
        }

          
          
        public bool Paint(object sender, PaintEventArgs e, Point player_location, Size showing_size)
        {
            if (ObjectStatus == DungeonObjectStatus.AddedNotDestroyed)
            {
                Size form_size = ((MainForm)sender).Size;
                Point center_screen = new Point(((MainForm)sender).ClientSize.Width / 2, ((MainForm)sender).ClientSize.Height / 2);
                Point screen_location = new DungeonPoint(Location.X - player_location.X + form_size.Width / 2 - Size.Width / 2, Location.Y - player_location.Y + form_size.Height / 2 - Size.Height / 2).Point;
                if (IsInView(screen_location, center_screen, showing_size))
                {
                    e.Graphics.DrawImage(m_image, screen_location);
                    return true;
                }
            }
            return false;
        }

          
                 
          
          
        protected bool IsInView(Point screen_location, Point center_screen, Size showing_size)
        {
            if ((screen_location.X - Size.Width >= center_screen.X - showing_size.Width / 2) &&
                (screen_location.X + Size.Width <= center_screen.X + showing_size.Width / 2) &&
                (screen_location.Y - Size.Height >= center_screen.Y - showing_size.Height / 2) &&
                (screen_location.Y + Size.Height <= center_screen.Y + showing_size.Height / 2))
            {
                return true;
            }
            return false;
        }
    }
}