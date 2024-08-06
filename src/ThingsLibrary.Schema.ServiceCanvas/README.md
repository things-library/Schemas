# Service Canvas 

The basis of the Service Canvas is to establish a strongly typed definition language for how web services operate which also standardizes the startup and configuration of the service.  This allows for consistency between services by establishing common cross cutting concerns such as logging and authentication.

By leveraging the cascading nature of AppSettings we can properly cascade settings based on environment and deployment differences.


# Canvas Sections:

* Info
* Health Checks
* Authentication
* Database
* Cache

This library establishes a strongly typed definition language around service definition language where a section in appsettings is a strongly typed class with predefined locations to find settings to keep things consistent.

The registration of various service, security settings, logging and such should really be standardized across various microservices within the same application portfolio and that is exactly the purpose of this definition language.

If we need to add or change how we do logging for example, we update the service canvas Library and have everyone get the latest nuget packages to pick up the changes.

