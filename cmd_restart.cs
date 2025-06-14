using System;
using Rocket.API;
using System.Collections.Generic;

using interception.server;
using interception.utils;

namespace interception.plugins.autorestart {
    internal class cmd_restart : IRocketCommand {
        public void Execute(IRocketPlayer caller, params string[] args) {
            int delay = 0;
            if (args.Length != 0)
                int.TryParse(args[0], out delay);
            restart_manager.restart(main.instance.Translate("kick_reason"), delay);
            chat_util.say(main.instance.Translate("cmd_restart_restart_performed", delay));
        }

        public AllowedCaller AllowedCaller => AllowedCaller.Both;
        public string Name => "restart";
        public string Help => "Perform a server restart";
        public string Syntax => "/restart [delay*]";
        public List<string> Aliases => new List<string>();
        public List<string> Permissions => new List<string> { "interception.autorestart.restart" };
    }
}