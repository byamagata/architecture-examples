# Hexagonal vs. Vertical Slice vs. Slimpify Architecture

## Introduction

This repo is a demo of each architecture implemented with .net 8 and aimed towards microservices.
In each example, we will implement a simple user management API containing the following endpoints:

- GET /users
- GET /users/{id}
- POST /users
- PUT /users/{id}
- DELETE /users/{id}

This should give a good idea of how each architecture can be implemented and how it can be tested.

The details of the API can be found in the openapi.json file at the root of this repo.

## Hexagonal Architecture (Ports and Adapters)


## Vertical Slice Architecture


## Slimpify Architecture

This is something of my own creation.  Something that I have been developing for a few months now as I consider the pros and cons of other architecture styles as well as the changing landscape of software development.  It is a mix of the other two architectures and is designed to be as simple as possible while still being able to scale and be maintainable.  Specifically designed for microservices.
