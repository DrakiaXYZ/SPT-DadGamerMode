﻿using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using Comfort.Common;
using EFT;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using System;
using System.Threading;
using System.Linq;

namespace dvize.AILimit
{
    [BepInPlugin("com.dvize.GodModeTest", "dvize.GodModeTest", "1.0.0")]

    public class Plugin : BaseUnityPlugin
    {
        public ConfigEntry<Boolean> Godmode { get; set; }
        
        internal void Awake()
        {
            Godmode = Config.Bind("Player | Health", "Godmode", false, "Invincible");
        }

        bool runOnce = false;
        public static Player player;
        void Update()
        {
            if (Godmode.Value == true)
            {
                if (!runOnce)
                {
                    player = Singleton<GameWorld>.Instance.MainPlayer;
                    player.PlayerHealthController.SetDamageCoeff(-1f);
                    player.PlayerHealthController.FallSafeHeight = 999999f;
                    player.OnDamageReceived += FixDamage();
                    runOnce = true;
                }
                
            }
        }
        private Player.GDelegate42 FixDamage()
        {
            player.PlayerHealthController.RemoveNegativeEffects(EBodyPart.Common);
            player.Heal(EBodyPart.Head, 999999f);
            player.Heal(EBodyPart.Stomach, 999999f);
            player.Heal(EBodyPart.Chest, 999999f);
            player.Heal(EBodyPart.LeftArm, 999999f);
            player.Heal(EBodyPart.LeftLeg, 999999f);
            player.Heal(EBodyPart.RightArm, 999999f);
            player.Heal(EBodyPart.RightLeg, 999999f);
            player.PlayerHealthController.RestoreFullHealth();
            return null;
        }



    }
}

