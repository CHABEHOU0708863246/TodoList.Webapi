Cet article explique comment cr�er une application de liste de t�ches � l�aide de l�API Web .Net 6, de l�authentification JWT et de l�identit� AspNetCore. Microsoft SQL Server est utilis� pour afficher la base de donn�es et les tables.

Ceci est la partie 1 (Backend) d�un travail en 2 parties. La partie 2 utilisera Angular pour cr�er l�interface de l�application de liste de t�ches.

Outils

Visual Studio Community 2022
Microsoft SQL Server


Baclog

En tant qu�utilisateur, je souhaite m�inscrire pour utiliser l�application to-do
L�utilisateur doit �tre en mesure d�enregistrer ses informations d�identification pour pouvoir utiliser l�application to-do.
En tant qu�utilisateur, je souhaite me connecter pour utiliser l�application to-do
L�utilisateur doit �tre en mesure de cr�er un nouvel �l�ment � faire, l��l�ment doit avoir un nom et une description.
En tant qu�utilisateur, je souhaite cr�er un �l�ment � faire
L�utilisateur doit �tre en mesure de cr�er un nouvel �l�ment � faire, l��l�ment doit avoir un nom et une description.
En tant qu�utilisateur, je souhaite modifier un �l�ment existant
L�utilisateur doit pouvoir modifier un �l�ment de t�che existant, l�utilisateur doit pouvoir modifier le nom et la description.
En tant qu�utilisateur, je souhaite afficher tous mes �l�ments � faire
L�utilisateur doit �tre en mesure de voir tous ses �l�ments � faire.
En tant qu�utilisateur, je souhaite mettre � jour le statut d�un �l�ment � faire
En tant qu�utilisateur, je souhaite marquer un �l�ment � faire comme termin�.


Liste des packages NuGet install� pour l'application : 
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
Microsoft.AspNetCore.Identity.EntityFrameworkCore
Microsoft.AspNetCore.Identity
Microsoft.AspNetCore.Authentication.JwtBearer
Microsoft.VisualStudio.Web.CodeGeneration.Design

Cr�ation un dossier Authentication qui contiendra une classe ApplicationUser.cs qui h�ritera de la classe IdentityUser et de la classe Response.cs qui renverra un message et un code d��tat lorsqu�un utilisateur s�inscrit ou se connecte � l�application. La classe IdentityUser fait partie d�AspNetCore Identity.

Cr�ation d'un dossier Models, qui contiendra une classe RegisterModel.cs pour l�inscription des utilisateurs, une classe LoginModel.cs pour la connexion utilisateur, UserRoles.cs pour les r�les d�utilisateur et ToDoItemModel.cs pour les �l�ments de t�ches. 

Cr�ation d'un dossier Data qui contiendra �galement le fichier ApplicationDbContext.cs qui mappe les mod�les aux tables qui seront cr��es lors de la migration.

RegisterModel.cs, LoginModel.cs et UserRoles.cs seront li�s aux tables d�identit�. Cela signifie que seuls les champs d�crits dans les mod�les seront requis lorsqu�un utilisateur s�inscrit et se connecte � l�application. Les r�les afficheront les r�les qu�un utilisateur peut avoir, par exemple, � admin �.


Cr�ation dans le dossier Controlers, des deux controllers (Authentification et TodoItem) qui seront utilis�s pour l�inscription et la connexion des utilisateurs et pour la cr�ation, la modification et l�affichage des �l�ments de t�ches.

Cr�ation du script de migration � l�aide de la commande � add-migration � dans la console du gestionnaire de packages.


Cr�ation de la base de donn�es � l�aide de la commande � update-database � dans la console du gestionnaire de packages.

Test de l�application

url de L'API avec le serveur IIS Express : https://localhost:44352/index.html

Pour etre autoriser � tester les Requetes (GET, POST, PUT, DELETE) vous devez renseigner le nom d'utilisateur et le Password que voici :
{
  "username": "Auxence",
  "password": "08863246Bebejo@"
}

sur l'url : https://localhost:44352/api/Authentication/login

recuperer le token et ensuite utiliser le bouton Authorize pour la validation et ensuite finaliser l'autorisation 


Pour plus de fonctionnalit� et de test approfondie, vous pouvez utiliser le backend avec le frontend Angular qui est disponible sur mon github : 