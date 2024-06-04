using System;
using System.Drawing;

namespace Dungeon
{

    [Serializable]
    public class DungeonItem : DungeonObject
    {
  
        private bool m_can_use;

  
        public bool CanUse
        {
            get
            {
                return m_can_use;
            }
        }
      
        private bool m_can_drop;
       
        public bool CanDrop
        {
            get
            {
                return m_can_drop;
            }
        }
     
        protected bool m_can_destroy;
 
        public bool CanDestroy
        {
            get
            {
                return m_can_destroy;
            }
        }

        private string m_name;

     
        public string Name
        {
            get
            {
                return m_name;
            }
        }

        protected string m_description;

    
        protected string m_full_description;

      
        public string FullDescription
        {
            get
            {
                return m_full_description;
            }
        }

     
        public DungeonContainer Container;

        protected DungeonItem(Bitmap image, bool can_use, bool can_drop, bool can_destroy, string name, string description)
            : base(image, DungeonObjectCollision.NoCollision)
        {
            m_can_use = can_use;
            m_can_drop = can_drop;
            m_can_destroy = can_destroy;
            m_name = name;
            m_description = description;
            m_full_description = description;

            DungeonLevel = null;
        }
    }
}