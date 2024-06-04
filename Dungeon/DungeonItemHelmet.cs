using System;
using System.Drawing;

namespace Dungeon
{
   
    [Serializable]
    public class DungeonItemHelmet : DungeonItemEquipment
    {
    
        private int m_quality_id;


        private int m_style_id;


    
   
    
  
        public DungeonItemHelmet(int quality_id = 0, int style_id = 0, string name = "Шолом", string description = "Елемент захисту. Зустрічається часто. Збільшує максимальний запас енергії, знижує швидкість пересування, може збільшувати або зменшувати відновлення енергії.")
             : base(Properties.Resources.error, false, true, true, name, description)
        {
            if (quality_id < 0) quality_id = 0;
            else if (quality_id > 9) quality_id = 9;

            if (style_id < 0) style_id = 0;
            else if (style_id > 2) style_id = 2;

            m_quality_id = quality_id;
            m_style_id = style_id;

            Image = MainForm.ImagesHelmets[quality_id, style_id, 4];
        }




        public Bitmap GetImageOnDirection(DungeonCreatureMoveDirection direction)
        {
            return MainForm.ImagesHelmets[m_quality_id, m_style_id, (int)direction];
        }
    }
}
