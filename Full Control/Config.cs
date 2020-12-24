using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Exiled.API.Features;
using Exiled.API.Interfaces;
using Scp914;

namespace Full_Control
{
    public class Config : IConfig
    {
        [Description("Is the plugin enabled?")]
        public bool IsEnabled { get; set; } = true;

        [Description("A list of DamageTypes and how much damage they should do before any barrel mods.")]
        public Dictionary<string, float> TotalDamageValues { get; set; } = new Dictionary<string, float>
        {
            {
                "Logicer-BODY", 19
            },
            {
                "Logicer-HEAD", 79
            },
            {
                "E11StandardRifle-BODY", 18
            },
            {
                "E11StandardRifle-HS", 73
            },
        };

        [Description("A list of DamageTypes and how much percentage damage they should do.(Applied after Total Damage)")]
        public Dictionary<string, float> PercentageDamageValues { get; set; } = new Dictionary<string, float>
        {
            {
                "Logicer-BODY", .9f
            },
            {
                "Logicer-HEAD", 1
            },
            {
                "E11StandardRifle-BODY", .9f
            },
            {
                "E11StandardRifle-HEAD", 1
            },
        };

        [Description("A list of Guns with Mods and how much the damage should be multiplied by.")]
        public Dictionary<string, float> BarrelValues { get; set; } = new Dictionary<string, float>
        {
            {
                "USP-1", 0.80f
            },
            {
                "USP-2", 1.25f
            },
            {
                "Com15-1", 0.90f
            },
            {
                "E11StandardRifle-1", 0.80f
            },
            {
                "E11StandardRifle-2", 0.90f
            },
            {
                "E11StandardRifle-3", 1.25f
            },
            {
                "E11StandardRifle-4", 1.1f
            },
            {
                "MP7-1", 0.90f
            },
            {
                "P90-1", 0.90f
            },
            {
                "P90-2", 0.80f
            },
            {
                "P90-3", 1.25f
            },

        };

        [Description("A list of classes and what percentage they should take from a DamageType")]
        public Dictionary<string, float> ClassException { get; set; } = new Dictionary<string, float>
        {
            {
                "Scp106-E11StandardRifle", 0.1f
            },
            {
                "Scp106-MP7", 0.1f
            },
            {
                "Scp106-P90", 0.1f
            },
            {
                "Scp106-Com15", 0.1f
            },
            {
                "Scp106-USP", 0.1f
            },
            {
                "Scp106-Logicier", 0.1f
            },
        };
    }
}