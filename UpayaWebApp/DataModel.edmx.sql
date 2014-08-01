
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/10/2014 01:10:36
-- Generated from EDMX file: C:\Simeon\Upaya\UpayaWebApp\UpayaWebApp\DataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [UpayaDb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_TownCountry]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Towns] DROP CONSTRAINT [FK_TownCountry];
GO
IF OBJECT_ID(N'[dbo].[FK_BeneficiaryGender]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Beneficiaries] DROP CONSTRAINT [FK_BeneficiaryGender];
GO
IF OBJECT_ID(N'[dbo].[FK_ReligionBeneficiary]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Beneficiaries] DROP CONSTRAINT [FK_ReligionBeneficiary];
GO
IF OBJECT_ID(N'[dbo].[FK_TownBeneficiary]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Beneficiaries] DROP CONSTRAINT [FK_TownBeneficiary];
GO
IF OBJECT_ID(N'[dbo].[FK_EducationLevelBeneficiary]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Beneficiaries] DROP CONSTRAINT [FK_EducationLevelBeneficiary];
GO
IF OBJECT_ID(N'[dbo].[FK_GenderAdult]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Adults] DROP CONSTRAINT [FK_GenderAdult];
GO
IF OBJECT_ID(N'[dbo].[FK_EducationLevelAdult]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Adults] DROP CONSTRAINT [FK_EducationLevelAdult];
GO
IF OBJECT_ID(N'[dbo].[FK_BeneficiaryAdult]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Adults] DROP CONSTRAINT [FK_BeneficiaryAdult];
GO
IF OBJECT_ID(N'[dbo].[FK_GenderChild]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Children] DROP CONSTRAINT [FK_GenderChild];
GO
IF OBJECT_ID(N'[dbo].[FK_ChildBeneficiary]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Children] DROP CONSTRAINT [FK_ChildBeneficiary];
GO
IF OBJECT_ID(N'[dbo].[FK_SchoolTypeChild]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Children] DROP CONSTRAINT [FK_SchoolTypeChild];
GO
IF OBJECT_ID(N'[dbo].[FK_ClassLevelChild]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Children] DROP CONSTRAINT [FK_ClassLevelChild];
GO
IF OBJECT_ID(N'[dbo].[FK_GenderPartnerStaffMember]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PartnerStaffMembers] DROP CONSTRAINT [FK_GenderPartnerStaffMember];
GO
IF OBJECT_ID(N'[dbo].[FK_StaffTypePartnerStaffMember]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PartnerStaffMembers] DROP CONSTRAINT [FK_StaffTypePartnerStaffMember];
GO
IF OBJECT_ID(N'[dbo].[FK_LanguageBeneficiary]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Beneficiaries] DROP CONSTRAINT [FK_LanguageBeneficiary];
GO
IF OBJECT_ID(N'[dbo].[FK_CasteBeneficiary]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Beneficiaries] DROP CONSTRAINT [FK_CasteBeneficiary];
GO
IF OBJECT_ID(N'[dbo].[FK_PartnerCompanyPartnerStaffMember]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PartnerStaffMembers] DROP CONSTRAINT [FK_PartnerCompanyPartnerStaffMember];
GO
IF OBJECT_ID(N'[dbo].[FK_PartnerCompanyPartnerAdmin]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PartnerAdmins] DROP CONSTRAINT [FK_PartnerCompanyPartnerAdmin];
GO
IF OBJECT_ID(N'[dbo].[FK_HouseholdAsset2AssetType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HouseholdAssets] DROP CONSTRAINT [FK_HouseholdAsset2AssetType];
GO
IF OBJECT_ID(N'[dbo].[FK_Beneficiary2HouseholdAsset]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HouseholdAssets] DROP CONSTRAINT [FK_Beneficiary2HouseholdAsset];
GO
IF OBJECT_ID(N'[dbo].[FK_Child2ChildRelationship]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Children] DROP CONSTRAINT [FK_Child2ChildRelationship];
GO
IF OBJECT_ID(N'[dbo].[FK_Adult2AdultRelationship]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Adults] DROP CONSTRAINT [FK_Adult2AdultRelationship];
GO
IF OBJECT_ID(N'[dbo].[FK_Beneficiary2Meals]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Meals] DROP CONSTRAINT [FK_Beneficiary2Meals];
GO
IF OBJECT_ID(N'[dbo].[FK_HAssetValue2AssetType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HAssetValues] DROP CONSTRAINT [FK_HAssetValue2AssetType];
GO
IF OBJECT_ID(N'[dbo].[FK_PartnerCompany2HAssetValue]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HAssetValues] DROP CONSTRAINT [FK_PartnerCompany2HAssetValue];
GO
IF OBJECT_ID(N'[dbo].[FK_Child2SchoolDistance]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Children] DROP CONSTRAINT [FK_Child2SchoolDistance];
GO
IF OBJECT_ID(N'[dbo].[FK_State2Country]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[States] DROP CONSTRAINT [FK_State2Country];
GO
IF OBJECT_ID(N'[dbo].[FK_District2State]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Districts] DROP CONSTRAINT [FK_District2State];
GO
IF OBJECT_ID(N'[dbo].[FK_Town2State]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Towns] DROP CONSTRAINT [FK_Town2State];
GO
IF OBJECT_ID(N'[dbo].[FK_Town2District]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Towns] DROP CONSTRAINT [FK_Town2District];
GO
IF OBJECT_ID(N'[dbo].[FK_SchoolDaysPerWeek2Child]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Children] DROP CONSTRAINT [FK_SchoolDaysPerWeek2Child];
GO
IF OBJECT_ID(N'[dbo].[FK_Beneficiary2MajorExpenses]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MajorExpenses] DROP CONSTRAINT [FK_Beneficiary2MajorExpenses];
GO
IF OBJECT_ID(N'[dbo].[FK_Beneficiary2HealthCareInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HealthCareInfos] DROP CONSTRAINT [FK_Beneficiary2HealthCareInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_Beneficiary2GovernmentServices]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GovernmentServices] DROP CONSTRAINT [FK_Beneficiary2GovernmentServices];
GO
IF OBJECT_ID(N'[dbo].[FK_HousingInfo2NumberOfRooms]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HousingInfoes] DROP CONSTRAINT [FK_HousingInfo2NumberOfRooms];
GO
IF OBJECT_ID(N'[dbo].[FK_HousingInfo2RoofMaterial]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HousingInfoes] DROP CONSTRAINT [FK_HousingInfo2RoofMaterial];
GO
IF OBJECT_ID(N'[dbo].[FK_HousingInfo2WallMaterial]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HousingInfoes] DROP CONSTRAINT [FK_HousingInfo2WallMaterial];
GO
IF OBJECT_ID(N'[dbo].[FK_HousingInfo2CookingEnergyType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HousingInfoes] DROP CONSTRAINT [FK_HousingInfo2CookingEnergyType];
GO
IF OBJECT_ID(N'[dbo].[FK_Beneficiary2HousingInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HousingInfoes] DROP CONSTRAINT [FK_Beneficiary2HousingInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_HousingInfo2ToiletType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HousingInfoes] DROP CONSTRAINT [FK_HousingInfo2ToiletType];
GO
IF OBJECT_ID(N'[dbo].[FK_HousingInfoHouseQuality]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HousingInfoes] DROP CONSTRAINT [FK_HousingInfoHouseQuality];
GO
IF OBJECT_ID(N'[dbo].[FK_Beneficiary2PriOccupation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Beneficiaries] DROP CONSTRAINT [FK_Beneficiary2PriOccupation];
GO
IF OBJECT_ID(N'[dbo].[FK_Beneficiary2SecOccupation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Beneficiaries] DROP CONSTRAINT [FK_Beneficiary2SecOccupation];
GO
IF OBJECT_ID(N'[dbo].[FK_Religion2HousingInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HousingInfoes] DROP CONSTRAINT [FK_Religion2HousingInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_Caste2HousingInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HousingInfoes] DROP CONSTRAINT [FK_Caste2HousingInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_HouseOwnershipType2HousingInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HousingInfoes] DROP CONSTRAINT [FK_HouseOwnershipType2HousingInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_WaterSourceDistanceType2HousingInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HousingInfoes] DROP CONSTRAINT [FK_WaterSourceDistanceType2HousingInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_AvgNonVegPerMType2Meals]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Meals] DROP CONSTRAINT [FK_AvgNonVegPerMType2Meals];
GO
IF OBJECT_ID(N'[dbo].[FK_PKGPerMType2Meals]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Meals] DROP CONSTRAINT [FK_PKGPerMType2Meals];
GO
IF OBJECT_ID(N'[dbo].[FK_PrimaryOccupation2Adult]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Adults] DROP CONSTRAINT [FK_PrimaryOccupation2Adult];
GO
IF OBJECT_ID(N'[dbo].[FK_SecondaryOccupation2Adult]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Adults] DROP CONSTRAINT [FK_SecondaryOccupation2Adult];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[PartnerCompanies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PartnerCompanies];
GO
IF OBJECT_ID(N'[dbo].[Towns]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Towns];
GO
IF OBJECT_ID(N'[dbo].[Countries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Countries];
GO
IF OBJECT_ID(N'[dbo].[Beneficiaries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Beneficiaries];
GO
IF OBJECT_ID(N'[dbo].[Genders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Genders];
GO
IF OBJECT_ID(N'[dbo].[Religions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Religions];
GO
IF OBJECT_ID(N'[dbo].[Castes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Castes];
GO
IF OBJECT_ID(N'[dbo].[Disabilities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Disabilities];
GO
IF OBJECT_ID(N'[dbo].[EducationLevels]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EducationLevels];
GO
IF OBJECT_ID(N'[dbo].[Adults]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Adults];
GO
IF OBJECT_ID(N'[dbo].[AdultRelationships]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AdultRelationships];
GO
IF OBJECT_ID(N'[dbo].[Children]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Children];
GO
IF OBJECT_ID(N'[dbo].[ChildRelationships]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ChildRelationships];
GO
IF OBJECT_ID(N'[dbo].[SchoolTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SchoolTypes];
GO
IF OBJECT_ID(N'[dbo].[ClassLevels]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClassLevels];
GO
IF OBJECT_ID(N'[dbo].[PartnerStaffMembers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PartnerStaffMembers];
GO
IF OBJECT_ID(N'[dbo].[StaffTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StaffTypes];
GO
IF OBJECT_ID(N'[dbo].[Languages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Languages];
GO
IF OBJECT_ID(N'[dbo].[PartnerAdmins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PartnerAdmins];
GO
IF OBJECT_ID(N'[dbo].[AssetTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AssetTypes];
GO
IF OBJECT_ID(N'[dbo].[HouseholdAssets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HouseholdAssets];
GO
IF OBJECT_ID(N'[dbo].[Meals]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Meals];
GO
IF OBJECT_ID(N'[dbo].[MealTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MealTypes];
GO
IF OBJECT_ID(N'[dbo].[FoodSourceTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FoodSourceTypes];
GO
IF OBJECT_ID(N'[dbo].[Logins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Logins];
GO
IF OBJECT_ID(N'[dbo].[HAssetValues]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HAssetValues];
GO
IF OBJECT_ID(N'[dbo].[SchoolDistances]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SchoolDistances];
GO
IF OBJECT_ID(N'[dbo].[Occupations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Occupations];
GO
IF OBJECT_ID(N'[dbo].[States]', 'U') IS NOT NULL
    DROP TABLE [dbo].[States];
GO
IF OBJECT_ID(N'[dbo].[Districts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Districts];
GO
IF OBJECT_ID(N'[dbo].[SchoolDaysPerWeek]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SchoolDaysPerWeek];
GO
IF OBJECT_ID(N'[dbo].[MajorExpenses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MajorExpenses];
GO
IF OBJECT_ID(N'[dbo].[HealthCareInfos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HealthCareInfos];
GO
IF OBJECT_ID(N'[dbo].[HcProviderTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HcProviderTypes];
GO
IF OBJECT_ID(N'[dbo].[HealthIssueTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HealthIssueTypes];
GO
IF OBJECT_ID(N'[dbo].[MedSourceTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MedSourceTypes];
GO
IF OBJECT_ID(N'[dbo].[GovernmentServices]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GovernmentServices];
GO
IF OBJECT_ID(N'[dbo].[GovCardTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GovCardTypes];
GO
IF OBJECT_ID(N'[dbo].[GovServiceTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GovServiceTypes];
GO
IF OBJECT_ID(N'[dbo].[HousingInfoes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HousingInfoes];
GO
IF OBJECT_ID(N'[dbo].[NumberOfRoomsTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NumberOfRoomsTypes];
GO
IF OBJECT_ID(N'[dbo].[RoofMaterialTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RoofMaterialTypes];
GO
IF OBJECT_ID(N'[dbo].[WallMaterialTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WallMaterialTypes];
GO
IF OBJECT_ID(N'[dbo].[WaterSourceTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WaterSourceTypes];
GO
IF OBJECT_ID(N'[dbo].[ToiletTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ToiletTypes];
GO
IF OBJECT_ID(N'[dbo].[CookingEnergyTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CookingEnergyTypes];
GO
IF OBJECT_ID(N'[dbo].[HouseQualityTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HouseQualityTypes];
GO
IF OBJECT_ID(N'[dbo].[WaterSourceDistanceTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WaterSourceDistanceTypes];
GO
IF OBJECT_ID(N'[dbo].[HouseOwnershipTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HouseOwnershipTypes];
GO
IF OBJECT_ID(N'[dbo].[SavingsTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SavingsTypes];
GO
IF OBJECT_ID(N'[dbo].[LoanPurposeTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LoanPurposeTypes];
GO
IF OBJECT_ID(N'[dbo].[GrainTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GrainTypes];
GO
IF OBJECT_ID(N'[dbo].[PulseTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PulseTypes];
GO
IF OBJECT_ID(N'[dbo].[VegetableTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VegetableTypes];
GO
IF OBJECT_ID(N'[dbo].[PKG_M_Type]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PKG_M_Type];
GO
IF OBJECT_ID(N'[dbo].[HistoryRecs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HistoryRecs];
GO
IF OBJECT_ID(N'[dbo].[AvgNonVegPerMTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AvgNonVegPerMTypes];
GO
IF OBJECT_ID(N'[dbo].[PKGPerMTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PKGPerMTypes];
GO
IF OBJECT_ID(N'[dbo].[WhyNotInSchoolTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WhyNotInSchoolTypes];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'PartnerCompanies'
CREATE TABLE [dbo].[PartnerCompanies] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(80)  NOT NULL,
    [Phone] varchar(20)  NULL,
    [Fax] varchar(20)  NULL,
    [Email] nvarchar(50)  NOT NULL,
    [StartDateDay] tinyint  NULL,
    [StartDateMonth] tinyint  NULL,
    [StartDateYear] smallint  NOT NULL,
    [Status] tinyint  NULL
);
GO

-- Creating table 'Towns'
CREATE TABLE [dbo].[Towns] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [CountryId] char(2)  NOT NULL,
    [StateId] smallint  NOT NULL,
    [DistrictId] smallint  NOT NULL,
    [PostalCode] nvarchar(max)  NULL
);
GO

-- Creating table 'Countries'
CREATE TABLE [dbo].[Countries] (
    [Id] char(2)  NOT NULL,
    [Name] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Beneficiaries'
CREATE TABLE [dbo].[Beneficiaries] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(100)  NOT NULL,
    [GenderId] tinyint  NOT NULL,
    [ReligionId] smallint  NOT NULL,
    [TownId] int  NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [Phone] nvarchar(max)  NULL,
    [Disability] bit  NOT NULL,
    [Disabilities] varchar(80)  NULL,
    [EducationLevelId] smallint  NOT NULL,
    [BirthDay] tinyint  NULL,
    [BirthMonth] tinyint  NULL,
    [BirthYear] smallint  NOT NULL,
    [LanguageId] smallint  NOT NULL,
    [CasteId] smallint  NOT NULL,
    [UniqueId] nvarchar(10)  NOT NULL,
    [Status] tinyint  NULL,
    [PhotoUrl] nvarchar(max)  NULL,
    [Block] nvarchar(60)  NULL,
    [PrimaryOccupationId] tinyint  NOT NULL,
    [SecondaryOccupationId] tinyint  NOT NULL,
    [PrimaryWorkDaysM] tinyint  NULL,
    [SecondaryWorkDaysM] tinyint  NULL,
    [PrimaryDailyWage] smallint  NULL,
    [SecondaryDailyWage] smallint  NULL,
    [StartDateDay] tinyint  NULL,
    [StartDateMonth] tinyint  NULL,
    [StartDateYear] smallint  NULL,
    [OrigEntryDate] datetime  NULL,
    [PCompanyId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Genders'
CREATE TABLE [dbo].[Genders] (
    [Id] tinyint IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Religions'
CREATE TABLE [dbo].[Religions] (
    [Id] smallint IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Castes'
CREATE TABLE [dbo].[Castes] (
    [Id] smallint IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Disabilities'
CREATE TABLE [dbo].[Disabilities] (
    [Id] smallint IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'EducationLevels'
CREATE TABLE [dbo].[EducationLevels] (
    [Id] smallint IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Adults'
CREATE TABLE [dbo].[Adults] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [GenderId] tinyint  NOT NULL,
    [EducationLevelId] smallint  NOT NULL,
    [BeneficiaryId] uniqueidentifier  NOT NULL,
    [AdultRelationshipId] tinyint  NOT NULL,
    [Disability] bit  NOT NULL,
    [Disabilities] varchar(80)  NULL,
    [BirthDay] tinyint  NULL,
    [BirthMonth] tinyint  NULL,
    [BirthYear] smallint  NOT NULL,
    [Status] tinyint  NULL,
    [EmplSameCompany] bit  NOT NULL,
    [PrimaryOccupationId] tinyint  NOT NULL,
    [SecondaryOccupationId] tinyint  NOT NULL,
    [PrimaryWorkDaysM] tinyint  NULL,
    [SecondaryWorkDaysM] tinyint  NULL,
    [PrimaryDailyWage] smallint  NULL,
    [SecondaryDailyWage] smallint  NULL,
    [OrigEntryDate] datetime  NULL
);
GO

-- Creating table 'AdultRelationships'
CREATE TABLE [dbo].[AdultRelationships] (
    [Id] tinyint IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Children'
CREATE TABLE [dbo].[Children] (
    [Id] uniqueidentifier  NOT NULL,
    [BeneficiaryId] uniqueidentifier  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [GenderId] tinyint  NOT NULL,
    [ChildRelationshipId] tinyint  NOT NULL,
    [Disability] bit  NOT NULL,
    [Disabilities] varchar(80)  NULL,
    [EnrolledInSchool] bit  NOT NULL,
    [WhyNotInSchool] nvarchar(max)  NULL,
    [SchoolTypeId] tinyint  NULL,
    [SchoolDistanceId] tinyint  NULL,
    [ClassLevelId] tinyint  NULL,
    [MonthlyEduExpenses] float  NULL,
    [BirthDay] tinyint  NULL,
    [BirthMonth] tinyint  NULL,
    [BirthYear] smallint  NOT NULL,
    [Status] tinyint  NULL,
    [SchoolAttendance] tinyint  NULL,
    [OrigEntryDate] datetime  NULL
);
GO

-- Creating table 'ChildRelationships'
CREATE TABLE [dbo].[ChildRelationships] (
    [Id] tinyint IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'SchoolTypes'
CREATE TABLE [dbo].[SchoolTypes] (
    [Id] tinyint IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ClassLevels'
CREATE TABLE [dbo].[ClassLevels] (
    [Id] tinyint IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'PartnerStaffMembers'
CREATE TABLE [dbo].[PartnerStaffMembers] (
    [Id] uniqueidentifier  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [GenderId] tinyint  NOT NULL,
    [Address] nvarchar(200)  NOT NULL,
    [Phone] nvarchar(60)  NOT NULL,
    [Email] nvarchar(60)  NOT NULL,
    [Title] nvarchar(max)  NULL,
    [BirthDay] tinyint  NULL,
    [BirthMonth] tinyint  NULL,
    [BirthYear] smallint  NOT NULL,
    [StaffTypeId] tinyint  NOT NULL,
    [InternalPartnerEmployeeId] nvarchar(max)  NULL,
    [PartnerCompanyId] uniqueidentifier  NOT NULL,
    [Status] tinyint  NULL
);
GO

-- Creating table 'StaffTypes'
CREATE TABLE [dbo].[StaffTypes] (
    [Id] tinyint IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Languages'
CREATE TABLE [dbo].[Languages] (
    [Id] smallint IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'PartnerAdmins'
CREATE TABLE [dbo].[PartnerAdmins] (
    [Id] uniqueidentifier  NOT NULL,
    [PartnerCompanyId] uniqueidentifier  NOT NULL,
    [Status] tinyint  NULL
);
GO

-- Creating table 'AssetTypes'
CREATE TABLE [dbo].[AssetTypes] (
    [Id] smallint IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'HouseholdAssets'
CREATE TABLE [dbo].[HouseholdAssets] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [AssetTypeId] smallint  NOT NULL,
    [BeneficiaryId] uniqueidentifier  NOT NULL,
    [Count] smallint  NOT NULL,
    [TotalValue] int  NULL,
    [OrigEntryDate] datetime  NULL
);
GO

-- Creating table 'Meals'
CREATE TABLE [dbo].[Meals] (
    [Id] uniqueidentifier  NOT NULL,
    [AvgMealsPerDay] tinyint  NOT NULL,
    [AnyNonVegetarian] bit  NOT NULL,
    [MealTypes] nvarchar(80)  NULL,
    [FoodSourceTypes] nvarchar(80)  NOT NULL,
    [MilkAsideAmount] decimal(4,1)  NULL,
    [CEGrains] nvarchar(max)  NULL,
    [CEPulse] nvarchar(max)  NULL,
    [CEVegetables] nvarchar(max)  NULL,
    [AvgNonVegPerMTypeId] tinyint  NOT NULL,
    [PKGPerMTypeId] tinyint  NOT NULL,
    [OrigEntryDate] datetime  NULL,
    [Beneficiary_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'MealTypes'
CREATE TABLE [dbo].[MealTypes] (
    [Id] tinyint IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'FoodSourceTypes'
CREATE TABLE [dbo].[FoodSourceTypes] (
    [Id] tinyint IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Logins'
CREATE TABLE [dbo].[Logins] (
    [Id] uniqueidentifier  NOT NULL,
    [LastLoginTime] datetime  NOT NULL
);
GO

-- Creating table 'HAssetValues'
CREATE TABLE [dbo].[HAssetValues] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PartnerCompanyId] uniqueidentifier  NOT NULL,
    [AssetTypeId] smallint  NOT NULL,
    [Value] int  NOT NULL,
    [OrigEntryDate] datetime  NULL
);
GO

-- Creating table 'SchoolDistances'
CREATE TABLE [dbo].[SchoolDistances] (
    [Id] tinyint IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(30)  NOT NULL
);
GO

-- Creating table 'Occupations'
CREATE TABLE [dbo].[Occupations] (
    [Id] tinyint IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(40)  NOT NULL
);
GO

-- Creating table 'States'
CREATE TABLE [dbo].[States] (
    [Id] smallint IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(60)  NOT NULL,
    [Abbr] char(3)  NOT NULL,
    [CountryId] char(2)  NOT NULL
);
GO

-- Creating table 'Districts'
CREATE TABLE [dbo].[Districts] (
    [Id] smallint IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(60)  NOT NULL,
    [Abbr] char(3)  NOT NULL,
    [StateId] smallint  NOT NULL
);
GO

-- Creating table 'SchoolDaysPerWeek'
CREATE TABLE [dbo].[SchoolDaysPerWeek] (
    [Id] tinyint  NOT NULL,
    [Title] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'MajorExpenses'
CREATE TABLE [dbo].[MajorExpenses] (
    [Id] uniqueidentifier  NOT NULL,
    [FoodM] int  NOT NULL,
    [RentM] int  NOT NULL,
    [SchoolFeesM] int  NOT NULL,
    [WaterAndElecM] smallint  NOT NULL,
    [CableTvDishM] int  NOT NULL,
    [LoanRepaymentsM] int  NOT NULL,
    [AlcoholM] smallint  NOT NULL,
    [CinemaFestivFunctA] int  NOT NULL,
    [LoomRelA] int  NOT NULL,
    [OtherExpM] int  NOT NULL,
    [OtherExpDescr] nvarchar(max)  NULL,
    [OrigEntryDate] datetime  NULL,
    [Beneficiary_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'HealthCareInfos'
CREATE TABLE [dbo].[HealthCareInfos] (
    [Id] uniqueidentifier  NOT NULL,
    [HcProviders] nvarchar(max)  NOT NULL,
    [HcFrequencyQ] smallint  NULL,
    [HealthIssues] nvarchar(max)  NULL,
    [TreatmentCostsQ] int  NULL,
    [MedSources] nvarchar(max)  NULL,
    [OrigEntryDate] datetime  NULL,
    [Beneficiary_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'HcProviderTypes'
CREATE TABLE [dbo].[HcProviderTypes] (
    [Id] tinyint IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(60)  NOT NULL
);
GO

-- Creating table 'HealthIssueTypes'
CREATE TABLE [dbo].[HealthIssueTypes] (
    [Id] tinyint IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(60)  NOT NULL
);
GO

-- Creating table 'MedSourceTypes'
CREATE TABLE [dbo].[MedSourceTypes] (
    [Id] tinyint IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(60)  NOT NULL
);
GO

-- Creating table 'GovernmentServices'
CREATE TABLE [dbo].[GovernmentServices] (
    [Id] uniqueidentifier  NOT NULL,
    [GovCards] nvarchar(max)  NOT NULL,
    [OtherCardDescr] nvarchar(max)  NULL,
    [RcvGovPension] bit  NULL,
    [RcvPrivatePension] bit  NULL,
    [GovServices] nvarchar(max)  NOT NULL,
    [OrigEntryDate] datetime  NULL,
    [Beneficiary_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'GovCardTypes'
CREATE TABLE [dbo].[GovCardTypes] (
    [Id] smallint IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(60)  NOT NULL
);
GO

-- Creating table 'GovServiceTypes'
CREATE TABLE [dbo].[GovServiceTypes] (
    [Id] smallint IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(60)  NOT NULL
);
GO

-- Creating table 'HousingInfoes'
CREATE TABLE [dbo].[HousingInfoes] (
    [Id] uniqueidentifier  NOT NULL,
    [HasElectricity] bit  NOT NULL,
    [NumberOfRoomsTypeId] tinyint  NOT NULL,
    [RoofMaterialTypeId] tinyint  NOT NULL,
    [WaterSourceTypes] nvarchar(max)  NOT NULL,
    [WallMaterialTypeId] tinyint  NOT NULL,
    [CookingEnergyTypeId] tinyint  NOT NULL,
    [ToiletTypeId] tinyint  NOT NULL,
    [HouseQualityTypeId] tinyint  NOT NULL,
    [ReligionId] smallint  NOT NULL,
    [CasteId] smallint  NOT NULL,
    [HouseOwnershipTypeId] tinyint  NOT NULL,
    [WaterSourceDistanceTypeId] tinyint  NOT NULL,
    [SavingsTypes] nvarchar(max)  NOT NULL,
    [LoanPurposeTypes] nvarchar(max)  NOT NULL,
    [OrigEntryDate] datetime  NULL,
    [Beneficiary_Id] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'NumberOfRoomsTypes'
CREATE TABLE [dbo].[NumberOfRoomsTypes] (
    [Id] tinyint IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(20)  NOT NULL
);
GO

-- Creating table 'RoofMaterialTypes'
CREATE TABLE [dbo].[RoofMaterialTypes] (
    [Id] tinyint IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'WallMaterialTypes'
CREATE TABLE [dbo].[WallMaterialTypes] (
    [Id] tinyint IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'WaterSourceTypes'
CREATE TABLE [dbo].[WaterSourceTypes] (
    [Id] tinyint IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'ToiletTypes'
CREATE TABLE [dbo].[ToiletTypes] (
    [Id] tinyint IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CookingEnergyTypes'
CREATE TABLE [dbo].[CookingEnergyTypes] (
    [Id] tinyint IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'HouseQualityTypes'
CREATE TABLE [dbo].[HouseQualityTypes] (
    [Id] tinyint  NOT NULL,
    [Title] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'WaterSourceDistanceTypes'
CREATE TABLE [dbo].[WaterSourceDistanceTypes] (
    [Id] tinyint IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'HouseOwnershipTypes'
CREATE TABLE [dbo].[HouseOwnershipTypes] (
    [Id] tinyint IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'SavingsTypes'
CREATE TABLE [dbo].[SavingsTypes] (
    [Id] tinyint IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'LoanPurposeTypes'
CREATE TABLE [dbo].[LoanPurposeTypes] (
    [Id] tinyint IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'GrainTypes'
CREATE TABLE [dbo].[GrainTypes] (
    [Id] tinyint IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'PulseTypes'
CREATE TABLE [dbo].[PulseTypes] (
    [Id] tinyint IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'VegetableTypes'
CREATE TABLE [dbo].[VegetableTypes] (
    [Id] tinyint IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'PKG_M_Type'
CREATE TABLE [dbo].[PKG_M_Type] (
    [Id] tinyint IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'HistoryRecs'
CREATE TABLE [dbo].[HistoryRecs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RefId] uniqueidentifier  NOT NULL,
    [Type] tinyint  NOT NULL,
    [TimeStamp] datetime  NOT NULL,
    [User] uniqueidentifier  NOT NULL,
    [Value] nvarchar(max)  NOT NULL,
    [Created] bit  NOT NULL
);
GO

-- Creating table 'AvgNonVegPerMTypes'
CREATE TABLE [dbo].[AvgNonVegPerMTypes] (
    [Id] tinyint IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'PKGPerMTypes'
CREATE TABLE [dbo].[PKGPerMTypes] (
    [Id] tinyint IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'WhyNotInSchoolTypes'
CREATE TABLE [dbo].[WhyNotInSchoolTypes] (
    [Id] tinyint IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'PartnerCompanies'
ALTER TABLE [dbo].[PartnerCompanies]
ADD CONSTRAINT [PK_PartnerCompanies]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Towns'
ALTER TABLE [dbo].[Towns]
ADD CONSTRAINT [PK_Towns]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Countries'
ALTER TABLE [dbo].[Countries]
ADD CONSTRAINT [PK_Countries]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Beneficiaries'
ALTER TABLE [dbo].[Beneficiaries]
ADD CONSTRAINT [PK_Beneficiaries]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Genders'
ALTER TABLE [dbo].[Genders]
ADD CONSTRAINT [PK_Genders]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Religions'
ALTER TABLE [dbo].[Religions]
ADD CONSTRAINT [PK_Religions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Castes'
ALTER TABLE [dbo].[Castes]
ADD CONSTRAINT [PK_Castes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Disabilities'
ALTER TABLE [dbo].[Disabilities]
ADD CONSTRAINT [PK_Disabilities]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EducationLevels'
ALTER TABLE [dbo].[EducationLevels]
ADD CONSTRAINT [PK_EducationLevels]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Adults'
ALTER TABLE [dbo].[Adults]
ADD CONSTRAINT [PK_Adults]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AdultRelationships'
ALTER TABLE [dbo].[AdultRelationships]
ADD CONSTRAINT [PK_AdultRelationships]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Children'
ALTER TABLE [dbo].[Children]
ADD CONSTRAINT [PK_Children]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ChildRelationships'
ALTER TABLE [dbo].[ChildRelationships]
ADD CONSTRAINT [PK_ChildRelationships]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SchoolTypes'
ALTER TABLE [dbo].[SchoolTypes]
ADD CONSTRAINT [PK_SchoolTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ClassLevels'
ALTER TABLE [dbo].[ClassLevels]
ADD CONSTRAINT [PK_ClassLevels]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PartnerStaffMembers'
ALTER TABLE [dbo].[PartnerStaffMembers]
ADD CONSTRAINT [PK_PartnerStaffMembers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'StaffTypes'
ALTER TABLE [dbo].[StaffTypes]
ADD CONSTRAINT [PK_StaffTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Languages'
ALTER TABLE [dbo].[Languages]
ADD CONSTRAINT [PK_Languages]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PartnerAdmins'
ALTER TABLE [dbo].[PartnerAdmins]
ADD CONSTRAINT [PK_PartnerAdmins]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AssetTypes'
ALTER TABLE [dbo].[AssetTypes]
ADD CONSTRAINT [PK_AssetTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'HouseholdAssets'
ALTER TABLE [dbo].[HouseholdAssets]
ADD CONSTRAINT [PK_HouseholdAssets]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Meals'
ALTER TABLE [dbo].[Meals]
ADD CONSTRAINT [PK_Meals]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MealTypes'
ALTER TABLE [dbo].[MealTypes]
ADD CONSTRAINT [PK_MealTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FoodSourceTypes'
ALTER TABLE [dbo].[FoodSourceTypes]
ADD CONSTRAINT [PK_FoodSourceTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Logins'
ALTER TABLE [dbo].[Logins]
ADD CONSTRAINT [PK_Logins]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'HAssetValues'
ALTER TABLE [dbo].[HAssetValues]
ADD CONSTRAINT [PK_HAssetValues]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SchoolDistances'
ALTER TABLE [dbo].[SchoolDistances]
ADD CONSTRAINT [PK_SchoolDistances]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Occupations'
ALTER TABLE [dbo].[Occupations]
ADD CONSTRAINT [PK_Occupations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'States'
ALTER TABLE [dbo].[States]
ADD CONSTRAINT [PK_States]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Districts'
ALTER TABLE [dbo].[Districts]
ADD CONSTRAINT [PK_Districts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SchoolDaysPerWeek'
ALTER TABLE [dbo].[SchoolDaysPerWeek]
ADD CONSTRAINT [PK_SchoolDaysPerWeek]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MajorExpenses'
ALTER TABLE [dbo].[MajorExpenses]
ADD CONSTRAINT [PK_MajorExpenses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'HealthCareInfos'
ALTER TABLE [dbo].[HealthCareInfos]
ADD CONSTRAINT [PK_HealthCareInfos]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'HcProviderTypes'
ALTER TABLE [dbo].[HcProviderTypes]
ADD CONSTRAINT [PK_HcProviderTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'HealthIssueTypes'
ALTER TABLE [dbo].[HealthIssueTypes]
ADD CONSTRAINT [PK_HealthIssueTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MedSourceTypes'
ALTER TABLE [dbo].[MedSourceTypes]
ADD CONSTRAINT [PK_MedSourceTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'GovernmentServices'
ALTER TABLE [dbo].[GovernmentServices]
ADD CONSTRAINT [PK_GovernmentServices]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'GovCardTypes'
ALTER TABLE [dbo].[GovCardTypes]
ADD CONSTRAINT [PK_GovCardTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'GovServiceTypes'
ALTER TABLE [dbo].[GovServiceTypes]
ADD CONSTRAINT [PK_GovServiceTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'HousingInfoes'
ALTER TABLE [dbo].[HousingInfoes]
ADD CONSTRAINT [PK_HousingInfoes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'NumberOfRoomsTypes'
ALTER TABLE [dbo].[NumberOfRoomsTypes]
ADD CONSTRAINT [PK_NumberOfRoomsTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RoofMaterialTypes'
ALTER TABLE [dbo].[RoofMaterialTypes]
ADD CONSTRAINT [PK_RoofMaterialTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WallMaterialTypes'
ALTER TABLE [dbo].[WallMaterialTypes]
ADD CONSTRAINT [PK_WallMaterialTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WaterSourceTypes'
ALTER TABLE [dbo].[WaterSourceTypes]
ADD CONSTRAINT [PK_WaterSourceTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ToiletTypes'
ALTER TABLE [dbo].[ToiletTypes]
ADD CONSTRAINT [PK_ToiletTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CookingEnergyTypes'
ALTER TABLE [dbo].[CookingEnergyTypes]
ADD CONSTRAINT [PK_CookingEnergyTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'HouseQualityTypes'
ALTER TABLE [dbo].[HouseQualityTypes]
ADD CONSTRAINT [PK_HouseQualityTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WaterSourceDistanceTypes'
ALTER TABLE [dbo].[WaterSourceDistanceTypes]
ADD CONSTRAINT [PK_WaterSourceDistanceTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'HouseOwnershipTypes'
ALTER TABLE [dbo].[HouseOwnershipTypes]
ADD CONSTRAINT [PK_HouseOwnershipTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SavingsTypes'
ALTER TABLE [dbo].[SavingsTypes]
ADD CONSTRAINT [PK_SavingsTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LoanPurposeTypes'
ALTER TABLE [dbo].[LoanPurposeTypes]
ADD CONSTRAINT [PK_LoanPurposeTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'GrainTypes'
ALTER TABLE [dbo].[GrainTypes]
ADD CONSTRAINT [PK_GrainTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PulseTypes'
ALTER TABLE [dbo].[PulseTypes]
ADD CONSTRAINT [PK_PulseTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'VegetableTypes'
ALTER TABLE [dbo].[VegetableTypes]
ADD CONSTRAINT [PK_VegetableTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PKG_M_Type'
ALTER TABLE [dbo].[PKG_M_Type]
ADD CONSTRAINT [PK_PKG_M_Type]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'HistoryRecs'
ALTER TABLE [dbo].[HistoryRecs]
ADD CONSTRAINT [PK_HistoryRecs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AvgNonVegPerMTypes'
ALTER TABLE [dbo].[AvgNonVegPerMTypes]
ADD CONSTRAINT [PK_AvgNonVegPerMTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PKGPerMTypes'
ALTER TABLE [dbo].[PKGPerMTypes]
ADD CONSTRAINT [PK_PKGPerMTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WhyNotInSchoolTypes'
ALTER TABLE [dbo].[WhyNotInSchoolTypes]
ADD CONSTRAINT [PK_WhyNotInSchoolTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CountryId] in table 'Towns'
ALTER TABLE [dbo].[Towns]
ADD CONSTRAINT [FK_TownCountry]
    FOREIGN KEY ([CountryId])
    REFERENCES [dbo].[Countries]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TownCountry'
CREATE INDEX [IX_FK_TownCountry]
ON [dbo].[Towns]
    ([CountryId]);
GO

-- Creating foreign key on [GenderId] in table 'Beneficiaries'
ALTER TABLE [dbo].[Beneficiaries]
ADD CONSTRAINT [FK_BeneficiaryGender]
    FOREIGN KEY ([GenderId])
    REFERENCES [dbo].[Genders]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BeneficiaryGender'
CREATE INDEX [IX_FK_BeneficiaryGender]
ON [dbo].[Beneficiaries]
    ([GenderId]);
GO

-- Creating foreign key on [ReligionId] in table 'Beneficiaries'
ALTER TABLE [dbo].[Beneficiaries]
ADD CONSTRAINT [FK_ReligionBeneficiary]
    FOREIGN KEY ([ReligionId])
    REFERENCES [dbo].[Religions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ReligionBeneficiary'
CREATE INDEX [IX_FK_ReligionBeneficiary]
ON [dbo].[Beneficiaries]
    ([ReligionId]);
GO

-- Creating foreign key on [TownId] in table 'Beneficiaries'
ALTER TABLE [dbo].[Beneficiaries]
ADD CONSTRAINT [FK_TownBeneficiary]
    FOREIGN KEY ([TownId])
    REFERENCES [dbo].[Towns]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TownBeneficiary'
CREATE INDEX [IX_FK_TownBeneficiary]
ON [dbo].[Beneficiaries]
    ([TownId]);
GO

-- Creating foreign key on [EducationLevelId] in table 'Beneficiaries'
ALTER TABLE [dbo].[Beneficiaries]
ADD CONSTRAINT [FK_EducationLevelBeneficiary]
    FOREIGN KEY ([EducationLevelId])
    REFERENCES [dbo].[EducationLevels]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EducationLevelBeneficiary'
CREATE INDEX [IX_FK_EducationLevelBeneficiary]
ON [dbo].[Beneficiaries]
    ([EducationLevelId]);
GO

-- Creating foreign key on [GenderId] in table 'Adults'
ALTER TABLE [dbo].[Adults]
ADD CONSTRAINT [FK_GenderAdult]
    FOREIGN KEY ([GenderId])
    REFERENCES [dbo].[Genders]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GenderAdult'
CREATE INDEX [IX_FK_GenderAdult]
ON [dbo].[Adults]
    ([GenderId]);
GO

-- Creating foreign key on [EducationLevelId] in table 'Adults'
ALTER TABLE [dbo].[Adults]
ADD CONSTRAINT [FK_EducationLevelAdult]
    FOREIGN KEY ([EducationLevelId])
    REFERENCES [dbo].[EducationLevels]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EducationLevelAdult'
CREATE INDEX [IX_FK_EducationLevelAdult]
ON [dbo].[Adults]
    ([EducationLevelId]);
GO

-- Creating foreign key on [BeneficiaryId] in table 'Adults'
ALTER TABLE [dbo].[Adults]
ADD CONSTRAINT [FK_BeneficiaryAdult]
    FOREIGN KEY ([BeneficiaryId])
    REFERENCES [dbo].[Beneficiaries]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BeneficiaryAdult'
CREATE INDEX [IX_FK_BeneficiaryAdult]
ON [dbo].[Adults]
    ([BeneficiaryId]);
GO

-- Creating foreign key on [GenderId] in table 'Children'
ALTER TABLE [dbo].[Children]
ADD CONSTRAINT [FK_GenderChild]
    FOREIGN KEY ([GenderId])
    REFERENCES [dbo].[Genders]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GenderChild'
CREATE INDEX [IX_FK_GenderChild]
ON [dbo].[Children]
    ([GenderId]);
GO

-- Creating foreign key on [BeneficiaryId] in table 'Children'
ALTER TABLE [dbo].[Children]
ADD CONSTRAINT [FK_ChildBeneficiary]
    FOREIGN KEY ([BeneficiaryId])
    REFERENCES [dbo].[Beneficiaries]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ChildBeneficiary'
CREATE INDEX [IX_FK_ChildBeneficiary]
ON [dbo].[Children]
    ([BeneficiaryId]);
GO

-- Creating foreign key on [SchoolTypeId] in table 'Children'
ALTER TABLE [dbo].[Children]
ADD CONSTRAINT [FK_SchoolTypeChild]
    FOREIGN KEY ([SchoolTypeId])
    REFERENCES [dbo].[SchoolTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SchoolTypeChild'
CREATE INDEX [IX_FK_SchoolTypeChild]
ON [dbo].[Children]
    ([SchoolTypeId]);
GO

-- Creating foreign key on [ClassLevelId] in table 'Children'
ALTER TABLE [dbo].[Children]
ADD CONSTRAINT [FK_ClassLevelChild]
    FOREIGN KEY ([ClassLevelId])
    REFERENCES [dbo].[ClassLevels]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ClassLevelChild'
CREATE INDEX [IX_FK_ClassLevelChild]
ON [dbo].[Children]
    ([ClassLevelId]);
GO

-- Creating foreign key on [GenderId] in table 'PartnerStaffMembers'
ALTER TABLE [dbo].[PartnerStaffMembers]
ADD CONSTRAINT [FK_GenderPartnerStaffMember]
    FOREIGN KEY ([GenderId])
    REFERENCES [dbo].[Genders]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GenderPartnerStaffMember'
CREATE INDEX [IX_FK_GenderPartnerStaffMember]
ON [dbo].[PartnerStaffMembers]
    ([GenderId]);
GO

-- Creating foreign key on [StaffTypeId] in table 'PartnerStaffMembers'
ALTER TABLE [dbo].[PartnerStaffMembers]
ADD CONSTRAINT [FK_StaffTypePartnerStaffMember]
    FOREIGN KEY ([StaffTypeId])
    REFERENCES [dbo].[StaffTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_StaffTypePartnerStaffMember'
CREATE INDEX [IX_FK_StaffTypePartnerStaffMember]
ON [dbo].[PartnerStaffMembers]
    ([StaffTypeId]);
GO

-- Creating foreign key on [LanguageId] in table 'Beneficiaries'
ALTER TABLE [dbo].[Beneficiaries]
ADD CONSTRAINT [FK_LanguageBeneficiary]
    FOREIGN KEY ([LanguageId])
    REFERENCES [dbo].[Languages]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LanguageBeneficiary'
CREATE INDEX [IX_FK_LanguageBeneficiary]
ON [dbo].[Beneficiaries]
    ([LanguageId]);
GO

-- Creating foreign key on [CasteId] in table 'Beneficiaries'
ALTER TABLE [dbo].[Beneficiaries]
ADD CONSTRAINT [FK_CasteBeneficiary]
    FOREIGN KEY ([CasteId])
    REFERENCES [dbo].[Castes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CasteBeneficiary'
CREATE INDEX [IX_FK_CasteBeneficiary]
ON [dbo].[Beneficiaries]
    ([CasteId]);
GO

-- Creating foreign key on [PartnerCompanyId] in table 'PartnerStaffMembers'
ALTER TABLE [dbo].[PartnerStaffMembers]
ADD CONSTRAINT [FK_PartnerCompanyPartnerStaffMember]
    FOREIGN KEY ([PartnerCompanyId])
    REFERENCES [dbo].[PartnerCompanies]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PartnerCompanyPartnerStaffMember'
CREATE INDEX [IX_FK_PartnerCompanyPartnerStaffMember]
ON [dbo].[PartnerStaffMembers]
    ([PartnerCompanyId]);
GO

-- Creating foreign key on [PartnerCompanyId] in table 'PartnerAdmins'
ALTER TABLE [dbo].[PartnerAdmins]
ADD CONSTRAINT [FK_PartnerCompanyPartnerAdmin]
    FOREIGN KEY ([PartnerCompanyId])
    REFERENCES [dbo].[PartnerCompanies]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PartnerCompanyPartnerAdmin'
CREATE INDEX [IX_FK_PartnerCompanyPartnerAdmin]
ON [dbo].[PartnerAdmins]
    ([PartnerCompanyId]);
GO

-- Creating foreign key on [AssetTypeId] in table 'HouseholdAssets'
ALTER TABLE [dbo].[HouseholdAssets]
ADD CONSTRAINT [FK_HouseholdAsset2AssetType]
    FOREIGN KEY ([AssetTypeId])
    REFERENCES [dbo].[AssetTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_HouseholdAsset2AssetType'
CREATE INDEX [IX_FK_HouseholdAsset2AssetType]
ON [dbo].[HouseholdAssets]
    ([AssetTypeId]);
GO

-- Creating foreign key on [BeneficiaryId] in table 'HouseholdAssets'
ALTER TABLE [dbo].[HouseholdAssets]
ADD CONSTRAINT [FK_Beneficiary2HouseholdAsset]
    FOREIGN KEY ([BeneficiaryId])
    REFERENCES [dbo].[Beneficiaries]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Beneficiary2HouseholdAsset'
CREATE INDEX [IX_FK_Beneficiary2HouseholdAsset]
ON [dbo].[HouseholdAssets]
    ([BeneficiaryId]);
GO

-- Creating foreign key on [ChildRelationshipId] in table 'Children'
ALTER TABLE [dbo].[Children]
ADD CONSTRAINT [FK_Child2ChildRelationship]
    FOREIGN KEY ([ChildRelationshipId])
    REFERENCES [dbo].[ChildRelationships]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Child2ChildRelationship'
CREATE INDEX [IX_FK_Child2ChildRelationship]
ON [dbo].[Children]
    ([ChildRelationshipId]);
GO

-- Creating foreign key on [AdultRelationshipId] in table 'Adults'
ALTER TABLE [dbo].[Adults]
ADD CONSTRAINT [FK_Adult2AdultRelationship]
    FOREIGN KEY ([AdultRelationshipId])
    REFERENCES [dbo].[AdultRelationships]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Adult2AdultRelationship'
CREATE INDEX [IX_FK_Adult2AdultRelationship]
ON [dbo].[Adults]
    ([AdultRelationshipId]);
GO

-- Creating foreign key on [Beneficiary_Id] in table 'Meals'
ALTER TABLE [dbo].[Meals]
ADD CONSTRAINT [FK_Beneficiary2Meals]
    FOREIGN KEY ([Beneficiary_Id])
    REFERENCES [dbo].[Beneficiaries]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Beneficiary2Meals'
CREATE INDEX [IX_FK_Beneficiary2Meals]
ON [dbo].[Meals]
    ([Beneficiary_Id]);
GO

-- Creating foreign key on [AssetTypeId] in table 'HAssetValues'
ALTER TABLE [dbo].[HAssetValues]
ADD CONSTRAINT [FK_HAssetValue2AssetType]
    FOREIGN KEY ([AssetTypeId])
    REFERENCES [dbo].[AssetTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_HAssetValue2AssetType'
CREATE INDEX [IX_FK_HAssetValue2AssetType]
ON [dbo].[HAssetValues]
    ([AssetTypeId]);
GO

-- Creating foreign key on [PartnerCompanyId] in table 'HAssetValues'
ALTER TABLE [dbo].[HAssetValues]
ADD CONSTRAINT [FK_PartnerCompany2HAssetValue]
    FOREIGN KEY ([PartnerCompanyId])
    REFERENCES [dbo].[PartnerCompanies]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PartnerCompany2HAssetValue'
CREATE INDEX [IX_FK_PartnerCompany2HAssetValue]
ON [dbo].[HAssetValues]
    ([PartnerCompanyId]);
GO

-- Creating foreign key on [SchoolDistanceId] in table 'Children'
ALTER TABLE [dbo].[Children]
ADD CONSTRAINT [FK_Child2SchoolDistance]
    FOREIGN KEY ([SchoolDistanceId])
    REFERENCES [dbo].[SchoolDistances]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Child2SchoolDistance'
CREATE INDEX [IX_FK_Child2SchoolDistance]
ON [dbo].[Children]
    ([SchoolDistanceId]);
GO

-- Creating foreign key on [CountryId] in table 'States'
ALTER TABLE [dbo].[States]
ADD CONSTRAINT [FK_State2Country]
    FOREIGN KEY ([CountryId])
    REFERENCES [dbo].[Countries]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_State2Country'
CREATE INDEX [IX_FK_State2Country]
ON [dbo].[States]
    ([CountryId]);
GO

-- Creating foreign key on [StateId] in table 'Districts'
ALTER TABLE [dbo].[Districts]
ADD CONSTRAINT [FK_District2State]
    FOREIGN KEY ([StateId])
    REFERENCES [dbo].[States]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_District2State'
CREATE INDEX [IX_FK_District2State]
ON [dbo].[Districts]
    ([StateId]);
GO

-- Creating foreign key on [StateId] in table 'Towns'
ALTER TABLE [dbo].[Towns]
ADD CONSTRAINT [FK_Town2State]
    FOREIGN KEY ([StateId])
    REFERENCES [dbo].[States]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Town2State'
CREATE INDEX [IX_FK_Town2State]
ON [dbo].[Towns]
    ([StateId]);
GO

-- Creating foreign key on [DistrictId] in table 'Towns'
ALTER TABLE [dbo].[Towns]
ADD CONSTRAINT [FK_Town2District]
    FOREIGN KEY ([DistrictId])
    REFERENCES [dbo].[Districts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Town2District'
CREATE INDEX [IX_FK_Town2District]
ON [dbo].[Towns]
    ([DistrictId]);
GO

-- Creating foreign key on [SchoolAttendance] in table 'Children'
ALTER TABLE [dbo].[Children]
ADD CONSTRAINT [FK_SchoolDaysPerWeek2Child]
    FOREIGN KEY ([SchoolAttendance])
    REFERENCES [dbo].[SchoolDaysPerWeek]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SchoolDaysPerWeek2Child'
CREATE INDEX [IX_FK_SchoolDaysPerWeek2Child]
ON [dbo].[Children]
    ([SchoolAttendance]);
GO

-- Creating foreign key on [Beneficiary_Id] in table 'MajorExpenses'
ALTER TABLE [dbo].[MajorExpenses]
ADD CONSTRAINT [FK_Beneficiary2MajorExpenses]
    FOREIGN KEY ([Beneficiary_Id])
    REFERENCES [dbo].[Beneficiaries]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Beneficiary2MajorExpenses'
CREATE INDEX [IX_FK_Beneficiary2MajorExpenses]
ON [dbo].[MajorExpenses]
    ([Beneficiary_Id]);
GO

-- Creating foreign key on [Beneficiary_Id] in table 'HealthCareInfos'
ALTER TABLE [dbo].[HealthCareInfos]
ADD CONSTRAINT [FK_Beneficiary2HealthCareInfo]
    FOREIGN KEY ([Beneficiary_Id])
    REFERENCES [dbo].[Beneficiaries]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Beneficiary2HealthCareInfo'
CREATE INDEX [IX_FK_Beneficiary2HealthCareInfo]
ON [dbo].[HealthCareInfos]
    ([Beneficiary_Id]);
GO

-- Creating foreign key on [Beneficiary_Id] in table 'GovernmentServices'
ALTER TABLE [dbo].[GovernmentServices]
ADD CONSTRAINT [FK_Beneficiary2GovernmentServices]
    FOREIGN KEY ([Beneficiary_Id])
    REFERENCES [dbo].[Beneficiaries]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Beneficiary2GovernmentServices'
CREATE INDEX [IX_FK_Beneficiary2GovernmentServices]
ON [dbo].[GovernmentServices]
    ([Beneficiary_Id]);
GO

-- Creating foreign key on [NumberOfRoomsTypeId] in table 'HousingInfoes'
ALTER TABLE [dbo].[HousingInfoes]
ADD CONSTRAINT [FK_HousingInfo2NumberOfRooms]
    FOREIGN KEY ([NumberOfRoomsTypeId])
    REFERENCES [dbo].[NumberOfRoomsTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_HousingInfo2NumberOfRooms'
CREATE INDEX [IX_FK_HousingInfo2NumberOfRooms]
ON [dbo].[HousingInfoes]
    ([NumberOfRoomsTypeId]);
GO

-- Creating foreign key on [RoofMaterialTypeId] in table 'HousingInfoes'
ALTER TABLE [dbo].[HousingInfoes]
ADD CONSTRAINT [FK_HousingInfo2RoofMaterial]
    FOREIGN KEY ([RoofMaterialTypeId])
    REFERENCES [dbo].[RoofMaterialTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_HousingInfo2RoofMaterial'
CREATE INDEX [IX_FK_HousingInfo2RoofMaterial]
ON [dbo].[HousingInfoes]
    ([RoofMaterialTypeId]);
GO

-- Creating foreign key on [WallMaterialTypeId] in table 'HousingInfoes'
ALTER TABLE [dbo].[HousingInfoes]
ADD CONSTRAINT [FK_HousingInfo2WallMaterial]
    FOREIGN KEY ([WallMaterialTypeId])
    REFERENCES [dbo].[WallMaterialTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_HousingInfo2WallMaterial'
CREATE INDEX [IX_FK_HousingInfo2WallMaterial]
ON [dbo].[HousingInfoes]
    ([WallMaterialTypeId]);
GO

-- Creating foreign key on [CookingEnergyTypeId] in table 'HousingInfoes'
ALTER TABLE [dbo].[HousingInfoes]
ADD CONSTRAINT [FK_HousingInfo2CookingEnergyType]
    FOREIGN KEY ([CookingEnergyTypeId])
    REFERENCES [dbo].[CookingEnergyTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_HousingInfo2CookingEnergyType'
CREATE INDEX [IX_FK_HousingInfo2CookingEnergyType]
ON [dbo].[HousingInfoes]
    ([CookingEnergyTypeId]);
GO

-- Creating foreign key on [Beneficiary_Id] in table 'HousingInfoes'
ALTER TABLE [dbo].[HousingInfoes]
ADD CONSTRAINT [FK_Beneficiary2HousingInfo]
    FOREIGN KEY ([Beneficiary_Id])
    REFERENCES [dbo].[Beneficiaries]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Beneficiary2HousingInfo'
CREATE INDEX [IX_FK_Beneficiary2HousingInfo]
ON [dbo].[HousingInfoes]
    ([Beneficiary_Id]);
GO

-- Creating foreign key on [ToiletTypeId] in table 'HousingInfoes'
ALTER TABLE [dbo].[HousingInfoes]
ADD CONSTRAINT [FK_HousingInfo2ToiletType]
    FOREIGN KEY ([ToiletTypeId])
    REFERENCES [dbo].[ToiletTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_HousingInfo2ToiletType'
CREATE INDEX [IX_FK_HousingInfo2ToiletType]
ON [dbo].[HousingInfoes]
    ([ToiletTypeId]);
GO

-- Creating foreign key on [HouseQualityTypeId] in table 'HousingInfoes'
ALTER TABLE [dbo].[HousingInfoes]
ADD CONSTRAINT [FK_HousingInfoHouseQuality]
    FOREIGN KEY ([HouseQualityTypeId])
    REFERENCES [dbo].[HouseQualityTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_HousingInfoHouseQuality'
CREATE INDEX [IX_FK_HousingInfoHouseQuality]
ON [dbo].[HousingInfoes]
    ([HouseQualityTypeId]);
GO

-- Creating foreign key on [PrimaryOccupationId] in table 'Beneficiaries'
ALTER TABLE [dbo].[Beneficiaries]
ADD CONSTRAINT [FK_Beneficiary2PriOccupation]
    FOREIGN KEY ([PrimaryOccupationId])
    REFERENCES [dbo].[Occupations]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Beneficiary2PriOccupation'
CREATE INDEX [IX_FK_Beneficiary2PriOccupation]
ON [dbo].[Beneficiaries]
    ([PrimaryOccupationId]);
GO

-- Creating foreign key on [SecondaryOccupationId] in table 'Beneficiaries'
ALTER TABLE [dbo].[Beneficiaries]
ADD CONSTRAINT [FK_Beneficiary2SecOccupation]
    FOREIGN KEY ([SecondaryOccupationId])
    REFERENCES [dbo].[Occupations]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Beneficiary2SecOccupation'
CREATE INDEX [IX_FK_Beneficiary2SecOccupation]
ON [dbo].[Beneficiaries]
    ([SecondaryOccupationId]);
GO

-- Creating foreign key on [ReligionId] in table 'HousingInfoes'
ALTER TABLE [dbo].[HousingInfoes]
ADD CONSTRAINT [FK_Religion2HousingInfo]
    FOREIGN KEY ([ReligionId])
    REFERENCES [dbo].[Religions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Religion2HousingInfo'
CREATE INDEX [IX_FK_Religion2HousingInfo]
ON [dbo].[HousingInfoes]
    ([ReligionId]);
GO

-- Creating foreign key on [CasteId] in table 'HousingInfoes'
ALTER TABLE [dbo].[HousingInfoes]
ADD CONSTRAINT [FK_Caste2HousingInfo]
    FOREIGN KEY ([CasteId])
    REFERENCES [dbo].[Castes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Caste2HousingInfo'
CREATE INDEX [IX_FK_Caste2HousingInfo]
ON [dbo].[HousingInfoes]
    ([CasteId]);
GO

-- Creating foreign key on [HouseOwnershipTypeId] in table 'HousingInfoes'
ALTER TABLE [dbo].[HousingInfoes]
ADD CONSTRAINT [FK_HouseOwnershipType2HousingInfo]
    FOREIGN KEY ([HouseOwnershipTypeId])
    REFERENCES [dbo].[HouseOwnershipTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_HouseOwnershipType2HousingInfo'
CREATE INDEX [IX_FK_HouseOwnershipType2HousingInfo]
ON [dbo].[HousingInfoes]
    ([HouseOwnershipTypeId]);
GO

-- Creating foreign key on [WaterSourceDistanceTypeId] in table 'HousingInfoes'
ALTER TABLE [dbo].[HousingInfoes]
ADD CONSTRAINT [FK_WaterSourceDistanceType2HousingInfo]
    FOREIGN KEY ([WaterSourceDistanceTypeId])
    REFERENCES [dbo].[WaterSourceDistanceTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_WaterSourceDistanceType2HousingInfo'
CREATE INDEX [IX_FK_WaterSourceDistanceType2HousingInfo]
ON [dbo].[HousingInfoes]
    ([WaterSourceDistanceTypeId]);
GO

-- Creating foreign key on [AvgNonVegPerMTypeId] in table 'Meals'
ALTER TABLE [dbo].[Meals]
ADD CONSTRAINT [FK_AvgNonVegPerMType2Meals]
    FOREIGN KEY ([AvgNonVegPerMTypeId])
    REFERENCES [dbo].[AvgNonVegPerMTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AvgNonVegPerMType2Meals'
CREATE INDEX [IX_FK_AvgNonVegPerMType2Meals]
ON [dbo].[Meals]
    ([AvgNonVegPerMTypeId]);
GO

-- Creating foreign key on [PKGPerMTypeId] in table 'Meals'
ALTER TABLE [dbo].[Meals]
ADD CONSTRAINT [FK_PKGPerMType2Meals]
    FOREIGN KEY ([PKGPerMTypeId])
    REFERENCES [dbo].[PKGPerMTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PKGPerMType2Meals'
CREATE INDEX [IX_FK_PKGPerMType2Meals]
ON [dbo].[Meals]
    ([PKGPerMTypeId]);
GO

-- Creating foreign key on [PrimaryOccupationId] in table 'Adults'
ALTER TABLE [dbo].[Adults]
ADD CONSTRAINT [FK_PrimaryOccupation2Adult]
    FOREIGN KEY ([PrimaryOccupationId])
    REFERENCES [dbo].[Occupations]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PrimaryOccupation2Adult'
CREATE INDEX [IX_FK_PrimaryOccupation2Adult]
ON [dbo].[Adults]
    ([PrimaryOccupationId]);
GO

-- Creating foreign key on [SecondaryOccupationId] in table 'Adults'
ALTER TABLE [dbo].[Adults]
ADD CONSTRAINT [FK_SecondaryOccupation2Adult]
    FOREIGN KEY ([SecondaryOccupationId])
    REFERENCES [dbo].[Occupations]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SecondaryOccupation2Adult'
CREATE INDEX [IX_FK_SecondaryOccupation2Adult]
ON [dbo].[Adults]
    ([SecondaryOccupationId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------