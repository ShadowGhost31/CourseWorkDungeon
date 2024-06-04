using System;
using System.IO;
using System.Windows.Media;

namespace Dungeon
{

    public class AudioEffect
    {

        private static int audio_effects_number = 0;


        private MediaPlayer m_media_player;

  
        private string m_temp_file_path;


        private bool m_is_repeat;


        private bool m_is_ended;


        public bool IsEnded
        {
            get
            {
                return m_is_ended;
            }
        }


        private bool m_is_paused;


        public bool IsPaused
        {
            get
            {
                return m_is_paused;
            }
        }





        public AudioEffect(UnmanagedMemoryStream resource, bool is_music, bool is_repeat = false)
        {
            m_is_repeat = is_repeat;
            m_media_player = new MediaPlayer();
            m_media_player.MediaOpened += media_player_MediaOpened;
            m_media_player.MediaEnded += media_player_MediaEnded;
            ExtractAudioResource(resource);
            m_media_player.Open(new Uri(m_temp_file_path));
            if (is_music)
            {
                SetVolume(MainForm.VolumeMusic);
            }
            else
            {
                SetVolume(MainForm.VolumeSound);
            }
            m_is_ended = false;
            m_is_paused = false;
            audio_effects_number++;
        }



        public AudioEffect(AudioEffect audio_effect)
        {
            m_is_repeat = audio_effect.m_is_repeat;
            m_media_player = audio_effect.m_media_player;
            m_media_player.Position = TimeSpan.FromSeconds(0);
            SetVolume(MainForm.VolumeSound);
            m_is_ended = false;
            m_is_paused = false;
            audio_effects_number++;
        }


        private void media_player_MediaOpened(object sender, EventArgs e)
        {
            File.Delete(m_temp_file_path); 
        }


        private void media_player_MediaEnded(object sender, EventArgs e)
        {
            if (m_is_repeat)
            {
                Play();
            }
            else
            {
                Stop();
            }
        }



        private void ExtractAudioResource(UnmanagedMemoryStream resource)
        {
            Random random = new Random();
            m_temp_file_path = Path.GetTempPath() + "DungeonTemp_" + audio_effects_number+ "_" + random.Next(0, 1000000) + ".mp3";
            Stream ReadResource = resource;
            byte[] buffer = new byte[ReadResource.Length];
            using (ReadResource)
            {
                ReadResource.Read(buffer, 0, (int)ReadResource.Length);
            }
            File.WriteAllBytes(m_temp_file_path, buffer);
        }



        public void SetVolume(byte volume)
        {
            m_media_player.Volume = (double)volume / 100;
        }

        public void Play()
        {
            m_is_paused = false;
            m_media_player.Play();
        }


        public void Stop()
        {
            m_media_player.Stop();
            m_is_ended = true;
        }


        public void Pause()
        {
            m_is_paused = true;
            m_media_player.Pause();
        }
    }
}
