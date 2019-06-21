﻿using Main.Domain.DefuseAttempts;
using UnityEngine;

namespace Main.Infrastructure.Data
{
    [CreateAssetMenu(fileName = "New Bomb", menuName = "SpotTheDefuser/Bomb")]
    public class Bomb : ScriptableObject, IBomb
    {
        public Sprite defuserSprite;
        public Sprite explosiveSprite;
        public BombLanguage language;

        public string Id => name;
        public BombLanguage Language => language;

        public Sprite GetSprite(bool isDefuser)
        {
            return isDefuser ? defuserSprite : explosiveSprite;
        }
    }
}
