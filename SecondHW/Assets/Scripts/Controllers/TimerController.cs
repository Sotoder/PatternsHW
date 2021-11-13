using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class TimerController: IController, IExecute
    {
        private static List<Timer> _timers = new List<Timer>();

        private const float CLIEAR_TIME = 20f;

        public void Add(Timer timer)
        {
            _timers.Add(timer);
        }

        public void Remove(Timer timer)
        {
            _timers.Remove(timer);
        }

        public void Execute(float deltatime)
        {
            for (int i = 0; i < _timers.Count; i++)
            {
                if (Time.time - _timers[i].StartTime >= _timers[i].Duration)
                {
                    _timers[i].timerIsOver.Invoke();
                    _timers.Remove(_timers[i]);
                }
            }
        }
    }
}