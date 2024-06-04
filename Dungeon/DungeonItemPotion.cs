﻿using System;
using System.Drawing;

namespace Dungeon
{
  
    [Serializable]
    public class DungeonItemPotion : DungeonItemEquipment
    {

   
      
   
   
        public DungeonItemPotion(int quality_id = 0, int style_id = 0, string name = "Зілля", string description = "Звичайне зілля. Збільшує характеристики на деякий час.")
            : base(Properties.Resources.error, true, true, true, name, description)
        {
            SetImagesFromTexture(quality_id, style_id);
        }



        private void SetImagesFromTexture(int quality_id = 0, int style_id = 0)
        {
            const int potion_width = 20;
            const int potion_height = 20;

            if (quality_id < 0) quality_id = 0;
            else if (quality_id >= 3) quality_id = 2;

            if (style_id < 0) style_id = 0;
            else if (style_id >= 3) style_id = 2;

            Bitmap texture = Properties.Resources.TEXTURE_item_potions;

            Bitmap new_image = new Bitmap(potion_width, potion_height);

            for (int i = potion_height * style_id; i < potion_height * style_id + potion_height; i++)
            {
                for (int i2 = potion_width * quality_id; i2 < potion_width * quality_id + potion_width; i2++)
                {
                    Color pixel_color = texture.GetPixel(i2, i);
                    new_image.SetPixel(i2 - potion_width * quality_id, i - potion_height * style_id, pixel_color);
                }
            }

            Image = new_image;
        }
    }
}