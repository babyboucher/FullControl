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
        public Dictionary<string, float> DamageValues { get; set; } = new Dictionary<string, float>
        {
            {
                "Logicer", 19
            },
            {
                "Logicer-HS", 79
            },
            {
                "E11StandardRifle", 18
            },
            {
                "E11StandardRifle-HS", 73
            },
        };

        [Description("A list of Guns with Mods and how much the damage should be multiplied by.")]
        public Dictionary<string, double> BarrelValues { get; set; } = new Dictionary<string, double>
        {
            {
                "USP-1", 0.80
            },
            {
                "USP-2", 1.25
            },
            {
                "COM15-1", 0.90
            },
            {
                "E11StandardRifle-1", 0.80
            },
            {
                "E11StandardRifle-2", 0.90
            },
            {
                "E11StandardRifle-3", 1.25
            },
            {
                "E11StandardRifle-4", 1.1
            },
            {
                "MP7-1", 0.90
            },
            {
                "P90-1", 0.90
            },
            {
                "P90-2", 0.80
            },
            {
                "P90-3", 1.25
            },

        };
    }
}