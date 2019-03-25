-- --------------------------------------------------------
-- Host:                         remotemysql.com
-- Server version:               8.0.13-4 - Percona Server (GPL), Release '4', Revision 'f0a32b8'
-- Server OS:                    debian-linux-gnu
-- HeidiSQL Version:             10.1.0.5464
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Dumping database structure for 0hsooZjaZg
CREATE DATABASE IF NOT EXISTS `0hsooZjaZg` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `0hsooZjaZg`;

-- Dumping structure for table 0hsooZjaZg.tblprocesskeyword
CREATE TABLE IF NOT EXISTS `tblprocesskeyword` (
  `sysid` int(11) NOT NULL AUTO_INCREMENT,
  `KeywordMatch` varchar(100) NOT NULL,
  `KeywordDetails` varchar(100) DEFAULT NULL,
  `Sender` varchar(50) DEFAULT NULL,
  `isEnabled` int(11) NOT NULL DEFAULT '1',
  `ReceivedDateTime` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`sysid`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=latin1;

-- Dumping data for table 0hsooZjaZg.tblprocesskeyword: ~0 rows (approximately)
DELETE FROM `tblprocesskeyword`;
/*!40000 ALTER TABLE `tblprocesskeyword` DISABLE KEYS */;
INSERT INTO `tblprocesskeyword` (`sysid`, `KeywordMatch`, `KeywordDetails`, `Sender`, `isEnabled`, `ReceivedDateTime`) VALUES
	(7, 'CREATE MEETING', 'test meeting by chris', '9458296295', 1, '2019-02-17 21:32:33');
/*!40000 ALTER TABLE `tblprocesskeyword` ENABLE KEYS */;

-- Dumping structure for table 0hsooZjaZg.tblreceivedmsg
CREATE TABLE IF NOT EXISTS `tblreceivedmsg` (
  `sysID` int(11) NOT NULL AUTO_INCREMENT,
  `SubscriberName` varchar(50) NOT NULL,
  `Message` varchar(50) NOT NULL,
  `DateReceived` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`sysID`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=latin1;

-- Dumping data for table 0hsooZjaZg.tblreceivedmsg: ~5 rows (approximately)
DELETE FROM `tblreceivedmsg`;
/*!40000 ALTER TABLE `tblreceivedmsg` DISABLE KEYS */;
INSERT INTO `tblreceivedmsg` (`sysID`, `SubscriberName`, `Message`, `DateReceived`) VALUES
	(4, '9458296295', 'Keyword', '2019-02-17 15:48:20'),
	(5, '9458296295', 'Create meeting test meeting by chris', '2019-02-17 21:32:33'),
	(6, '9458296295', 'Register user biagtan,christopher', '2019-02-17 21:39:32'),
	(7, '9458296295', 'Keywords', '2019-02-18 08:20:13'),
	(8, '9458296295', 'Keywords', '2019-02-18 09:45:50');
/*!40000 ALTER TABLE `tblreceivedmsg` ENABLE KEYS */;

-- Dumping structure for table 0hsooZjaZg.tblresponse
CREATE TABLE IF NOT EXISTS `tblresponse` (
  `sysid` int(11) NOT NULL AUTO_INCREMENT,
  `SubscriberNumber` varchar(20) NOT NULL,
  `RefID` int(11) NOT NULL,
  `Response` varchar(10) NOT NULL,
  `DateReceived` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `isEnabled` int(1) NOT NULL,
  PRIMARY KEY (`sysid`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Dumping data for table 0hsooZjaZg.tblresponse: ~0 rows (approximately)
DELETE FROM `tblresponse`;
/*!40000 ALTER TABLE `tblresponse` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblresponse` ENABLE KEYS */;

-- Dumping structure for table 0hsooZjaZg.tblsentmessage
CREATE TABLE IF NOT EXISTS `tblsentmessage` (
  `sysID` int(11) NOT NULL AUTO_INCREMENT,
  `SubscriberNumber` varchar(50) NOT NULL,
  `MessageSent` varchar(100) NOT NULL,
  `DateSend` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`sysID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

-- Dumping data for table 0hsooZjaZg.tblsentmessage: ~2 rows (approximately)
DELETE FROM `tblsentmessage`;
/*!40000 ALTER TABLE `tblsentmessage` DISABLE KEYS */;
INSERT INTO `tblsentmessage` (`sysID`, `SubscriberNumber`, `MessageSent`, `DateSend`) VALUES
	(3, '9458296295', 'Error: Invalid or ID not found. Please try again.', '2019-02-17 21:24:46'),
	(4, '9458296295', 'You have successfully registered your number under your name. (9458296295) -christopher,biagtan', '2019-02-17 21:39:33');
/*!40000 ALTER TABLE `tblsentmessage` ENABLE KEYS */;

-- Dumping structure for table 0hsooZjaZg.tblsmsusers
CREATE TABLE IF NOT EXISTS `tblsmsusers` (
  `sysid` int(11) NOT NULL AUTO_INCREMENT,
  `SubscriberNumber` varchar(20) NOT NULL,
  `TokenKeys` varchar(50) NOT NULL,
  `LastName` varchar(50) DEFAULT NULL,
  `FirstName` varchar(50) DEFAULT NULL,
  `isEnabled` int(11) NOT NULL DEFAULT '1',
  `RegisteredDateTime` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`sysid`,`SubscriberNumber`),
  UNIQUE KEY `sysid` (`sysid`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Dumping data for table 0hsooZjaZg.tblsmsusers: ~0 rows (approximately)
DELETE FROM `tblsmsusers`;
/*!40000 ALTER TABLE `tblsmsusers` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblsmsusers` ENABLE KEYS */;

-- Dumping structure for table 0hsooZjaZg.tbl_CONDO_CustomerInfo
CREATE TABLE IF NOT EXISTS `tbl_CONDO_CustomerInfo` (
  `sysID` int(11) NOT NULL AUTO_INCREMENT,
  `PrimaryNames` varchar(50) NOT NULL,
  `LastName` varchar(50) NOT NULL,
  `FirstName` varchar(50) NOT NULL,
  `MiddleName` varchar(50) NOT NULL,
  `Alias` varchar(50) NOT NULL,
  `UseAlias` int(11) NOT NULL DEFAULT '0',
  `ContactNumber` varchar(50) NOT NULL,
  `UnitNo` int(11) NOT NULL DEFAULT '0',
  `PhotoPath` varchar(100) NOT NULL,
  `Notes` varchar(100) NOT NULL,
  `isTenant` int(11) NOT NULL DEFAULT '0',
  `CustomerRef` int(11) NOT NULL DEFAULT '0',
  `isEnabled` int(11) NOT NULL DEFAULT '1',
  `CreatedBy` int(11) NOT NULL DEFAULT '-1',
  `UpdatedBy` int(11) NOT NULL DEFAULT '-1',
  `DateCreated` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `DateUpdated` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`sysID`),
  UNIQUE KEY `PrimaryNames` (`PrimaryNames`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

-- Dumping data for table 0hsooZjaZg.tbl_CONDO_CustomerInfo: ~1 rows (approximately)
DELETE FROM `tbl_CONDO_CustomerInfo`;
/*!40000 ALTER TABLE `tbl_CONDO_CustomerInfo` DISABLE KEYS */;
INSERT INTO `tbl_CONDO_CustomerInfo` (`sysID`, `PrimaryNames`, `LastName`, `FirstName`, `MiddleName`, `Alias`, `UseAlias`, `ContactNumber`, `UnitNo`, `PhotoPath`, `Notes`, `isTenant`, `CustomerRef`, `isEnabled`, `CreatedBy`, `UpdatedBy`, `DateCreated`, `DateUpdated`) VALUES
	(1, 'Biagtan_Christopher_Arabos', 'Biagtan', 'Christopher', 'Arabos', '', 0, '1234', 1, 'C:UsersokPicturessystempics_sample.jpg', 'this is for test only', 0, 0, 1, 0, -1, '2019-03-24 22:03:17', '2019-03-24 22:03:17'),
	(2, 'Reyes_Dolor_Matocabe', 'Reyes', 'Dolor', 'Matocabe', '', 0, '1234', 1, '', 'This is a test only', 0, 0, 1, 0, -1, '2019-03-25 00:39:36', '2019-03-25 00:39:36'),
	(3, 'Sanchez_Malaya_Weshy', 'Sanchez', 'Malaya', 'Weshy', '', 0, '1234', 2, '', 'This is me', 0, 0, 1, 0, -1, '2019-03-25 01:35:36', '2019-03-25 01:35:36');
/*!40000 ALTER TABLE `tbl_CONDO_CustomerInfo` ENABLE KEYS */;

-- Dumping structure for table 0hsooZjaZg.tbl_CONDO_FloorInfo
CREATE TABLE IF NOT EXISTS `tbl_CONDO_FloorInfo` (
  `sysID` int(11) NOT NULL AUTO_INCREMENT,
  `FloorName` varchar(50) NOT NULL,
  `FloorDescription` varchar(50) DEFAULT NULL,
  `userID` int(11) NOT NULL DEFAULT '-1',
  `isEnabled` int(11) NOT NULL DEFAULT '1',
  `DateDefined` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `LastUpdateUser` int(11) NOT NULL DEFAULT '-1',
  `LastDateDefined` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`sysID`),
  UNIQUE KEY `FloorName` (`FloorName`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

-- Dumping data for table 0hsooZjaZg.tbl_CONDO_FloorInfo: ~2 rows (approximately)
DELETE FROM `tbl_CONDO_FloorInfo`;
/*!40000 ALTER TABLE `tbl_CONDO_FloorInfo` DISABLE KEYS */;
INSERT INTO `tbl_CONDO_FloorInfo` (`sysID`, `FloorName`, `FloorDescription`, `userID`, `isEnabled`, `DateDefined`, `LastUpdateUser`, `LastDateDefined`) VALUES
	(1, 'First Floor SW', 'First Floor South Wing', -1, 1, '2019-03-13 23:24:12', 0, '2019-03-18 22:01:52'),
	(2, 'Second Floor SW', 'Second Floor South Wing', -1, 1, '2019-03-13 23:24:16', -1, '2019-03-19 06:38:36');
/*!40000 ALTER TABLE `tbl_CONDO_FloorInfo` ENABLE KEYS */;

-- Dumping structure for table 0hsooZjaZg.tbl_CONDO_UnitInfo
CREATE TABLE IF NOT EXISTS `tbl_CONDO_UnitInfo` (
  `SysID` int(11) NOT NULL AUTO_INCREMENT,
  `UnitName` varchar(50) NOT NULL DEFAULT '0',
  `FloorAssociate` int(11) NOT NULL DEFAULT '0',
  `Description` varchar(50) DEFAULT '0',
  `AreaSQM` int(11) DEFAULT '0',
  `isMontlyDueComputed` tinyint(1) DEFAULT '0',
  `MonthlyDue` decimal(10,2) NOT NULL DEFAULT '0.00',
  `TotalDue` decimal(10,2) NOT NULL DEFAULT '0.00',
  `isEnabled` tinyint(1) NOT NULL DEFAULT '1',
  `CreatedBy` int(11) NOT NULL DEFAULT '-1',
  `CreatedDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedBy` int(11) NOT NULL DEFAULT '-1',
  `UpdateDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`SysID`),
  UNIQUE KEY `UnitName` (`UnitName`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

-- Dumping data for table 0hsooZjaZg.tbl_CONDO_UnitInfo: ~0 rows (approximately)
DELETE FROM `tbl_CONDO_UnitInfo`;
/*!40000 ALTER TABLE `tbl_CONDO_UnitInfo` DISABLE KEYS */;
INSERT INTO `tbl_CONDO_UnitInfo` (`SysID`, `UnitName`, `FloorAssociate`, `Description`, `AreaSQM`, `isMontlyDueComputed`, `MonthlyDue`, `TotalDue`, `isEnabled`, `CreatedBy`, `CreatedDate`, `UpdatedBy`, `UpdateDate`) VALUES
	(1, '101', 1, 'Near Elevator', 48, 1, 50.00, 2400.00, 1, -1, '2019-03-20 01:06:19', -1, '2019-03-20 01:06:19'),
	(2, '102', 1, 'South Wing room', 40, 1, 100.00, 4000.00, 1, -1, '2019-03-25 01:34:50', -1, '2019-03-25 01:34:50');
/*!40000 ALTER TABLE `tbl_CONDO_UnitInfo` ENABLE KEYS */;

-- Dumping structure for table 0hsooZjaZg.tbl_SYSTEM_AccountType
CREATE TABLE IF NOT EXISTS `tbl_SYSTEM_AccountType` (
  `sysID` int(11) NOT NULL AUTO_INCREMENT,
  `AccountType` varchar(50) NOT NULL,
  `Description` varchar(50) DEFAULT NULL,
  `HasOpeningBalance` tinyint(1) NOT NULL DEFAULT '0',
  `Notes` varchar(50) DEFAULT NULL,
  `DateCreated` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `CreatedBy` int(11) NOT NULL DEFAULT '0',
  `isEnabled` int(11) NOT NULL DEFAULT '1',
  PRIMARY KEY (`AccountType`),
  UNIQUE KEY `sysID` (`sysID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Dumping data for table 0hsooZjaZg.tbl_SYSTEM_AccountType: ~0 rows (approximately)
DELETE FROM `tbl_SYSTEM_AccountType`;
/*!40000 ALTER TABLE `tbl_SYSTEM_AccountType` DISABLE KEYS */;
/*!40000 ALTER TABLE `tbl_SYSTEM_AccountType` ENABLE KEYS */;

-- Dumping structure for table 0hsooZjaZg.tbl_SYSTEM_ChartOfAccounts
CREATE TABLE IF NOT EXISTS `tbl_SYSTEM_ChartOfAccounts` (
  `sysid` int(11) NOT NULL AUTO_INCREMENT,
  `AccountCode` varchar(50) NOT NULL,
  `AccountName` varchar(50) NOT NULL,
  `AccountType` varchar(50) DEFAULT NULL,
  `Description` varchar(100) DEFAULT NULL,
  `SubAccountID` int(11) DEFAULT NULL,
  `OpeningBalance` decimal(11,0) NOT NULL DEFAULT '0',
  `BalanceTotal` decimal(11,0) NOT NULL DEFAULT '0',
  `isEnabled` int(11) NOT NULL DEFAULT '1',
  `CreatedBy` varchar(20) DEFAULT NULL,
  `DateCreated` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`AccountCode`),
  UNIQUE KEY `sysid` (`sysid`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Dumping data for table 0hsooZjaZg.tbl_SYSTEM_ChartOfAccounts: ~0 rows (approximately)
DELETE FROM `tbl_SYSTEM_ChartOfAccounts`;
/*!40000 ALTER TABLE `tbl_SYSTEM_ChartOfAccounts` DISABLE KEYS */;
/*!40000 ALTER TABLE `tbl_SYSTEM_ChartOfAccounts` ENABLE KEYS */;

-- Dumping structure for table 0hsooZjaZg.tbl_SYSTEM_Company
CREATE TABLE IF NOT EXISTS `tbl_SYSTEM_Company` (
  `sysID` int(11) NOT NULL AUTO_INCREMENT,
  `CompanyCode` varchar(50) NOT NULL,
  `CompanyName` varchar(100) NOT NULL,
  `CompanyAddress1` varchar(100) DEFAULT NULL,
  `CompanyAddress2` varchar(100) DEFAULT NULL,
  `CompanyAddress3` varchar(100) DEFAULT NULL,
  `ContactNumber` varchar(50) DEFAULT NULL,
  `isEnabled` int(1) NOT NULL DEFAULT '1',
  `DateCreated` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `isDefault` int(1) DEFAULT '0',
  PRIMARY KEY (`CompanyName`),
  UNIQUE KEY `sysID` (`sysID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Dumping data for table 0hsooZjaZg.tbl_SYSTEM_Company: ~0 rows (approximately)
DELETE FROM `tbl_SYSTEM_Company`;
/*!40000 ALTER TABLE `tbl_SYSTEM_Company` DISABLE KEYS */;
/*!40000 ALTER TABLE `tbl_SYSTEM_Company` ENABLE KEYS */;

-- Dumping structure for table 0hsooZjaZg.tbl_SYSTEM_INFO
CREATE TABLE IF NOT EXISTS `tbl_SYSTEM_INFO` (
  `sysID` int(11) NOT NULL AUTO_INCREMENT,
  `ProgramCode` varchar(50) NOT NULL,
  `LicenseCode` varchar(50) NOT NULL,
  `ActivationCode` varchar(50) NOT NULL,
  `DateInstalled` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `isEnabled` int(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`ProgramCode`),
  UNIQUE KEY `sysID` (`sysID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

-- Dumping data for table 0hsooZjaZg.tbl_SYSTEM_INFO: ~0 rows (approximately)
DELETE FROM `tbl_SYSTEM_INFO`;
/*!40000 ALTER TABLE `tbl_SYSTEM_INFO` DISABLE KEYS */;
INSERT INTO `tbl_SYSTEM_INFO` (`sysID`, `ProgramCode`, `LicenseCode`, `ActivationCode`, `DateInstalled`, `isEnabled`) VALUES
	(1, 'iHpdg0zjV2uIng3ilb1gHaeq4m9+Jnmu', 'p6brcwWbLgQyhYCMJdvugfI4NEQLUfVo', 'JmdYr5O8yyhWMSO8fqHsGA==', '2019-02-18 23:57:56', 1),
	(2, 'MNAM6QxrvVOawceYcJ/taV1So1ihf3St', 'p6brcwWbLgQyhYCMJdvugfI4NEQLUfVo', 'JmdYr5O8yyhWMSO8fqHsGA==', '2019-03-07 02:43:53', 1);
/*!40000 ALTER TABLE `tbl_SYSTEM_INFO` ENABLE KEYS */;

-- Dumping structure for table 0hsooZjaZg.tbl_SYSTEM_UserRoles
CREATE TABLE IF NOT EXISTS `tbl_SYSTEM_UserRoles` (
  `sysid` int(11) NOT NULL AUTO_INCREMENT,
  `RoleName` varchar(50) NOT NULL,
  `Description` varchar(100) DEFAULT NULL,
  `isEnabled` int(11) NOT NULL DEFAULT '1',
  PRIMARY KEY (`RoleName`),
  UNIQUE KEY `sysid` (`sysid`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

-- Dumping data for table 0hsooZjaZg.tbl_SYSTEM_UserRoles: ~0 rows (approximately)
DELETE FROM `tbl_SYSTEM_UserRoles`;
/*!40000 ALTER TABLE `tbl_SYSTEM_UserRoles` DISABLE KEYS */;
INSERT INTO `tbl_SYSTEM_UserRoles` (`sysid`, `RoleName`, `Description`, `isEnabled`) VALUES
	(1, 'Administrator', 'Administrator Account', 1);
/*!40000 ALTER TABLE `tbl_SYSTEM_UserRoles` ENABLE KEYS */;

-- Dumping structure for table 0hsooZjaZg.tbl_SYSTEM_Users
CREATE TABLE IF NOT EXISTS `tbl_SYSTEM_Users` (
  `sysID` int(11) NOT NULL AUTO_INCREMENT,
  `Username` varchar(20) NOT NULL,
  `Password` varchar(50) NOT NULL,
  `RoleID` int(11) NOT NULL DEFAULT '0',
  `isEnabled` int(11) NOT NULL DEFAULT '1',
  `systemreference` int(11) DEFAULT NULL,
  `Keys` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`sysID`),
  UNIQUE KEY `sysID` (`sysID`),
  UNIQUE KEY `Keys` (`Keys`),
  KEY `FK_tbl_SYSTEM_Users` (`RoleID`),
  KEY `FK_tbl_SYSTEM_Users_tbl_SYSTEM_INFO` (`systemreference`),
  CONSTRAINT `FK_tbl_SYSTEM_Users` FOREIGN KEY (`RoleID`) REFERENCES `tbl_SYSTEM_UserRoles` (`sysid`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_tbl_SYSTEM_Users_tbl_SYSTEM_INFO` FOREIGN KEY (`systemreference`) REFERENCES `tbl_SYSTEM_INFO` (`sysid`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;

-- Dumping data for table 0hsooZjaZg.tbl_SYSTEM_Users: ~2 rows (approximately)
DELETE FROM `tbl_SYSTEM_Users`;
/*!40000 ALTER TABLE `tbl_SYSTEM_Users` DISABLE KEYS */;
INSERT INTO `tbl_SYSTEM_Users` (`sysID`, `Username`, `Password`, `RoleID`, `isEnabled`, `systemreference`, `Keys`) VALUES
	(1, 'admin', '+13/1FAZDRs=', 1, 1, 1, '1_admin'),
	(6, 'admin', '+13/1FAZDRs=', 1, 1, 2, '2_admin');
/*!40000 ALTER TABLE `tbl_SYSTEM_Users` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
