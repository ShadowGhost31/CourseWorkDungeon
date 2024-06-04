using System;
using System.Drawing;

namespace Dungeon
{
   
    [Serializable]
    public class DungeonItemArtifact : DungeonItemEquipment
    {
      
        private bool m_is_special;

       
        public bool IsSpecial
        {
            get
            {
                return m_is_special;
            }
        }

     
      
    
      
     
        public DungeonItemArtifact(int quality_id = 0, int style_id = 0, string name = "Артефакт", string description = "Згусток магічної енергії, що матеріалізувався в незвичайній формі. Зустрічається вкрай рідко. Може змінювати будь-які характеристики.")
              : base(Properties.Resources.error, false, true, true, name, description)
        {
            m_is_special = false;
            SetImagesFromTexture(quality_id, style_id);
        }

   
 
  
        private void SetImagesFromTexture(int quality_id = 0, int style_id = 0)
        {
            const int artifact_width = 16;
            const int artifact_height = 16;

            if (quality_id < 0) quality_id = 0;
            else if (quality_id >= 31) quality_id = 30;

            if (style_id < 0) style_id = 0;
            else if (style_id >= 6) style_id = 5;

            if (quality_id == 30) 
            {
                style_id = 0;
                m_can_destroy = false;
                m_is_special = true;
            }

            Bitmap texture = Properties.Resources.TEXTURE_item_artifacts;

            Bitmap new_image = new Bitmap(artifact_width, artifact_height);

            for (int i = artifact_height * quality_id; i < artifact_height * quality_id + artifact_height; i++)
            {
                for (int i2 = artifact_width * style_id; i2 < artifact_width * style_id + artifact_width; i2++)
                {
                    Color pixel = texture.GetPixel(i2, i);
                    new_image.SetPixel(i2 - artifact_width * style_id, i - artifact_height * quality_id, pixel);
                }
            }

            Image = new_image;
        }
    }
}
