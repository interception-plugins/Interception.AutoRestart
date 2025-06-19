using System;
using System.Collections.Generic;

using UnityEngine;
using Steamworks;
using SDG.Unturned;
using Rocket.API;
using Rocket.Core.Plugins;
using Rocket.Unturned.Player;
using Rocket.API.Collections;

using interception.process;
using interception.utils;
using interception.cron;

namespace interception.plugins.autorestart {   
    public class config : IRocketPluginConfiguration, IDefaultable {
        public string chat_message_icon_url;
        public List<shutdown_event> shutdown_events;
       
        public void LoadDefaults() {
            chat_message_icon_url = "http://example.com/image.png";
            shutdown_events = new List<shutdown_event>() {
                new shutdown_event() {
                    time = "11:58:00",
                    delay = 120,
                    should_restart = true,
                    print_messages = true
                },
                new shutdown_event() {
                    time = "23:58:00",
                    delay = 120,
                    should_restart = false,
                    print_messages = true
                },
            };
        }
    }

    public class main : RocketPlugin<config> {
        internal static main instance;
        internal static config cfg;

        IEnumerator<WaitForSecondsRealtime> restart_routine(int delay, bool should_restart, bool print_messages) {
            if (print_messages) {
                for (int i = delay; i > 0; i--) {
                    chat_util.say(main.instance.Translate("restart_in", i));
                    yield return new WaitForSecondsRealtime(1f);
                }
            }
            else {
                yield return new WaitForSecondsRealtime((float)delay);
            }
            if (should_restart)
                restart_manager.restart(main.instance.Translate("kick_reason"));
            else
                Provider.shutdown();
        }

        void restart(object[] args) {
            int delay = (int)args[0];
            bool should_restart = (bool)args[1];
            bool print_messages = (bool)args[2];
            StartCoroutine(restart_routine(delay, should_restart, print_messages));
        }

        protected override void Load() {
            instance = this;
            cfg = instance.Configuration.Instance;
            var len = cfg.shutdown_events.Count;
            for (int i = 0; i < len; i++)
                cron_manager.register_event(new cron_event($"restart_event_{i}", 
                    DateTime.Parse(cfg.shutdown_events[i].time), true, restart, cfg.shutdown_events[i].delay, cfg.shutdown_events[i].should_restart, cfg.shutdown_events[i].print_messages));
            GC.Collect();
        }

        protected override void Unload() {
            var len = cfg.shutdown_events.Count;
            for (int i = 0; i < len; i++)
                cron_manager.unregister_event($"restart_event_{i}");
            cfg = null;
            instance = null;
            GC.Collect();
        }

        public override TranslationList DefaultTranslations => new TranslationList
        {
            { "kick_reason", "Server is restarting, please wait..." },
            { "restart_in", "Server will restart in {0}..." },
            { "cmd_restart_restart_performed", "Server will restart in {0} seconds!" },
            { "cmd_restart_restart_performed_toplayer", "What's done cannot be undone" }
        };
    }
}

