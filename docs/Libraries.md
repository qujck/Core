---
layout: default
title: Libraries
---

----------

### Inversion of Control

[Simple Injector](https://simpleinjector.org)

----------

### SOAP

[WCF](http://en.wikipedia.org/wiki/Windows_Communication_Foundation) is used by both the Web and App tiers to communicate with EngageOne.

----------

### REST

[RestSharp](http://restsharp.org/) is used for communication between the Web and App tiers.

----------

### Database

[Dapper](https://code.google.com/p/dapper-dot-net/)

----------

### Error handling

The application uses a [modified](http://qujck.com/elmah-in-the-enterprise/) installation of [Elmah](https://code.google.com/p/elmah/). Elmah is designed to store errors on the hard drive or in a database. The modification is to send the error messages to the App layer for logging in the database.

----------

### Unit and Integration Tests

[xUnit](http://xunit.github.io/)

----------
