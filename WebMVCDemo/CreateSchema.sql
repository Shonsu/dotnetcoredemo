CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    PRIMARY KEY (`MigrationId`)
);

START TRANSACTION;

CREATE TABLE `Address` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `City` varchar(64) NOT NULL,
    `PostalCode` varchar(10) NOT NULL DEFAULT '00-000',
    `Street` longtext NOT NULL,
    `HouseNumber` int NOT NULL,
    `FlatNumber` int NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `Employees` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `FirstName` longtext NULL,
    `LastName` longtext NULL,
    `DateHired` datetime(6) NOT NULL,
    `Department` varchar(255) NULL DEFAULT 'Information Technology',
    `SupervisorId` int NULL,
    `AddressId` int NOT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Employees_Address_AddressId` FOREIGN KEY (`AddressId`) REFERENCES `Address` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Employees_Employees_SupervisorId` FOREIGN KEY (`SupervisorId`) REFERENCES `Employees` (`Id`)
);

CREATE TABLE `EmployeePhoneNumber` (
    `EmployeeId` int NOT NULL,
    `PhoneNumber` varchar(255) NOT NULL,
    PRIMARY KEY (`EmployeeId`, `PhoneNumber`),
    CONSTRAINT `FK_EmployeePhoneNumber_Employees_EmployeeId` FOREIGN KEY (`EmployeeId`) REFERENCES `Employees` (`Id`) ON DELETE CASCADE
);

CREATE INDEX `IX_Employees_AddressId` ON `Employees` (`AddressId`);

CREATE INDEX `IX_Employees_SupervisorId` ON `Employees` (`SupervisorId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20240405121918_InitialCreate', '8.0.3');

COMMIT;

