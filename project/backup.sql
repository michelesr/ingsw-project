PRAGMA foreign_keys=OFF;
BEGIN TRANSACTION;
CREATE TABLE `ApiKey` (`user_id` INTEGER NOT NULL, `key` VARCHAR UNIQUE NOT NULL, `id` INTEGER PRIMARY KEY NOT NULL, FOREIGN KEY(`user_id`) REFERENCES `User`(`id`) ON DELETE RESTRICT ON UPDATE CASCADE);
INSERT INTO "ApiKey" VALUES(1,'8A307DF4275913B1636E45C00F3A575A',2);
INSERT INTO "ApiKey" VALUES(6,'FC203C623199B297752C18F5DA5CE656',3);
CREATE TABLE `User` (`email` VARCHAR UNIQUE NOT NULL, `password` VARCHAR NOT NULL, `first_name` VARCHAR NOT NULL, `last_name` VARCHAR NOT NULL, `type` INTEGER, `id` INTEGER PRIMARY KEY NOT NULL);
INSERT INTO "User" VALUES('admin@example.org','admin','admin','admin',1,1);
INSERT INTO "User" VALUES('verdi-moda@example.org','jgriegoejgoreigjioergj','Luigi','Verdi',0,2);
INSERT INTO "User" VALUES('stefanelli@example.org','greorekogroepkgopeopgkoep','Giacomo','Stefanelli',0,3);
INSERT INTO "User" VALUES('lacasa@example.org','fowrkorkfkrwpofkopwekofewop','Giovanni','Berluti',0,4);
INSERT INTO "User" VALUES('supermario@example.org','frekokfoerkoperkgopekopge','Mario','Rossi',1,5);
INSERT INTO "User" VALUES('test@example.org','test','Test','Supplier',0,6);
CREATE TABLE `Admin` (`user_id` INTEGER NOT NULL, `id` INTEGER PRIMARY KEY NOT NULL, FOREIGN KEY(`user_id`) REFERENCES `User`(`id`) ON DELETE RESTRICT ON UPDATE CASCADE);
INSERT INTO "Admin" VALUES(1,1);
INSERT INTO "Admin" VALUES(5,2);
CREATE TABLE `Supplier` (`vat` VARCHAR NOT NULL, `supplier_name` VARCHAR NOT NULL, `city` INTEGER NOT NULL, `user_id` INTEGER NOT NULL, `id` INTEGER PRIMARY KEY NOT NULL, FOREIGN KEY(`city`) REFERENCES `City`(`id`) ON DELETE RESTRICT ON UPDATE CASCADE, FOREIGN KEY(`user_id`) REFERENCES `User`(`id`) ON DELETE RESTRICT ON UPDATE CASCADE);
INSERT INTO "Supplier" VALUES('001','Verdi Moda',4,2,1);
INSERT INTO "Supplier" VALUES('002','Prodotti biologici Stefanelli',6,3,2);
INSERT INTO "Supplier" VALUES('003','La tua casa',5,4,3);
INSERT INTO "Supplier" VALUES('007','TestSupplier',1,6,4);
CREATE TABLE `ProductCategory` (`name` VARCHAR UNIQUE NOT NULL, `id` INTEGER PRIMARY KEY NOT NULL);
INSERT INTO "ProductCategory" VALUES('Abbigliamento',1);
INSERT INTO "ProductCategory" VALUES('Alimenti',2);
INSERT INTO "ProductCategory" VALUES('Domestici',3);
CREATE TABLE `ProductStock` (`product_id` INTEGER NOT NULL, `price` FLOAT NOT NULL, `min` INTEGER NOT NULL, `max` INTEGER NOT NULL, `availability` INTEGER NOT NULL, `id` INTEGER PRIMARY KEY NOT NULL, FOREIGN KEY(`product_id`) REFERENCES `Product`(`id`) ON DELETE RESTRICT ON UPDATE CASCADE);
INSERT INTO "ProductStock" VALUES(2,1500.0,1,100,200,1);
INSERT INTO "ProductStock" VALUES(3,150.0,1,2000,4000,2);
INSERT INTO "ProductStock" VALUES(1,200.0,1,1000,4000,3);
INSERT INTO "ProductStock" VALUES(4,300.0,1,20,1000,4);
INSERT INTO "ProductStock" VALUES(4,400.0,30,40,2000,5);
INSERT INTO "ProductStock" VALUES(4,100.0,1,1000,10000,6);
INSERT INTO "ProductStock" VALUES(4,250.0,1,2,3,7);
CREATE TABLE `Product` (`name` VARCHAR UNIQUE NOT NULL, `supplier_id` INTEGER NOT NULL, `product_category` INTEGER NOT NULL, `id` INTEGER PRIMARY KEY NOT NULL, FOREIGN KEY(`supplier_id`) REFERENCES `Supplier`(`id`) ON DELETE RESTRICT ON UPDATE CASCADE, FOREIGN KEY(`product_category`) REFERENCES `ProductCategory`(`id`) ON DELETE RESTRICT ON UPDATE CASCADE);
INSERT INTO "Product" VALUES('Orzo',2,2,1);
INSERT INTO "Product" VALUES('T-Shirt',1,1,2);
INSERT INTO "Product" VALUES('Deodorante per il bagno',3,3,3);
INSERT INTO "Product" VALUES('Test',4,1,4);
CREATE TABLE `City` (`name` VARCHAR UNIQUE NOT NULL, `id` INTEGER PRIMARY KEY NOT NULL);
INSERT INTO "City" VALUES('Mondolfo',1);
INSERT INTO "City" VALUES('Urbino',2);
INSERT INTO "City" VALUES('Fano',3);
INSERT INTO "City" VALUES('Senigallia',4);
INSERT INTO "City" VALUES('Ancona',5);
INSERT INTO "City" VALUES('Fermignano',6);
INSERT INTO "City" VALUES('Fabriano',7);
CREATE TABLE `Session` (`user_id` INTEGER NOT NULL, `start` VARCHAR, `end` VARCHAR, `id` INTEGER PRIMARY KEY NOT NULL, FOREIGN KEY(`user_id`) REFERENCES `User`(`id`) ON DELETE RESTRICT ON UPDATE CASCADE);
INSERT INTO "Session" VALUES(1,'16/09/2014 12:17:10','16/09/2014 12:33:52',1);
INSERT INTO "Session" VALUES(1,'16/09/2014 12:33:52','16/09/2014 12:37:18',2);
INSERT INTO "Session" VALUES(6,'16/09/2014 12:37:31','16/09/2014 16:55:16',3);
INSERT INTO "Session" VALUES(1,'16/09/2014 15:39:01','16/09/2014 15:40:36',4);
INSERT INTO "Session" VALUES(1,'16/09/2014 15:40:36','16/09/2014 17:10:16',5);
INSERT INTO "Session" VALUES(6,'16/09/2014 16:55:16','16/09/2014 16:55:32',6);
INSERT INTO "Session" VALUES(6,'16/09/2014 16:55:42','16/09/2014 16:56:46',7);
INSERT INTO "Session" VALUES(6,'16/09/2014 16:56:47','16/09/2014 16:57:20',8);
INSERT INTO "Session" VALUES(6,'16/09/2014 16:57:21','16/09/2014 17:01:06',9);
INSERT INTO "Session" VALUES(6,'16/09/2014 17:01:06','16/09/2014 17:07:02',10);
INSERT INTO "Session" VALUES(6,'16/09/2014 17:07:02','16/09/2014 17:23:36',11);
INSERT INTO "Session" VALUES(1,'16/09/2014 17:10:16','17/09/2014 17:12:48',12);
INSERT INTO "Session" VALUES(6,'16/09/2014 17:23:36','16/09/2014 17:23:58',13);
INSERT INTO "Session" VALUES(6,'16/09/2014 17:23:58','',14);
INSERT INTO "Session" VALUES(1,'17/09/2014 17:12:48','',15);
CREATE TRIGGER it__ApiKey__user_id BEFORE INSERT ON `ApiKey` FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'FK value "user_id" references null value')  WHERE NEW.`user_id` IS NOT NULL AND ((SELECT `id` FROM `User` WHERE `id`=NEW.`user_id`) IS NULL); END;
CREATE TRIGGER ut__ApiKey__user_id BEFORE UPDATE ON `ApiKey` FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'FK value "user_id" references null value')  WHERE NEW.`user_id` IS NOT NULL AND ((SELECT `id` FROM `User` WHERE `id`=NEW.`user_id`) IS NULL); END;
CREATE TRIGGER it__Supplier__user_id BEFORE INSERT ON `Supplier` FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'FK value "user_id" references null value')  WHERE NEW.`user_id` IS NOT NULL AND ((SELECT `id` FROM `User` WHERE `id`=NEW.`user_id`) IS NULL); END;
CREATE TRIGGER ut__Supplier__user_id BEFORE UPDATE ON `Supplier` FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'FK value "user_id" references null value')  WHERE NEW.`user_id` IS NOT NULL AND ((SELECT `id` FROM `User` WHERE `id`=NEW.`user_id`) IS NULL); END;
CREATE TRIGGER it__Supplier__city BEFORE INSERT ON `Supplier` FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'FK value "city" references null value')  WHERE NEW.`city` IS NOT NULL AND ((SELECT `id` FROM `City` WHERE `id`=NEW.`city`) IS NULL); END;
CREATE TRIGGER ut__Supplier__city BEFORE UPDATE ON `Supplier` FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'FK value "city" references null value')  WHERE NEW.`city` IS NOT NULL AND ((SELECT `id` FROM `City` WHERE `id`=NEW.`city`) IS NULL); END;
CREATE TRIGGER it__Admin__user_id BEFORE INSERT ON `Admin` FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'FK value "user_id" references null value')  WHERE NEW.`user_id` IS NOT NULL AND ((SELECT `id` FROM `User` WHERE `id`=NEW.`user_id`) IS NULL); END;
CREATE TRIGGER ut__Admin__user_id BEFORE UPDATE ON `Admin` FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'FK value "user_id" references null value')  WHERE NEW.`user_id` IS NOT NULL AND ((SELECT `id` FROM `User` WHERE `id`=NEW.`user_id`) IS NULL); END;
CREATE TRIGGER it__Product__supplier_id BEFORE INSERT ON `Product` FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'FK value "supplier_id" references null value')  WHERE NEW.`supplier_id` IS NOT NULL AND ((SELECT `id` FROM `Supplier` WHERE `id`=NEW.`supplier_id`) IS NULL); END;
CREATE TRIGGER ut__Product__supplier_id BEFORE UPDATE ON `Product` FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'FK value "supplier_id" references null value')  WHERE NEW.`supplier_id` IS NOT NULL AND ((SELECT `id` FROM `Supplier` WHERE `id`=NEW.`supplier_id`) IS NULL); END;
CREATE TRIGGER it__Product__product_category BEFORE INSERT ON `Product` FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'FK value "product_category" references null value')  WHERE NEW.`product_category` IS NOT NULL AND ((SELECT `id` FROM `ProductCategory` WHERE `id`=NEW.`product_category`) IS NULL); END;
CREATE TRIGGER ut__Product__product_category BEFORE UPDATE ON `Product` FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'FK value "product_category" references null value')  WHERE NEW.`product_category` IS NOT NULL AND ((SELECT `id` FROM `ProductCategory` WHERE `id`=NEW.`product_category`) IS NULL); END;
CREATE TRIGGER it__Session__user_id BEFORE INSERT ON `Session` FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'FK value "user_id" references null value')  WHERE NEW.`user_id` IS NOT NULL AND ((SELECT `id` FROM `User` WHERE `id`=NEW.`user_id`) IS NULL); END;
CREATE TRIGGER ut__Session__user_id BEFORE UPDATE ON `Session` FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'FK value "user_id" references null value')  WHERE NEW.`user_id` IS NOT NULL AND ((SELECT `id` FROM `User` WHERE `id`=NEW.`user_id`) IS NULL); END;
CREATE TRIGGER it__ProductStock__product_id BEFORE INSERT ON `ProductStock` FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'FK value "product_id" references null value')  WHERE NEW.`product_id` IS NOT NULL AND ((SELECT `id` FROM `Product` WHERE `id`=NEW.`product_id`) IS NULL); END;
CREATE TRIGGER ut__ProductStock__product_id BEFORE UPDATE ON `ProductStock` FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'FK value "product_id" references null value')  WHERE NEW.`product_id` IS NOT NULL AND ((SELECT `id` FROM `Product` WHERE `id`=NEW.`product_id`) IS NULL); END;
COMMIT;
