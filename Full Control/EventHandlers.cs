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
                    NewDamage *= (float)plugin.Config.BarrelValues[ThisDamage + "-" + ev.Attacker.CurrentItem.modBarrel];
                }
            }
            else
            {
                if (ThisDamage == "FALL" || ThisDamage == "GRENADE" || ThisDamage == "SCP-207")
                {
                    NewDamage *= plugin.Config.DamageValues[ThisDamage];
                }
                else if (plugin.Config.DamageValues.ContainsKey(ThisDamage))
                {
                    NewDamage = plugin.Config.DamageValues[ThisDamage];
                }
            }
            if (ev.Target.IsGodModeEnabled)
            {
                ev.Amount = 0;
            }
            ev.Amount = NewDamage;
        }

        public float HeadshotFinder(string gunName, float damage )
        {
            if (damage > 35)
            {
                if (plugin.Config.DamageValues.ContainsKey(gunName + "-HS"))
                {
                    damage = plugin.Config.DamageValues[gunName +"-HS"];
                }
            }
            else
            {
                if (plugin.Config.DamageValues.ContainsKey(gunName))
                {
                    damage = plugin.Config.DamageValues[gunName];
                }
            }
            return damage;
        }
    }
}