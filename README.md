# Recipes
This is an simple recipes application implementation.
API includes custom authentication method based on JWT and role-based model. It is also provides CRU methods to work with recipe.

## Solution structure
| Directory | Description |
| ------ | ------------------------------------------------------------ |
| client | Directory with API Client generated during the build process |
| src | Directory with source code of API |
| src\core | Contains business logic layer |
| src\presentation | Contains API application |
| tests | Directory with test projects |

## Used external packages
* AutoMapper - for mapping entities to DTOs
* MediatR - for CQRS way
* Swagger - for generation web and json documentation
* NSwag - for API client generation