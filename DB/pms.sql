CREATE DATABASE `pms` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
CREATE TABLE `pms_category` (
  `Category_ID` int(11) NOT NULL AUTO_INCREMENT,
  `Category_Name` longtext,
  `Category_Description` longtext,
  `Category_CreatedDateTime` datetime DEFAULT NULL,
  `Category_LastUpdatedDateTime` datetime DEFAULT NULL,
  `Category_RecordStatus` int(11) DEFAULT NULL,
  PRIMARY KEY (`Category_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `pms_product` (
  `Product_ID` int(11) NOT NULL AUTO_INCREMENT,
  `Product_Name` longtext,
  `Product_Description` longtext,
  `Product_Category` int(11) DEFAULT '0',
  `Product_MRP` double DEFAULT '0',
  `Product_CreatedDateTime` datetime DEFAULT NULL,
  `Product_RecordStatus` longtext,
  `Product_LastUpdatedDateTime` datetime DEFAULT NULL,
  PRIMARY KEY (`Product_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=51 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

