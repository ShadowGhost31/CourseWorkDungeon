using System;
using System.Collections.Generic;
using System.Drawing;

namespace Dungeon
{
  
    [Serializable]
    public class DungeonItemEquipment : DungeonItem
    {
        
        private List<DungeonEffect> m_effects;

       
        public List<DungeonEffect> Effects
        {
            get
            {
                return m_effects;
            }
        }

      
        private int TotalEffectsNumber
        {
            get
            {
                return m_effects.Count;
            }
        }

    
        private bool m_is_equip;

   
        public bool IsEquip
        {
            get
            {
                return m_is_equip;
            }
        }

     
    
       
     
    
   
   
        protected DungeonItemEquipment(Bitmap image, bool can_use, bool can_drop, bool can_destroy, string name, string description)
            : base(image, can_use, can_drop, can_destroy, name, description)
        {
            m_effects = new List<DungeonEffect>();
            m_is_equip = false;
        }


      
   
        private void SetShowingEffectColor(int id, Color color)
        {
            if (id >= 0 && id <= 2)
            {
                Bitmap image = Image;
                for (int i = 0; i < image.Height; i++)
                {
                    for (int i2 = 0; i2 < image.Height; i2++)
                    {
                        Color pixel = image.GetPixel(i2, i);
                        bool check = false;
                        if (id == 0)
                        {
                            if (pixel.R == 255 && pixel.G == 0 && pixel.B == 0) check = true;
                        }
                        else if (id == 1)
                        {
                            if (pixel.R == 0 && pixel.G == 255 && pixel.B == 0) check = true;
                        }
                        else if (id == 2)
                        {
                            if (pixel.R == 0 && pixel.G == 0 && pixel.B == 255) check = true;
                        }
                        if (check)
                        {
                            image.SetPixel(i2, i, color);
                        }
                    }
                }
                Image = image;
            }
        }

  
        private void UpdateFullDescription()
        {
            int effects_number = 0;
            for (int i = 0; i < 9; i++)
            {
                double result_effect_value = GetEffectValue(i);
                if (result_effect_value > 0.005 || result_effect_value < -0.005)
                {
                    effects_number++;
                }
            }
            if (effects_number > 0)
            {
                if (this is DungeonItemPotion)
                {
                    if (effects_number == 1)
                    {
                        m_description = "Звичайне зілля. Зустрічається дуже часто. Збільшує або зменшує характеристику на деякий час.";
                    }
                    else if (effects_number == 2)
                    {
                        m_description = "Зілля подвійної дії. Зустрічається часто. Збільшує або зменшує дві характеристики на деякий час.";
                    }
                    else if (effects_number == 3)
                    {
                        m_description = "Зілля потрійної дії. Зустрічається рідко. Збільшує або зменшує три характеристики на деякий час.";
                    }
                }

                m_full_description = m_description + "\n\nЗмінює характеристики персонажа:";
                for (int i = 0; i < 9; i++)
                {
                    double result_effect_value = GetEffectValue(i);
                    if (result_effect_value > 0.005)
                    {
                        m_full_description += "\n+ " + DungeonStatsInfo.Name((DungeonStats)i) + ": +" + result_effect_value;
                    }
                    else if (result_effect_value < -0.005)
                    {
                        m_full_description += "\n- " + DungeonStatsInfo.Name((DungeonStats)i) + ": " + result_effect_value;
                    }
                }

                if (this is DungeonItemPotion)
                {
                    m_full_description += "\n\nТривалість: " + m_effects[0].Duration + " секунд";
                }
            }
            else
            {
                m_full_description = m_description;
            }
        }

 
    
 
  
        public bool AddEffect(DungeonStats stat, double value, int duration = -1)
        {
            for (int i = 0; i < m_effects.Count; i++)
            {
                if (m_effects[i].Stat == stat) 
                {
                    return false;
                }
            }
            if (this is DungeonItemArtifact || this is DungeonItemPotion)
            {
                if (this is DungeonItemPotion && duration == -1) return false;
                if (TotalEffectsNumber < 3 || (this is DungeonItemArtifact && (this as DungeonItemArtifact).IsSpecial))
                {
                    m_effects.Add(new DungeonEffect(stat, value, duration));
                    UpdateFullDescription();
                    if (!(this is DungeonItemArtifact && (this as DungeonItemArtifact).IsSpecial))
                    {
                        SetShowingEffectColor(TotalEffectsNumber - 1, DungeonStatsInfo.GetColor(stat));
                    }
                    return true;
                }
            }
            else
            {
                m_effects.Add(new DungeonEffect(stat, value, duration));
                UpdateFullDescription();
                return true;
            }
            return false;
        }

   

  
        public double GetEffectValue(int id_stat)
        {
            for (int i = 0; i < m_effects.Count; i++)
            {
                if ((int)m_effects[i].Stat == id_stat)
                {
                    return m_effects[i].Value;
                }
            }
            return 0;
        }

   
  
        public void StartEffects(DungeonCreature creature)
        {
            for (int i = 0; i < m_effects.Count; i++)
            {
                m_effects[i].AddToCreature(creature);
                m_effects[i].TurnEffectOn();
            }
            if (this is DungeonItemPotion)
            {
                Destroy();
            }
        }

  
        private void StopEffects()
        {
            for (int i = 0; i < m_effects.Count; i++)
            {
                m_effects[i].TurnEffectOff();
            }
        }


  
        public void Equip(DungeonCreature creature)
        {
            if (!(this is DungeonItemPotion)) StartEffects(creature);
            m_is_equip = true;
        }

 
        public void UnEquip()
        {
            if (!(this is DungeonItemPotion)) StopEffects();
            m_is_equip = false;
        }
    }
}
