╔════════════════════════════════════════════════════════════════════════════╗
║                        EVENT EASE - ATTRIBUTIONS                            ║
║                     Code Credits and Third-Party Resources                  ║
╚════════════════════════════════════════════════════════════════════════════╝

PROJECT: EventEase Event Management System
FRAMEWORK: ASP.NET Core MVC with Entity Framework Core
TARGET: .NET 10
AUTHOR: Bongumusa Cele

═════════════════════════════════════════════════════════════════════════════

THIRD-PARTY LIBRARIES & FRAMEWORKS:

1. ✅ ASPNET CORE FRAMEWORK
   ─────────────────────────
   - Framework: Microsoft ASP.NET Core
   - Version: .NET 10
   - License: MIT
   - Source: https://github.com/dotnet/aspnetcore
   - Usage: Web application framework, MVC routing, authentication

2. ✅ ENTITY FRAMEWORK CORE
   ─────────────────────────
   - Package: Microsoft.EntityFrameworkCore
   - Version: 10.0.3
   - License: MIT
   - Source: https://github.com/dotnet/efcore
   - Usage: Object-Relational Mapping (ORM), database operations

3. ✅ SQL SERVER PROVIDER
   ──────────────────────
   - Package: Microsoft.EntityFrameworkCore.SqlServer
   - Version: 10.0.3
   - License: MIT
   - Source: https://github.com/dotnet/efcore
   - Usage: SQL Server database provider for EF Core

4. ✅ BCRYPT.NET-NEXT
   ──────────────────
   - Package: BCrypt.Net-Next
   - Version: 4.0.3
   - License: MIT
   - Source: https://github.com/BcryptNet/bcrypt.net
   - Usage: Password hashing and verification

5. ✅ FONT AWESOME ICONS
   ────────────────────
   - Name: Font Awesome 6.4.0
   - License: CC BY 4.0 License (free icons)
   - CDN: https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/
   - Source: https://fontawesome.com
   - Usage: Icon library for UI elements throughout the application

═════════════════════════════════════════════════════════════════════════════

DESIGN PATTERNS & RESOURCES:

1. ✅ MODERN DASHBOARD DESIGN PATTERN
   ────────────────────────────────
   - Pattern: Sidebar navigation + content layout
   - Inspiration: Common SaaS dashboard patterns
   - Implementation: Custom CSS with responsive design
   - Responsive Breakpoints:
     • Mobile: 320px - 640px
     • Tablet: 641px - 1024px
     • Desktop: 1025px+

2. ✅ FORM DESIGN PATTERN
   ─────────────────────
   - Pattern: Card-based centered form layout
   - Design: Modern gradient buttons, icon labels
   - Validation: Bootstrap validation summary pattern
   - Inspiration: Material Design principles

3. ✅ CSS VARIABLES (CUSTOM PROPERTIES)
   ────────────────────────────────────
   - Pattern: Root-level CSS variable system
   - Implementation: Color palette, spacing, shadows
   - Reference: CSS3 Custom Properties specification
   - Source: https://developer.mozilla.org/en-US/docs/Web/CSS/--*

4. ✅ RESPONSIVE WEB DESIGN
   ────────────────────────
   - Approach: Mobile-first responsive design
   - Breakpoints: Mobile, Tablet, Desktop
   - Pattern: Flexible grid layouts, media queries
   - Reference: https://www.w3schools.com/css/css_rwd_intro.asp

═════════════════════════════════════════════════════════════════════════════

ARCHITECTURE & PATTERNS:

1. ✅ MVC (MODEL-VIEW-CONTROLLER)
   ──────────────────────────────
   - Pattern: ASP.NET Core MVC architecture
   - Models: Domain models with validation attributes
   - Views: Razor templates with HTML markup
   - Controllers: Action methods for request handling

2. ✅ AUTHENTICATION & AUTHORIZATION
   ──────────────────────────────────
   - Pattern: Cookie-based authentication
   - Framework: ASP.NET Core Identity concepts
   - Implementation: Custom AuthController with role-based access
   - Roles: Admin, BookingSpecialist, Customer

