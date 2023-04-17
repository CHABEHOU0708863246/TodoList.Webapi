Cet article explique comment créer une application de liste de tâches à l’aide de l’API Web .Net 6, de l’authentification JWT et de l’identité AspNetCore. Microsoft SQL Server est utilisé pour afficher la base de données et les tables.

Ceci est la partie 1 (Backend) d’un travail en 2 parties. La partie 2 utilisera Angular pour créer l’interface de l’application de liste de tâches.

Outils

Visual Studio Community 2022
Microsoft SQL Server


Baclog

En tant qu’utilisateur, je souhaite m’inscrire pour utiliser l’application to-do
L’utilisateur doit être en mesure d’enregistrer ses informations d’identification pour pouvoir utiliser l’application to-do.
En tant qu’utilisateur, je souhaite me connecter pour utiliser l’application to-do
L’utilisateur doit être en mesure de créer un nouvel élément à faire, l’élément doit avoir un nom et une description.
En tant qu’utilisateur, je souhaite créer un élément à faire
L’utilisateur doit être en mesure de créer un nouvel élément à faire, l’élément doit avoir un nom et une description.
En tant qu’utilisateur, je souhaite modifier un élément existant
L’utilisateur doit pouvoir modifier un élément de tâche existant, l’utilisateur doit pouvoir modifier le nom et la description.
En tant qu’utilisateur, je souhaite afficher tous mes éléments à faire
L’utilisateur doit être en mesure de voir tous ses éléments à faire.
En tant qu’utilisateur, je souhaite mettre à jour le statut d’un élément à faire
En tant qu’utilisateur, je souhaite marquer un élément à faire comme terminé.


Liste des packages NuGet installé pour l'application : 
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
Microsoft.AspNetCore.Identity.EntityFrameworkCore
Microsoft.AspNetCore.Identity
Microsoft.AspNetCore.Authentication.JwtBearer
Microsoft.VisualStudio.Web.CodeGeneration.Design

Création un dossier Authentication qui contiendra une classe ApplicationUser.cs qui héritera de la classe IdentityUser et de la classe Response.cs qui renverra un message et un code d’état lorsqu’un utilisateur s’inscrit ou se connecte à l’application. La classe IdentityUser fait partie d’AspNetCore Identity.

Création d'un dossier Models, qui contiendra une classe RegisterModel.cs pour l’inscription des utilisateurs, une classe LoginModel.cs pour la connexion utilisateur, UserRoles.cs pour les rôles d’utilisateur et ToDoItemModel.cs pour les éléments de tâches. 

Création d'un dossier Data qui contiendra également le fichier ApplicationDbContext.cs qui mappe les modèles aux tables qui seront créées lors de la migration.

RegisterModel.cs, LoginModel.cs et UserRoles.cs seront liés aux tables d’identité. Cela signifie que seuls les champs décrits dans les modèles seront requis lorsqu’un utilisateur s’inscrit et se connecte à l’application. Les rôles afficheront les rôles qu’un utilisateur peut avoir, par exemple, « admin ».


Création dans le dossier Controlers, des deux controllers (Authentification et TodoItem) qui seront utilisés pour l’inscription et la connexion des utilisateurs et pour la création, la modification et l’affichage des éléments de tâches.

Création du script de migration à l’aide de la commande « add-migration » dans la console du gestionnaire de packages.


Création de la base de données à l’aide de la commande « update-database » dans la console du gestionnaire de packages.

Test de l’application

url de L'API avec le serveur IIS Express : https://localhost:44352/index.html

Pour etre autoriser à tester les Requetes (GET, POST, PUT, DELETE) vous devez renseigner le nom d'utilisateur et le Password que voici :
{
  "username": "Auxence",
  "password": "08863246Bebejo@"
}

sur l'url : https://localhost:44352/api/Authentication/login

recuperer le token et ensuite utiliser le bouton Authorize pour la validation et ensuite finaliser l'autorisation 


Pour plus de fonctionnalité et de test approfondie, vous pouvez utiliser le backend avec le frontend Angular qui est disponible sur mon github : 