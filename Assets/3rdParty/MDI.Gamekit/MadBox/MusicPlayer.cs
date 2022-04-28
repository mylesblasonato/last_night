using System.Collections.Generic;
using UnityEngine;

namespace MDI.Gamekit.MadBox
{
    public class MusicPlayer : IMusicPlayer
    {
        Dictionary<int, AudioClip> tracks = new Dictionary<int, AudioClip>();
        AudioSource channel;

        int currentTrack = 0;
        bool isPlaying = false;
        bool isPowered = false;

        public MusicPlayer(AudioSource channel)
        {
            this.channel = channel;
        }

        public bool IsMusicPlaying()
        {
            return this.channel.isPlaying;
        }

        public string GetTrackTitle()
        {
            return this.channel.clip.name;
        }

        public void TurnOn()
        {
            isPowered = true;
            this.channel.enabled = true;
        }

        public void TurnOff()
        {
            this.channel.Stop();
            currentTrack = 0;
            isPlaying = false;
            isPowered = false;
            this.channel.enabled = false;
        }

        public void LoadTracks(AudioClip[] tracks)
        {
            var trackIndex = 0;
            this.tracks.Clear();
            foreach (var track in tracks)
            {
                this.tracks.Add(trackIndex, track);
                trackIndex++;
            }
        }

        public void UnloadTracks()
        {
            tracks.Clear();
        }

        public void Play(int track)
        {
            if (tracks.Count <= 0 && !isPowered) return;
            isPlaying = true;
            currentTrack = track;
            this.channel.clip = tracks[currentTrack];
            this.channel.Play();
        }

        public void Pause()
        {
            if (!isPlaying) return;
            this.channel.Pause();
        }

        public void NextTrack()
        {
            if (currentTrack > tracks.Count) return;
            currentTrack++;
        }

        public void PreviousTrack()
        {
            if (currentTrack <= 0) return;
            currentTrack--;
        }
    }
}