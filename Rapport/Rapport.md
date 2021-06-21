# Snackis

Snackis är ett diskussionsforum där användare kan skapa konton och diskutera olika ämnen.
Startdatum: 17 Maj 2021 - Slutdatum: 18 Juni 2021
Timmar: 160 - 175

## Tekniska krav:
* Windows
* IIS
* .Net Core 5
* Nuget-paket:
	* Microsoft.AspNetCore.Identity.EntityFrameworkCore
	* Microsoft.AspNetCore.Identity.UI
	* Microsoft.EntityFrameworkCore.SqlServer
	* Microsoft.EntityFrameworkCore.Tools
	* Microsoft.VisualStudio.Web.CodeGeneration.Design
* MS SQL
* Azure App Service

## Applikationsstruktur

Gjort med Razor Pages och har projekten SnackisWebApp.sln och SnackisAPI.sln.

Startup har följande dependencies:
* SnackisUserContext SqlServer DI
* Identity SnackisUser DI
* CategoryGateway API-gateway DI
* SubcategoryGateway API-gateway DI
* PostGateway API-gateway DI
* CommentGateway API-gateway DI
* ReportGateway API-gateway DI
* MessageGateway API-Gateway DI

API Controllers:
* CategoriesController
* CommentsController
* MessagesController
* PostsController
* ReportsController
* SubCategoriesController

Micro Services:
* Snackis API: SnackisAPI.sln

## Installation

För att installera via Azure, så behövs en App Service plan, två App Services och en SQL server med två SQL databaser på.
Lägg databas connection-strängarna i appsetings.json, en för SnackisWebApp och en för SnackisAPI.
SnackisAPI behöver köra en "Update-Database".
Kör "Publish" via Visual Studio och följ instruktionerna.

När detta är klart och applikationen är igång ska två roller (User, Admin) och ett Admin konto (Admin, AdminPassword1!) ha skapats.

## API Endpoints

|### Controllers |
|----------------|
|#### Categories |
|----------------|
| Endpoint | Beskrivning |
|------------------------|
|GET: /api/Categories | Hämtar alla kategorier |
|----------------------------------------------|
|POST: /api/Categories | Skapar en kategori |
|-------------------------------------------|
|GET: /api/Categories/{id} | Hämtar en kategori via id |
|------------------------------------------------------|
|PUT: /api/Categories/{id} | Ändrar en kategori via id |
|------------------------------------------------------|
|DELETE: /api/Categories/{id} | Tar bort en kategori via id |
|-----------------------------------------------------------|
|#### Comments   |
|----------------|
| Endpoint | Beskrivning |
|------------------------|
|GET: /api/Comments | Hämtar alla kommentarer |
|---------------------------------------------|
|POST: /api/Comments | Skapar en kommentar |
|------------------------------------------|
|GET: /api/Comments/{id} | Hämtar en kommentar via id |
|-----------------------------------------------------|
|PUT: /api/Comments/{id} | Ändrar en kommentar via id |
|-----------------------------------------------------|
|DELETE: /api/Comments/{id} | Tar bort en kommentar via id |
|----------------------------------------------------------|
|GET: /api/Comments/PostId/{postId} | Hämtar alla kommentarer baserat på postId |
|-------------------------------------------------------------------------------|
|#### Messages   |
|----------------|
| Endpoint | Beskrivning |
|------------------------|
|GET: /api/Messages | Hämtar alla meddelanden |
|---------------------------------------------|
|POST: /api/Messages | Skapar ett meddelande |
|--------------------------------------------|
|GET: /api/Messages/{id} | Hämtar ett meddelande via id |
|-------------------------------------------------------|
|PUT: /api/Messages/{id} | Ändrar ett meddelande via id |
|-------------------------------------------------------|
|DELETE: /api/Messages/{id} | Tar bort ett meddelande via id |
|------------------------------------------------------------|
|GET: /api/Messages/UserId/{userId} | Hämtar alla meddelanden baserat userId |
|------------------------------------------------------------------------|
|GET: /api/Messages/Unread/{userId} | Hämtar hur många olästa meddelanden baserat på userId |
|-------------------------------------------------------------------------------------------|
|#### Posts      |
|----------------|
| Endpoint | Beskrivning |
|------------------------|
|GET: /api/Posts | Hämtar alla inlägg |
|-------------------------------------|
|POST: /api/Posts | Skapar ett inlägg |
|-------------------------------------|
|GET: /api/Posts/{id} | Hämtar ett inlägg via id |
|------------------------------------------------|
|PUT: /api/Posts/{id} | Ändrar ett inlägg via id |
|------------------------------------------------|
|DELETE: /api/Posts/{id} | Tar bort ett inlägg via id |
|-----------------------------------------------------|
|GET: /api/Posts/subcategoryId/{subcategoryId} | Hämtar alla inlägg baserat på subcategoryId |
|--------------------------------------------------------------------------------------------|
|#### Reports    |
|----------------|
| Endpoint | Beskrivning |
|------------------------|
|GET: /api/Reports | Hämtar alla rapporter |
|------------------------------------------|
|POST: /api/Reports | Skapar en rapport |
|---------------------------------------|
|GET: /api/Reports/{id} | Hämtar en rapport via id |
|--------------------------------------------------|
|PUT: /api/Reports/{id} | Ändrar en rapport via id |
|--------------------------------------------------|
|DELETE: /api/Reports/{id} | Tar bort en rapport via id |
|-------------------------------------------------------|
|#### Subcategories |
|-------------------|
| Endpoint | Beskrivning |
|------------------------|
|GET: /api/SubCategories | Hämtar alla underkategorier |
|------------------------------------------------------|
|POST: /api/SubCategories | Skapar en underkategori |
|---------------------------------------------------|
|GET: /api/SubCategories/{id} | Hämtar en underkategori via id |
|--------------------------------------------------------------|
|PUT: /api/SubCategories/{id} | Ändrar en underkategori via id |
|--------------------------------------------------------------|
|DELETE: /api/SubCategories/{id} | Tar bort en underkategori via id |
|-------------------------------------------------------------------|

## Funktionalitet

Som besökare kan man registrera sig / logga in, välja kategori / underkategori och läsa inlägg / kommentarer.
Som användare kan man ladda upp en profilbild, skapa inlägg / kommentarer, skicka meddelanden och skapa rapporter.
Som admin kan du gör det som användare kan, men även hantera användare och deras roller, hantera roller, hantera kategorier / underkategorier
och hantera rapporter.

## Brister / Förbättringsförslag

Användare bör kunna ändra / ta bort inlägg eller kommentarer.
Meddelande sidans ui är buggig och bör ändras.
Admins bör kunna ändra kategori / underkategori namn.
Säkra SnackisAPI med CORS.
SnackisWebApp som SPA istället för Razor pages.

## Summering

Arbetet har gått bra och jag har lärt mig en hel del om Identity och Razor pages mot API.
De flesta utmaningar som jag har stött på har varit frontend,(Bootstrap) så det är något jag vill bli bättre på.

Tonny Frisk