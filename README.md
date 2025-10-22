# ☁️ Afleveringsopgave M4.04 – CosmosDB med WebApp
**Udarbejdet af:** Mohammad Haj  
**Uddannelse:** IT-arkitektur – Erhvervsakademi Aarhus  
**Fag:** Cloud Computing (Modul 4)  
**Afleveringsdato:** 22 Oktober 2025

---

## 🎯 Projektets formål
Formålet med projektet er at udvikle en **.NET Blazor WebApp**, som kommunikerer med en **CosmosDB-database** i Azure.  
Applikationen viser, hvordan en cloudbaseret løsning kan håndtere data via et API og samtidig have et moderne, brugervenligt frontend-interface bygget i Blazor.

Projektet demonstrerer forståelsen for:
- Cloud-arkitektur og databaser i Azure
- Kommunikation mellem backend (Web API) og frontend (Blazor WASM)
- Datavalidering, datahåndtering og brugeroplevelse i en moderne webapplikation

Applikationen er bygget som en **Blazor WebAssembly Hosted-løsning**,  
hvilket betyder, at **Server-projektet hoster både API’et og Client-delen**.  
Når man kører **SupportCosmos.Server**, kører klienten automatisk med.

---

## 🧱 Projektstruktur

| Lag | Beskrivelse |
|-----|--------------|
| **Client** | Blazor WebAssembly frontend, som automatisk hostes af serveren. Indeholder sider til oprettelse og visning af supporthenvendelser. |
| **Server** | ASP.NET Core Web API, som håndterer forbindelsen til CosmosDB og eksponerer endpoints. Host for både API og Client. |
| **Shared** | Fælles modelklasse (`SupportMessage.cs`) brugt af både Client og Server for at sikre fælles datakontrakter. |

---

## ⚙️ Teknologier
- **.NET 9.0**
- **Blazor WebAssembly (Hosted)**
- **ASP.NET Core Web API**
- **Azure CosmosDB (NoSQL)**
- **C# / Razor / CSS**
- **Dependency Injection**
- **JSON Serialization**

---

## 💡 Funktionalitet
- Formular til oprettelse af supporthenvendelse (`CreateSupport.razor`)
- Validering med **DataAnnotations** (navn, email, telefon, kategori, beskrivelse)
- Automatisk generering af ID i formatet `support-0001`, `support-0002`, osv.
- Data sendes til CosmosDB via Web API (`SupportController`)
- Listevisning af alle henvendelser (`SupportList.razor`)
- **Modal-dialog** der vises ved succesfuld indsendelse
- **Responsivt og moderne design** med animationer og grønt farvetema

---

## 🧩 Arkitektur og dataflow

```mermaid
graph TD
    A[Bruger udfylder formular i Blazor (CreateSupport)] --> B[POST /api/support]
    B --> C[SupportController.cs]
    C --> D[CosmosService.AddItemAsync()]
    D --> E[Azure CosmosDB Container]
    E --> F[Gemmer data som JSON-dokument]
    F --> G[SupportList.razor henter data via GET /api/support]
```
---
## ☁️ Oprettelse af ny CosmosDB-database via Azure CLI

Hvis man ønsker at oprette en ny **CosmosDB-database**, der passer til løsningen, kan man gøre det via **Azure CLI** med følgende kommandoer:
```
- az group create --name SupportResourceGroup --location "North Europe"

- az cosmosdb create --name ibas-db-account-1731551469 --resource-group SupportResourceGroup --kind GlobalDocumentDB

- az cosmosdb sql database create --account-name ibas-db-account-1731551469 --resource-group SupportResourceGroup --name IBasSupportDB

- az cosmosdb sql container create --account-name ibas-db-account-1731551469 --resource-group SupportResourceGroup --database-name IBasSupportDB --name ibassupport --partition-key-path "/category"
```

Partition key **/category** matcher CosmosService-implementeringen i projektet.

---

For at køre projektet lokalt kan man klone GitHub-repositoriet med:


git clone https://github.com/[DIT_GITHUB_NAVN]/SupportCosmos.git


Åbn løsningen i **Visual Studio** eller **JetBrains Rider**, sørg for at **SupportCosmos.Server** er sat som *startup-projekt* (det hoster både API’et og Blazor-klienten), og kør projektet.  
Når serveren starter, kører klienten automatisk under samme adresse — fx `https://localhost:7173/`.

---

### 📊 Projektstatus
- Blazor WebApp oprettet og hostet korrekt ✔️
- CosmosDB integration og API-forbindelse ✔️
- CRUD-funktionalitet (Create + Read) ✔️
- Validering og DataAnnotations ✔️
- Moderne og responsivt design ✔️

---

### 🔮 Hvad mangler og næste trin
Tilføje mulighed for at redigere og slette henvendelser, filtrering og søgning efter kategori, rolleopdeling mellem bruger og administrator, email-notifikation ved nye henvendelser, og mulighed for at sortere henvendelser efter dato.

---

Løsningen viser, hvordan **Blazor WebAssembly** kan integreres med **CosmosDB i Azure** for at skabe en cloudbaseret webapp med fokus på struktur, datahåndtering og brugervenlighed.
