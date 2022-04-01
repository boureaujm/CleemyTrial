
sql server
******************************************************************

docker-compose.yml

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

initialize base
******************************************************************

Automatique quand on lance l'API

Choix
*************************************************

J'utilise fluentvalidation pour valider objets en entrée de l'API j'ai cherché à reproduire ce système

J'ai quand même ajouté Swagger lors du lancement de l'API

Par simplicité les messages de valdation/erreur sont sous forme de constantes mais en général je 
préfère être plus configurable (afin de prévoir les localization des chaines de caractères)

Même si non demandé j'ai limiter la taille des champs nom, prénom et commentaires (en base)

la conversion des objets utilise un pattern adapter

Pour éviter l'utilisation de nugget externe je n'ai pas ajouté de logger (log4net, serilog), ce que je fais d'habitude

Les services sont register old school mais j'utilise autofac par exemple pour effectuer des register par convention de nommage ex "Services","Repository" etc. 

Architecture
******************************************************************

La nommage des projets est inspiré du clean code
l'idée est de pouvoir r�utiliser les ervices et séparer la partie Data Access

Le projet commons fait le lien avec les objets communs

les classes de type RegisterServicesExtension
sont des classes de register que j'appelle dans le startup
Elles permettent d'isoler le code de register et d'ajouter les projets facilement (en passant la configuration)