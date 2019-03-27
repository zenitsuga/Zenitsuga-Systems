-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               10.1.37-MariaDB - mariadb.org binary distribution
-- Server OS:                    Win32
-- HeidiSQL Version:             10.1.0.5464
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Dumping database structure for 0hsoozjazg
CREATE DATABASE IF NOT EXISTS `0hsoozjazg` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `0hsoozjazg`;

-- Dumping structure for table 0hsoozjazg.tblprocesskeyword
CREATE TABLE IF NOT EXISTS `tblprocesskeyword` (
  `sysid` int(11) NOT NULL AUTO_INCREMENT,
  `KeywordMatch` varchar(100) NOT NULL,
  `KeywordDetails` varchar(100) DEFAULT NULL,
  `Sender` varchar(50) DEFAULT NULL,
  `isEnabled` int(11) NOT NULL DEFAULT '1',
  `ReceivedDateTime` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`sysid`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=latin1;

-- Dumping data for table 0hsoozjazg.tblprocesskeyword: ~0 rows (approximately)
DELETE FROM `tblprocesskeyword`;
/*!40000 ALTER TABLE `tblprocesskeyword` DISABLE KEYS */;
INSERT INTO `tblprocesskeyword` (`sysid`, `KeywordMatch`, `KeywordDetails`, `Sender`, `isEnabled`, `ReceivedDateTime`) VALUES
	(7, 'CREATE MEETING', 'test meeting by chris', '9458296295', 1, '2019-02-17 21:32:33');
/*!40000 ALTER TABLE `tblprocesskeyword` ENABLE KEYS */;

-- Dumping structure for table 0hsoozjazg.tblreceivedmsg
CREATE TABLE IF NOT EXISTS `tblreceivedmsg` (
  `sysID` int(11) NOT NULL AUTO_INCREMENT,
  `SubscriberName` varchar(50) NOT NULL,
  `Message` varchar(50) NOT NULL,
  `DateReceived` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`sysID`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=latin1;

-- Dumping data for table 0hsoozjazg.tblreceivedmsg: ~5 rows (approximately)
DELETE FROM `tblreceivedmsg`;
/*!40000 ALTER TABLE `tblreceivedmsg` DISABLE KEYS */;
INSERT INTO `tblreceivedmsg` (`sysID`, `SubscriberName`, `Message`, `DateReceived`) VALUES
	(4, '9458296295', 'Keyword', '2019-02-17 15:48:20'),
	(5, '9458296295', 'Create meeting test meeting by chris', '2019-02-17 21:32:33'),
	(6, '9458296295', 'Register user biagtan,christopher', '2019-02-17 21:39:32'),
	(7, '9458296295', 'Keywords', '2019-02-18 08:20:13'),
	(8, '9458296295', 'Keywords', '2019-02-18 09:45:50');
/*!40000 ALTER TABLE `tblreceivedmsg` ENABLE KEYS */;

-- Dumping structure for table 0hsoozjazg.tblresponse
CREATE TABLE IF NOT EXISTS `tblresponse` (
  `sysid` int(11) NOT NULL AUTO_INCREMENT,
  `SubscriberNumber` varchar(20) NOT NULL,
  `RefID` int(11) NOT NULL,
  `Response` varchar(10) NOT NULL,
  `DateReceived` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `isEnabled` int(1) NOT NULL,
  PRIMARY KEY (`sysid`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Dumping data for table 0hsoozjazg.tblresponse: ~0 rows (approximately)
DELETE FROM `tblresponse`;
/*!40000 ALTER TABLE `tblresponse` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblresponse` ENABLE KEYS */;

-- Dumping structure for table 0hsoozjazg.tblsentmessage
CREATE TABLE IF NOT EXISTS `tblsentmessage` (
  `sysID` int(11) NOT NULL AUTO_INCREMENT,
  `SubscriberNumber` varchar(50) NOT NULL,
  `MessageSent` varchar(100) NOT NULL,
  `DateSend` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`sysID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

-- Dumping data for table 0hsoozjazg.tblsentmessage: ~2 rows (approximately)
DELETE FROM `tblsentmessage`;
/*!40000 ALTER TABLE `tblsentmessage` DISABLE KEYS */;
INSERT INTO `tblsentmessage` (`sysID`, `SubscriberNumber`, `MessageSent`, `DateSend`) VALUES
	(3, '9458296295', 'Error: Invalid or ID not found. Please try again.', '2019-02-17 21:24:46'),
	(4, '9458296295', 'You have successfully registered your number under your name. (9458296295) -christopher,biagtan', '2019-02-17 21:39:33');
/*!40000 ALTER TABLE `tblsentmessage` ENABLE KEYS */;

-- Dumping structure for table 0hsoozjazg.tblsmsusers
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

-- Dumping data for table 0hsoozjazg.tblsmsusers: ~0 rows (approximately)
DELETE FROM `tblsmsusers`;
/*!40000 ALTER TABLE `tblsmsusers` DISABLE KEYS */;
/*!40000 ALTER TABLE `tblsmsusers` ENABLE KEYS */;

-- Dumping structure for table 0hsoozjazg.tbl_condo_customerinfo
CREATE TABLE IF NOT EXISTS `tbl_condo_customerinfo` (
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
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

-- Dumping data for table 0hsoozjazg.tbl_condo_customerinfo: ~3 rows (approximately)
DELETE FROM `tbl_condo_customerinfo`;
/*!40000 ALTER TABLE `tbl_condo_customerinfo` DISABLE KEYS */;
INSERT INTO `tbl_condo_customerinfo` (`sysID`, `PrimaryNames`, `LastName`, `FirstName`, `MiddleName`, `Alias`, `UseAlias`, `ContactNumber`, `UnitNo`, `PhotoPath`, `Notes`, `isTenant`, `CustomerRef`, `isEnabled`, `CreatedBy`, `UpdatedBy`, `DateCreated`, `DateUpdated`) VALUES
	(1, 'Biagtan_Christopher_Arabos', 'Biagtan', 'Christopher', 'Arabos', '', 0, '1234', 1, 'C:UsersokPicturessystempics_sample.jpg', 'this is for test only', 1, 3, 1, 0, -1, '2019-03-24 22:03:17', '2019-03-27 09:46:00'),
	(3, 'Sanchez_Malaya_Weshy', 'Sanchez', 'Malaya', 'Weshy', '', 0, '1234', 2, '', 'This is me', 0, 0, 1, 0, -1, '2019-03-25 01:35:36', '2019-03-25 01:35:36');
/*!40000 ALTER TABLE `tbl_condo_customerinfo` ENABLE KEYS */;

-- Dumping structure for table 0hsoozjazg.tbl_condo_floorinfo
CREATE TABLE IF NOT EXISTS `tbl_condo_floorinfo` (
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
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

-- Dumping data for table 0hsoozjazg.tbl_condo_floorinfo: ~2 rows (approximately)
DELETE FROM `tbl_condo_floorinfo`;
/*!40000 ALTER TABLE `tbl_condo_floorinfo` DISABLE KEYS */;
INSERT INTO `tbl_condo_floorinfo` (`sysID`, `FloorName`, `FloorDescription`, `userID`, `isEnabled`, `DateDefined`, `LastUpdateUser`, `LastDateDefined`) VALUES
	(1, 'First Floor SW', 'First Floor South Wing', -1, 1, '2019-03-13 23:24:12', 0, '2019-03-18 22:01:52'),
	(2, 'Second Floor SW', 'Second Floor South Wing', -1, 1, '2019-03-13 23:24:16', -1, '2019-03-19 06:38:36');
/*!40000 ALTER TABLE `tbl_condo_floorinfo` ENABLE KEYS */;

-- Dumping structure for table 0hsoozjazg.tbl_condo_unitinfo
CREATE TABLE IF NOT EXISTS `tbl_condo_unitinfo` (
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

-- Dumping data for table 0hsoozjazg.tbl_condo_unitinfo: ~2 rows (approximately)
DELETE FROM `tbl_condo_unitinfo`;
/*!40000 ALTER TABLE `tbl_condo_unitinfo` DISABLE KEYS */;
INSERT INTO `tbl_condo_unitinfo` (`SysID`, `UnitName`, `FloorAssociate`, `Description`, `AreaSQM`, `isMontlyDueComputed`, `MonthlyDue`, `TotalDue`, `isEnabled`, `CreatedBy`, `CreatedDate`, `UpdatedBy`, `UpdateDate`) VALUES
	(1, '101', 1, 'Near Elevator', 48, 1, 50.00, 2400.00, 1, -1, '2019-03-20 01:06:19', -1, '2019-03-20 01:06:19'),
	(2, '102', 1, 'South Wing room', 40, 1, 100.00, 4000.00, 1, -1, '2019-03-25 01:34:50', -1, '2019-03-25 01:34:50');
/*!40000 ALTER TABLE `tbl_condo_unitinfo` ENABLE KEYS */;

-- Dumping structure for table 0hsoozjazg.tbl_system_accounttype
CREATE TABLE IF NOT EXISTS `tbl_system_accounttype` (
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

-- Dumping data for table 0hsoozjazg.tbl_system_accounttype: ~0 rows (approximately)
DELETE FROM `tbl_system_accounttype`;
/*!40000 ALTER TABLE `tbl_system_accounttype` DISABLE KEYS */;
/*!40000 ALTER TABLE `tbl_system_accounttype` ENABLE KEYS */;

-- Dumping structure for table 0hsoozjazg.tbl_system_chartofaccounts
CREATE TABLE IF NOT EXISTS `tbl_system_chartofaccounts` (
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

-- Dumping data for table 0hsoozjazg.tbl_system_chartofaccounts: ~0 rows (approximately)
DELETE FROM `tbl_system_chartofaccounts`;
/*!40000 ALTER TABLE `tbl_system_chartofaccounts` DISABLE KEYS */;
/*!40000 ALTER TABLE `tbl_system_chartofaccounts` ENABLE KEYS */;

-- Dumping structure for table 0hsoozjazg.tbl_system_company
CREATE TABLE IF NOT EXISTS `tbl_system_company` (
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

-- Dumping data for table 0hsoozjazg.tbl_system_company: ~0 rows (approximately)
DELETE FROM `tbl_system_company`;
/*!40000 ALTER TABLE `tbl_system_company` DISABLE KEYS */;
/*!40000 ALTER TABLE `tbl_system_company` ENABLE KEYS */;

-- Dumping structure for table 0hsoozjazg.tbl_system_info
CREATE TABLE IF NOT EXISTS `tbl_system_info` (
  `sysID` int(11) NOT NULL AUTO_INCREMENT,
  `ProgramCode` varchar(50) NOT NULL,
  `LicenseCode` varchar(50) NOT NULL,
  `ActivationCode` varchar(50) NOT NULL,
  `DateInstalled` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `isEnabled` int(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`ProgramCode`),
  UNIQUE KEY `sysID` (`sysID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

-- Dumping data for table 0hsoozjazg.tbl_system_info: ~2 rows (approximately)
DELETE FROM `tbl_system_info`;
/*!40000 ALTER TABLE `tbl_system_info` DISABLE KEYS */;
INSERT INTO `tbl_system_info` (`sysID`, `ProgramCode`, `LicenseCode`, `ActivationCode`, `DateInstalled`, `isEnabled`) VALUES
	(1, 'iHpdg0zjV2uIng3ilb1gHaeq4m9+Jnmu', 'p6brcwWbLgQyhYCMJdvugfI4NEQLUfVo', 'JmdYr5O8yyhWMSO8fqHsGA==', '2019-02-18 23:57:56', 1),
	(2, 'MNAM6QxrvVOawceYcJ/taV1So1ihf3St', 'p6brcwWbLgQyhYCMJdvugfI4NEQLUfVo', 'JmdYr5O8yyhWMSO8fqHsGA==', '2019-03-07 02:43:53', 1);
/*!40000 ALTER TABLE `tbl_system_info` ENABLE KEYS */;

-- Dumping structure for table 0hsoozjazg.tbl_system_userroles
CREATE TABLE IF NOT EXISTS `tbl_system_userroles` (
  `sysid` int(11) NOT NULL AUTO_INCREMENT,
  `RoleName` varchar(50) NOT NULL,
  `Description` varchar(100) DEFAULT NULL,
  `isEnabled` int(11) NOT NULL DEFAULT '1',
  PRIMARY KEY (`RoleName`),
  UNIQUE KEY `sysid` (`sysid`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

-- Dumping data for table 0hsoozjazg.tbl_system_userroles: ~0 rows (approximately)
DELETE FROM `tbl_system_userroles`;
/*!40000 ALTER TABLE `tbl_system_userroles` DISABLE KEYS */;
INSERT INTO `tbl_system_userroles` (`sysid`, `RoleName`, `Description`, `isEnabled`) VALUES
	(1, 'Administrator', 'Administrator Account', 1);
/*!40000 ALTER TABLE `tbl_system_userroles` ENABLE KEYS */;

-- Dumping structure for table 0hsoozjazg.tbl_system_users
CREATE TABLE IF NOT EXISTS `tbl_system_users` (
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
  CONSTRAINT `FK_tbl_SYSTEM_Users` FOREIGN KEY (`RoleID`) REFERENCES `tbl_system_userroles` (`sysid`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_tbl_SYSTEM_Users_tbl_SYSTEM_INFO` FOREIGN KEY (`systemreference`) REFERENCES `tbl_system_info` (`sysID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;

-- Dumping data for table 0hsoozjazg.tbl_system_users: ~2 rows (approximately)
DELETE FROM `tbl_system_users`;
/*!40000 ALTER TABLE `tbl_system_users` DISABLE KEYS */;
INSERT INTO `tbl_system_users` (`sysID`, `Username`, `Password`, `RoleID`, `isEnabled`, `systemreference`, `Keys`) VALUES
	(1, 'admin', '+13/1FAZDRs=', 1, 1, 1, '1_admin'),
	(6, 'admin', '+13/1FAZDRs=', 1, 1, 2, '2_admin');
/*!40000 ALTER TABLE `tbl_system_users` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
