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
            if (ev.DamageType.isWeapon != true)
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
            Log.Error(ev.Amount);
        }
        public float DamageGetter(string gunName, string Extention, float damage)
        {
            if (plugin.Config.DamageValues.ContainsKey(gunName + Extention))
            {
                damage = plugin.Config.DamageValues[gunName + Extention];
            }
            return damage;
        }

        public void OnPlayerShot(ShotEventArgs ev)
        {
            string Gun = ev.Shooter.CurrentItem.id.ToString().Replace("Gun", "");
            float NewDamage = DamageGetter(Gun, "-"+ev.HitboxTypeEnum.ToString(), ev.Damage);
            if (plugin.Config.BarrelValues.ContainsKey(Gun + "-" + ev.Shooter.CurrentItem.modBarrel))
            {
                NewDamage *= plugin.Config.BarrelValues[Gun + "-" + ev.Shooter.CurrentItem.modBarrel];
            }
            ev.Damage = NewDamage;
        }
    }
}