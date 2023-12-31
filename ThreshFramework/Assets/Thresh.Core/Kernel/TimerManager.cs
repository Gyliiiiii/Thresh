﻿using System.Collections.Generic;
using System.Linq;
using Thresh.Core.Data;
using Thresh.Core.Interface;
using Thresh.Core.Utility;

namespace Thresh.Core.Kernel
{
    public delegate void TimerCallback(IKernel kernel, PersistID pid, string name, long now, int count);
    public class TimerManager
    {
        class Timer
        {
            public int Serial { get; private set; }
            public PersistID Pid { get; private set; }
            public long GapTicks { get; private set; }
            public int RemainBeatCount { get; private set; }
            public int MaxBeatCount { get; private set; }
            public TimerCallback Func { get; private set; }
            public string Name { get; private set; }
            public long StartTicks { get; private set; }
            public long StopTicks { get; private set; }

            public static Timer New(int new_serial, PersistID pid, string name, long gap_ticks, int beat_count,
                long start_ticks, long stop_ticks, TimerCallback func)
            {
                Timer timer = new Timer();
                timer.Pid = pid;
                timer.Name = name;
                timer.GapTicks = gap_ticks;
                timer.Func = func;
                timer.RemainBeatCount = beat_count;
                timer.MaxBeatCount = beat_count;
                timer.Serial = new_serial;
                timer.StartTicks = start_ticks;
                timer.StopTicks = stop_ticks;

                return timer;
            }

            public void Beat()
            {
                RemainBeatCount--;
            }

            public void Reset()
            {
                StopTicks = StartTicks + (MaxBeatCount - RemainBeatCount + 1) * GapTicks;
            }
        }

        private IKernel _kernel;

        public TimerManager(IKernel kernel)
        {
            _kernel = kernel;
            _TimerDic = new Dictionary<int, Timer>();
            _EntityTimerDic = new Dictionary<Group<PersistID, string>, int>();
            _TimerDic = new Dictionary<int, Timer>();
            _DelTimers = new Queue<int>();
            _AddTimers = new Queue<int>();
        }

        private Dictionary<int, Timer> _TimerDic;

        private Dictionary<Group<PersistID, string>, int> _EntityTimerDic;

        private SortedDictionary<long, HashSet<int>> _TimerHeap;

        private Queue<int> _DelTimers;

        private Queue<int> _AddTimers;

        private int _Serial = 0;

        public void AddTimer(PersistID pid, string timer_name, long over_millseconds, TimerCallback callback)
        {
            long now_time = TimeUtil.NowMilliseconds;
            if (now_time <= over_millseconds)
            {
                return;
            }

            long gap_millseconds = over_millseconds - now_time;
            AddTimer(pid,timer_name,gap_millseconds,1,callback);
        }

        public void AddTimer(PersistID pid, string timer_name, long gap_millseconds, int count, TimerCallback callback)
        {
            if (gap_millseconds <= 0)
            {
                return;
            }

            if (count == 0)
            {
                return;
            }

            Group<PersistID, string> group = new Group<PersistID, string>(pid,timer_name);
            if (_EntityTimerDic.ContainsKey(group))
            {
                return;
            }

            long start_time = TimeUtil.NowMilliseconds;
            long stop_time = TimeUtil.NowMilliseconds + gap_millseconds;

            _Serial++;
            Timer timer = Timer.New(_Serial, pid, timer_name, gap_millseconds, count, start_time, stop_time, callback);
            _TimerDic.Add(timer.Serial,timer);
            
            _EntityTimerDic.Add(group,timer.Serial);

            HashSet<int> timers = null;
            if (!_TimerHeap.TryGetValue(stop_time,out timers))
            {
                timers = new HashSet<int>();
                _TimerHeap.Add(stop_time,timers);
            }

            timers.Add(timer.Serial);
        }

        public void DelTimer(PersistID pid, string timer_name)
        {
            Group<PersistID, string> group = new Group<PersistID, string>(pid, timer_name);

            int serial = 0;
            if (!_EntityTimerDic.TryGetValue(group,out serial))
            {
                return;
            }
            
            _DelTimers.Enqueue(serial);
        }

        public void UpdateTime(long now_ticks)
        {
            while (_TimerHeap.Count > 0 && _TimerHeap.Keys.First() <= now_ticks)
            {
                foreach (var serial in _TimerHeap.Values.First())
                {
                    Timer timer = null;
                    if (!_TimerDic.TryGetValue(serial,out timer))
                    {
                        continue;
                    }
                    
                    timer.Beat();

                    if (timer.RemainBeatCount == 0)
                    {
                        _DelTimers.Enqueue(timer.Serial);
                    }else if (timer.RemainBeatCount > 0)
                    {
                        _AddTimers.Enqueue(timer.Serial);
                        timer.Reset();
                    }
                    
                    timer.Func.Invoke(_kernel,timer.Pid,timer.Name,now_ticks,timer.RemainBeatCount);
                }

                _TimerHeap.Remove(_TimerHeap.Keys.First());
            }

            while (_DelTimers.Count > 0)
            {
                int serial = _DelTimers.Dequeue();
                Timer timer = null;
                if (!_TimerDic.TryGetValue(serial,out timer))
                {
                    continue;
                }

                Group<PersistID, string> group = new Group<PersistID, string>(timer.Pid, timer.Name);

                if (_EntityTimerDic.ContainsKey(group))
                {
                    _EntityTimerDic.Remove(group);
                }

                HashSet<int> timers = null;
                if (!_TimerHeap.TryGetValue(timer.StopTicks,out timers))
                {
                    continue;
                }

                if (!timers.Contains(serial))
                {
                    continue;
                }

                timers.Remove(serial);
            }

            while (_AddTimers.Count > 0)
            {
                int serial = _AddTimers.Dequeue();
                Timer timer = null;
                if (!_TimerDic.TryGetValue(serial,out timer))
                {
                    continue;
                }

                HashSet<int> timers = null;
                if (!_TimerHeap.TryGetValue(timer.StopTicks,out timers))
                {
                    timers = new HashSet<int>();
                    _TimerHeap.Add(timer.StopTicks,timers);
                }

                timers.Add(timer.Serial);
            }
        }
    }
}