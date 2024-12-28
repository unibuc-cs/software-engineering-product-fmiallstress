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
