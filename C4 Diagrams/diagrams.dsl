workspace "Name" "Description" {

    !identifiers hierarchical

    model {

        user = person "User" {
            description "A person that wants to simulate crypto transactions"
        }

        cryptoPred = softwareSystem "CryptoPred" {
            description "Allows users to trade cryptocurrencies based on built-in predictions"
            database = container "Database" {
                description "Stores user and assets data"
                technology "MySQL"
            }
            server = container "Api Application" {
                description "Provides user requests via a JSON/HTTP Api"
                technology "C# and .Net"

                chatGPTController = component "ChatGPT Controller" {
                    description "Predicts the next price target for a coin"
                    technology ".Net ApiController"
                }
                userController = component "User Controller" {
                    description "Provides user CRUD functions"
                    technology ".Net ApiController"
                }
                authenticationController = component "Authentication Controller" {
                    description "Provides functions like login, logout, sign-in"
                    technology ".Net ApiController"
                }
                coinController = component "Coin Controller" {
                    description "Provides coin data and live stats"
                    technology ".Net ApiController"
                }
                tradingController = component "Trading Controller" {
                    description "Provides trading functions like buy, sell, deposit, withdraw"
                    technology ".Net ApiController"
                }
                userManager = component "User Manager Service" {
                    description "Provides user functions for Identity pattern"
                    technology "Collection of C# classes"
                }
                userService = component "User Service" {
                    description "Provides user functions through Identity library"
                    technology "Collection of C# classes"
                }
                coinService = component "Coin Service" {
                    description "Provides coin general data and CRUD functions"
                    technology "Collection of C# classes"
                }
                TradingService = component "Trading Service" {
                    description "Provides trading functions for users"
                    technology "Collection of C# classes"
                }
                candlestickService = component "Candlestick Service" {
                    description "Provides chart data for different timeframes"
                    technology "Collection of C# classes"
                }
                userwalletService = component "User Wallet Service" {
                    description "Provides wallet data and deposit/withdraw functions"
                    technology "Collection of C# classes"
                }
                assetRepository = component "Asset Repository" {
                    description "Provides CRUD functions for tokens(wallet assets)"
                    technology "Collection of C# classes"
                }
                candlestickRepository = component "CandleStick Repository" {
                    description "Provides chart data"
                    technology "Collection of C# classes"
                }
                coinRepository = component "Coin Repository" {
                    description "Provides CRUD functions for cryptocurrencies and live/past data"
                    technology "Collection of C# classes"
                }
                genericRepository = component "Generic Repository" {
                    description "Provides CRUD functions for given object"
                    technology "Collection of C# classes"
                }
                transactionRepository = component "Transaction Repository" {
                    description "Provides CRUD functions for past transactions of tokens"
                    technology "Collection of C# classes"
                }
                userRepository = component "User Repository" {
                    description "Provides CRUD functions through Identity and balance functions"
                    technology "Collection of C# classes"
                }
                walletRepository = component "Wallet Repository" {
                    description "Provides CRUD functions and general data about the wallet"
                    technology "Collection of C# classes"
                }
                
                databaseContext = component "Database Context" {
                    description "Provides CRUD functionality"
                    technology "Collection of C# classes (repositories)"
                }
                
            }
            web = container "Web Application" {
                description "Provides all the platform functionality via a web browser"
                technology "TypeScript and React"

                chartComponent = component "Chart Component" {
                    description "Allows users to visualize price fluctuations and wallet data"
                }
                coinComponent = component "Coin Component" {
                    description "Allows users to trade and get data for cryptocurrencies"
                }
                userComponent = component "User Component" {
                    description "Allows users to authenticate and view their profile"
                }
            }
        }

        repositoryNote = element "RepositoryNote" {
            tag "note"
            description "Each repository needs the have a repository interface. Each service apart from UserManager needs repositories"
        }

        user -> cryptoPred "browses and registers for events via"
        user -> cryptoPred.web "accesses platform via"
        cryptoPred.server -> cryptoPred.database "queries and updates"
        cryptoPred.web -> cryptoPred.server "makes HTTP/HTTPS requests"

        cryptoPred.web -> cryptoPred.server.chatGPTController "makes API calls to"
        cryptoPred.web -> cryptoPred.server.authenticationController "makes API calls to"
        cryptoPred.web -> cryptoPred.server.userController "makes API calls to"
        cryptoPred.web -> cryptoPred.server.coinController "makes API calls to"
        cryptoPred.web -> cryptoPred.server.tradingController "makes API calls to"


              
                                        
        cryptoPred.server.chatGPTController -> cryptoPred.server.coinService "uses"
        
        cryptoPred.server.authenticationController -> cryptoPred.server.userManager "uses"
        cryptoPred.server.authenticationController -> cryptoPred.server.userService "uses"
        cryptoPred.server.authenticationController -> cryptoPred.server.userwalletService "uses"
        
        cryptoPred.server.coinController -> cryptoPred.server.coinService "uses"
        cryptoPred.server.coinController -> cryptoPred.server.candlestickService "uses"
        
        cryptoPred.server.tradingController -> cryptoPred.server.coinService "uses"
        cryptoPred.server.tradingController -> cryptoPred.server.tradingService "uses"
        
        cryptoPred.server.userController -> cryptoPred.server.userService "uses"
        cryptoPred.server.userController -> cryptoPred.server.userManager "uses"
        cryptoPred.server.userController -> cryptoPred.server.userwalletService "uses"
        
        
        
        
        cryptoPred.server.candlestickService -> cryptoPred.server.candlestickRepository "uses"
        
        cryptoPred.server.userService -> cryptoPred.server.userRepository "uses"
        
        cryptoPred.server.coinService -> cryptoPred.server.coinRepository "uses"
        
        cryptoPred.server.tradingService -> cryptoPred.server.coinRepository "uses"
        cryptoPred.server.tradingService -> cryptoPred.server.userRepository "uses"
        cryptoPred.server.tradingService -> cryptoPred.server.walletRepository "uses"
        cryptoPred.server.tradingService -> cryptoPred.server.assetRepository "uses"
        cryptoPred.server.tradingService -> cryptoPred.server.transactionRepository "uses"
        
        
        cryptoPred.server.userwalletService -> cryptoPred.server.userRepository "uses"
        cryptoPred.server.userwalletService -> cryptoPred.server.walletRepository "uses"
        
        
        
        cryptoPred.server.userRepository -> cryptoPred.server.databaseContext "interacts with database via"
        cryptoPred.server.candlestickRepository -> cryptoPred.server.databaseContext "interacts with database via"
        
        cryptoPred.server.walletRepository -> cryptoPred.server.genericRepository "uses"
        cryptoPred.server.transactionRepository -> cryptoPred.server.genericRepository "uses"
        cryptoPred.server.coinRepository -> cryptoPred.server.genericRepository "uses"
        cryptoPred.server.assetRepository -> cryptoPred.server.genericRepository "uses"
        
        cryptoPred.server.genericRepository -> cryptoPred.server.databaseContext "interacts with database via"
        cryptoPred.server.databaseContext -> cryptoPred.database "accesses database"
        
        
        
        
        
        user -> cryptoPred.web.chartComponent "visualize via"
        user -> cryptoPred.web.coinComponent "trade and predict via"
        user -> cryptoPred.web.userComponent "authenticate via"
        
        
        cryptoPred.web.chartComponent -> cryptoPred.server "makes HTTP requests to"
        cryptoPred.web.coinComponent -> cryptoPred.server "makes HTTP requests to"
        cryptoPred.web.userComponent -> cryptoPred.server "makes HTTP requests to"
        
        
    }

    views {
        systemContext cryptoPred "CryptoPred-Context" {
            include *
            autolayout lr
        }

        container cryptoPred "CryptoPred-Container" {
            include *
            autolayout lr
        }

        component cryptoPred.web "WebApp-Component" {
            include *
            autolayout lr
        }
        component cryptoPred.server "ApiApplication-Component" {
            include *
            include repositoryNote
            autolayout lr
        }

        styles {
            element "Element" {
                color #ffffff
            }
            element "Person" {
                background #1a73e8
                shape person
            }
            element "Software System" {
                background #0d47a1
            }
            element "Container" {
                background #1976d2
            }
            element "Component" {
                background #1976d2
            }
            element "Database" {
                shape cylinder
                background #1565c0
            }
            element "note" {
                background #333333
            }
        }
    }

    configuration {
        scope softwaresystem
    }

}
