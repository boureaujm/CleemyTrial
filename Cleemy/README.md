docker-compose.yml

+ sql server
******************************************************************

version: "3"
services:
  sql-server:
    image: mcr.microsoft.com/mssql/server:2019-latest
    hostname: sql-server
    container_name: sql-server
    ports:
      - "1433:1433"
    environment:
      - ACCEPT_EULA=Y
      - SA_PASSWORD=XNc7PA5nxxW8ny

docker-compose up

+ initialize base
******************************************************************

Add-Migration -Name "Initial"

Create a ApplicationContextFactory for providing connection string

Update-Database

+ Choix
******************************************************************

J'utilise fluentvalidation pour valider objets en entr�e de l'API j'ai cherch� � reproduire ce syst�me

J'ai quand m�me ajout� Swagger lors du lancement de l'API

Par simplicit� les messages de valdation/erreur sont sous forme de constantes mais en g�n�ral je 
pr�f�re �tre plus configurable (afin de pr�voir les localization des chaines de caract�res)

M�me si non demand� j'ai limiter la taille des champs nom, pr�nom et commentaires (en base)

