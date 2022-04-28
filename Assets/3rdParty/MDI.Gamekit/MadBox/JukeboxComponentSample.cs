using UnityEngine;
namespace MDI.Gamekit.MadBox
{
    public class JukeboxComponentSample : MonoBehaviour
    {
        [SerializeField] AudioSource channel;
        [SerializeField] int currentTrack;
        [SerializeField] AudioClip[] tracks;

        bool isPlaying = false;
        IMusicPlayer musicPlayer;

        void Start()
        {
            musicPlayer = new MusicPlayer(channel);
            musicPlayer.LoadTracks(tracks);
            musicPlayer.Play(currentTrack);
        }

        void Update()
        {
            if(isPlaying && !musicPlayer.IsMusicPlaying())
            {
                NextTrack();
            }
        }

        public string GetTrackTitle()
        {
            return musicPlayer.GetTrackTitle();
        }

        #region CONTEXT CONTROLS
        [ContextMenu("Turn On Jukebox")]
        public void TurnOn()
        {
            musicPlayer.TurnOn();
        }

        [ContextMenu("Turn Off Jukebox")]
        public void TurnOff()
        {
            isPlaying = false;
            musicPlayer.TurnOff();
        }

        [ContextMenu("Set Track")]
        public void SetTrack(int index)
        {
            currentTrack = index;
        }

        [ContextMenu("Play Track")]
        public void PlayTrack()
        {
            isPlaying = true;
            musicPlayer.Play(currentTrack);
        }

        [ContextMenu("Pause Track")]
        public void StopTrack()
        {
            isPlaying = false;
            musicPlayer.Pause();
        }

        [ContextMenu("Next Track")]
        public void NextTrack()
        {
            if (currentTrack < (tracks.Length - 1))
                currentTrack++;
            else
                currentTrack = 0;
            musicPlayer.Play(currentTrack);
        }

        [ContextMenu("Prev Track")]
        public void PrevTrack()
        {
            if (currentTrack > 0)
                currentTrack--;
            else
                currentTrack = (tracks.Length - 1);
            musicPlayer.Play(currentTrack);
        }
        #endregion
    }
}
