# RecipeManager_API

## Übersicht
Eine REST-API für die Verwaltung von Rezepten.  
Sie wurde mit ASP.NET Core Web API umgesetzt und verwendet eine In-Memory-Datenbank mit Entity Framework Core. 

---

## API-Vertrag

Die Kommunikation erfolgt über JSON und HTTP-Statuscodes.

### Endpunkte

#### GET /api/recipes
Gibt alle Rezepte zurück (optional mit Filter).

Query-Parameter:
- category (string)
- difficulty (int)
- isFavorite (bool)
- maxPreparationTime (int)

Antwort:
- 200 OK → Liste von RecipeDto

---

#### GET /api/recipes/{id}
Gibt ein Rezept anhand der ID zurück.

Antwort:
- 200 OK → RecipeDto
- 404 NotFound → wenn das Rezept oder die ID nicht existiert

---

#### POST /api/recipes
Erstellt ein neues Rezept.

Eingabedaten (JSON):
- CreateRecipeDto 

Antwort:
- 201 Created → erstelltes RecipeDto
- 400 BadRequest → ungültige Eingaben (Name/Kategorie erforderlich)

---

#### PUT /api/recipes/{id}
Aktualisiert ein Rezept komplett (alle Daten).

Eingabedaten (JSON):
- UpdateRecipeDto 

Antwort:
- 204 NoContent → erfolgreich
- 400 BadRequest → ungültige Eingaben (Name/Kategorie erforderlich)
- 404 NotFound → wenn das Rezept oder die ID nicht existiert

---

#### PATCH /api/recipes/{id}/favorite
Ändert den Favoritenstatus eines Rezepts.

Request Body:
- FavoriteDto (JSON)

Antwort:
- 204 NoContent → erfolgreich
- 404 NotFound → wenn das Rezept oder ID nicht existiert

---

#### DELETE /api/recipes/{id}
Löscht ein Rezept.

Antwort:
- 204 NoContent → erfolgreich
- 404 NotFound → wenn das Rezept oder die ID nicht existiert

---

## Datenformat

Die API verwendet DTOs, die automatisch in JSON umgewandelt werden.

Beispiel:


```json
{
  "name": "Pizza",
  "category": "Hauptspeise",
  "difficulty": 2,
  "preparationTime": 20,
  "isFavorite": false,
  "ingredients": [
    {
      "name": "Käse",
      "amount": "200g"
    },
    {
      "name": "Tomatensouce",
      "amount": "100ml"
    }
  ]
}
```

---

## Startanleitung

### Voraussetzungen
- .NET 8 (höher auch möglich, aber in diesem Projekt wurde auf .NET 8 gesetzt)
- Visual Studio 

---

### Anwendung starten

1. Projekt in Visual Studio öffnen  
2. `RecipeManager_API` als Startprojekt festlegen  
3. Anwendung starten (F5 oder Start-Button)  

---

### Zugriff auf die API

- API:
https://localhost:xxxx/api/recipes

- Swagger UI:
https://localhost:xxxx/swagger

Hinweis: Der Port (xxxx) kann je nach Konfiguration variieren.
