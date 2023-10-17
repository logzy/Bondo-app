# Bondo-app

This project is desinged using clean architecture

Domain Layer = This layer should be the backbone of the this architecture and all other projects defined in other layers should depend on this layer. 
This layer is highly abstracted and it contains domain entities, events, value objects, aggregates, etc. 

Application Layer = The application business rules and use cases are available in this layer. It should be used to define interfaces that are implemented by the outer layers. 
This layer should contains business services, DTOs, mappers, validators, etc.

Infrastructure Layer = This layer should contains the implementation of the interfaces defined in the Application layer. 
This layer is responsible for implementing the technical aspects of the system, such as data access, logging, and security, etc. 
It can have multiple projects using different third party libraries or frameworks. 
The contexts folder should have the DBContext(s)

Presentation Layer = This layer is responsible for presenting some user interface and handling user interaction with the system. 
It includes the views, controllers, and other web components

See link here https://www.ezzylearning.net/tutorial/building-asp-net-core-apps-with-clean-architecture documentation





