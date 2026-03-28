namespace SharedModels;

public class DDDLearningPath
{
  public readonly List<ContentMetaData> FullContents = new(23);

  public DDDLearningPath()
  {
    FullContents =
    [
      new ContentMetaData
      {
        Order = 23,
        Title = "Storing Data in Its Richest Form in DDD - Preventing Information Loss in .NET",
        Description = "In this post I will teach you how to prevent information loss in your domain model by always storing data in its most atomic, specific form using DDD Value Objects in .NET.",
        Author = "Abdul Rahman",
        Slug = "ddd-storing-data-in-its-richest-form-preventing-information-loss-in-dotnet",
        PosterUrl = "image/blogs/ddd/ddd-storing-data-in-its-richest-form-preventing-information-loss-in-dotnet.webp",
        ThumbnailUrl = "image/blogs/ddd/ddd-storing-data-in-its-richest-form-preventing-information-loss-in-dotnet.webp",
        ContentUrl = "blogs/ddd-storing-data-in-its-richest-form-preventing-information-loss-in-dotnet",
        IconUrl = "image/icons/ddd.webp",
        Channel = "DDD",
        Type = "blogs",
        CreatedOn = new DateTime(2026, 8, 23, 22, 30, 0, DateTimeKind.Utc),
        ModifiedOn = new DateTime(2026, 8, 23, 22, 30, 0, DateTimeKind.Utc),
        Keywords = ["Domain-Driven Design", "DDD", "Value Objects", "Information Loss", "Primitive Obsession", ".NET", "C#", "Rich Domain Models", "Data Integrity", "Immutability"]
      },
      new ContentMetaData
      {
        Order = 22,
        Title = "Specification Pattern - Eliminating Scattered Business Logic in DDD",
        Description = "In this post I will teach you how to eliminate scattered and duplicated business logic using the Specification Pattern from Domain-Driven Design. All with live working demo.",
        Author = "Abdul Rahman",
        Slug = "ddd-specification-pattern-eliminating-scattered-business-logic",
        PosterUrl = "image/blogs/ddd/ddd-specification-pattern-eliminating-scattered-business-logic.webp",
        ThumbnailUrl = "image/blogs/ddd/ddd-specification-pattern-eliminating-scattered-business-logic.webp",
        ContentUrl = "blogs/ddd-specification-pattern-eliminating-scattered-business-logic",
        IconUrl = "image/icons/ddd.webp",
        Channel = "DDD",
        Type = "blogs",
        CreatedOn = new DateTime(2025, 12, 28, 22, 30, 0, DateTimeKind.Utc),
        ModifiedOn = new DateTime(2025, 12, 28, 22, 30, 0, DateTimeKind.Utc),
        Keywords = ["Specification Pattern", "Domain-Driven Design", "DDD", "Business Logic", "Entity Framework Core", "Expression Trees", "LINQ", "Design Patterns"]
      },
      new ContentMetaData
      {
        Order = 1,
        Title = "DDD Mindset and Language - Building Software That Speaks Your Business Domain",
        Description = "In this post I will teach you the fundamental mindset and language concepts of Domain-Driven Design, showing how to bridge the gap between business requirements and technical implementation.",
        Author = "Abdul Rahman",
        Slug = "ddd-mindset-and-language-introduction",
        PosterUrl = "image/blogs/ddd/ddd-mindset-and-language-introduction.webp",
        ThumbnailUrl = "image/blogs/ddd/ddd-mindset-and-language-introduction.webp",
        ContentUrl = "blogs/ddd-mindset-and-language-introduction",
        IconUrl = "image/icons/ddd.webp",
        Channel = "DDD",
        Type = "blogs",
        CreatedOn = new DateTime(2026, 3, 29, 22, 30, 0, DateTimeKind.Utc),
        ModifiedOn = new DateTime(2026, 3, 29, 22, 30, 0, DateTimeKind.Utc),
        Keywords = ["Domain-Driven Design", "DDD", "Ubiquitous Language", "Rich Domain Models", "Bounded Contexts", "Tactical Patterns", "Strategic Design", "Business Logic"]
      },
      new ContentMetaData
      {
        Order = 2,
        Title = "When to Use DDD and When to Keep It Simple",
        Description = "In this post I will teach you how to evaluate whether Domain-Driven Design is appropriate for your project using YAGNI and KISS principles, complete with a practical decision framework.",
        Author = "Abdul Rahman",
        Slug = "ddd-when-to-use-and-when-to-avoid",
        PosterUrl = "image/blogs/ddd/ddd-when-to-use-and-when-to-avoid.webp",
        ThumbnailUrl = "image/blogs/ddd/ddd-when-to-use-and-when-to-avoid.webp",
        ContentUrl = "blogs/ddd-when-to-use-and-when-to-avoid",
        IconUrl = "image/icons/ddd.webp",
        Channel = "DDD",
        Type = "blogs",
        CreatedOn = new DateTime(2026, 4, 5, 22, 30, 0, DateTimeKind.Utc),
        ModifiedOn = new DateTime(2026, 4, 5, 22, 30, 0, DateTimeKind.Utc),
        Keywords = ["Domain-Driven Design", "DDD", "YAGNI", "KISS", "Software Architecture", "Complexity Management", "CRUD", "Tactical DDD", "Strategic DDD"]
      },
      new ContentMetaData
      {
        Order = 3,
        Title = "Core DDD Principles in Practice - Transforming Procedural Code to Domain Models",
        Description = "In this post I will teach you the three core principles of Domain-Driven Design in practice: using ubiquitous language in code, choosing intent-revealing names, and favoring behaviors over getters.",
        Author = "Abdul Rahman",
        Slug = "ddd-core-principles-in-practice",
        PosterUrl = "image/blogs/ddd/ddd-core-principles-in-practice.webp",
        ThumbnailUrl = "image/blogs/ddd/ddd-core-principles-in-practice.webp",
        ContentUrl = "blogs/ddd-core-principles-in-practice",
        IconUrl = "image/icons/ddd.webp",
        Channel = "DDD",
        Type = "blogs",
        CreatedOn = new DateTime(2026, 4, 12, 22, 30, 0, DateTimeKind.Utc),
        ModifiedOn = new DateTime(2026, 4, 12, 22, 30, 0, DateTimeKind.Utc),
        Keywords = ["Domain-Driven Design", "DDD", "Ubiquitous Language", "Intent-Revealing Names", "Rich Domain Models", "Encapsulation", "Business Logic", "Domain Modeling"]
      },
      new ContentMetaData
      {
        Order = 4,
        Title = "Ubiquitous Language QuickStart - Building a Shared Vocabulary for Your Domain",
        Description = "In this post I will teach you how to create and maintain a domain glossary that eliminates translation gaps between developers and business experts, complete with practical categorization techniques.",
        Author = "Abdul Rahman",
        Slug = "ddd-ubiquitous-language-quickstart",
        PosterUrl = "image/blogs/ddd/ddd-ubiquitous-language-quickstart.webp",
        ThumbnailUrl = "image/blogs/ddd/ddd-ubiquitous-language-quickstart.webp",
        ContentUrl = "blogs/ddd-ubiquitous-language-quickstart",
        IconUrl = "image/icons/ddd.webp",
        Channel = "DDD",
        Type = "blogs",
        CreatedOn = new DateTime(2026, 4, 19, 22, 30, 0, DateTimeKind.Utc),
        ModifiedOn = new DateTime(2026, 4, 19, 22, 30, 0, DateTimeKind.Utc),
        Keywords = ["Domain-Driven Design", "DDD", "Ubiquitous Language", "Domain Glossary", "Entities", "Value Objects", "Commands", "Domain Events", "Shared Vocabulary"]
      },
      new ContentMetaData
      {
        Order = 5,
        Title = "Event Storming Workshop - Collaborative Domain Discovery in 60 Minutes",
        Description = "In this post I will teach you how to facilitate an Event Storming workshop using colored sticky notes to create visual timelines that reveal business workflows and gaps in understanding.",
        Author = "Abdul Rahman",
        Slug = "ddd-event-storming-collaborative-workshop",
        PosterUrl = "image/blogs/ddd/ddd-event-storming-collaborative-workshop.webp",
        ThumbnailUrl = "image/blogs/ddd/ddd-event-storming-collaborative-workshop.webp",
        ContentUrl = "blogs/ddd-event-storming-collaborative-workshop",
        IconUrl = "image/icons/ddd.webp",
        Channel = "DDD",
        Type = "blogs",
        CreatedOn = new DateTime(2026, 4, 26, 22, 30, 0, DateTimeKind.Utc),
        ModifiedOn = new DateTime(2026, 4, 26, 22, 30, 0, DateTimeKind.Utc),
        Keywords = ["Domain-Driven Design", "DDD", "Event Storming", "Workshop Facilitation", "Domain Events", "Commands", "Visual Modeling", "Collaborative Design", "Business Process Mapping"]
      },
      new ContentMetaData
      {
        Order = 6,
        Title = "Core vs Supporting vs Generic Domains - Strategic Investment Framework",
        Description = "In this post I will teach you how to classify your system into Core, Supporting, and Generic domains to make smart investment decisions about where to apply full DDD and where to integrate third-party solutions.",
        Author = "Abdul Rahman",
        Slug = "ddd-core-supporting-generic-domains",
        PosterUrl = "image/blogs/ddd/ddd-core-supporting-generic-domains.webp",
        ThumbnailUrl = "image/blogs/ddd/ddd-core-supporting-generic-domains.webp",
        ContentUrl = "blogs/ddd-core-supporting-generic-domains",
        IconUrl = "image/icons/ddd.webp",
        Channel = "DDD",
        Type = "blogs",
        CreatedOn = new DateTime(2026, 5, 3, 22, 30, 0, DateTimeKind.Utc),
        ModifiedOn = new DateTime(2026, 5, 3, 22, 30, 0, DateTimeKind.Utc),
        Keywords = ["Domain-Driven Design", "DDD", "Strategic Design", "Core Domain", "Supporting Domain", "Generic Domain", "Investment Strategy", "Build vs Buy", "Domain Classification"]
      },
      new ContentMetaData
      {
        Order = 7,
        Title = "Entities vs Value Objects in DDD - Understanding Identity and Equality in .NET",
        Description = "In this post I will teach you the fundamental distinction between entities and value objects in Domain-Driven Design, with practical .NET implementations showing when to use identity-based vs value-based equality.",
        Author = "Abdul Rahman",
        Slug = "ddd-entities-vs-value-objects-in-dotnet",
        PosterUrl = "image/blogs/ddd/ddd-entities-vs-value-objects-in-dotnet.webp",
        ThumbnailUrl = "image/blogs/ddd/ddd-entities-vs-value-objects-in-dotnet.webp",
        ContentUrl = "blogs/ddd-entities-vs-value-objects-in-dotnet",
        IconUrl = "image/icons/ddd.webp",
        Channel = "DDD",
        Type = "blogs",
        CreatedOn = new DateTime(2026, 5, 10, 22, 30, 0, DateTimeKind.Utc),
        ModifiedOn = new DateTime(2026, 5, 10, 22, 30, 0, DateTimeKind.Utc),
        Keywords = ["Domain-Driven Design", "DDD", "Entities", "Value Objects", "Tactical Patterns", ".NET", "C#", "Identity", "Equality", "Domain Modeling"]
      },
      new ContentMetaData
      {
        Order = 8,
        Title = "Designing a Money Value Object in .NET - Eliminating Primitive Obsession",
        Description = "In this post I will teach you how to design a robust Money value object in .NET that prevents currency mismatches, eliminates floating-point errors, and enforces business rules at compile-time.",
        Author = "Abdul Rahman",
        Slug = "ddd-designing-money-value-object-eliminating-primitive-obsession-in-dotnet",
        PosterUrl = "image/blogs/ddd/ddd-designing-money-value-object-eliminating-primitive-obsession-in-dotnet.webp",
        ThumbnailUrl = "image/blogs/ddd/ddd-designing-money-value-object-eliminating-primitive-obsession-in-dotnet.webp",
        ContentUrl = "blogs/ddd-designing-money-value-object-eliminating-primitive-obsession-in-dotnet",
        IconUrl = "image/icons/ddd.webp",
        Channel = "DDD",
        Type = "blogs",
        CreatedOn = new DateTime(2026, 5, 17, 22, 30, 0, DateTimeKind.Utc),
        ModifiedOn = new DateTime(2026, 5, 17, 22, 30, 0, DateTimeKind.Utc),
        Keywords = ["Domain-Driven Design", "DDD", "Value Objects", "Money Pattern", "Primitive Obsession", ".NET", "C#", "Financial Calculations", "Immutability", "Type Safety"]
      },
      new ContentMetaData
      {
        Order = 9,
        Title = "From Anemic to Rich Domain Models in .NET - Moving Logic into Entities",
        Description = "In this post I will teach you how to transform anemic domain models into rich domain models by moving business logic from service classes into entities, creating testable and maintainable code.",
        Author = "Abdul Rahman",
        Slug = "ddd-from-anemic-to-rich-domain-models-in-dotnet",
        PosterUrl = "image/blogs/ddd/ddd-from-anemic-to-rich-domain-models-in-dotnet.webp",
        ThumbnailUrl = "image/blogs/ddd/ddd-from-anemic-to-rich-domain-models-in-dotnet.webp",
        ContentUrl = "blogs/ddd-from-anemic-to-rich-domain-models-in-dotnet",
        IconUrl = "image/icons/ddd.webp",
        Channel = "DDD",
        Type = "blogs",
        CreatedOn = new DateTime(2026, 5, 24, 22, 30, 0, DateTimeKind.Utc),
        ModifiedOn = new DateTime(2026, 5, 24, 22, 30, 0, DateTimeKind.Utc),
        Keywords = ["Domain-Driven Design", "DDD", "Anemic Models", "Rich Domain Models", "Encapsulation", ".NET", "C#", "Business Logic", "Entity Design", "Tell Don't Ask"]
      },
      new ContentMetaData
      {
        Order = 10,
        Title = "Understanding Aggregates and Boundaries in DDD - Defining Consistency Boundaries",
        Description = "In this post I will teach you how to design aggregates and define their boundaries in Domain-Driven Design, focusing on consistency rules, transaction scope, and when to split large aggregates.",
        Author = "Abdul Rahman",
        Slug = "ddd-understanding-aggregates-and-boundaries-in-dotnet",
        PosterUrl = "image/blogs/ddd/ddd-understanding-aggregates-and-boundaries-in-dotnet.webp",
        ThumbnailUrl = "image/blogs/ddd/ddd-understanding-aggregates-and-boundaries-in-dotnet.webp",
        ContentUrl = "blogs/ddd-understanding-aggregates-and-boundaries-in-dotnet",
        IconUrl = "image/icons/ddd.webp",
        Channel = "DDD",
        Type = "blogs",
        CreatedOn = new DateTime(2026, 5, 31, 22, 30, 0, DateTimeKind.Utc),
        ModifiedOn = new DateTime(2026, 5, 31, 22, 30, 0, DateTimeKind.Utc),
        Keywords = ["Domain-Driven Design", "DDD", "Aggregates", "Aggregate Root", "Consistency Boundaries", "Transaction Boundaries", ".NET", "C#", "Domain Modeling", "Tactical Patterns"]
      },
      new ContentMetaData
      {
        Order = 11,
        Title = "Implementing Order Aggregate Pattern in .NET - Complete DDD Example",
        Description = "In this post I will teach you how to implement a complete Order aggregate in .NET with order lines, discounts, lifecycle management, and invariants enforcement following DDD best practices.",
        Author = "Abdul Rahman",
        Slug = "ddd-implementing-order-aggregate-pattern-in-dotnet",
        PosterUrl = "image/blogs/ddd/ddd-implementing-order-aggregate-pattern-in-dotnet.webp",
        ThumbnailUrl = "image/blogs/ddd/ddd-implementing-order-aggregate-pattern-in-dotnet.webp",
        ContentUrl = "blogs/ddd-implementing-order-aggregate-pattern-in-dotnet",
        IconUrl = "image/icons/ddd.webp",
        Channel = "DDD",
        Type = "blogs",
        CreatedOn = new DateTime(2026, 6, 7, 22, 30, 0, DateTimeKind.Utc),
        ModifiedOn = new DateTime(2026, 6, 7, 22, 30, 0, DateTimeKind.Utc),
        Keywords = ["Domain-Driven Design", "DDD", "Aggregates", "Order Pattern", "E-commerce", ".NET", "C#", "Tactical DDD", "Business Rules", "Entity Framework"]
      },
      new ContentMetaData
      {
        Order = 12,
        Title = "Domain Events Pattern in .NET - Decoupling Aggregates with Event-Driven Design",
        Description = "In this post I will teach you how to implement domain events in .NET to enable loose coupling between aggregates, provide audit trails, and build event-driven domain models following DDD principles.",
        Author = "Abdul Rahman",
        Slug = "ddd-domain-events-pattern-in-dotnet",
        PosterUrl = "image/blogs/ddd/ddd-domain-events-pattern-in-dotnet.webp",
        ThumbnailUrl = "image/blogs/ddd/ddd-domain-events-pattern-in-dotnet.webp",
        ContentUrl = "blogs/ddd-domain-events-pattern-in-dotnet",
        IconUrl = "image/icons/ddd.webp",
        Channel = "DDD",
        Type = "blogs",
        CreatedOn = new DateTime(2026, 6, 14, 22, 30, 0, DateTimeKind.Utc),
        ModifiedOn = new DateTime(2026, 6, 14, 22, 30, 0, DateTimeKind.Utc),
        Keywords = ["Domain-Driven Design", "DDD", "Domain Events", "Event-Driven Architecture", "Loose Coupling", ".NET", "C#", "Tactical Patterns", "Audit Trail", "Event Handlers"]
      },
      new ContentMetaData
      {
        Order = 13,
        Title = "Publishing and Handling Domain Events in DDD with .NET",
        Description = "In this post I will teach you how to safely publish and handle domain events in a DDD application using the Event Collection Pattern, showing how to separate domain recording from infrastructure publishing.",
        Author = "Abdul Rahman",
        Slug = "ddd-publishing-and-handling-domain-events-in-dotnet",
        PosterUrl = "image/blogs/ddd/ddd-publishing-and-handling-domain-events-in-dotnet.webp",
        ThumbnailUrl = "image/blogs/ddd/ddd-publishing-and-handling-domain-events-in-dotnet.webp",
        ContentUrl = "blogs/ddd-publishing-and-handling-domain-events-in-dotnet",
        IconUrl = "image/icons/ddd.webp",
        Channel = "DDD",
        Type = "blogs",
        CreatedOn = new DateTime(2026, 6, 21, 22, 30, 0, DateTimeKind.Utc),
        ModifiedOn = new DateTime(2026, 6, 21, 22, 30, 0, DateTimeKind.Utc),
        Keywords = ["Domain-Driven Design", "DDD", "Domain Events", "Event Collection Pattern", "Application Service", "Event Handlers", "Idempotency", ".NET", "C#", "Event-Driven Architecture"]
      },
      new ContentMetaData
      {
        Order = 14,
        Title = "Repository Pattern Done Right in DDD with .NET",
        Description = "In this post I will teach you how to implement the Repository Pattern correctly in DDD as a collection-like abstraction that hides persistence completely, with the interface defined in the domain layer.",
        Author = "Abdul Rahman",
        Slug = "ddd-repository-pattern-done-right-in-dotnet",
        PosterUrl = "image/blogs/ddd/ddd-repository-pattern-done-right-in-dotnet.webp",
        ThumbnailUrl = "image/blogs/ddd/ddd-repository-pattern-done-right-in-dotnet.webp",
        ContentUrl = "blogs/ddd-repository-pattern-done-right-in-dotnet",
        IconUrl = "image/icons/ddd.webp",
        Channel = "DDD",
        Type = "blogs",
        CreatedOn = new DateTime(2026, 6, 28, 22, 30, 0, DateTimeKind.Utc),
        ModifiedOn = new DateTime(2026, 6, 28, 22, 30, 0, DateTimeKind.Utc),
        Keywords = ["Domain-Driven Design", "DDD", "Repository Pattern", "Aggregate Root", "Persistence", "Collection Abstraction", "Domain Interface", ".NET", "C#", "EF Core"]
      },
      new ContentMetaData
      {
        Order = 15,
        Title = "Thin Application Services in DDD with .NET",
        Description = "In this post I will teach you how to keep application services thin by making them pure coordinators that load aggregates, call domain methods, save results, and publish events without containing any business logic.",
        Author = "Abdul Rahman",
        Slug = "ddd-thin-application-services-in-dotnet",
        PosterUrl = "image/blogs/ddd/ddd-thin-application-services-in-dotnet.webp",
        ThumbnailUrl = "image/blogs/ddd/ddd-thin-application-services-in-dotnet.webp",
        ContentUrl = "blogs/ddd-thin-application-services-in-dotnet",
        IconUrl = "image/icons/ddd.webp",
        Channel = "DDD",
        Type = "blogs",
        CreatedOn = new DateTime(2026, 7, 5, 22, 30, 0, DateTimeKind.Utc),
        ModifiedOn = new DateTime(2026, 7, 5, 22, 30, 0, DateTimeKind.Utc),
        Keywords = ["Domain-Driven Design", "DDD", "Application Services", "Coordination Layer", "Transaction Boundary", "Separation of Concerns", ".NET", "C#", "Clean Architecture", "Domain Orchestration"]
      },
      new ContentMetaData
      {
        Order = 16,
        Title = "Testing Domain Models in DDD with .NET",
        Description = "In this post I will teach you how to test a DDD domain model in .NET using Given-When-Then patterns, ubiquitous language in test names, test builders, event assertions, idempotency testing, and integration fixtures.",
        Author = "Abdul Rahman",
        Slug = "ddd-testing-domain-models-in-dotnet",
        PosterUrl = "image/blogs/ddd/ddd-testing-domain-models-in-dotnet.webp",
        ThumbnailUrl = "image/blogs/ddd/ddd-testing-domain-models-in-dotnet.webp",
        ContentUrl = "blogs/ddd-testing-domain-models-in-dotnet",
        IconUrl = "image/icons/ddd.webp",
        Channel = "DDD",
        Type = "blogs",
        CreatedOn = new DateTime(2026, 7, 12, 22, 30, 0, DateTimeKind.Utc),
        ModifiedOn = new DateTime(2026, 7, 12, 22, 30, 0, DateTimeKind.Utc),
        Keywords = ["Domain-Driven Design", "DDD", "Testing", "Given-When-Then", "Test Builders", "Domain Events", "Idempotency", "Test Fixtures", ".NET", "C#", "xUnit", "Unit Tests"]
      },
      new ContentMetaData
      {
        Order = 17,
        Title = "Bounded Contexts in Practice - Strategic DDD for Clean System Design in .NET",
        Description = "In this post I will teach you the most important strategic concept in DDD - Bounded Contexts. Learn why a single shared model always fails, how to split bloated models into intentional contexts, and how those contexts integrate using domain events.",
        Author = "Abdul Rahman",
        Slug = "ddd-bounded-contexts-in-practice-strategic-ddd-in-dotnet",
        PosterUrl = "image/blogs/ddd/ddd-bounded-contexts-in-practice-strategic-ddd-in-dotnet.webp",
        ThumbnailUrl = "image/blogs/ddd/ddd-bounded-contexts-in-practice-strategic-ddd-in-dotnet.webp",
        ContentUrl = "blogs/ddd-bounded-contexts-in-practice-strategic-ddd-in-dotnet",
        IconUrl = "image/icons/ddd.webp",
        Channel = "DDD",
        Type = "blogs",
        CreatedOn = new DateTime(2026, 7, 19, 22, 30, 0, DateTimeKind.Utc),
        ModifiedOn = new DateTime(2026, 7, 19, 22, 30, 0, DateTimeKind.Utc),
        Keywords = ["Domain-Driven Design", "DDD", "Bounded Contexts", "Strategic Design", "Context Integration", "Domain Events", "System Design", ".NET", "C#", "Microservices"]
      },
      new ContentMetaData
      {
        Order = 18,
        Title = "Context Maps and Relationships in DDD - Visualizing Bounded Context Integrations in .NET",
        Description = "In this post I will teach you how to use Context Maps to document and implement relationships between Bounded Contexts, covering Customer-Supplier, Partnership, and Anti-Corruption Layer patterns with real C# examples.",
        Author = "Abdul Rahman",
        Slug = "ddd-context-maps-and-relationships-in-dotnet",
        PosterUrl = "image/blogs/ddd/ddd-context-maps-and-relationships-in-dotnet.webp",
        ThumbnailUrl = "image/blogs/ddd/ddd-context-maps-and-relationships-in-dotnet.webp",
        ContentUrl = "blogs/ddd-context-maps-and-relationships-in-dotnet",
        IconUrl = "image/icons/ddd.webp",
        Channel = "DDD",
        Type = "blogs",
        CreatedOn = new DateTime(2026, 7, 26, 22, 30, 0, DateTimeKind.Utc),
        ModifiedOn = new DateTime(2026, 7, 26, 22, 30, 0, DateTimeKind.Utc),
        Keywords = ["Domain-Driven Design", "DDD", "Context Maps", "Bounded Contexts", "Customer-Supplier", "Partnership", "Anti-Corruption Layer", "Strategic Design", ".NET", "C#"]
      },
      new ContentMetaData
      {
        Order = 19,
        Title = "Anti-Corruption Layer Pattern in DDD - Protecting Your Domain from External Systems in .NET",
        Description = "In this post I will teach you how to implement the Anti-Corruption Layer pattern in .NET to protect your domain model from external system concepts, with a complete email gateway example using ports, adapters, and translators.",
        Author = "Abdul Rahman",
        Slug = "ddd-anti-corruption-layer-pattern-in-dotnet",
        PosterUrl = "image/blogs/ddd/ddd-anti-corruption-layer-pattern-in-dotnet.webp",
        ThumbnailUrl = "image/blogs/ddd/ddd-anti-corruption-layer-pattern-in-dotnet.webp",
        ContentUrl = "blogs/ddd-anti-corruption-layer-pattern-in-dotnet",
        IconUrl = "image/icons/ddd.webp",
        Channel = "DDD",
        Type = "blogs",
        CreatedOn = new DateTime(2026, 8, 2, 22, 30, 0, DateTimeKind.Utc),
        ModifiedOn = new DateTime(2026, 8, 2, 22, 30, 0, DateTimeKind.Utc),
        Keywords = ["Domain-Driven Design", "DDD", "Anti-Corruption Layer", "ACL", "External Integration", "Ports and Adapters", "Domain Protection", ".NET", "C#", "Clean Architecture"]
      },
      new ContentMetaData
      {
        Order = 20,
        Title = "Onion Architecture in DDD - Keeping Your Domain Pure and Testable in .NET",
        Description = "In this post I will teach you how to implement Onion Architecture in .NET to keep domain logic pure and infrastructure-independent, with a complete request flow walkthrough using a licensing domain example.",
        Author = "Abdul Rahman",
        Slug = "ddd-onion-architecture-in-dotnet",
        PosterUrl = "image/blogs/ddd/ddd-onion-architecture-in-dotnet.webp",
        ThumbnailUrl = "image/blogs/ddd/ddd-onion-architecture-in-dotnet.webp",
        ContentUrl = "blogs/ddd-onion-architecture-in-dotnet",
        IconUrl = "image/icons/ddd.webp",
        Channel = "DDD",
        Type = "blogs",
        CreatedOn = new DateTime(2026, 8, 9, 22, 30, 0, DateTimeKind.Utc),
        ModifiedOn = new DateTime(2026, 8, 9, 22, 30, 0, DateTimeKind.Utc),
        Keywords = ["Domain-Driven Design", "DDD", "Onion Architecture", "Clean Architecture", "Domain Layer", "Application Layer", "Infrastructure Layer", ".NET", "C#", "EF Core"]
      },
      new ContentMetaData
      {
        Order = 21,
        Title = "Ports and Adapters Pattern in DDD - Implementing Hexagonal Architecture in .NET",
        Description = "In this post I will teach you how to implement the Ports and Adapters pattern in .NET with a complete five-step walkthrough: define the port, create an in-memory adapter for tests, create an EF Core adapter for production, swap adapters, and protect contracts with shared tests.",
        Author = "Abdul Rahman",
        Slug = "ddd-ports-and-adapters-pattern-in-dotnet",
        PosterUrl = "image/blogs/ddd/ddd-ports-and-adapters-pattern-in-dotnet.webp",
        ThumbnailUrl = "image/blogs/ddd/ddd-ports-and-adapters-pattern-in-dotnet.webp",
        ContentUrl = "blogs/ddd-ports-and-adapters-pattern-in-dotnet",
        IconUrl = "image/icons/ddd.webp",
        Channel = "DDD",
        Type = "blogs",
        CreatedOn = new DateTime(2026, 8, 16, 22, 30, 0, DateTimeKind.Utc),
        ModifiedOn = new DateTime(2026, 8, 16, 22, 30, 0, DateTimeKind.Utc),
        Keywords = ["Domain-Driven Design", "DDD", "Ports and Adapters", "Hexagonal Architecture", "Repository Pattern", "Contract Tests", "In-Memory Adapter", ".NET", "C#", "xUnit"]
      }
    ];
  }
}
