using System;
using System.Windows.Forms;

namespace Dungeon
{

    [Serializable]
    public class DungeonEffect
    {
 
        private DungeonStats m_stat;

     
        public DungeonStats Stat
        {
            get
            {
                return m_stat;
            }
        }

      
        private double m_value;


        public double Value
        {
            get
            {
                return m_value;
            }
        }


        private int m_duration;


        public int Duration
        {
            get
            {
                return m_duration;
            }
        }


        private DungeonCreature m_creature;


        private bool m_is_active;


        private bool m_timer_usage_is_working;


        [NonSerialized]
        private Timer m_timer_usage;


        private int m_usage_time;


        public int UsageTime
        {
            get
            {
                return m_usage_time;
            }
        }

 



        public DungeonEffect(DungeonStats stat, double value = 0, int duration = -1)
        {
            m_stat = stat;
            m_value = value;
            m_duration = duration;

            m_creature = null;

            m_is_active = false;
            m_usage_time = m_duration * 1000;

            m_timer_usage_is_working = false;
            InitializeTimers();
        }


        public void InitializeTimers()
        {
            m_timer_usage = new Timer();
            m_timer_usage.Tick += timer_usage_Tick;
            m_timer_usage.Interval = 100;
            if (m_timer_usage_is_working)
            {
                m_timer_usage.Start();
            }
        }



        public void AddToCreature(DungeonCreature creature)
        {
            m_creature = creature;
        }


        public void TurnEffectOn()
        {
            if (!m_is_active)
            {
                if (m_creature != null)
                {
                    m_creature.AddEffect(this);
                    if (m_duration != -1)
                    {
                        m_usage_time = m_duration * 1000; // ms
                        m_timer_usage.Start();
                        m_timer_usage_is_working = true;
                    }
                    m_is_active = true;

                    m_creature.DungeonLevel.Form.CalculateInterfaceHeroActivePotions();
                }
                else throw new Exception("Власник ефекту не заданий");
            }
        }


        public void TurnEffectOff()
        {
            if (m_is_active)
            {
                if (m_creature != null)
                {
                    m_creature.RemoveEffect(this);
                    if (m_duration != -1)
                    {
                        m_timer_usage.Stop();
                        m_timer_usage_is_working = false;
                    }
                    m_is_active = false;

                    m_creature.DungeonLevel.Form.CalculateInterfaceHeroActivePotions();

                    m_creature = null;
                }
                else throw new Exception("Власник ефекту не заданий");
            }
        }

        private void timer_usage_Tick(object sender, EventArgs e)
        {
            if (m_creature != null && m_creature.DungeonLevel != null)
            {
                if (!m_creature.DungeonLevel.Form.IsGamePaused) 
                {
                    m_usage_time -= m_timer_usage.Interval;
                    m_creature.DungeonLevel.Form.CalculateInterfaceHeroActivePotions();
                    if (m_usage_time <= 0)
                    {
                        TurnEffectOff();
                    }
                }
            }
        }
    }
}
