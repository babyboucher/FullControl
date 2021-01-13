namespace Full_Control
{
    using System;
    using System.Collections.Generic;
    using MEC;
    using Player = Exiled.Events.Handlers.Player;

    public class Plugin : Exiled.API.Features.Plugin<Config>
    {
        public override string Name { get; } = "Full Control";
        public override string Author { get; } = "Babyboucher20";
        public override Version Version { get; } = new Version(2, 5, 2);
        public override Version RequiredExiledVersion { get; } = new Version(2, 0, 9);
        public override string Prefix { get; } = "FullControl";
        
        public EventHandlers.EventHandlers PlayerHandlers;
        public static Plugin Singleton;

        public override void OnEnabled()
        {
            Singleton = this;
            PlayerHandlers = new EventHandlers.EventHandlers(this);

            Player.Hurting += PlayerHandlers.OnPlayerHurt;
            Player.Shot += PlayerHandlers.OnPlayerShot;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Player.Hurting -= PlayerHandlers.OnPlayerHurt;
            Player.Shot -= PlayerHandlers.OnPlayerShot;

            base.OnDisabled();
        }
    }
}
