# SharpGrip OpenIddict API [![NuGet](https://img.shields.io/nuget/v/SharpGrip.OpenIddict.Api)](https://www.nuget.org/packages/SharpGrip.OpenIddict.Api)

## Builds

[![OpenIddict.Api [Build]](https://github.com/SharpGrip/OpenIddict.Api/actions/workflows/Build.yaml/badge.svg)](https://github.com/SharpGrip/OpenIddict.Api/actions/workflows/Build.yaml)

## Introduction

SharpGrip OpenIddict API is an extension of the OpenIddict library exposing the OpenIddict entities through a RESTful API.

## Installation

Reference NuGet package `SharpGrip.OpenIddict.Api` (https://www.nuget.org/packages/SharpGrip.OpenIddict.Api).

Follow the OpenIddict installation guide [Getting started](https://documentation.openiddict.com/guides/getting-started.html) and call the `AddApi<TKey>` extension method on
the `OpenIddictEntityFrameworkCoreBuilder`.

```csharp
serviceCollection.AddOpenIddict().AddCore(options =>
{
    options.UseEntityFrameworkCore()
        .UseDbContext<ApplicationDbContext>()
        .AddApi<long>();
});
```

### Configuration

In case you are overriding the OpenIddict entities you need to use the `AddApi<TApplication, TAuthorization, TScope, TToken, TKey>` overload to make sure the API is working with the correct entity types.

Both `AddApi` extension methods expose an options builder with which you can configure your API. In the example below the `Application` API route will become `/open-id-api/application` and the endpoints exposed in that route will require an
access token with the `my_application_access_scope` scope.

```csharp
serviceCollection.AddOpenIddict().AddCore(options =>
{
    options.UseEntityFrameworkCore()
        .UseDbContext<ApplicationDbContext>()
        .AddApi<OpenIdApplication, OpenIdAuthorization, OpenIdScope, OpenIdToken, long>(apiOptions =>
        {
            apiOptions.ApiRoutePrefix = "open-id-api";
            apiOptions.ApplicationApiRoute = "application";
            apiOptions.ApplicationApiAccessScope = "my_application_access_scope";
        });
});
```

### Properties

| Property                    | Default value                      | Description                                                      |
|-----------------------------|------------------------------------|------------------------------------------------------------------|
| ApiRoutePrefix              | `api/open-id`                      | The prefix used in all the API routes.                           |
| ApplicationApiRoute         | `application`                      | The `Application` API route.                                     |
| ApplicationApiAccessScope   | `open_id_application_api_access`   | The access scope needed to access the `Application` endpoints.   |
| AuthorizationApiRoute       | `authorization`                    | The `Authorization` API route.                                   |
| AuthorizationApiAccessScope | `open_id_authorization_api_access` | The access scope needed to access the `Authorization` endpoints. |
| ScopeApiRoute               | `scope`                            | The `Scope` API route.                                           |
| ScopeApiAccessScope         | `open_id_scope_api_access`         | The access scope needed to access the `Scope` endpoints.         |
| TokenApiRoute               | `token`                            | The `Token` API route.                                           |
| TokenApiAccessScope         | `open_id_token_api_access`         | The access scope needed to access the `Token` endpoints.         |

## Supported operations

Please find below an overview of the supported operations using the default route configuration.

### Application

| Method   | Endpoint                       | Description                   |
|----------|--------------------------------|-------------------------------|
| `GET`    | `api/open-id/application`      | Returns all applications.     |
| `GET`    | `api/open-id/application/{id}` | Returns an application by ID. |
| `POST`   | `api/open-id/application`      | Creates an application.       |
| `PUT`    | `api/open-id/application/{id}` | Updates an application by ID. |
| `DELETE` | `api/open-id/application/{id}` | Deletes an application by ID. |

### Authorization

| Method | Endpoint                       | Description                     |
|--------|--------------------------------|---------------------------------|
| `GET`  | `api/open-id/authorization`    | Returns all authorizations.     |
| `GET`  | `api/open-id/application/{id}` | Returns an authorization by ID. |

### Scope

| Method   | Endpoint                 | Description            |
|----------|--------------------------|------------------------|
| `GET`    | `api/open-id/scope`      | Returns all scopes.    |
| `GET`    | `api/open-id/scope/{id}` | Returns a scope by ID. |
| `POST`   | `api/open-id/scope`      | Creates a scope.       |
| `PUT`    | `api/open-id/scope/{id}` | Updates a scope by ID. |
| `DELETE` | `api/open-id/scope/{id}` | Deletes a scope by ID. |

### Token

| Method | Endpoint                 | Description            |
|--------|--------------------------|------------------------|
| `GET`  | `api/open-id/token`      | Returns all tokens.    |
| `GET`  | `api/open-id/token/{id}` | Returns a token by ID. |

