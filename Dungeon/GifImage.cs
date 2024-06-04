using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Dungeon
{
      
    [Serializable]
    public class GifImage
    {
          
        private Bitmap m_image;

          
        private FrameDimension m_dimension;

          
        private int m_frames_count;

          
        public int FramesCount
        {
            get
            {
                return m_frames_count;
            }
        }

          
        private int m_current_frame;

          
        public int CurrentFrame
        {
            get
            {
                return m_current_frame;
            }
        }

          
          
        public GifImage(Bitmap image)
        {
            m_image = image;
            m_dimension = new FrameDimension(m_image.FrameDimensionsList[0]);   
            m_frames_count = m_image.GetFrameCount(m_dimension);
            m_current_frame = -1;
        }

          
          
        public Bitmap GetNextFrame()
        {
            m_current_frame++;

            if (m_current_frame >= m_frames_count)
            {
                m_current_frame = 0;
            }
            return GetFrame(m_current_frame);
        }

          
          
          
        public Bitmap GetFrame(int frame_id)
        {
            m_image.SelectActiveFrame(m_dimension, frame_id);   
            return m_image;   
        }
    }
}
