# Enterprise Audit & Delivery System — Clean Architecture Monorepo

[![Platform](https://img.shields.io/badge/.NET-9.0-blue.svg)](https://dotnet.microsoft.com/)
[![Angular](https://img.shields.io/badge/Angular-17+-red.svg)](https://angular.dev/)
[![React](https://img.shields.io/badge/React-18+-blue.svg)](https://react.dev/)
[![Vue](https://img.shields.io/badge/Vue-3+-green.svg)](https://vuejs.org/)

This repository implements a production-ready, highly scalable **Enterprise Audit and Delivery Management System**. The core focus of this project is to demonstrate clean code principles, high-performance database tuning, and agnostic API contract design by serving four distinct frontend environments from a single backend.

> 🚧 **Status: Under Active Construction** > *The architectural skeleton and database persistence layers have been successfully scaffolded, verified, and migrated. Active development is shifting towards AI integrations.*

---

## 🏗️ System Architecture

The project is structured as a **Monorepo** divided into a highly decoupled backend and interchangeable, modern frontend clients.

```text
/senior-audit-monorepo
│
├── /backend
│   ├── /database            # DB Seeds, SQL maintenance, and DB Tuning assets
│   ├── /src
│   │   ├── /Domain          # Enterprise Business Rules & Immutable Entities
│   │   ├── /Infrastructure  # EF Core, SQL Server Query Tuning & External Services
│   │   └── /API             # REST Endpoints, CORS Policy & Custom Middlewares
│   └── Backend.sln
│
└── /frontends
    ├── /angular-audit-app   # Client with Angular (Standalone, Control Flow, RxJS)
    ├── /react-audit-app     # Client with React (Vite, TS, Hooks, Strict ESLint)
    ├── /vue-audit-app       # Client with Vue 3 (Vite, TS, Composition API)
    └── /mvc-audit-app       # Client with ASP.NET Core MVC (C# & Razor Views for SSR)

🚀 Key Architectural & Technical Features
1. Backend Core (.NET 9 & Clean Architecture)
Domain-Driven Focus: Zero external dependencies in the Domain layer to ensure pristine business logic.

Decoupled Infrastructure: Integration with SQL Server optimized via Advanced Query Tuning, clean migrations, and custom retention strategies.

Modern Web API: Lightweight controllers featuring strict DTO contracts, robust Exception Handling middleware, and custom CORS policies.

2. AI-Assisted Engineering & Gemini Integration (AI Orchestration)
AI-Assisted SDLC: Use of workspace-level system instructions (gemini-instructions.md) and specialized Markdown prompt templates to standardize AI context, ensuring generated code adheres to our architectural boundaries.

Gemini Flash Core Integration: Direct integration within our backend infrastructure utilizing the Gemini API (Flash model) for automated log processing, pattern recognition in audit trails, and automated error diagnostics.

3. Multi-Frontend Ecosystem
Interchangeable Clients: Angular, React, Vue 3, and ASP.NET MVC all consuming the same underlying API contracts.

Strict Tooling: Configured with strict TypeScript compilers, ESLint rulesets, and native server-side rendering (SSR) via MVC to validate real-world production constraints.

## 🤖 Developer AI Orchestration
This monorepo utilizes **Cursor** paired with **Gemini** to accelerate development safely and systematically:
* **System-wide Rules:** Guided by `.cursorrules` to enforce C# 13 clean architecture practices and strict frontend structures.
* **Specialized Prompts:** Reusable development templates located in `.github/prompts/` to run isolated routines such as `generate-unit-tests` and `optimize-sql-query`.

## 🗺️ Implementation Roadmap

* [x] **Phase 1: Project Scaffolding & Tooling Verification** *Backend Solution + 4 Frontend clients configured & linted.*
* [x] **Phase 2: Domain Modeling & Database Persistence** *Entity Framework Core, SQL migrations applied, Scalar UI integrated, and SQL Dev-Seeding.*
* [x] **Phase 3: AI Orchestration & Developer Environment Tuning** *Configured root-level `.cursorrules` to standardize coding patterns across backend and all four frontends. Integrated reusable markdown prompt templates under `.github/prompts/` for Unit Testing and SQL Optimization using Gemini.*
* [ ] **Phase 4: Core API & CORS Integration** *Controller pipelines, custom error handling middleware, and secure multi-origin routing.*
* [ ] **Phase 5: Multi-Frontend Development** *State management, reusable component structures, and API consumption across all four clients.*
* [ ] **Phase 6: High-Performance Database Tuning** *Execution of database query optimization, performance indexes, telemetry logging, and SQL Server execution plans.*