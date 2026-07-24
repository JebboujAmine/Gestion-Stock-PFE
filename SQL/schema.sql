USE [Gestion_Stock]
GO
/****** Object:  Table [dbo].[Audit]    Script Date: 20/07/2026 01:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Audit](
	[id_audit] [int] IDENTITY(1,1) NOT NULL,
	[id_cible] [nvarchar](255) NULL,
	[nom_table] [varchar](50) NULL,
	[type_op] [varchar](50) NULL,
	[Date_op] [datetime] NULL,
	[nom_utilisateur] [varchar](50) NULL,
	[ancienne_valeur] [nvarchar](500) NULL,
	[nouvelle_valeur] [nvarchar](500) NULL,
	[id_utilisateur] [int] NOT NULL,
	[id_utilisateur_connecte] [nvarchar](max) NULL,
 CONSTRAINT [PK__Audit__5CD6CB2B] PRIMARY KEY CLUSTERED 
(
	[id_audit] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bon_Livraison]    Script Date: 20/07/2026 01:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bon_Livraison](
	[id_bon_Livraison] [int] NOT NULL,
	[date_livraison] [datetime] NULL,
	[adresse] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_bon_Livraison] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categorie]    Script Date: 20/07/2026 01:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categorie](
	[id_categorie] [int] NOT NULL,
	[nom_categorie] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_categorie] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Client]    Script Date: 20/07/2026 01:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[id_client] [int] NOT NULL,
	[nom_complet] [varchar](50) NULL,
	[telephone] [varchar](25) NULL,
	[email] [varchar](50) NULL,
	[adresse] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_client] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Commande_Achat]    Script Date: 20/07/2026 01:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Commande_Achat](
	[id_commande] [int] NOT NULL,
	[date_commande] [datetime] NULL,
	[statut] [varchar](50) NULL,
	[id_utilisateur] [int] NULL,
	[id_fournisseur] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_commande] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Emplacement]    Script Date: 20/07/2026 01:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Emplacement](
	[id_emplacement] [int] NOT NULL,
	[code_emplacement] [varchar](50) NULL,
	[id_entrpot] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_emplacement] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Entrepot]    Script Date: 20/07/2026 01:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Entrepot](
	[id_entrpot] [int] NOT NULL,
	[nom_magasin] [varchar](50) NULL,
	[adresse] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_entrpot] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Fournir]    Script Date: 20/07/2026 01:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Fournir](
	[id_produit] [int] NULL,
	[id_fournisseur] [int] NULL,
	[quantite] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Fournisseur]    Script Date: 20/07/2026 01:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Fournisseur](
	[id_Fournisseur] [int] NOT NULL,
	[nom_complet] [varchar](50) NULL,
	[mail] [varchar](50) NULL,
	[adresse] [varchar](50) NULL,
	[telephone] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_Fournisseur] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Inventaire]    Script Date: 20/07/2026 01:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inventaire](
	[id_inventaire] [int] NOT NULL,
	[date_inventaire] [datetime] NULL,
	[remarque] [varchar](50) NULL,
	[id_entrpot] [int] NULL,
	[id_produit] [int] NULL,
	[id_utilisateur] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_inventaire] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ligne_Achat]    Script Date: 20/07/2026 01:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ligne_Achat](
	[id_produit] [int] NULL,
	[id_commande] [int] NULL,
	[id_lot] [int] NULL,
	[Quantite] [int] NULL,
	[Prix_achat] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ligne_inventaire]    Script Date: 20/07/2026 01:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ligne_inventaire](
	[id_produit] [int] NULL,
	[id_inventaire] [int] NULL,
	[stock_theorique] [int] NULL,
	[stock_reel] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ligne_Stock]    Script Date: 20/07/2026 01:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ligne_Stock](
	[id_produit] [int] NULL,
	[id_emplacement] [int] NULL,
	[Quantite] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ligne_Vente]    Script Date: 20/07/2026 01:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ligne_Vente](
	[id_vente] [int] NULL,
	[id_produit] [int] NULL,
	[id_lot] [int] NULL,
	[prix_Vente] [money] NULL,
	[Quantite] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lot]    Script Date: 20/07/2026 01:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lot](
	[id_lot] [int] NOT NULL,
	[date_fabrication] [datetime] NULL,
	[date_peremption] [datetime] NULL,
	[id_produit] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_lot] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Paiement]    Script Date: 20/07/2026 01:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Paiement](
	[id_paiement] [int] NOT NULL,
	[date_paiement] [datetime] NULL,
	[mode_paiement] [varchar](50) NULL,
	[id_vente] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_paiement] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Produit]    Script Date: 20/07/2026 01:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Produit](
	[id_produit] [int] NOT NULL,
	[designation] [varchar](50) NULL,
	[prix_unitaire] [decimal](14, 2) NULL,
	[seuil_alerte] [varchar](50) NULL,
	[description_p] [varchar](50) NULL,
	[id_categorie] [int] NULL,
	[quantité] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_produit] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 20/07/2026 01:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[id_role] [int] NOT NULL,
	[libelle] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_role] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Utilisateur]    Script Date: 20/07/2026 01:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utilisateur](
	[id_utilisateur] [int] NOT NULL,
	[nom_utilisateur] [varchar](50) NULL,
	[mot_passe] [varchar](50) NULL,
	[id_role] [int] NULL,
	[is_active] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_utilisateur] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vente]    Script Date: 20/07/2026 01:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vente](
	[id_vente] [int] NOT NULL,
	[date_vente] [datetime] NULL,
	[id_utilisateur] [int] NULL,
	[id_bon_Livraison] [int] NULL,
	[id_client] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_vente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Audit] ADD  CONSTRAINT [DF__Audit__Date_op__5DCAEF64]  DEFAULT (getdate()) FOR [Date_op]
GO
ALTER TABLE [dbo].[Commande_Achat] ADD  CONSTRAINT [DF_Commande_Achat_date_commande]  DEFAULT (getdate()) FOR [date_commande]
GO
ALTER TABLE [dbo].[Utilisateur] ADD  DEFAULT ((1)) FOR [is_active]
GO
ALTER TABLE [dbo].[Vente] ADD  CONSTRAINT [DF_Vente_date_vente]  DEFAULT (getdate()) FOR [date_vente]
GO
ALTER TABLE [dbo].[Audit]  WITH CHECK ADD  CONSTRAINT [FK__Audit__id_utilis__5EBF139D] FOREIGN KEY([id_utilisateur])
REFERENCES [dbo].[Utilisateur] ([id_utilisateur])
GO
ALTER TABLE [dbo].[Audit] CHECK CONSTRAINT [FK__Audit__id_utilis__5EBF139D]
GO
ALTER TABLE [dbo].[Commande_Achat]  WITH CHECK ADD FOREIGN KEY([id_fournisseur])
REFERENCES [dbo].[Fournisseur] ([id_Fournisseur])
GO
ALTER TABLE [dbo].[Commande_Achat]  WITH CHECK ADD FOREIGN KEY([id_utilisateur])
REFERENCES [dbo].[Utilisateur] ([id_utilisateur])
GO
ALTER TABLE [dbo].[Emplacement]  WITH CHECK ADD FOREIGN KEY([id_entrpot])
REFERENCES [dbo].[Entrepot] ([id_entrpot])
GO
ALTER TABLE [dbo].[Fournir]  WITH CHECK ADD FOREIGN KEY([id_fournisseur])
REFERENCES [dbo].[Fournisseur] ([id_Fournisseur])
GO
ALTER TABLE [dbo].[Fournir]  WITH CHECK ADD FOREIGN KEY([id_produit])
REFERENCES [dbo].[Produit] ([id_produit])
GO
ALTER TABLE [dbo].[Inventaire]  WITH CHECK ADD FOREIGN KEY([id_entrpot])
REFERENCES [dbo].[Entrepot] ([id_entrpot])
GO
ALTER TABLE [dbo].[Inventaire]  WITH CHECK ADD FOREIGN KEY([id_produit])
REFERENCES [dbo].[Produit] ([id_produit])
GO
ALTER TABLE [dbo].[Inventaire]  WITH CHECK ADD FOREIGN KEY([id_utilisateur])
REFERENCES [dbo].[Utilisateur] ([id_utilisateur])
GO
ALTER TABLE [dbo].[Ligne_Achat]  WITH CHECK ADD FOREIGN KEY([id_commande])
REFERENCES [dbo].[Commande_Achat] ([id_commande])
GO
ALTER TABLE [dbo].[Ligne_Achat]  WITH CHECK ADD FOREIGN KEY([id_lot])
REFERENCES [dbo].[Lot] ([id_lot])
GO
ALTER TABLE [dbo].[Ligne_Achat]  WITH CHECK ADD FOREIGN KEY([id_produit])
REFERENCES [dbo].[Produit] ([id_produit])
GO
ALTER TABLE [dbo].[Ligne_inventaire]  WITH CHECK ADD FOREIGN KEY([id_inventaire])
REFERENCES [dbo].[Inventaire] ([id_inventaire])
GO
ALTER TABLE [dbo].[Ligne_inventaire]  WITH CHECK ADD FOREIGN KEY([id_produit])
REFERENCES [dbo].[Produit] ([id_produit])
GO
ALTER TABLE [dbo].[Ligne_Stock]  WITH CHECK ADD FOREIGN KEY([id_emplacement])
REFERENCES [dbo].[Emplacement] ([id_emplacement])
GO
ALTER TABLE [dbo].[Ligne_Stock]  WITH CHECK ADD FOREIGN KEY([id_produit])
REFERENCES [dbo].[Produit] ([id_produit])
GO
ALTER TABLE [dbo].[Ligne_Vente]  WITH CHECK ADD  CONSTRAINT [FK__Ligne_Ven__id_lo__31EC6D26] FOREIGN KEY([id_lot])
REFERENCES [dbo].[Lot] ([id_lot])
GO
ALTER TABLE [dbo].[Ligne_Vente] CHECK CONSTRAINT [FK__Ligne_Ven__id_lo__31EC6D26]
GO
ALTER TABLE [dbo].[Ligne_Vente]  WITH CHECK ADD  CONSTRAINT [FK__Ligne_Ven__id_pr__30F848ED] FOREIGN KEY([id_produit])
REFERENCES [dbo].[Produit] ([id_produit])
GO
ALTER TABLE [dbo].[Ligne_Vente] CHECK CONSTRAINT [FK__Ligne_Ven__id_pr__30F848ED]
GO
ALTER TABLE [dbo].[Ligne_Vente]  WITH CHECK ADD  CONSTRAINT [FK__Ligne_Ven__id_ve__300424B4] FOREIGN KEY([id_vente])
REFERENCES [dbo].[Vente] ([id_vente])
GO
ALTER TABLE [dbo].[Ligne_Vente] CHECK CONSTRAINT [FK__Ligne_Ven__id_ve__300424B4]
GO
ALTER TABLE [dbo].[Lot]  WITH CHECK ADD FOREIGN KEY([id_produit])
REFERENCES [dbo].[Produit] ([id_produit])
GO
ALTER TABLE [dbo].[Paiement]  WITH CHECK ADD FOREIGN KEY([id_vente])
REFERENCES [dbo].[Vente] ([id_vente])
GO
ALTER TABLE [dbo].[Produit]  WITH CHECK ADD FOREIGN KEY([id_categorie])
REFERENCES [dbo].[Categorie] ([id_categorie])
GO
ALTER TABLE [dbo].[Utilisateur]  WITH CHECK ADD FOREIGN KEY([id_role])
REFERENCES [dbo].[Role] ([id_role])
GO
ALTER TABLE [dbo].[Vente]  WITH CHECK ADD FOREIGN KEY([id_bon_Livraison])
REFERENCES [dbo].[Bon_Livraison] ([id_bon_Livraison])
GO
ALTER TABLE [dbo].[Vente]  WITH CHECK ADD FOREIGN KEY([id_client])
REFERENCES [dbo].[Client] ([id_client])
GO
ALTER TABLE [dbo].[Vente]  WITH CHECK ADD FOREIGN KEY([id_utilisateur])
REFERENCES [dbo].[Utilisateur] ([id_utilisateur])
GO
