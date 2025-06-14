# Interception.AutoRestart

Unturned RocketMod plugin for server's auto restarting (or auto shutdowning if you wish)
	
A part of [Interception.Module](https://github.com/interception-plugins/Interception.Module) example plugins

### Configuration

```xml
<?xml version="1.0" encoding="utf-8"?>
<config xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <chat_message_icon_url>http://example.com/image.png</chat_message_icon_url>
  <shutdown_events>
    <shutdown_event time="14:43:00" delay="120" should_restart="true" print_messages="true" />
    <shutdown_event time="23:58:00" delay="120" should_restart="false" print_messages="true" />
  </shutdown_events>
</config>
```

### Translations

```xml
<?xml version="1.0" encoding="utf-8"?>
<Translations xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <Translation Id="kick_reason" Value="Server is restarting, please wait..." />
  <Translation Id="restart_in" Value="Server will restart in {0}..." />
  <Translation Id="cmd_restart_restart_performed" Value="Server will restart in {0} seconds!" />
</Translations>
```