## CI/CD 
Am folosit **GitHub** pentru a avea un mod de lucru cat mai organizat, ceea ce ne-a ajutat sa detectam si sa rezolvam din timp bug-urile aparute si eventualele conflicte.

Fiecare membru al echipei a lucrat pe branch-ul sau. Dupa ce un membru isi termina task-ul, realiza un merge request pentru integrarea modificarilor in branch-ul principal.

## DEV
**Scop:** Dezvoltarea functionalitatilor noi si testare initiala.  
**Baza de date:** Instanta salvata local, nehostata (MSSQL).    
**Caracteristici:**  
- Posibilele erori minore au fost acceptate, fiind un mediu experimental.  
- Loguri detaliate si debugging activ.  

## STAGING
**Scop:** Testare finala, simuland mediul de productie (MSSQL).  
**Baza de date:** Instanta salvata local, dar cu date apropiate de productie.  
**Caracteristici:**  
- S-a folosit **FakeItEasy** pentru a mima comportamentul bazei de date in timpul testarii automatelor.  
- La final, toate functionalitatile au fost testate manual, folosind date reale, pentru a simula mediul de productie.  

## PRODUCTION
**Scop:** Mediul final utilizat de utilizatori (MSSQL).   
**Baza de date:** Baza de date ruleaza local intr-un container Docker, accesibil din reteaua Docker definita (backend_network).
**Caracteristici:**  
- Performanta si frontend optimizate.
- Functionalitatile esentiale au fost testate anterior manual in staging, pentru a asigura stabilitatea in productie.  

<br><br>

**CONFIGURATIA DOCKER COMPOSE**  
Atat backend-ul aplicatiei, cat si baza de date MSSQL sunt configurate folosind containere Docker, dupa cum urmeaza:  

- **MSSQL**:  
  - Ruleaza intr-un container Docker separat.  
  - Configuratia include:  
    - Parola setata prin `SA_PASSWORD`.  
    - Baza de date `baza`, definita prin `MSSQL_DATABASE`.  
    - Persistenta datelor este asigurata prin volumul Docker `my-volume`.  
    - Conectata la reteaua Docker `backend_network`.  

- **Backend**:  
  - Ruleaza intr-un alt container Docker.  
  - Conectat la baza de date folosind string-ul de conexiune:  
    `Server=sql-server,1433;Database=baza;User Id=SA;Password=Parecanumerge_1`.  
  - Mediul de lucru este definit prin variabila `ASPNETCORE_ENVIRONMENT`.  

- **Retea comună (backend_network)**:  
  - Toate containerele comunica printr-o retea Docker de tip `bridge`, care izoleaza serviciile de exterior.  

- **Mediile folosite (dev, staging, production)**:  
  - **Dev**: Configuratie rapidă, debug activ, date fictive.  
  - **Staging**: Configuratie similară productiei, dar cu date semi-reale pentru testare.  
  - **Production**: Configuratie finala, cu date reale si optimizari de performanta.

## QA
**Obiectivele testarii:**
  - **Acuratetea predictiilor**
    - **Obiectivul Testarii**: testarea algoritmilor de predictie pentru asigurarea rezultatelor optime
    - **Momentul Testarii**: Testare si Mentenanta
    - **Metoda Testarii**: Analiza logicii algoritmilor ***(White Box Testing)***, Verificarea rezultatelor predictiilor ***(Black Box Testing)***
    - **Rezultate**: Predictia AI poate da rezultate eronate

  - **Corectitudinea datelor utilizatorilor**
    - **Obiectivul Testarii**: validarea corectitudinii si actualizarii datelor utilizatorilor
    - **Momentul Testarii**: Testare, Dezvoltare si Analiza
    - **Metoda Testarii**: Verificarea fluxului datelor intre componentele aplicatiei ***(Integration testing)***, Verificarea manuala a datelor ***(Manual testing)***
    - **Rezultate**: Datele sunt colectate in mod corect

  - **Performanta aplicatiei**
    - **Obiectivul Testarii**: evaluarea timpului de raspuns al aplicatiei si al algoritmilor de predictie  
    - **Momentul Testarii**: Testare si Mentenanta
    - **Metoda Testarii**: Verificarea timpilor de raspuns ai aplicatiei
    - **Rezultate**: Endpoint-ul pentru predictia AI este putin mai lenta, face alte call-uri catre API-uri

  - **Functionalitatea generala**
    - **Obiectivul Testarii**: testarea fluxului de utilizare
    - **Momentul Testarii**: Testare si Implementare
    - **Metoda Testarii**: ***Unit tests***
    - **Rezultate**: Totul functioneaza in mod asteptat

  - **Compatibilitatea**
    - **Obiectivul Testarii**: testarea aplicatiei pe diverse dispozitive, sisteme de operare si browsere
    - **Momentul Testarii**: Testare si Implementare
    - **Metoda Testarii**: Verificarea aplicatiei pe diferite browsere, versiuni si sisteme de operare
    - **Rezultate**: Aplicatia este compatibila pe mai multe browsere si sisteme de operare

  - **Scalabilitatea**
    - **Obiectivul Testarii**: asigurarea functionalitatii aplicatiei pentru un numar mare de utilizatori
    - **Momentul Testarii**: Dezvoltare, Testare si Implementare
    - **Metoda Testarii**: Evaluarea performantei aplicatiei pe o perioada lunga de timp sub incarcatura constanta
    - **Rezultate**: Aplicatia face fata unui numar moderat de utilizatori

  - **UX/UI**
    - **Obiectivul Testarii**: testarea interfetei pentru a asigura navigare usoara si accesibilitate intuitiva
    - **Momentul Testarii**: Design, Testare si Implementare
    - **Metoda Testarii**: Evaluarea interactiunii utilizatorilor cu aplicatia
    - **Rezultate**: Aplicatia este usor de accesat si folosit

  - **Testare in scenarii reale**
    - **Obiectivul Testarii**: simularea unei interactiuni reale a unui utilizator cu aplicatia
    - **Momentul Testarii**: Testare si Implementare
    - **Metoda Testarii**: Rularea aplicatiei cu stimuli obisnuiti corespunzatori unui user real
    - **Rezultate**: Aplicatia se comporta normal
## Security Report
### HTTPS (HyperText Transfer Protocol Secure)
- HTTPS este o extensie securizată a HTTP care criptează comunicarea dintre un server web și un client folosind TLS (Transport Layer Security). Acest lucru asigură protecția datelor sensibile, cum ar fi datele de autentificare și informațiile personale, împotriva interceptării și atacurilor de tip man-in-the-middle.

### ORM (Object-Relational Mapping)
- Entity Framework este un instrument ORM pentru .NET care permite dezvoltatorilor să interacționeze cu baze de date relaționale folosind obiecte specifice domeniului. Acesta include funcționalități integrate pentru:
  - Prevenirea atacurilor de tip SQL injection
  - Validarea datelor
  - Gestionarea securizată a conexiunilor la bazele de date
- Identity Framework este o bibliotecă .NET pentru gestionarea autentificării și autorizării utilizatorilor. Aceasta suportă funcționalități precum:
  - Hash-ul parolelor 
  - Acces bazat pe roluri
  - Autentificări externe (de exemplu, Google, Facebook)
