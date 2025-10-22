# ☁️ Afleveringsopgave M4.04 – CosmosDB med WebApp
**Udarbejdet af:** Mohammad Haj  
**Uddannelse:** IT-arkitektur – Erhvervsakademi Aarhus  
**Fag:** Cloud Computing (Modul 4)

---

## 🎯 Formål
Formålet med denne opgave er at udvikle en **.NET Blazor WebApp**, som kommunikerer med en **CosmosDB-database** i Azure.  
Løsningen demonstrerer forståelse for **cloud-arkitektur**, **dataintegration** og **interaktion mellem frontend og backend** i en moderne webapplikation.

Projektet skal vise, hvordan CosmosDB kan anvendes som en skalerbar og fleksibel cloud-database, og hvordan data kan indlæses og sendes gennem en webapplikation udviklet i Blazor.

---

## 🧱 Projektstruktur
Projektet består af tre hoveddele:

| Lag | Beskrivelse |
|-----|--------------|
| **Client** | Blazor WebAssembly frontend med sider til oprettelse og visning af supporthenvendelser |
| **Server** | ASP.NET Core Web API, som håndterer forbindelsen til CosmosDB og eksponerer endpoints |
| **Shared** | Fælles modelklasse (`SupportMessage.cs`) brugt af både Client og Server |

---

## ⚙️ Teknologier
- **.NET 9.0**
- **Blazor WebAssembly**
- **ASP.NET Core Web API**
- **Azure CosmosDB (NoSQL)**
- **C# / Razor / CSS**
- **Dependency Injection**
- **JSON Serialization**

---

## 💡 Funktionalitet
- Formular til oprettelse af supporthenvendelse (`CreateSupport.razor`)
- Validering med **DataAnnotations** (navn, email, kategori, beskrivelse)
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
