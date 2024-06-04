using System;
using System.Collections.Generic;
using System.Drawing;

namespace Dungeon
{
    
    [Serializable]
    public class DungeonDoor : DungeonObject
    {
      
        private Bitmap m_image_opened;

  
        private Bitmap m_image_closed;

        
        private string m_code;

      
        public string Code
        {
            get
            {
                return m_code;
            }
        }

    
        public bool IsCode
        {
            get
            {
                if (m_code == null) return false;
                else return true;
            }
        }

      
        private List<DungeonItemPaper> m_papers;

      
        public DungeonItemKey Key;

   
        private bool m_is_opened;

       
      
        private DungeonDoor(DungeonDoorImageType door_image_type)
            : base(Properties.Resources.error, DungeonObjectCollision.StaticCollision)
        {
            m_is_opened = false;
            m_collision_type = DungeonObjectCollision.StaticCollision;
        }

   
  

 
        public DungeonDoor(DungeonDoorImageType door_image_type, string code, List<DungeonItemPaper> papers)
            : this(door_image_type)
        {
            m_code = code;
            Key = null;
            m_papers = new List<DungeonItemPaper>();
            for (int i = 0; i < 4; i++)
            {
                m_papers.Add(papers[i]);
            }
            SetImages(door_image_type, Color.Yellow);
        }

  
 
 
        public DungeonDoor(DungeonDoorImageType door_image_type, DungeonItemKey key)
            : this(door_image_type)
        {
            m_code = null;
            Key = key;
            SetImages(door_image_type, Key.Color);
        }

    

  
        private void SetImages(DungeonDoorImageType door_image_type, Color color)
        {
    
            Bitmap image_closed;
            if (door_image_type == DungeonDoorImageType.Horizontal)
            {
                if (IsCode) image_closed = new Bitmap(Properties.Resources.door_code_h_closed_100px);
                else image_closed = new Bitmap(Properties.Resources.door_h_closed_100px);
            }
            else
            {
                if (IsCode) image_closed = new Bitmap(Properties.Resources.door_code_v_closed_100px);
                else image_closed = new Bitmap(Properties.Resources.door_v_closed_100px);
            }
            for (int y = 0; y < image_closed.Height; y++)
            {
                for (int x = 0; x < image_closed.Width; x++)
                {
                    Color pixel_color = image_closed.GetPixel(x, y);
                    if (pixel_color.R == 0 && pixel_color.G == 255 && pixel_color.B == 0)
                    {
                        image_closed.SetPixel(x, y, color);
                    }
                }
            }
            m_image_closed = image_closed;
            Image = m_image_closed;

     
            Color m_color_alpha = Color.FromArgb(128, color);
            Bitmap image_opened;
            if (door_image_type == DungeonDoorImageType.Horizontal)
            {
                if (IsCode) image_opened = new Bitmap(Properties.Resources.door_code_h_opened_100px);
                else image_opened = new Bitmap(Properties.Resources.door_h_opened_100px);
            }
            else
            {
                if (IsCode) image_opened = new Bitmap(Properties.Resources.door_code_v_opened_100px);
                else image_opened = new Bitmap(Properties.Resources.door_v_opened_100px);
            }
            for (int y = 0; y < image_opened.Height; y++)
            {
                for (int x = 0; x < image_opened.Width; x++)
                {
                    Color pixel_color = image_opened.GetPixel(x, y);
                    if (pixel_color.R == 0 && pixel_color.G == 255 && pixel_color.B == 0)
                    {
                        image_opened.SetPixel(x, y, m_color_alpha);
                    }
                }
            }
            m_image_opened = image_opened;
        }
    
        public void Open()
        {
            if (!m_is_opened)
            {
                m_is_opened = true;
                m_collision_type = DungeonObjectCollision.NoCollision;
                Image = m_image_opened;
                if (IsCode)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        m_papers[i].Destroy();
                    }
                    m_papers = null;
                }
                else
                {
                    Key.Destroy();
                    Key = null;
                }
            }
        }
    }
}
