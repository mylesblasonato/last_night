using UnityEngine;
namespace MDI.Gamekit.MadBox
{
    public interface IMusicPlayer
    {
        public string GetTrackTitle();
        public bool IsMusicPlaying();
        public void TurnOn();
        public void TurnOff();
        public void LoadTracks(AudioClip[] tracks);
        public void UnloadTracks();
        public void Play(int track);
        public void Pause();
        public void NextTrack();
        public void PreviousTrack();
    }
}
