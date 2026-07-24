CREATE TRIGGER trg_Audit_utilisateur_insert_Login     
ON [dbo].[Utilisateur]    
AFTER INSERT   
AS      
BEGIN      
    SET NOCOUNT ON;      
    DECLARE @id_u INT;      
    SELECT @id_u = CAST(CONTEXT_INFO() AS INT);      
      
   INSERT INTO Audit (  
    id_cible,  
    nom_table,  
    type_op,  
    Date_op,  
    id_utilisateur,  
    [id_utilisateur_connecte],  
    nom_utilisateur,  
    ancienne_valeur,  
    nouvelle_valeur  
)  
SELECT   
    i.id_utilisateur,  
    'Utilisateur',  
    'Insertion',  
    GETDATE(),  
    i.id_utilisateur,  
    @id_u,  
    i.nom_utilisateur,  
    NULL,  
    'nom_utilisateur :' + ISNULL(CAST(i.nom_utilisateur AS NVARCHAR(80)), '') +  
    ' mot_passe :' + ISNULL(CAST(i.mot_passe AS NVARCHAR(80)), '') +  
    ' id_role :' + ISNULL(CAST(i.id_role AS NVARCHAR(80)), '')  
FROM inserted i;    
END
GO


CREATE TRIGGER trg_Audit_utilisateur_update_Login
 ON [dbo].[Utilisateur] 
AFTER UPDATE 
AS BEGIN     
SET NOCOUNT ON;
DECLARE @id_u INT;     
SELECT @id_u = CAST(CONTEXT_INFO() AS INT);      
INSERT INTO Audit (         
id_cible,         
nom_table,       
type_op,         
Date_op,         
id_utilisateur,         
id_utilisateur_connecte,         
nom_utilisateur,         
ancienne_valeur,         
nouvelle_valeur     )     
SELECT          
i.id_utilisateur,         
'Utilisateur',         
'Modification',
GETDATE(),         
i.id_utilisateur,          
@id_u,         
i.nom_utilisateur,         
'nom_utilisateur :' + ISNULL(CAST(d.nom_utilisateur AS NVARCHAR(80)), '') +         
' mot_passe :' + ISNULL(CAST(d.mot_passe AS NVARCHAR(80)), '') +         
' id_role :' + ISNULL(CAST(d.id_role AS NVARCHAR(80)), ''),
'nom_utilisateur :' + ISNULL(CAST(i.nom_utilisateur AS NVARCHAR(80)), '') +
' mot_passe :' + ISNULL(CAST(i.mot_passe AS NVARCHAR(80)), '') +         
' id_role :' + ISNULL(CAST(i.id_role AS NVARCHAR(80)), '')     
FROM inserted i     
INNER JOIN deleted d 
ON 
i.id_utilisateur = d.id_utilisateur; 
END;
GO

CREATE TRIGGER trg_Audit_utilisateur_softdelete  
ON [dbo].[Utilisateur]  
AFTER UPDATE  
AS  
BEGIN  
    SET NOCOUNT ON;  
    DECLARE @id_u INT;  
    SELECT @id_u = CAST(CONTEXT_INFO() AS INT);  
  
    INSERT INTO Audit (  
        id_cible, 
	nom_table, 
	type_op, 
	Date_op,  
        id_utilisateur,
	id_utilisateur_connecte,
	nom_utilisateur,  
        ancienne_valeur, 
	nouvelle_valeur  
    )  
    SELECT d.id_utilisateur, 
          'Utilisateur', 
          'Suppression (soft)', 
	   GETDATE(),  
           d.id_utilisateur,
 	   @id_u,
	   d.nom_utilisateur,  
           'nom_utilisateur :' + ISNULL(CAST(d.nom_utilisateur AS NVARCHAR(80)), '') +  
           ' mot_passe :' + ISNULL(CAST(d.mot_passe AS NVARCHAR(80)), '') +  
           ' id_role :' + ISNULL(CAST(d.id_role AS NVARCHAR(80)), '') +  
           ' is_active :' + ISNULL(CAST(d.is_active AS NVARCHAR(10)), ''),  
           'is_active :' + ISNULL(CAST(i.is_active AS NVARCHAR(10)), '')  
    FROM deleted d  
    JOIN inserted i ON d.id_utilisateur = i.id_utilisateur  
    WHERE d.is_active = 1 AND i.is_active = 0;  
END;  

GO

CREATE TRIGGER trg_Seuil_Alerte  
ON [dbo].[Produit]  
AFTER INSERT, UPDATE  
AS  
BEGIN  
    SET NOCOUNT ON;  
  
    UPDATE p  
    SET p.seuil_alerte = CASE  
        WHEN i.quantité < 50 THEN 'Produit en état minimale'  
        WHEN i.quantité = 50 THEN 'Produit doit être fourni'  
        ELSE 'Produit existe en stockage'  
    END  
    FROM Produit p  
    INNER JOIN inserted i ON p.id_produit = i.id_produit;  
END;  