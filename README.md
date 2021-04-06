# Aimrank.CSGO

Microservice responsible for managing lifetime of CS:GO servers.
Web application calls this service when match has to be started.
When game is running it sends some events to event bus.
Those events are handled by web application.