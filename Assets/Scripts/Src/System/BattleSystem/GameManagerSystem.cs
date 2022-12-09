﻿using QFramework;
using UnityEngine;

namespace BrotatoM
{
    public class GameManagerSystem : AbstractSystem
    {
        public GameState State { get; set; } = GameState.PLAY;
        protected override void OnInit()
        {
            var go = new GameObject("GameManager");
            go.AddComponent<DontDestroyOnLoadScript>();
        }
    }
}