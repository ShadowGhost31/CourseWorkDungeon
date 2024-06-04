using System;

namespace Dungeon
{
 
    [Serializable]
    public class DungeonItemPaper : DungeonItem
    {
 
        private string m_text;

   
 
 
    
 
        public DungeonItemPaper(int level_id, string text = "", string name = "Записка", string description = "")
            : base(Properties.Resources.paper, false, true, false, name, description)
        {
            m_text = text;
            m_description = "Шматок старого паперу, знайдений в одній із скриньок" + (level_id + 1) + "-го поверху.\n\nУ записці вказано: ";
            UpdateFullDescription();
        }


        private void UpdateFullDescription()
        {
            m_full_description = m_description + m_text;
        }

      
    
     
    
        public void GenerateTextWithNumber(ref Random world_key, byte number_char, byte number_position = 0)
        {
            m_text = "";
            for (int i = 0; i < number_position; i++)
            {
                m_text += "*";
            }
            m_text += number_char.ToString();
            for (int i = number_position + 1; i < 4; i++)
            {
                m_text += "*";
            }
            m_text += ".";
            UpdateFullDescription();
        }
    }
}
