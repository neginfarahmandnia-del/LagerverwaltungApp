# Lagerverwaltungs-System (ASP.NET Core Web App)

Ein modernes webbasiertes Lagerverwaltungssystem mit Benutzer- und Rollenverwaltung, Warenausgangs-, Bestell- und Artikelmanagement sowie umfangreichen Berichten – ideal für Unternehmen, die ihre Lagerprozesse digitalisieren möchten.

---

## 📌 Projektname

**Lagerverwaltungs-System mit ASP.NET Core & Razor Pages**

---

## 🧭 Funktionsübersicht

### ✅ Artikelverwaltung
- Artikel anlegen, bearbeiten, löschen
- Verwaltung nach Kategorien und Unterkategorien
- Filterung und Suche nach Bestand, Preis, Kategorie

### ✅ Bestellungen
- Erfassung von Artikelbestellungen
- Zuordnung zu Benutzer
- Automatische Erhöhung des Lagerbestands

### ✅ Warenausgänge
- Erfassung von Warenausgängen mit Grund
- Automatische Reduzierung des Lagerbestands
- Statistische Auswertungen

### ✅ Benutzer- & Rollenmanagement
- Login, Logout, Passwortschutz
- Rollenbasierte Navigation (Admin, Manager, Mitarbeiter)
- Benutzerübersicht und Verwaltung

### ✅ Berichte und Statistiken
- Abgänge pro Benutzer
- Abgänge nach Grund
- Artikel mit niedrigem Bestand
- Top-Abgänge
- Lagerverlauf
- Export-Funktion (Excel)

---

## 🛠️ Technologien

| Komponente            | Beschreibung                        |
|----------------------|-------------------------------------|
| ASP.NET Core 8       | Webframework                        |
| Razor Pages          | UI-Architektur                      |
| Entity Framework Core| ORM für SQL Server                  |
| Bootstrap            | Responsives UI                      |
| Identity             | Benutzer- & Rollensystem            |
| EPPlus (optional)    | Excel-Export                        |

---

## 📁 Projektstruktur

Pages/
├── Artikel/
├── Benutzer/
├── Bestellungen/
├── Warenausgaenge/
├── Berichte/
│ ├── TopAbgaenge.cshtml
│ ├── AbgaengeProBenutzer.cshtml
│ ├── AbgaengeNachGrund.cshtml
│ ├── NiedrigerBestand.cshtml
│ ├── Verlauf.cshtml
├── Kategorien/
├── LagerAbgang/
├── Shared/
Program.cs
appsettings.json

---

## 👤 Rollenübersicht

| Rolle        | Beschreibung                              |
|--------------|-------------------------------------------|
| Admin        | Volle Rechte, inkl. Benutzerverwaltung    |
| Manager      | Artikel, Bestellungen, Berichte           |
| Mitarbeiter  | Zugriff auf Artikel und Warenausgänge     |

---

## 🧪 Installation & Start (lokal)

### Voraussetzungen

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- SQL Server oder LocalDB
- Visual Studio 2022 oder höher

### Schritte

```bash
git clone https://github.com/dein-benutzername/lagerverwaltung.git
cd lagerverwaltung
dotnet ef database update
dotnet run
Anwendung startet unter: https://localhost:5001
dotnet ef migrations add InitialCreate
dotnet ef database update
Die Datenbank wird automatisch mit Beispieltabellen für Artikel, Benutzer, Kategorien, Warenausgänge etc. erstellt.
---

## 🔐 Standard-Zugangsdaten für Login

| Rolle        | E-Mail-Adresse             | Passwort       |
|--------------|----------------------------|----------------|
| Admin        | admin@lager.local          | Admin123!      |
| Manager      | manager@lager.local        | Manager123!    |
| Lagerist     | lagerist@lager.local       | Lagerist123!   |

Die Benutzer werden automatisch beim Start des Projekts erzeugt. Die zugehörige Logik befindet sich im `Program.cs` oder in einer `SeedData.cs`.

> Tipp: Melde dich mit dem Admin-Zugang an, um alle Funktionen wie Benutzerverwaltung, Berichte und Artikelmanagement zu testen.

👨‍💻 Autor
[Negin Farahmandnia]
📧 [neginfarahmandnia@gmail.com]
