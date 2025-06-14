using System;
using System.Xml.Serialization;

namespace interception.plugins.autorestart {
    public class shutdown_event {
        [XmlAttribute]
        public string time { get; set; }
        [XmlAttribute]
        public int delay { get; set; }
        [XmlAttribute]
        public bool should_restart { get; set; }
        [XmlAttribute]
        public bool print_messages { get; set; }
    }
}
