-- MySQL Script generated by MySQL Workbench
-- Mon Aug 31 20:29:34 2020
-- Model: New Model    Version: 1.0
-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema cmsDatabase
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema cmsDatabase
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `cmsDatabase` DEFAULT CHARACTER SET utf8 ;
USE `cmsDatabase` ;

-- -----------------------------------------------------
-- Table `cmsDatabase`.`Studies`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `cmsDatabase`.`Studies` (
  `ID` INT UNSIGNED NOT NULL,
  `Name` VARCHAR(45) NULL,
  PRIMARY KEY (`ID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `cmsDatabase`.`Creator`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `cmsDatabase`.`Creator` (
  `CreatorID` INT NOT NULL,
  `Name` VARCHAR(45) NULL,
  `Studies` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`CreatorID`, `Studies`),
  UNIQUE INDEX `CreatorID_UNIQUE` (`CreatorID` ASC) VISIBLE,
  INDEX `fk_Creator_Studies1_idx` (`Studies` ASC) VISIBLE,
  CONSTRAINT `fk_Creator_Studies1`
    FOREIGN KEY (`Studies`)
    REFERENCES `cmsDatabase`.`Studies` (`Name`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `cmsDatabase`.`Course`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `cmsDatabase`.`Course` (
  `CourseID` INT NOT NULL AUTO_INCREMENT,
  `Programme` VARCHAR(45) NOT NULL,
  `Course` VARCHAR(45) NULL,
  PRIMARY KEY (`CourseID`, `Programme`),
  UNIQUE INDEX `CourseID_UNIQUE` (`CourseID` ASC) VISIBLE,
  INDEX `fk_Course_Studies1_idx` (`Programme` ASC) VISIBLE,
  CONSTRAINT `fk_Course_Studies1`
    FOREIGN KEY (`Programme`)
    REFERENCES `cmsDatabase`.`Studies` (`Name`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `cmsDatabase`.`AssetType`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `cmsDatabase`.`AssetType` (
  `AssetTypeID` INT NOT NULL,
  `Designator` VARCHAR(45) NULL,
  PRIMARY KEY (`AssetTypeID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `cmsDatabase`.`SceneAsset`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `cmsDatabase`.`SceneAsset` (
  `AssetID` INT NOT NULL AUTO_INCREMENT,
  `Creator` INT NOT NULL,
  `Course` INT NOT NULL,
  `Name` VARCHAR(45) NULL,
  `Filename` VARCHAR(45) NULL,
  `Filetype` VARCHAR(45) NULL,
  `Date` DATETIME NULL,
  `Link` VARCHAR(1024) NULL,
  `Thumbnail` VARCHAR(45) NULL,
  `Type` VARCHAR(45) NULL,
  `Power` VARCHAR(45) NULL,
  `Color` VARCHAR(45) NULL,
  `Deleted` TINYINT NULL,
  `AssetType` INT NULL,
  PRIMARY KEY (`AssetID`, `Creator`, `Course`),
  INDEX `fk_SceneAsset_Creator1_idx` (`Creator` ASC) VISIBLE,
  INDEX `fk_SceneAsset_Course1_idx` (`Course` ASC) VISIBLE,
  UNIQUE INDEX `AssetID_UNIQUE` (`AssetID` ASC) VISIBLE,
  INDEX `fk_SceneAsset_AssetType1_idx` (`AssetType` ASC) VISIBLE,
  CONSTRAINT `fk_SceneAsset_Creator1`
    FOREIGN KEY (`Creator`)
    REFERENCES `cmsDatabase`.`Creator` (`CreatorID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_SceneAsset_Course1`
    FOREIGN KEY (`Course`)
    REFERENCES `cmsDatabase`.`Course` (`CourseID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_SceneAsset_AssetType1`
    FOREIGN KEY (`AssetType`)
    REFERENCES `cmsDatabase`.`AssetType` (`AssetTypeID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `cmsDatabase`.`Scene`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `cmsDatabase`.`Scene` (
  `SceneID` INT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(45) NULL,
  PRIMARY KEY (`SceneID`),
  UNIQUE INDEX `SceneID_UNIQUE` (`SceneID` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `cmsDatabase`.`Anchor`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `cmsDatabase`.`Anchor` (
  `AnchorID` INT NOT NULL,
  `SceneID` INT NOT NULL,
  `AssetID` INT NULL,
  `Translate` VARCHAR(45) NULL,
  `Rotate` VARCHAR(45) NULL,
  `Scale` VARCHAR(45) NULL,
  PRIMARY KEY (`AnchorID`, `SceneID`),
  INDEX `fk_Anchor_Scene1_idx` (`SceneID` ASC) VISIBLE,
  INDEX `fk_Anchor_SceneAsset1_idx` (`AssetID` ASC) VISIBLE,
  CONSTRAINT `fk_Anchor_Scene1`
    FOREIGN KEY (`SceneID`)
    REFERENCES `cmsDatabase`.`Scene` (`SceneID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Anchor_SceneAsset1`
    FOREIGN KEY (`AssetID`)
    REFERENCES `cmsDatabase`.`SceneAsset` (`AssetID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
