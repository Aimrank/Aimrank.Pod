# Aimrank.Pod

Microservice responsible for managing lifetime of CS:GO servers.
Cluster application calls this service when match has to be started.
When game is running it sends some events to event bus that are later handled by web application or pods cluster.
