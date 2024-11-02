# Crypto Exchange

### You can find the initial repository [here](https://github.com/Matoka26/Crypto-Exchange).

## Documenting the existing application from MDS 

### 1. The list of initial project requirements:
    
[x] Enter app: As a user, I should be able sign up or to securely log in to the platform using my email and password and log out.

[ ] Dashboard: As a user, I should have access to my personalized dashboard, where I can view my cryptocurrency holdings, portfolio performance metrics, and recent transactions.

[x] Real-time data: The platform should fetch real-time market data from cryptocurrency exchanges and display it on the dashboard, including current prices and volume for selected cryptocurrencies.

[ ] Banking system: As a user, I should be able to deposit and withdraw money from balance.

[x] ChatGPT: As a user, I should be able to request informations from ChatGPT API regarding predictions of a chosen currency.

[ ] Commission fee: As a user, there will be a commission fee applied when withdrawing and depositing money from my account.


[x] Responsiveness: As a user, I want to use a web application that is responsive and adapts to different screen sizes So that I can access the application seamlessly from any device.

[x] Clean interface: As a user,I want the frontend to have an appealing design and intuitive user interface, So that I can easily navigate the application and have a pleasant experience while interacting with it.

[x] Quick render: As a user, I want the application to respond rapidly to my requests for data retrieval, analysis, and transactions, So that I can make timely decisions and take actions without experiencing delays or interruptions.

[x] Coins info: As a user, I want to see informations about crypto currencies of my interest, such as graphs of price fluctuation 

#### Unachieved or incomplete User Stories:
- Dashobard:
    - The implementation is currently a hardcoded prototype to prioritize visual layout and user interaction flows
    - A more dynamic, data-driven version is planned for the next iteration

- Banking system & Commission fee:
    - The main focus has been on structuring the user interface and user experience
    - Are pending implementation as part of the next development phase

### 2. Team description
#### Initial team members:
- Mircea Andrei Razvan (Team Leader)
    - Responsabilities: Project Structure, DataBase, Endpoints & AI-Integration

- Alexandru Andrei 
    - Responsabilities: Responsive Design, Optimization & Dashboard

- Dogaru Mihail Danut
    - Responsabilities: Unit Testing, AI-Integration & Documentation

- Tirila Patric Gabriel
    - Responsabilities: UX Design, Chart Plotting & User Profile

### 3. Software Architecture Report 
#### a) What are the technologies that you have used, and why? 
- The backend is written in <b>C#</b> for using multple frameworks such as:
    - <b><i>Identity</b></i> for user entity
    - <b><i>Fake it Easy</b></i> for unit testing
    - <b><i>Binance</b></i> for real time currencies values
    - <b><i>OpenAI</b></i> for AI incorporation
- The frontend is written with <b>JavaScript</b> and <b>Vue.js</b> from previous experiences of the developers and easy design of components

#### b) What are the architectural patterns you have implemented and were they appropriate, and why? 
- We implemented the <b>Repository-Service</b> design pattern in the backend to streamline database interactions, making the code cleaner and more manageable.

#### c) Were the coding principles established enforced successfully? 
- We've tried to establish a certain directory and file structure to keep everything at place for better organization. For example,
    - for the backend : Controllers, Models, Repositories, Services etc.
    - for the frontend: assets, components, router, etc

- Also, for model names and attribues we've got CamelCase

#### d) What are the faults that were discovered during development that haven’t been addressed by the time of delivery? 
- There is an open issue regarding a wrong RSI value for the input data
- A page is hardcoded in frontend for the moment

#### e) Does any part of the project require refactoring? 
- For the moment the code base seems well built and modular. There is no refactoring planned yet.