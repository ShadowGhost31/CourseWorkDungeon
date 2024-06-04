using System;
using System.Drawing;

namespace Dungeon
{
   
    [Serializable]
    public class DungeonItemKey : DungeonItem
    {
     
        private static int colors_count = -1;


        private Color m_color;


        public Color Color
        {
            get
            {
                return m_color;
            }
        }

     
     
   
     
   
        public DungeonItemKey(int level_id, Color color, string name = "Ключ", string description = "Відчиняє двері того ж кольору, що й сам ключ. Раніше зберігався у монстра.")
             : base(Properties.Resources.item_key_75px, false, true, false, name, description)
        {
            m_color = color;
            SetImages();
            m_description = "Відчиняє одну з дверей" + (level_id + 1) + "-го поверху, замок якої того ж кольору, що і сам ключ. Раніше зберігався у монстра.";
            UpdateFullDescription();
        }

 
        private void SetImages()
        {
            Bitmap image = new Bitmap(Properties.Resources.item_key_75px);
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color pixel_color = image.GetPixel(x, y);
                    if (pixel_color == Color.FromArgb(0, 255, 0))
                    {
                        image.SetPixel(x, y, m_color);
                    }
                }
            }
            Image = image;
        }

 
        private void UpdateFullDescription()
        {
            m_full_description = m_description;
        }

  
        public static void ResetColorsCount()
        {
            colors_count = -1;
        }

   
      

        public static Color GetNextColor(ref Random world_key)
        {
            colors_count++;
            if (colors_count == 0) return Color.Green;
            else if (colors_count == 1) return Color.Blue;
            else if (colors_count == 2) return Color.White;
            else if (colors_count == 3) return Color.Aqua;
            else if (colors_count == 4) return Color.Purple;
            else if (colors_count == 5) return Color.Maroon;
            else if (colors_count == 6) return Color.Lime;
            else if (colors_count == 7) return Color.Teal;
            else if (colors_count == 8) return Color.Olive;
            else if (colors_count == 9) return Color.Gray;
            else return Color.FromArgb(world_key.Next(0, 256), world_key.Next(0, 256), world_key.Next(0, 256));
        }
    }
}