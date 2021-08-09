namespace Full_Control.EventHandlers
{
    using Exiled.API.Extensions;
    using Exiled.API.Features;
    using Exiled.Events.EventArgs;

    public class EventHandlers
    {
        private readonly Plugin plugin;
        public EventHandlers(Plugin plugin) => this.plugin = plugin;


        public void OnPlayerHurt(HurtingEventArgs ev)
        {
            float NewDamage = ev.Amount;
            string ThisDamage = ev.DamageType.Name.Trim();
            if (!ev.DamageType.Weapon.IsWeapon())
            {
                NewDamage = TotalDamageGetter(ThisDamage, "", NewDamage);
                if (plugin.Config.PercentageDamageValues.ContainsKey(ThisDamage))
                {
                    NewDamage *= plugin.Config.PercentageDamageValues[ThisDamage];
                }
            }
            if (ev.Target.IsGodModeEnabled)
            {
                ev.IsAllowed = false;
            }
            if (plugin.Config.ClassException.ContainsKey(ev.Target.Role.ToString()+"-"+ThisDamage))
            {
                NewDamage *=  plugin.Config.ClassException[(ev.Target.Role.ToString()+ "-" + ThisDamage)]; 
            }
            ev.Amount = NewDamage;
        }
        public float TotalDamageGetter(string gunName, string Extention, float damage)
        {
            if (plugin.Config.TotalDamageValues.ContainsKey(gunName + Extention))
            {
                damage = plugin.Config.TotalDamageValues[gunName + Extention];
            }
            return damage;
        }

        public void OnPlayerShot(ShotEventArgs ev)
        {
            string Gun = ev.Shooter.CurrentItem.Type.ToString().Replace("Gun", "");
            ev.Damage = TotalDamageGetter(Gun, "-"+ ev.Hitbox._dmgMultiplier, ev.Damage);
            if (plugin.Config.BarrelValues.ContainsKey(Gun + "-" + ev.Shooter.CurrentItem.modBarrel))
            {
                ev.Damage *= plugin.Config.BarrelValues[Gun + "-" + ev.Shooter.CurrentItem.modBarrel];
            }
            if (plugin.Config.PercentageDamageValues.ContainsKey(Gun+ "-" + ev.Hitbox._dmgMultiplier))
            {
                ev.Damage *= plugin.Config.PercentageDamageValues[Gun + "-" + ev.Hitbox._dmgMultiplier];
            }
        }
    }
}