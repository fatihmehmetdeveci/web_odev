/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

CREATE DATABASE IF NOT EXISTS `web_odev` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `web_odev`;

CREATE TABLE IF NOT EXISTS `products` (
  `id` int NOT NULL AUTO_INCREMENT,
  `title` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `content` varchar(5000) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `img` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `url` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

INSERT INTO `products` (`id`, `title`, `content`, `img`, `url`) VALUES
	(1, 'Ürün deneme', 'Ürün deneme İçerik', '/Uploads/Product/1.jpg', '#'),
	(2, 'Ürün deneme', 'Ürün deneme İçerik', '/Uploads/Product/2.jpg', '#'),
	(3, 'Ürün deneme', 'Ürün deneme İçerik', '/Uploads/Product/2.jpg', '#'),
	(4, 'Ürün deneme', 'Ürün deneme İçerik', '/Uploads/Product/2.jpg', '#'),
	(5, 'Ürün deneme', 'Ürün deneme İçerik', '/Uploads/Product/2.jpg', '#'),
	(6, 'Ürün deneme', 'Ürün deneme İçerik', '/Uploads/Product/2.jpg', '#'),
	(7, 'Ürün deneme', 'Ürün deneme İçerik', '/Uploads/Product/2.jpg', '#'),
	(8, 'Ürün deneme', 'Ürün deneme İçerik', '/Uploads/Product/2.jpg', '#'),
	(9, 'Ürün deneme', 'Ürün deneme İçerik', '/Uploads/Product/2.jpg', '#'),
	(10, 'Ürün deneme', 'Ürün deneme İçerik', '/Uploads/Product/2.jpg', '#'),
	(11, 'Ürün deneme', 'Ürün deneme İçerik', '/Uploads/Product/2.jpg', '#'),
	(12, 'Ürün deneme', 'Ürün deneme İçerik', '/Uploads/Product/2.jpg', '#'),
	(13, 'Ürün deneme', 'Ürün deneme İçerik', '/Uploads/Product/2.jpg', '#'),
	(14, 'Ürün deneme', 'Ürün deneme İçerik', '/Uploads/Product/2.jpg', '#'),
	(15, 'Ürün deneme', 'Ürün deneme İçerik', '/Uploads/Product/2.jpg', '#');

CREATE TABLE IF NOT EXISTS `sliders` (
  `id` int NOT NULL AUTO_INCREMENT,
  `title` varchar(500) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `content` varchar(5000) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `img` varchar(500) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `url` varchar(500) COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

INSERT INTO `sliders` (`id`, `title`, `content`, `img`, `url`) VALUES
	(2, 'Örnek Güncellem', 'Slider Örnek', '/Uploads/1.jpg', '#'),
	(3, 'Deneme Ekleme', 'İçerik Deneme', '/Uploads/2.jpg', '#');

CREATE TABLE IF NOT EXISTS `users` (
  `id` int NOT NULL AUTO_INCREMENT,
  `username` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  `password` varchar(50) COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

INSERT INTO `users` (`id`, `username`, `password`) VALUES
	(1, 'deneme', '1234');

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
