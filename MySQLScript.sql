CREATE SCHEMA `esgdbo` ;
CREATE TABLE `esgdbo`.`custrec` (
  `customerref` INT NOT NULL,
  `customername` VARCHAR(45) NOT NULL,
  `addressline1` VARCHAR(100) NOT NULL,
  `addressline2` VARCHAR(100) NOT NULL,
  `town` VARCHAR(45) NOT NULL,
  `county` VARCHAR(45) NOT NULL,
  `country` VARCHAR(45) NOT NULL,
  `postcode` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`customereef`),
  UNIQUE INDEX `customereef_UNIQUE` (`customereef` ASC) VISIBLE);
