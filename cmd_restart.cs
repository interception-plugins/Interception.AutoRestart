using System;
using System.Collections.Generic;

using UnityEngine;

using Rocket.API;
using Rocket.Unturned.Player;

using interception.extensions;
using interception.process;
using interception.utils;

namespace interception.plugins.autorestart {
    internal class cmd_restart : IRocketCommand {
        public void Execute(IRocketPlayer caller, params string[] args) {
            int delay = 0;
            if (args.Length != 0)
                int.TryParse(args[0], out delay);
            restart_manager.restart(main.instance.Translate("kick_reason"), delay);
            if (!(caller is ConsolePlayer))
                ((UnturnedPlayer)caller).Player.say_to(main.instance.Translate("cmd_restart_restart_performed_toplayer"), new Color(1f, 0.075f, 0.075f, 1f), null, true);
            chat_util.say(main.instance.Translate("cmd_restart_restart_performed", delay), Color.magenta, main.cfg.chat_message_icon_url);
        }

        public AllowedCaller AllowedCaller => AllowedCaller.Both;
        public string Name => "restart";
        public string Help => "Perform a server restart";
        public string Syntax => "/restart [delay*]";
        public List<string> Aliases => new List<string>();
        public List<string> Permissions => new List<string> { "interception.autorestart.restart" };
    }
}