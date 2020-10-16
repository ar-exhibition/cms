-- MySQL Script generated by MySQL Workbench
-- Fri Oct 16 02:52:59 2020
-- Model: New Model    Version: 1.0
-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema cmsXARCH
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema cmsXARCH
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `cmsXARCH` DEFAULT CHARACTER SET utf8mb4 ;
USE `cmsXARCH` ;

-- -----------------------------------------------------
-- Table `cmsXARCH`.`Studies`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `cmsXARCH`.`Studies` (
  `ProgrammeID` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `Programme` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`ProgrammeID`),
  UNIQUE INDEX `Studies_UNIQUE` (`Programme` ASC) VISIBLE,
  UNIQUE INDEX `ID_UNIQUE` (`ProgrammeID` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `cmsXARCH`.`Creator`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `cmsXARCH`.`Creator` (
  `CreatorID` INT NOT NULL AUTO_INCREMENT,
  `Creator` VARCHAR(45) NULL,
  `Programme` VARCHAR(45) NULL,
  PRIMARY KEY (`CreatorID`),
  UNIQUE INDEX `CreatorID_UNIQUE` (`CreatorID` ASC) VISIBLE,
  INDEX `fk_Creator_Studies1_idx` (`Programme` ASC) VISIBLE,
  CONSTRAINT `fk_Creator_Studies`
    FOREIGN KEY (`Programme`)
    REFERENCES `cmsXARCH`.`Studies` (`Programme`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `cmsXARCH`.`Term`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `cmsXARCH`.`Term` (
  `TermID` INT NOT NULL AUTO_INCREMENT,
  `Term` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`TermID`),
  UNIQUE INDEX `Termn_UNIQUE` (`Term` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `cmsXARCH`.`Course`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `cmsXARCH`.`Course` (
  `CourseID` INT NOT NULL AUTO_INCREMENT,
  `Programme` VARCHAR(45) NULL,
  `Course` VARCHAR(45) NULL,
  `Term` VARCHAR(45) NULL,
  PRIMARY KEY (`CourseID`),
  UNIQUE INDEX `CourseID_UNIQUE` (`CourseID` ASC) VISIBLE,
  INDEX `fk_Course_Studies1_idx` (`Programme` ASC) VISIBLE,
  INDEX `fk_Course_Term1_idx` (`Term` ASC) VISIBLE,
  CONSTRAINT `fk_Course_Studies`
    FOREIGN KEY (`Programme`)
    REFERENCES `cmsXARCH`.`Studies` (`Programme`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Course_Term`
    FOREIGN KEY (`Term`)
    REFERENCES `cmsXARCH`.`Term` (`Term`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `cmsXARCH`.`Thumbnail`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `cmsXARCH`.`Thumbnail` (
  `ThumbnailID` INT NOT NULL AUTO_INCREMENT,
  `ThumbnailUUID` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`ThumbnailID`),
  UNIQUE INDEX `ThumbnailUUID_UNIQUE` (`ThumbnailUUID` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `cmsXARCH`.`AssetType`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `cmsXARCH`.`AssetType` (
  `AssetTypeID` INT NOT NULL AUTO_INCREMENT,
  `Designator` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`AssetTypeID`),
  UNIQUE INDEX `Designator_UNIQUE` (`Designator` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `cmsXARCH`.`SceneAsset`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `cmsXARCH`.`SceneAsset` (
  `AssetID` INT NOT NULL AUTO_INCREMENT,
  `Creator` INT NULL,
  `Course` INT NULL,
  `AssetName` VARCHAR(45) NULL,
  `FileUUID` VARCHAR(45) NULL,
  `ExternalLink` VARCHAR(45) NULL,
  `ThumbnailUUID` VARCHAR(45) NULL,
  `CreationDate` DATETIME NULL,
  `Deleted` TINYINT NULL,
  `AssetType` INT NULL,
  PRIMARY KEY (`AssetID`),
  INDEX `fk_SceneAsset_Creator1_idx` (`Creator` ASC) VISIBLE,
  INDEX `fk_SceneAsset_Course1_idx` (`Course` ASC) VISIBLE,
  UNIQUE INDEX `AssetID_UNIQUE` (`AssetID` ASC) VISIBLE,
  UNIQUE INDEX `ThumbnailUUID_UNIQUE` (`ThumbnailUUID` ASC) VISIBLE,
  INDEX `fk_SceneAsset_AssetType1_idx` (`AssetType` ASC) VISIBLE,
  CONSTRAINT `fk_SceneAsset_Creator`
    FOREIGN KEY (`Creator`)
    REFERENCES `cmsXARCH`.`Creator` (`CreatorID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_SceneAsset_Course`
    FOREIGN KEY (`Course`)
    REFERENCES `cmsXARCH`.`Course` (`CourseID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_SceneAsset_Thumbnail1`
    FOREIGN KEY (`ThumbnailUUID`)
    REFERENCES `cmsXARCH`.`Thumbnail` (`ThumbnailUUID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_SceneAsset_AssetType1`
    FOREIGN KEY (`AssetType`)
    REFERENCES `cmsXARCH`.`AssetType` (`AssetTypeID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `cmsXARCH`.`Scene`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `cmsXARCH`.`Scene` (
  `SceneID` INT NOT NULL AUTO_INCREMENT,
  `SceneName` VARCHAR(45) NULL,
  `FileUUID` VARCHAR(45) NULL,
  `MarkerUUID` VARCHAR(45) NULL COMMENT 'UUID',
  PRIMARY KEY (`SceneID`),
  UNIQUE INDEX `SceneID_UNIQUE` (`SceneID` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `cmsXARCH`.`Anchor`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `cmsXARCH`.`Anchor` (
  `AnchorID` VARCHAR(45) NOT NULL,
  `AssetID` INT NOT NULL,
  `Scale` FLOAT NULL DEFAULT 1,
  PRIMARY KEY (`AnchorID`),
  INDEX `fk_Anchor_SceneAsset1_idx` (`AssetID` ASC) VISIBLE,
  UNIQUE INDEX `AnchorID_UNIQUE` (`AnchorID` ASC) VISIBLE,
  CONSTRAINT `fk_Anchor_SceneAsset`
    FOREIGN KEY (`AssetID`)
    REFERENCES `cmsXARCH`.`SceneAsset` (`AssetID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
