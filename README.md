# EcoChef
 EcoChef — Restaurant ERP System

Bachelor's thesis project, Economic Informatics @ FSEGA, Babeș-Bolyai University
Author: Iulia-Raluca Vișan · Coordinator: Conf. univ. dr. Loredana Mocean

What is this?

EcoChef is a small ERP system I built for restaurants, aimed at a problem that's pretty common in the HoReCa world: stock and costs are still tracked manually in a lot of small places, usually on paper or in scattered spreadsheets. That leads to food waste and pricing that's more guesswork than actual calculation.

EcoChef replaces that with one system where a restaurant's ingredients, recipes, cooking, and losses are all tracked in one place, with automatic cost calculations instead of manual ones.

What it does


Tracks ingredients and stock levels, including expiration dates
Lets you define recipes, and automatically calculates cost and profit per dish based on ingredient prices and a configurable profit margin
Logs cooking events and deducts stock automatically
Tracks losses (both inventory and financial), so waste is actually visible instead of guessed at
Dashboard that flags ingredients about to expire and suggests recipes that use them up before they go bad
Three separate roles (Admin, Manager, Chef) with different permissions


Built with

ASP.NET Core 8, Razor Pages, Entity Framework Core, and SQL Server LocalDB for the backend and data layer, ASP.NET Core Identity for authentication, and Chart.js for the reporting side. The UI has a custom design (logo made in Figma).

How it's designed

Before writing any code, I mapped out the actual process first: a BPMN diagram of how a restaurant handles this manually (As-Is), then how it would work with EcoChef (To-Be), plus a Fishbone diagram to dig into why food waste happens in the first place. From there I moved to a UML Use Case diagram for the three roles, an ERD for the data model, and a Deployment diagram for the overall architecture (browser → ASP.NET Core app → SQL Server, over HTTPS).


Testing

Every module was tested manually in-browser against realistic scenarios, plus specific edge cases like trying to cook something with insufficient stock, or accidentally creating a duplicate role. Found a few of these along the way and added proper validation messages instead of silent failures.

What's next

Some ideas I'd want to explore if I kept building this: supplier integration for automatic ordering, a mobile version, and predictive alerts based on consumption patterns instead of just fixed expiration dates.
