﻿using System;
using System.Collections.Generic;
using Main.Domain.Players;
using UnityEngine;
using UnityEngine.Analytics;
using Zenject;

namespace Main.Domain.DefuseAttempts
{
    public class DefusingState : ITickable
    {
        public const float STARTING_DEFUSING_TIME = 50f;

        public DefuseAttempt CurrentDefuseAttempt { get; private set; }
        public virtual int NbBombsDefused { get; private set; }
        public virtual float RemainingTime { get; private set; }
        public bool TimerEnabled { get; private set; }

        private readonly IDefusingTime _defusingTime;
        private readonly IDefusingTimerUpdatedListener _defusingTimerUpdatedListener;
        private readonly IDefuseFailedListener _defuseFailedListener;

        public DefusingState(IDefusingTime defusingTime, IDefusingTimerUpdatedListener defusingTimerUpdatedListener,
            IDefuseFailedListener defuseFailedListener)
        {
            _defuseFailedListener = defuseFailedListener;
            _defusingTimerUpdatedListener = defusingTimerUpdatedListener;
            _defusingTime = defusingTime;
            NbBombsDefused = 0;
        }

        public void SetNewDefuseAttempt(DefuseAttempt defuseAttempt)
        {
            CurrentDefuseAttempt = defuseAttempt;
            RemainingTime += defuseAttempt.TimeToDefuse;
            _defusingTimerUpdatedListener.OnDefusingTimerUpdated(RemainingTime);
        }

        public virtual bool IsCurrentAttemptDefuser(Player player)
        {
            return CurrentDefuseAttempt.IsDefuser(player);
        }

        public virtual void IncrementBombsDefused()
        {
            NbBombsDefused++;
        }

        public virtual void StartNewTimer()
        {
            TimerEnabled = true;
            RemainingTime = STARTING_DEFUSING_TIME;
            _defusingTimerUpdatedListener.OnDefusingTimerUpdated(RemainingTime);
        }

        public void Tick()
        {
            if (!TimerEnabled) return;
            
            RemainingTime -= _defusingTime.GetDeltaTime();
            CheckTimerBelowZero();
        }

        private void CheckTimerBelowZero()
        {
            if (RemainingTime > 0) return;
            
            _defuseFailedListener.OnDefuseFailed(NbBombsDefused);
            TimerEnabled = false;
        }
    }
}