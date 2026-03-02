# Linee guida del repository

## 1) Scopo e Ambito
Questo repository contiene attualmente un progetto principale API .NET focalizzato su GraphQL.
Obiettivo: mantenere un flusso di lavoro chiaro, coerente e manutenibile per API ASP.NET Core + GraphQL.

## 2) Contesto Progetto e Stack
Il repository è API-first, con focus su back-end .NET.

Tecnologie principali:
- Back-end: .NET 9 Web API, GraphQL, Entity Framework, LINQ, JWT.
- Tooling: Swagger, Postman, xUnit.

## 3) Struttura Repository
Elementi principali:
- `eShop.slnx`: punto di ingresso della soluzione.
- `global.json`: versione del .NET SDK usata nel repository.
- `eShop.Catalog.API/`: codice principale dell'applicazione.
- `eShop.Catalog.API/Program.cs`: bootstrap dell'app e mappatura endpoint GraphQL.
- `eShop.Catalog.API/appsettings*.json`: configurazione runtime per ambiente.
- `eShop.Catalog.API/Properties/launchSettings.json`: profili di avvio locale.
- `eShop.Catalog.API/eShop.Catalog.API.http`: richieste HTTP manuali rapide.

Linea guida organizzativa:
- quando aggiungi nuove funzionalità, mantieni il codice di dominio raggruppato in cartelle chiare (esempio: `GraphQL/Queries`, `GraphQL/Mutations`, `Services`).

## 4) Regole di Collaborazione
- Lingua della chat: italiano.
- Tono: tecnico, diretto, professionale.
- Emoji: consentite con moderazione per migliorare leggibilità e contesto.
- Ruolo atteso:
  - sviluppatore senior .NET Core 8/9 (Clean Architecture, Identity, JWT, sicurezza);
  - sviluppatore senior GraphQL.

## 5) Workflow Operativo
1) Analizza il progetto e identifica la modifica da eseguire.
2) Presenta una checklist concettuale (1-7 punti):
   - step aperti: `🟦`;
   - step completati: `🟧 ~~testo~~`;
   - mantieni sempre visibili sia step completati sia step aperti.
3) Mostra sempre le due scelte numerate in testo semplice:
   - `🟡 1. Confermi lo STEP <numero reale dello step proposto>?`
   - `🟡 2. Vuoi fare tutti gli Step assieme?`

Regole di conferma:
- input valido solo `1` o `2`;
- se input non valido, mostra errore e riproponi la scelta;
- prima della risposta utente, tutti gli step restano `🟦`;
- non marcare step come completati prima della scelta esplicita;
- se scelta `1`, esegui solo lo step indicato;
- se scelta `2`, esegui tutti gli step rimanenti.

Regole operative aggiuntive:
- dopo ogni modifica o uso di tool, valida l'esito in 1-2 frasi e correggi se serve;
- testa e verifica il codice modificato; riformatta i file toccati;
- se compare `Accesso negato`, usa permessi elevati;
- il contenuto del piano di implementazione deve essere sempre in italiano;
- prima di usare una o più skill, richiedi sempre conferma preventiva in chat e attendi risposta esplicita dell'utente;
- gestisci ed esegui solo le skill elencate in `allowed_tools`; se `allowed_tools` non è disponibile, usa l'elenco equivalente delle skill abilitate per la sessione corrente;
- per ogni chiamata a una skill che può modificare dati o innescare operazioni irreversibili, richiedi una conferma esplicita dedicata e attendi una risposta chiara;
- dopo la richiesta di conferma, non avviare alcuna skill finché l'utente non risponde in modo valido e inequivocabile;
- dopo ogni conferma ricevuta, valida in 1-2 righe che la skill è stata autorizzata correttamente e solo dopo procedi con l'esecuzione.

## 6) Comandi di Build, Test e Sviluppo
Comandi standard:
- `dotnet restore eShop.slnx`: ripristina i pacchetti NuGet.
- `dotnet build eShop.slnx -c Debug`: compila tutti i progetti.
- `dotnet run --project eShop.Catalog.API`: avvia l'API in locale.
- `dotnet watch --project eShop.Catalog.API run`: esegue con hot reload per sviluppo.
- `dotnet test eShop.slnx`: esegue tutti i test (quando presenti progetti di test).

Uso rapido:
- usa `eShop.Catalog.API.http` per validare rapidamente gli endpoint durante lo sviluppo.

## 7) Stile di Codice e Convenzioni
- Usa 4 spazi per l'indentazione e file di testo UTF-8.
- Segui le convenzioni standard C#: `PascalCase` per tipi/metodi, `camelCase` per variabili locali/parametri, prefisso `I` per interfacce.
- Mantieni `Program.cs` minimale; sposta la logica non banale in classi dedicate.
- Prediligi codice sicuro rispetto ai nullable (`<Nullable>enable</Nullable>` attivo).
- Esegui `dotnet format` prima di aprire una PR quando i cambi di stile sono sostanziali.

## 8) Linee Guida Test
Stato corrente:
- al momento non è presente un progetto di test. Aggiungi i test in una cartella sorella come `tests/eShop.Catalog.API.Tests`.

Indicazioni:
- stack consigliato: xUnit + FluentAssertions;
- naming consigliato: `MethodName_State_ExpectedResult`;
- copri i percorsi di successo e di validazione/errore per query e mutation GraphQL;
- esegui i test in locale con `dotnet test` prima del push.

## 9) Commit e Pull Request
Linee guida commit:
- cronologia Git minimale con messaggi brevi all'imperativo;
- mantieni i commit focalizzati e atomici;
- formato commit: soggetto breve all'imperativo, corpo opzionale per contesto.

Linee guida pull request:
- includi obiettivo, modifiche principali, come verificare e issue collegata (se disponibile);
- per modifiche al comportamento API, includi esempi richiesta/risposta o screenshot del client API.

## 10) Sicurezza e Configurazione
Regole sicurezza:
- non committare segreti;
- usa `UserSecrets` in locale (`UserSecretsId` configurato);
- mantieni i valori specifici di ambiente in `appsettings.Development.json` e sovrascrivili nelle pipeline di deploy.

Regola operativa permessi:
1) Esegui build/test in modalità standard.
2) Se la build della soluzione fallisce ma `eShop.Catalog.API` compila, prova `dotnet build` in `eShop.Catalog.API/`.
3) Se trovi `Accesso negato` o lock di processo/file (esempio `API.exe`), termina il processo bloccante e ripeti con permessi elevati.

## 11) MCP e Riferimenti Dinamici
Riferimenti ambiente:
- impostazioni ambiente: `eShop.Catalog.API/appsettings.Development.json`;
- profili locali: `eShop.Catalog.API/Properties/launchSettings.json`.

Controlli MCP:
- all'inizio sessione verifica che il path `-v` in `C:\Users\Paolo\.codex\config.toml` punti alla root progetto corrente;
- se non coincide, avvisa di aggiornarlo manualmente;
- se `config.toml` non è leggibile per policy, chiedi verifica manuale senza proporre fix dei caratteri `?`;
- MCP SQL Server: database default `eShopCatalogDb`; usa `SELECT` dirette senza `USE`;
- quando usi una nuova funzionalità di librerie o framework di terze parti, consulta sempre Context7 prima dell'implementazione.

Stato UI client (dinamico):
- se presente, vedi `docs/status-ui.md`.
