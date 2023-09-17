﻿CREATE TABLE [dbo].[Enderecos] (
    [Id] [uniqueidentifier] NOT NULL,
    [Logradouro] [varchar](200) NOT NULL,
    [Numero] [varchar](5) NOT NULL,
    [Complemento] [varchar](50) NOT NULL,
    [Cep] [varchar](8) NOT NULL,
    [Bairro] [varchar](100) NOT NULL,
    [Cidade] [varchar](100) NOT NULL,
    [Estado] [varchar](100) NOT NULL,
    CONSTRAINT [PK_dbo.Enderecos] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[Fornecedores] (
    [Id] [uniqueidentifier] NOT NULL,
    [Nome] [varchar](200) NOT NULL,
    [Documento] [varchar](14) NOT NULL,
    [TipoFornecedor] [int] NOT NULL,
    [Ativo] [bit] NOT NULL,
    CONSTRAINT [PK_dbo.Fornecedores] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[Produtos] (
    [Id] [uniqueidentifier] NOT NULL,
    [FornecedorId] [uniqueidentifier] NOT NULL,
    [Nome] [varchar](100) NOT NULL,
    [Descricao] [varchar](1000) NOT NULL,
    [Imagem] [varchar](100) NOT NULL,
    [Valor] [decimal](18, 2) NOT NULL,
    [DataCadastro] [datetime] NOT NULL,
    [Ativo] [bit] NOT NULL,
    CONSTRAINT [PK_dbo.Produtos] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_Id] ON [dbo].[Enderecos]([Id])
CREATE INDEX [IX_FornecedorId] ON [dbo].[Produtos]([FornecedorId])
ALTER TABLE [dbo].[Enderecos] ADD CONSTRAINT [FK_dbo.Enderecos_dbo.Fornecedores_Id] FOREIGN KEY ([Id]) REFERENCES [dbo].[Fornecedores] ([Id])
ALTER TABLE [dbo].[Produtos] ADD CONSTRAINT [FK_dbo.Produtos_dbo.Fornecedores_FornecedorId] FOREIGN KEY ([FornecedorId]) REFERENCES [dbo].[Fornecedores] ([Id])
CREATE TABLE [dbo].[__MigrationHistory] (
    [MigrationId] [nvarchar](150) NOT NULL,
    [ContextKey] [nvarchar](300) NOT NULL,
    [Model] [varbinary](max) NOT NULL,
    [ProductVersion] [nvarchar](32) NOT NULL,
    CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY ([MigrationId], [ContextKey])
)
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'202308050004536_AutomaticMigration', N'DevIO.Infra.Migrations.Configuration',  0x1F8B0800000000000400E55B5F6FDB36107F1FB0EF20E87148AD385D812EB05B247652786B9222768ABE05B474768849A42A52418C629F6C0FFB48FB0A3B59FF28EA8F655BCE52147D8929F277E4F1EEC7E31DFBEFDFFF0CDE3F79AEF10881A09C0DCD7EEFD83480D9DCA16C393443B978F5D67CFFEEE79F06178EF7647C4EFBBD8EFAE1482686E68394FEA96509FB013C227A1EB5032EF842F66CEE59C4E1D6C9F1F16F56BF6F01429888651883DB9049EAC1FA07FE1C7166832F43E25E71075C91B4E397E91AD5B8261E089FD83034C7F038B9E94DD82220BD3191A48783253C49D3387329C1F94CC15D9806618C4B2271B6A77702A632E06C39F5B181B8B3950FD86F415C01C92A4EF3EE6D17747C122DC8CA07A650762824F7B604ECBF4E3464E9C377D2B3996910757881BA96AB68D56B3D0ECD0BE6400036370D5DD8E9C80DA28EA99ACF43411908D18BF7A577C9030636383C00D14B618E8C62E7A3CC4ED09CA27F47C628746518C090412803E21E199FC2B94BED3F6035E37F021BB2D075D539E3ACF15BA1019B3E05DC8740AE6E6191AC64E29886551C67E903B361CA98788D1F428A7F5FA36C327721B308AB71F847BE0C50DB61C05318342EF416D3B8224F1F812DE5C3D0C4AD308D4BFA044EDA9260DF318ACE85836410C2D6B2AF430F1AE5BE3984D411F77C173C60B251F441563C02BF41E65B4D642C623F89E784068D3AEE1F666F47D4210E3CBFDC0B21D19C9F41EE3579A4CB35CB6833C849C5346EC15D77110FD48FA95CE19CFB9CB82E03EEDD72B7303AFB7C3F23C112F04498F1FA3E5374615B9BE5C0CAB9B29141D529EFC5A1F98F1F8B45AFB9D764EB07E2CF31B7C34D3CD6FFF5109267D4E7AAD124E223FBD63F6D897C26E963B69E738EF64ED81ECE98BB5837AE98BA59932BA6EEDA768EF8D309251795734C3EDEAB2ACDA758FE5A228B8A2E7B714582B73D51A4CB4CFFF8B1182257FF61A9E640C7EA18841D509B6C38590F227BE2912578CFBFE6CFC4CDB96D0C36F5886B1A9FD0C76972BDC4806D6A9308F0647B8DE2656F441C22641E9E611BCCA8B7FD543B66CD0D21CCD69CA4B366036DB5E3A4D05318493F7126E2D225CBFC4ABC57445304EF8AB3D032F1B0705768C9F14C264CBE3ED1A8EC0ABC390429EDA2344E2ED1F26C621A689A2136F64B5B5A31E6F730A08E3AEAA4ACE5589F6AE3190EB5E95A5F25A3C84FCBA2706C365A1C9D79ACA0EEDA15EA8DFAA829DCF0AA95358367A75E0E9E1FE99BA0F160C09E2C4AA68CD0D071DB2893E55384329BFAC4DDBC446D68CB2328DA894C88FE650C3EA0142637EB6077E99910ED68DCA49F81A5584BB311555047DD3637F148BECB593852DCE45F1AECA72968DACD367732A0FA693C83FDD42BB78DF06230F30C86149F03516A144764FC76B51ACFB3746989E5EF0424442F920040378808730A52BB2A601C9E1F3B252E29595511443D3B2A7054A3DA8094DF0A4A2899CD6B108AF2AA66A45C72949E0D77217D5FDBF17BB6105D1B253B6947E90A9EB2457AF85E5C7B0BBD54053165B56CE2ABB68CA52C22DFD806853470544BFD36A8248DAB327FCA0B14565CA1482B19564D296370457C1F8317A5B491B418D3B8AE317A35DD3ED5EFC518962D2A32FED96C334992077833D0BE46095E072E6920641462CF4914028E1CAFD44D658F1A574C25E90451DEB7D443D311D1DFE9CDA9BAC0D3AB73B35C9797B8BC28BFB35E2954F84179E8BAC6445C1254DC5E47DC0D3D567F7CD48F568B042A8ADADE1E2D4DFBAB48695B7B94421A5F852A7CD8022F4ACD1770A286F6E3D344BB0A91B66D318B246D5E9848D2D61E254D82AB28695B19656069F6563ACF4BF6AD318DEE30ADDCA9405E9D78543D49B7F0A9A6C187F1AA389353F082754B7B042501ACC228CDEDB1F45BB40AA87F6B8F9A242654B0A4E9C5D861761E776283696CB6BD01D68E3C8CF515637815A739BAAF47ECC09EF32C63C19EF3E6F65869D6B0A0A1A4AD3D4A9203544192A62D5655C8F4151656F8F21D7A552992D4BB64D2B388528B1C074914B7F9A54C29AC8BBB441959FE489D28A49BAE84042F76C8E95777E4525C6FDEE18A30BA0021E3449C7972DC3FD19ED9BC9C272F96108EBBF3BB97097E7A1A9ADF8CBF9EBF3C1232FA35041A5DFFE982425549399B5E34F6D4987CB99F3847C64D80EB39358E71D2FBBF62792481FD4082721976AF372A55A86FBA78815209BCFD6C95F72555886FF77C3E5285D9DF41A9C5C7215DA1169F7EEC86DACD938997E57E7B1418BBF2A2D24B850CB8ACBB2FF759EFA82E829A3CCBBF1ADF8C89B85B2FF1D49805214464A13D77E8E42D038D0E8E7DAA7073BA09A08392FB776D685525F19D0E0F1568AF636493E9EFC24AA5CA790DF0F6C8C5BA7857F32D54BD9DE7A87A3B4482DCBBEABDBBBF7555EAECA0B49987742D0A99C7BD5EE7B5CCE7A83D35243EBFCF4A65A7269091FD2E45CEEF60F7EB13242FB6CE582E28B4AB24361612E3DB2BB2DF3CDAEB98B4EAEB5BF575C60D55C62A318D65A39A3A645319B24A465D95EB7F2952D614CD1A2B70FA26160B2F2FB2FEB8DF82B58D2EE4240F525B2C677ED07395FF4985D421E8328788FE5F152EA4E0B3599F095BF09440B419A55DB4E8E10AF0BA880E7D1660A8496C899F6D1062FDB830792975E1CDC199B09B50FAA1C42583377757AA32220A6A92BF2EA016E73CB8F1D7CFE9BA58024E9346D1D30D3B0FA9EB64F3BEAC087E6A20226EFB00D81EEF2552A584E52A43BAE6AC2550A2BE8C9267E0F92E82891B36258FB0CBDCEE047C8425B1576902AF1E64F34614D53E1853B20C8827128C7C3CFE441B76BCA777FF01C9C7C5C250380000 , N'6.4.4')