3. ✅ ENTITY FRAMEWORK CONVENTIONS
   ──────────────────────────────
   - Pattern: Code-First approach with DbContext
   - Relationships: Foreign keys, navigation properties
   - Migrations: Database schema versioning
   - Conventions: EF Core naming and mapping conventions

4. ✅ DATA ANNOTATIONS (VALIDATION)
   ────────────────────────────────
   - Pattern: Declarative validation using attributes
   - Attributes:
     • [Required] - Required field validation
     • [StringLength] - String length constraints
     • [EmailAddress] - Email format validation
     • [Phone] - Phone number validation
     • [Range] - Numeric range validation
   - Reference: https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations

═════════════════════════════════════════════════════════════════════════════

UI/UX RESOURCES:

1. ✅ COLOR PALETTE
   ────────────────
   - Primary: Indigo (#6366f1) - Modern, professional
   - Accent: Pink (#ec4899) - Complementary highlight
   - Success: Green (#10b981) - Positive actions
   - Warning: Amber (#f59e0b) - Caution/warning states
   - Danger: Red (#ef4444) - Destructive actions
   - Grays: Neutral color scale

2. ✅ TYPOGRAPHY
   ──────────────
   - Font Stack: System fonts (-apple-system, Segoe UI, Roboto, Ubuntu)
   - Line Height: 1.5 for body text (accessibility best practice)
   - Font Weights: 400 (regular), 500 (medium), 600 (semibold), 700 (bold)

3. ✅ SPACING SYSTEM
   ─────────────────
   - Base Unit: 0.25rem (4px)
   - Scale: xs (0.25rem), sm (0.5rem), md (1rem), lg (1.5rem), xl (2rem), 2xl (3rem)
   - Reference: Tailwind CSS spacing scale

4. ✅ SHADOWS & ELEVATION
   ───────────────────────
   - Pattern: Subtle box-shadow for depth perception
   - Levels: sm, md, lg, xl for different elevations
   - Usage: Cards, buttons, modals for visual hierarchy

═════════════════════════════════════════════════════════════════════════════

AUTHENTICATION PATTERN SOURCE:

The cookie-based authentication implementation follows ASP.NET Core documentation:
- Microsoft Docs: Authentication in ASP.NET Core
- Reference: https://learn.microsoft.com/en-us/aspnet/core/security/authentication
- Pattern: Custom authentication middleware with cookie claims

═════════════════════════════════════════════════════════════════════════════

BROWSER SUPPORT:

- Modern browsers (ES6+ JavaScript support)
- Chrome, Firefox, Safari, Edge (latest versions)
- Mobile browsers (iOS Safari, Chrome Mobile)
- Responsive CSS media queries for all screen sizes

═════════════════════════════════════════════════════════════════════════════

OPEN SOURCE LICENSES:

✅ MIT License (Most dependencies)
   - Allows: Commercial use, modification, distribution, private use
   - Conditions: License and copyright notice must be included
   - Limitations: No warranty/liability

✅ Font Awesome CC BY 4.0
   - Free icons available under Creative Commons
   - Requires: Attribution (link to fontawesome.com)
   - Premium icons available at: https://fontawesome.com/plans

═════════════════════════════════════════════════════════════════════════════

DEVELOPMENT CREDITS:

- IDE: Microsoft Visual Studio Community 2026
- Version Control: Git
- Repository: GitHub (https://github.com/BongumusaCele/EventEase)
- Language: C# 14.0
- Framework: .NET 10

═════════════════════════════════════════════════════════════════════════════

SPECIAL THANKS:

✅ Microsoft - ASP.NET Core framework and tooling
✅ .NET Foundation - Open-source .NET ecosystem
✅ Font Awesome - Icon library
✅ StackOverflow community - Programming solutions
✅ Microsoft Learn - Documentation and best practices

═════════════════════════════════════════════════════════════════════════════

CREATION DATE: 2025
LAST UPDATED: 2025-02-25
