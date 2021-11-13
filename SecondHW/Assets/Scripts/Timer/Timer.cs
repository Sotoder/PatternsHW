using System;
using UnityEngine;

namespace Asteroids
{
    public class Timer
    {
        private float _startTime;
        private float _duration;

        public Action timerIsOver = delegate() { };

        public float StartTime { get => _startTime; }
        public float Duration { get => _duration; }

        public Timer(float duration)
        {
            _startTime = Time.time;
            _duration = duration;
        }
    }
}