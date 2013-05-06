CREATE DATABASE `myschema` /*!40100 DEFAULT CHARACTER SET utf8 */;

DROP TABLE IF EXISTS `myschema`.`account`;
CREATE TABLE  `myschema`.`account` (
  `ID` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `Code` varchar(50) NOT NULL,
  `GroupID` int(11) DEFAULT NULL,
  `ParentID` int(11) unsigned DEFAULT NULL,
  `CurrencyID` int(11) unsigned DEFAULT NULL,
  `Account` varchar(50) DEFAULT NULL,
  `AllocAccount` varchar(50) DEFAULT NULL,
  `ClearingAccount` varchar(50) DEFAULT NULL,
  `SODNLV` decimal(25,10) NOT NULL DEFAULT '0.0000000000',
  `BeginBalance` decimal(25,10) DEFAULT '0.0000000000',
  `Liquidity` decimal(25,10) NOT NULL DEFAULT '0.0000000000',
  `LastModified` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `CreateTime` timestamp NULL DEFAULT NULL,
  `TypeID` int(11) unsigned NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_GroupID` (`GroupID`),
  KEY `FK_account_2` (`ParentID`),
  KEY `FK_account_3` (`CurrencyID`),
  CONSTRAINT `FK_account_1` FOREIGN KEY (`GroupID`) REFERENCES `account_group` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


DROP TABLE IF EXISTS `myschema`.`account_group`;
CREATE TABLE  `myschema`.`account_group` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `GroupName` varchar(50) NOT NULL,
  `OrganisationID` int(11) DEFAULT NULL,
  `LastModified` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `ParentAccountID` int(11) unsigned DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_OrganisationID` (`OrganisationID`),
  CONSTRAINT `FK_account_group_1` FOREIGN KEY (`OrganisationID`) REFERENCES `organisation` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;



DROP TABLE IF EXISTS `myschema`.`currency`;
CREATE TABLE  `myschema`.`currency` (
  `ID` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `Code` varchar(8) NOT NULL,
  `Name` varchar(45) DEFAULT NULL,
  `FxRate` decimal(16,6) DEFAULT NULL,
  `LastModified` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `Code_UNIQUE` (`Code`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;



DROP TABLE IF EXISTS `myschema`.`exchange`;
CREATE TABLE  `myschema`.`exchange` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `Code` varchar(45) NOT NULL,
  `Name` varchar(48) DEFAULT NULL,
  `OrderTypes` varchar(50) DEFAULT NULL,
  `LastModified` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `UK_CODE` (`Code`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;






DROP TABLE IF EXISTS `myschema`.`instrument`;
CREATE TABLE  `myschema`.`instrument` (
  `ID` int(16) unsigned NOT NULL AUTO_INCREMENT,
  `Code` varchar(48) NOT NULL,
  `TypeID` int(16) unsigned NOT NULL,
  `CurrencyID` int(11) unsigned DEFAULT NULL,
  `ExchangeID` int(16) unsigned NOT NULL,
  `DecimalPlace` int(16) unsigned NOT NULL,
  `MaturityDate` varchar(48) NOT NULL,
  `TickSize` double DEFAULT NULL,
  `TickValue` double DEFAULT NULL,
  `StrikePrice` double DEFAULT NULL,
  `Symbol` varchar(48) DEFAULT NULL,
  `Currency` varchar(48) DEFAULT NULL,
  `IDSource` varchar(48) DEFAULT NULL,
  `SecurityID` varchar(48) DEFAULT NULL,
  `SecurityType` varchar(1) DEFAULT NULL,
  `SecurityExchange` varchar(48) DEFAULT NULL,
  `Suspended` smallint(1) DEFAULT '0',
  `LastModified` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `dateCreated` timestamp NOT NULL DEFAULT '2010-12-31 16:00:00',
  PRIMARY KEY (`ID`),
  UNIQUE KEY `Code` (`Code`),
  KEY `FK_ExchangeID` (`ExchangeID`),
  KEY `FK_CurrencyID` (`CurrencyID`),
  KEY `FK_instrument_1` (`TypeID`),
  CONSTRAINT `FK_instrument_1` FOREIGN KEY (`TypeID`) REFERENCES `instrument_type` (`ID`),
  CONSTRAINT `FK_instrument_2` FOREIGN KEY (`CurrencyID`) REFERENCES `currency` (`ID`),
  CONSTRAINT `FK_instrument_3` FOREIGN KEY (`ExchangeID`) REFERENCES `exchange` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;











DROP TABLE IF EXISTS `myschema`.`instrument_type`;
CREATE TABLE  `myschema`.`instrument_type` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `CurrencyID` int(11) unsigned NOT NULL,
  `ExchangeID` int(10) unsigned NOT NULL,
  `GroupID` int(10) unsigned DEFAULT NULL,
  `Code` varchar(64) NOT NULL,
  `Symbol` varchar(45) DEFAULT NULL,
  `SecurityType` char(1) NOT NULL,
  `OrderTypes` varchar(128) DEFAULT NULL,
  `SecurityExchange` varchar(45) DEFAULT NULL,
  `ReferencePrice` decimal(25,10) DEFAULT NULL,
  `LastModified` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `TickSize` double NOT NULL,
  `TickValue` double NOT NULL,
  `DecimalPlace` int(10) unsigned NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_CurrencyID` (`CurrencyID`),
  KEY `FK_ExchangeID` (`ExchangeID`),
  KEY `FK_GroupID` (`GroupID`),
  CONSTRAINT `FK_GroupID` FOREIGN KEY (`GroupID`) REFERENCES `instrument_type_group` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;













DROP TABLE IF EXISTS `myschema`.`instrument_type_group`;
CREATE TABLE  `myschema`.`instrument_type_group` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `Code` varchar(45) NOT NULL,
  `LastModified` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `UK_CODE` (`Code`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;










DROP TABLE IF EXISTS `myschema`.`limit`;
CREATE TABLE  `myschema`.`limit` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `AccountID` int(10) unsigned NOT NULL,
  `InstrumentTypeID` int(10) unsigned NOT NULL,
  `MaxOrderSize` int(11) NOT NULL,
  `PositionLimit` int(11) NOT NULL,
  `LastModified` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`ID`),
  KEY `FK_limit_2` (`InstrumentTypeID`),
  KEY `FK_limit_1` (`AccountID`),
  CONSTRAINT `FK_limit_1` FOREIGN KEY (`AccountID`) REFERENCES `account` (`ID`),
  CONSTRAINT `FK_limit_2` FOREIGN KEY (`InstrumentTypeID`) REFERENCES `instrument_type` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;










DROP TABLE IF EXISTS `myschema`.`organisation`;
CREATE TABLE  `myschema`.`organisation` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) DEFAULT NULL,
  `CompanyName` varchar(45) DEFAULT NULL,
  `Country` varchar(45) DEFAULT NULL,
  `City` varchar(45) DEFAULT NULL,
  `Address` varchar(100) DEFAULT NULL,
  `PostCode` varchar(16) DEFAULT NULL,
  `LastModified` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `ParentOrgID` int(10) unsigned NOT NULL DEFAULT '0',
  `ParentAccountID` int(10) unsigned DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;















DROP TABLE IF EXISTS `myschema`.`rating`;
CREATE TABLE  `myschema`.`rating` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `AccountGroupID` int(11) DEFAULT NULL,
  `InstrumentTypeID` int(11) unsigned DEFAULT NULL,
  `MarginRatio` decimal(25,10) DEFAULT NULL,
  `MarginAdjustment` decimal(25,10) DEFAULT NULL,
  `CommissionRatio` decimal(25,10) DEFAULT NULL,
  `CommissionAdjustment` decimal(25,10) DEFAULT NULL,
  `LastModified` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`ID`),
  KEY `FK_AccountGroup` (`AccountGroupID`),
  KEY `FK_rating_2` (`InstrumentTypeID`),
  CONSTRAINT `FK_rating_1` FOREIGN KEY (`AccountGroupID`) REFERENCES `account_group` (`ID`),
  CONSTRAINT `FK_rating_2` FOREIGN KEY (`InstrumentTypeID`) REFERENCES `instrument_type` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;










DROP TABLE IF EXISTS `myschema`.`routing_rule`;
CREATE TABLE  `myschema`.`routing_rule` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `RoutingID` int(11) DEFAULT NULL,
  `ExchangeID` int(10) unsigned DEFAULT NULL,
  `AccountGroupID` int(11) DEFAULT NULL,
  `LastModified` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`ID`),
  KEY `FK_AccoutGroup` (`AccountGroupID`),
  KEY `FK_Exchange` (`ExchangeID`),
  CONSTRAINT `FK_routing_rule_1` FOREIGN KEY (`AccountGroupID`) REFERENCES `account_group` (`ID`),
  CONSTRAINT `FK_routing_rule_2` FOREIGN KEY (`ExchangeID`) REFERENCES `exchange` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;









DROP TABLE IF EXISTS `myschema`.`user`;
CREATE TABLE  `myschema`.`user` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `Name` varchar(48) DEFAULT NULL,
  `Password` varchar(48) NOT NULL,
  `Enable` smallint(1) NOT NULL DEFAULT '1',
  `Block` smallint(1) NOT NULL DEFAULT '0',
  `DefaultAccountID` int(10) unsigned DEFAULT NULL,
  `LastModified` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `Online` smallint(1) DEFAULT '0',
  `ParentAccountID` int(10) unsigned DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_Account` (`DefaultAccountID`),
  KEY `Name` (`Name`),
  KEY `ParentAccountID` (`ParentAccountID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;











DROP TABLE IF EXISTS `myschema`.`user_account_relation`;
CREATE TABLE  `myschema`.`user_account_relation` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `UserID` int(10) unsigned NOT NULL,
  `AccountID` int(10) unsigned NOT NULL,
  `LastModified` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`ID`),
  KEY `FK_user_account_relation_1` (`UserID`),
  KEY `FK_user_account_relation_2` (`AccountID`),
  CONSTRAINT `FK_user_account_relation_1` FOREIGN KEY (`UserID`) REFERENCES `user` (`ID`),
  CONSTRAINT `FK_user_account_relation_2` FOREIGN KEY (`AccountID`) REFERENCES `account` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;










DROP TABLE IF EXISTS `myschema`.`user_instrument_subscription`;
CREATE TABLE  `myschema`.`user_instrument_subscription` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `UserID` int(10) unsigned NOT NULL,
  `InstrumentTypeGroupID` int(10) unsigned DEFAULT NULL,
  `LastModified` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`ID`),
  KEY `FK_user_instrument_sub1` (`UserID`),
  KEY `FK_user_instrument_sub2` (`InstrumentTypeGroupID`),
  CONSTRAINT `FK_user_instrument_subscription_1` FOREIGN KEY (`UserID`) REFERENCES `user` (`ID`),
  CONSTRAINT `FK_user_instrument_subscription_2` FOREIGN KEY (`InstrumentTypeGroupID`) REFERENCES `instrument_type_group` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;





























































