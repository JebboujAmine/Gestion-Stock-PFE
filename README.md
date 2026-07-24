
# Gestion de Stock - PFE

Application Desktop développée en **C# WinForms** avec **SQL Server**.

## Fonctionnalités
- CRUD complet sur 20 tables principales et secondaires :
  - **Tables principales** : Clients,  Paiement,Produits, Fournisseurs, Ventes, Achats, Inventaire, Audit,Entrepôt, Emplacement, Lot, Utilisateur, Rôles, Fournir .
  - **Tables secondaires** : Commande_Achat, LigneVente, LigneAchat, LigneInventaire, LigneStock, Bon_Livraison, Catégorie, …
- Audit automatique via triggers SQL et procédures stockées.
- Seuil d’alerte sur La quantité de produits.
- Interfaces simples et modernes.
- Installeur généré avec **Inno Setup**.

## Installation
1. Créer la base avec `sql/create_database.sql`.
2. Restaurer la structure avec `sql/schema.sql`.
3. Exécuter `sql/procedures.sql` et `sql/triggers.sql`.
4. Ouvrir `src/APP_SOURCE/GestionDeStock.sln` dans Visual Studio.
5. Lancer l’application ou utiliser l’installeur dans `installer/`.

## Structure du dépôt
- **sql/** : scripts SQL (`Shema.sql`, `procedures.sql`, `triggers.sql`).
- **src/APP_SOURCE/** : code source et solution Visual Studio.
- **docs/** : rapport PFE et présentation PowerPoint.
- **installer/** : installeur et script Inno Setup.
- **screenshots/** : captures d’écran de l’application.

## Documentation
- Rapport complet : `docs/Rapport de projet de fin de formation_AMINE JEBBOUJ.pdf`
- Présentation PowerPoint : `docs/P.Projet de fin Formation.pptx`

## Auteur
Projet réalisé par **Amine Jebbouj** – Sous L'encadrement de **M. Mustapha Laghzil** – ITAG 2026.
