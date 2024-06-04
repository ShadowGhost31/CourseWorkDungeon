using System;
using System.Drawing;

namespace Dungeon
{
      
    [Serializable]
    public class DungeonItemSword : DungeonItemEquipment
    {
          
        private int m_quality_id;

          
        public int TotalFrame;

          
          
          
          
        public DungeonItemSword(int quality_id = 0, string name = "Меч", string description = "Холодна зброя. Зустрічається дуже часто. Збільшує силу, може зменшує спритність.")
             : base(Properties.Resources.error, false, true, true, name, description)
        {
            if (quality_id < 0) quality_id = 0;
            else if (quality_id > 29) quality_id = 29;

            m_quality_id = quality_id;

            Image = MainForm.ImagesSwords[quality_id];

            TotalFrame = 0;
        }

          
          
          
        public Bitmap GetImageOnDirection(DungeonCreatureMoveDirection direction)
        {
            return MainForm.ImagesSwordsGif[m_quality_id, (int)direction].GetFrame(TotalFrame);
        }
    }
}
