namespace Full_Control.EventHandlers
{
    using Exiled.API.Features;
    using Exiled.Events.EventArgs;

    public class EventHandlers
    {
        private readonly Plugin plugin;
        public EventHandlers(Plugin plugin) => this.plugin = plugin;


        public void OnPlayerHurt(HurtingEventArgs ev)
        {
            float NewDamage = ev.Amount;
            string ThisDamage = ev.DamageType.name.Replace(" ", "");
            if (ev.DamageType.isWeapon == true)
            {
                NewDamage = HeadshotFinder(ThisDamage, ev.Amount);
                if (plugin.Config.BarrelValues.ContainsKey(ThisDamage + "-"+ ev.Attacker.CurrentItem.modBarrel))
                {
                    NewDamage *= plugin.Config.BarrelValues[ThisDamage + "-" + ev.Attacker.CurrentItem.modBarrel];
                }
            }
            else
            {
                if (ThisDamage == "FALL" || ThisDamage == "GRENADE" || ThisDamage == "SCP-207")
                {
                    if (plugin.Config.DamageValues.ContainsKey(ThisDamage))
                    {
                        NewDamage *= plugin.Config.DamageValues[ThisDamage];
                    }
                }
                else
                {
                    NewDamage = DamageGetter(ThisDamage, "", NewDamage);
                }
            }
            if (ev.Target.IsGodModeEnabled)
            {
                ev.Amount = 0;
            }
            if (plugin.Config.ClassException.ContainsKey(ev.Target.Role.ToString()+"-"+ThisDamage))
            {
                NewDamage *=  plugin.Config.ClassException[(ev.Target.Role.ToString()+ "-" + ThisDamage)]; 
            }
            ev.Amount = NewDamage;
        }

        public float HeadshotFinder(string gunName, float damage )
        {
            if (damage > 35)
            {
                damage = DamageGetter(gunName, "-HS", damage);
            }
            else
            {
                if (gunName == "USP" || gunName == "Com15")
                {
                    if (damage < 17)
                    {
                            damage = DamageGetter(gunName, "-LEG", damage);
                    }
                    else
                    {
                        damage = DamageGetter(gunName, "-BODY", damage);
                    }
                }
                if (gunName == "P90" || gunName == "Logicier" || gunName == "MP7")
                {
                    if (damage < 10)
                    {
                        damage = DamageGetter(gunName, "-LEG", damage);
                    }
                    else
                    {
                        damage = DamageGetter(gunName, "-BODY", damage);
                    }
                }
                if (gunName == "E11StandardRifle")
                {
                    if (damage < 13)
                    {
                        damage = DamageGetter(gunName, "-LEG", damage);
                    }
                    else
                    {
                        damage = DamageGetter(gunName, "-BODY", damage);
                    }
                }
            }
            return damage;
        }
        public float DamageGetter(string gunName, string Extention, float damage)
        {
            if (plugin.Config.DamageValues.ContainsKey(gunName + Extention))
            {
                damage = plugin.Config.DamageValues[gunName + Extention];
            }
            return damage;
        }
    }
}